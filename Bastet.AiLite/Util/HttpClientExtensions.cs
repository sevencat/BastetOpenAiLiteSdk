using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bastet.AiLite.OpenAi.Model.Common;
using Newtonsoft.Json;

namespace Bastet.AiLite.Util;

internal static class HttpClientExtensions
{
	public static async Task<TResponse> GetReadAsAsync<TResponse>(this HttpClient client, string uri,
		CancellationToken cancellationToken = default) where TResponse : BaseResponse, new()
	{
		var response = await client.GetAsync(uri, cancellationToken);
		return await HandleResponseContent<TResponse>(response, cancellationToken);
	}

	private static readonly JsonSerializerSettings defaultJsonSerializerSettings = new JsonSerializerSettings()
	{
		NullValueHandling = NullValueHandling.Ignore
	};

	public static async Task<TResponse> PostAndReadAsAsync<TResponse>(this HttpClient client, string uri,
		object requestModel, CancellationToken cancellationToken = default) where TResponse : BaseResponse, new()
	{
		var response =
			await client.PostAsJsonAsync(uri, requestModel, defaultJsonSerializerSettings, cancellationToken);
		return await HandleResponseContent<TResponse>(response, cancellationToken);
	}

	public static string GetHeaderValue(this HttpResponseHeaders headers, string headerName)
	{
		return headers.Contains(headerName) ? headers.GetValues(headerName).FirstOrDefault() : null;
	}

	public static Task<HttpResponseMessage> PostAsJsonAsync<TValue>(
		this HttpClient client,
		string requestUri,
		TValue value,
		JsonSerializerSettings options = null,
		CancellationToken cancellationToken = default(CancellationToken))
	{
		if (client == null)
			throw new ArgumentNullException(nameof(client));
		var jreq = JsonConvert.SerializeObject(value, options);
		var content = new StringContent(jreq, Encoding.UTF8, "application/json");
		return client.PostAsync(requestUri, content, cancellationToken);
	}

	public static async Task<TResponse> PostAndReadAsDataAsync<TResponse, TData>(this HttpClient client, string uri,
		object requestModel, CancellationToken cancellationToken = default)
		where TResponse : DataBaseResponse<TData>, new()
	{
		var response =
			await client.PostAsJsonAsync(uri, requestModel, defaultJsonSerializerSettings, cancellationToken);

		if (!response.IsSuccessStatusCode)
		{
			return await HandleResponseContent<TResponse>(response, cancellationToken);
		}

		TData data;
		if (typeof(TData) == typeof(byte[]))
		{
			data = (TData)(object)await response.Content.ReadAsByteArrayAsync(cancellationToken);
		}
		else if (typeof(TData) == typeof(Stream))
		{
			data = (TData)(object)await response.Content.ReadAsStreamAsync(cancellationToken);
		}
		else if (typeof(TData) == typeof(string))
		{
			data = (TData)(object)await response.Content.ReadAsStringAsync(cancellationToken);
		}
		else
		{
			throw new NotSupportedException("Unsupported type for TData");
		}

		return new() { data = data };
	}


	public static HttpResponseMessage PostAsStreamAsync(this HttpClient client, string uri, object requestModel,
		CancellationToken cancellationToken = default)
	{
		var jreq = JsonConvert.SerializeObject(requestModel, defaultJsonSerializerSettings);
		var content = new StringContent(jreq, Encoding.UTF8, "application/json");

		using var request = CreatePostEventStreamRequest(uri, content);

		return SendRequestPreNet6(client, request, cancellationToken);
	}

	private static HttpResponseMessage SendRequestPreNet6(HttpClient client, HttpRequestMessage request,
		CancellationToken cancellationToken)
	{
		var responseTask = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
		var response = responseTask.GetAwaiter().GetResult();
		return response;
	}

	private static HttpRequestMessage CreatePostEventStreamRequest(string uri, HttpContent content)
	{
		var request = new HttpRequestMessage(HttpMethod.Post, uri);
		request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/event-stream"));
		request.Content = content;

		return request;
	}

