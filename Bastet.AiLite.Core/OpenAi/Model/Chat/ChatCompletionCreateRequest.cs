using System.Collections.Generic;
using Bastet.AiLite.OpenAi.Model.Common;
using Newtonsoft.Json;

namespace Bastet.AiLite.OpenAi.Model.Chat;

public class ChatCompletionCreateRequest
{
	public IList<ChatMessage> messages { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public float? top_p { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public int? n { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public bool? stream { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public StreamOptions stream_options { get; set; }

	[JsonIgnore]
	public string Stop { get; set; }

	[JsonIgnore]
	public IList<string> StopAsList { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public IList<string> stop => Stop != null ? new List<string> { Stop } : StopAsList;

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public int? max_tokens { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public int? max_completion_tokens { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public float? presence_penalty { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public float? frequency_penalty { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public object logit_bias { get; set; }


	[JsonIgnore]
	public IList<ToolDefinition> Tools { get; set; }


	[JsonIgnore]
	public object ToolsAsObject { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public object tools => Tools ?? ToolsAsObject;

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public ResponseFormat response_format { get; set; }

	/// <summary>
	///     This feature is in Beta. If specified, our system will make a best effort to sample deterministically, such that
	///     repeated requests with the same seed and parameters should return the same result. Determinism is not guaranteed,
	///     and you should refer to the system_fingerprint response parameter to monitor changes in the backend.
	/// </summary>
	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public int? seed { get; set; }

	/// <summary>
	///     Whether to return log probabilities of the output tokens or not. If true, returns the log probabilities of each
	///     output token returned in the content of message.
	/// </summary>
	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public bool? logprobs { get; set; }


	/// <summary>
	///     An integer between 0 and 20 specifying the number of most likely tokens to return at each token position, each with
	///     an associated log probability. logprobs must be set to true if this parameter is used.
	/// </summary>
	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public int? top_logprobs { get; set; }

	/// <summary>
	///     Whether to enable parallel <a href="https://platform.openai.com/docs/guides/function-calling/parallel-function-calling">function calling</a> during tool use.
	/// </summary>
	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public bool? parallel_tool_calls { get; set; }

	/// <summary>
	///     ID of the model to use. For models supported see <see cref="OpenAI.ObjectModels.Models" /> start with <c>Gpt_</c>
	/// </summary>
	public string model { get; set; }


	/// <summary>
	///     What sampling temperature to use, between 0 and 2. Higher values like 0.8 will make the output more random, while
	///     lower values like 0.2 will make it more focused and deterministic.
	///     We generally recommend altering this or top_p but not both.
	/// </summary>
	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public float? temperature { get; set; }

	/// <summary>
	///     A unique identifier representing your end-user, which can help OpenAI to monitor and detect abuse. Learn more.
	/// </summary>
	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public string user { get; set; }

	/// <summary>
	/// Specifies the latency tier to use for processing the request. This parameter is relevant for customers subscribed to the scale tier service:
	/// If set to 'auto', and the Project is Scale tier enabled, the system will utilize scale tier credits until they are exhausted.
	/// If set to 'auto', and the Project is not Scale tier enabled, the request will be processed using the default service tier with a lower uptime SLA and no latency guarentee.
	/// If set to 'default', the request will be processed using the default service tier with a lower uptime SLA and no latency guarentee.
	/// When not set, the default behavior is 'auto'.
	/// When this parameter is set, the response body will include the service_tier utilized.
	/// </summary>
	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public string service_tier { get; set; }


	/// <summary>
	/// Whether or not to store the output of this chat completion request for use in our model distillation or evals products.
	/// https://platform.openai.com/docs/api-reference/chat/create?lang=python#chat-create-store
	/// 
	/// <para /> 
	/// more about distillation: https://platform.openai.com/docs/guides/distillation
	/// <para /> 
	/// more about evals: https://platform.openai.com/docs/guides/evals
	/// </summary>
	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public bool? store { get; set; }
}