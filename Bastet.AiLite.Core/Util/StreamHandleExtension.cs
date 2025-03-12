using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using Bastet.AiLite.OpenAi.Model;
using Bastet.AiLite.OpenAi.Model.Chat;
using Bastet.AiLite.OpenAi.Model.Common;
using Newtonsoft.Json;

namespace Bastet.AiLite.Util;

public static class StreamHandleExtension
{
	public static async IAsyncEnumerable<BaseResponse> AsStream(this HttpResponseMessage response,
		bool justDataMode = true, [EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		await foreach (var baseResponse in AsStream<BaseResponse>(response, justDataMode, cancellationToken))
			yield return baseResponse;
	}

	public static async IAsyncEnumerable<TResponse> AsStream<TResponse>(this HttpResponseMessage response,
		bool justDataMode = true, [EnumeratorCancellation] CancellationToken cancellationToken = default)
		where TResponse : BaseResponse, new()
	{
		// Helper data in case we need to reassemble a multi-packet response
		ReassemblyContext ctx = new();

		// Ensure that we parse headers only once to improve performance a little bit.
		var httpStatusCode = response.StatusCode;
		var headerValues = response.ParseHeaders();

		await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
		using var reader = new StreamReader(stream);
		string tempStreamEvent = null;
		bool isEventDelta;
		// Continuously read the stream until the end of it
		while (true)
		{
			cancellationToken.ThrowIfCancellationRequested();

			var line = await reader.ReadLineAsync();
			//   Console.WriteLine("---" + line);
			// Break the loop if we have reached the end of the stream
			if (line == null)
			{
				break;
			}

			// Skip empty lines
			if (string.IsNullOrEmpty(line))
			{
				continue;
			}

			if (line.StartsWith("event: "))
			{
				line = line.RemoveIfStartWith("event: ");
				tempStreamEvent = line;
				isEventDelta = true;
			}
			else
			{
				isEventDelta = false;
			}

			if (justDataMode && !line.StartsWith("data: "))
			{
				continue;
			}

			if (!justDataMode && isEventDelta)
			{
				yield return new() { ObjectTypeName = "base.stream.event", StreamEvent = tempStreamEvent };
				continue;
			}

			line = line.RemoveIfStartWith("data: ");

			// Exit the loop if the stream is done
			if (line.StartsWith("[DONE]"))
			{
				break;
			}

			TResponse block;
			try
			{
				// When the response is good, each line is a serializable CompletionCreateRequest
				if (typeof(TResponse) == typeof(BaseResponse))
				{
					block = JsonConvert.DeserializeObject(line, Route(line)) as TResponse;
				}
				else
				{
					block = JsonConvert.DeserializeObject<TResponse>(line);
				}
			}
			catch (Exception)
			{
				// When the API returns an error, it does not come back as a block, it returns a single character of text ("{").
				// In this instance, read through the rest of the response, which should be a complete object to parse.
				line += await reader.ReadToEndAsync();
				block = JsonConvert.DeserializeObject<TResponse>(line);
			}


			if (null != block)
			{
				if (typeof(TResponse) == typeof(ChatCompletionCreateResponse))
				{
					ctx.Process(block as ChatCompletionCreateResponse ?? throw new InvalidOperationException());
				}

				if (!ctx.IsFnAssemblyActive)
				{
					block.HttpStatusCode = httpStatusCode;
					block.HeaderValues = headerValues;
					block.StreamEvent = tempStreamEvent;
					tempStreamEvent = null;
					yield return block;
				}
			}
		}
	}

	public static Type Route(string json)
	{
		var apiResponse = JsonConvert.DeserializeObject<ObjectBaseResponse>(json);

		return apiResponse?.ObjectTypeName switch
		{
			// "thread.run.step" => typeof(RunStepResponse),
			// "thread.run" => typeof(RunResponse),
			// "thread.message" => typeof(MessageResponse),
			// "thread.message.delta" => typeof(MessageResponse),
			_ => typeof(BaseResponse)
		};
	}

	private class ReassemblyContext
	{
		private IList<ToolCall> _deltaFnCallList = new List<ToolCall>();
		public bool IsFnAssemblyActive => _deltaFnCallList.Count > 0;


		/// <summary>
		///     Detects if a response block is a part of a multi-chunk
		///     streamed tool call response of type == "function". As long as that's true,
		///     it keeps accumulating block contents even handling multiple parallel tool calls, and once all the function call
		///     streaming is done, it produces the assembled results in the final block.
		/// </summary>
		/// <param name="block"></param>
		public void Process(ChatCompletionCreateResponse block)
		{
			var firstChoice = block.choices?.FirstOrDefault();
			if (firstChoice == null)
			{
				return;
			} // not a valid state? nothing to do

			var isStreamingFnCall = IsStreamingFunctionCall();
			var isStreamingFnCallEnd = firstChoice.finish_reason != null;

			var justStarted = false;

			// Check if the streaming block has a tool_call segment of "function" type, according to the value returned by IsStreamingFunctionCall() above.
			// If so, this is the beginning entry point of a function call assembly for each tool_call main item, even in case of multiple parallel tool calls.
			// We're going to steal the partial message and squirrel it away for the time being.
			if (isStreamingFnCall)
			{
				foreach (var t in firstChoice.message.tool_calls!)
				{
					//Handles just ToolCall type == "function" as according to the value returned by IsStreamingFunctionCall() above
					if (t.function != null && t.type == StaticValues.CompletionStatics.ToolType.Function)
						_deltaFnCallList.Add(t);
				}

				justStarted = true;
			}

			// As long as we're assembling, keep on appending those args,
			// respecting the stream arguments sequence aligned with the last tool call main item which the arguments belong to.
			if (IsFnAssemblyActive && !justStarted)
			{
				//Get current toolcall metadata in order to search by index reference which to bind arguments to.
				var tcMetadata = GetToolCallMetadata();

				if (tcMetadata.index > -1)
				{
					//Handles just ToolCall type == "function"
					using var argumentsList = ExtractArgsSoFar().GetEnumerator();
					var existItems = argumentsList.MoveNext();

					if (existItems)
					{
						//toolcall item must exists as added in previous steps, otherwise First() will raise an InvalidOperationException
						var tc = _deltaFnCallList!.First(t => t.index == tcMetadata.index);
						tc.function!.arguments += argumentsList.Current;
						argumentsList.MoveNext();
					}
				}
			}

			// If we were assembling and it just finished, fill this block with the info we've assembled, and we're done.
			if (IsFnAssemblyActive && isStreamingFnCallEnd)
			{
				firstChoice.message ??= ChatMessage.FromAssistant(""); // just in case? not sure it's needed
				// TODO When more than one function call is in a single index, OpenAI only returns the role delta at the beginning, which causes an issue.
				// TODO The current solution addresses this problem, but we need to fix it by using the role of the index.
				firstChoice.message.role ??= "assistant";
				firstChoice.message.tool_calls = new List<ToolCall>(_deltaFnCallList);
				_deltaFnCallList.Clear();
			}

			// Returns true if we're actively streaming, and also have a partial tool call main item ( id != (null | "")) of type "function" in the response
			bool IsStreamingFunctionCall()
			{
				return
					firstChoice.finish_reason ==
					null && // actively streaming, is a tool call main item, and have a function call
					firstChoice.message?.tool_calls?.Count > 0 && (firstChoice.message?.tool_calls.Any(t =>
						t.function != null && !string.IsNullOrEmpty(t.id) &&
						t.type == StaticValues.CompletionStatics.ToolType.Function) ?? false);
			}

			(int index, string id, string type) GetToolCallMetadata()
			{
				var tc = block.choices?.FirstOrDefault()?.message?.tool_calls?.Where(t => t.function != null)
					.Select(t => t).FirstOrDefault();

				return tc switch
				{
					not null => (tc.index, tc.id, tc.type),
					_ => (-1, null, null)
				};
			}

			IEnumerable<string> ExtractArgsSoFar()
			{
				var toolCalls = block.choices?.FirstOrDefault()?.message?.tool_calls;

				if (toolCalls != null)
				{
					var functionCallList = toolCalls.Where(t => t.function != null).Select(t => t.function);

					foreach (var functionCall in functionCallList)
					{
						yield return functionCall!.arguments ?? "";
					}
				}
			}
		}
	}
}