	public static async Task<TResponse> PostFileAndReadAsAsync<TResponse>(this HttpClient client, string uri,
		HttpContent content, CancellationToken cancellationToken = default) where TResponse : BaseResponse, new()
	{
		var response = await client.PostAsync(uri, content, cancellationToken);
		return await HandleResponseContent<TResponse>(response, cancellationToken);
	}

	// public static async Task<(string stringResponse, TResponse baseResponse)> PostFileAndReadAsStringAsync<TResponse>(
	// 	this HttpClient client, string uri, HttpContent content, CancellationToken cancellationToken = default)
	// 	where TResponse : BaseResponse, new()
	// {
	// 	var response = await client.PostAsync(uri, content, cancellationToken);
	// 	if (response.IsSuccessStatusCode)
	// 	{
	// 		var tResponse = new TResponse
	// 		{
	// 			HttpStatusCode = response.StatusCode,
	// 			HeaderValues = response.ParseHeaders()
	// 		};
	// 		return (await response.Content.ReadAsStringAsync(cancellationToken), tResponse);
	// 	}
	// 	else
	// 	{
	// 		return (null, await HandleResponseContent<TResponse>(response, cancellationToken));
	// 	}
	// }

	public static async Task<TResponse> DeleteAndReadAsAsync<TResponse>(this HttpClient client, string uri,
		CancellationToken cancellationToken = default) where TResponse : BaseResponse, new()
	{
		var response = await client.DeleteAsync(uri, cancellationToken);
		return await HandleResponseContent<TResponse>(response, cancellationToken);
	}

	public static async Task<TResponse> HandleResponseContent<TResponse>(this HttpResponseMessage response,
		CancellationToken cancellationToken) where TResponse : BaseResponse, new()
	{
		TResponse result;

		if (!response.Content.Headers.ContentType?.MediaType?.Equals("application/json",
			    StringComparison.OrdinalIgnoreCase) ?? true)
		{
			result = new()
			{
				error = new Error
				{
					message = await response.Content.ReadAsStringAsync(cancellationToken)
				}
			};
		}
		else
		{
			var str = await response.Content.ReadAsStringAsync(cancellationToken: cancellationToken);
			result = JsonConvert.DeserializeObject<TResponse>(str);
		}

		result.HttpStatusCode = response.StatusCode;
		result.HeaderValues = response.ParseHeaders();

		return result;
	}

	public static ResponseHeaderValues ParseHeaders(this HttpResponseMessage response)
	{
		return new()
		{
			Date = response.Headers.Date,
			Connection = response.Headers.Connection?.ToString(),
			AccessControlAllowOrigin = response.Headers.GetHeaderValue("access-control-allow-origin"),
			CacheControl = response.Headers.GetHeaderValue("cache-control"),
			Vary = response.Headers.Vary?.ToString(),
			XRequestId = response.Headers.GetHeaderValue("x-request-id"),
			StrictTransportSecurity = response.Headers.GetHeaderValue("strict-transport-security"),
			CFCacheStatus = response.Headers.GetHeaderValue("cf-cache-status"),
			SetCookie = response.Headers.Contains("set-cookie")
				? response.Headers.GetValues("set-cookie").ToList()
				: null,
			Server = response.Headers.Server?.ToString(),
			CF_RAY = response.Headers.GetHeaderValue("cf-ray"),
			AltSvc = response.Headers.GetHeaderValue("alt-svc"),
			All = response.Headers.ToDictionary(x => x.Key, x => x.Value.AsEnumerable()),

			RateLimits = new()
			{
				LimitRequests = response.Headers.GetHeaderValue("x-ratelimit-limit-requests"),
				LimitTokens = response.Headers.GetHeaderValue("x-ratelimit-limit-tokens"),
				LimitTokensUsageBased = response.Headers.GetHeaderValue("x-ratelimit-limit-tokens_usage_based"),
				RemainingRequests = response.Headers.GetHeaderValue("x-ratelimit-remaining-requests"),
				RemainingTokens = response.Headers.GetHeaderValue("x-ratelimit-remaining-tokens"),
				RemainingTokensUsageBased = response.Headers.GetHeaderValue("x-ratelimit-remaining-tokens_usage_based"),
				ResetRequests = response.Headers.GetHeaderValue("x-ratelimit-reset-requests"),
				ResetTokens = response.Headers.GetHeaderValue("x-ratelimit-reset-tokens"),
				ResetTokensUsageBased = response.Headers.GetHeaderValue("x-ratelimit-reset-tokens_usage_based")
			},

			OpenAI = new()
			{
				Model = response.Headers.GetHeaderValue("openai-model"),
				Organization = response.Headers.GetHeaderValue("openai-organization"),
				ProcessingMs = response.Headers.GetHeaderValue("openai-processing-ms"),
				Version = response.Headers.GetHeaderValue("openai-version")
			}
		};
	}

	public static async Task<string> ReadAsStringAsync(this HttpContent content, CancellationToken cancellationToken)
	{
		var stream = await content.ReadAsStreamAsync().WithCancellation(cancellationToken);
		using var sr = new StreamReader(stream);
		return await sr.ReadToEndAsync().WithCancellation(cancellationToken);
	}

	public static async Task<AsyncDisposableStream> ReadAsStreamAsync(this HttpContent content,
		CancellationToken cancellationToken)
	{
		var stream = await content.ReadAsStreamAsync().WithCancellation(cancellationToken);
		return new AsyncDisposableStream(stream);
	}

	public static async Task<byte[]> ReadAsByteArrayAsync(this HttpContent content, CancellationToken cancellationToken)
	{
		return await content.ReadAsByteArrayAsync().WithCancellation(cancellationToken);
	}

	public static async Task<Stream> GetStreamAsync(this HttpClient client, string requestUri,
		CancellationToken cancellationToken)
	{
		var response = await client.GetAsync(requestUri, cancellationToken);
		return await response.Content.ReadAsStreamAsync(cancellationToken);
	}

	public static async Task<T> WithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
	{
		var tcs = new TaskCompletionSource<bool>();
		using (cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).TrySetResult(true), tcs))
		{
			if (task != await Task.WhenAny(task, tcs.Task))
			{
				throw new OperationCanceledException(cancellationToken);
			}
		}

		return await task;
	}


	private static Task<Stream> ReadHttpContentStreamAsync(
		HttpContent content,
		CancellationToken cancellationToken)
	{
		return content.ReadAsStreamAsync();
	}

	internal static Encoding GetEncoding(string charset)
	{
		Encoding encoding = (Encoding)null;
		if (charset != null)
		{
			try
			{
				encoding = charset.Length <= 2 || charset[0] != '"' || charset[charset.Length - 1] != '"'
					? Encoding.GetEncoding(charset)
					: Encoding.GetEncoding(charset.Substring(1, charset.Length - 2));
			}
			catch (ArgumentException ex)
			{
				throw new InvalidOperationException("Error Charset:" + charset, (Exception)ex);
			}
		}

		return encoding ??= Encoding.UTF8;
	}

	public static Task<TValue> GetFromJsonAsync<TValue>(
		this HttpClient client,
		string requestUri,
		CancellationToken cancellationToken = default(CancellationToken))
	{
		return client.GetFromJsonAsync<TValue>(requestUri, (JsonSerializerSettings)null, cancellationToken);
	}

	public static async Task<TValue> GetFromJsonAsync<TValue>(
		this HttpClient client,
		string requestUri,
		JsonSerializerSettings options,
		CancellationToken cancellationToken = default(CancellationToken))
	{
		if (client == null)
			throw new ArgumentNullException(nameof(client));
		using var msg = await client.GetAsync(requestUri, HttpCompletionOption.ResponseContentRead, cancellationToken);
		return await GetFromJsonAsyncCore<TValue>(msg, options, cancellationToken);
	}

	public static async Task<T> GetFromJsonAsyncCore<T>(
		this HttpResponseMessage taskResponse,
		JsonSerializerSettings options,
		CancellationToken cancellationToken)
	{
		var str = await taskResponse.Content.ReadAsStringAsync(cancellationToken);
		return JsonConvert.DeserializeObject<T>(str);
	}
}