using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Bastet.AiLite.OpenAi.Model.Chat;
using Bastet.AiLite.Util;

namespace Bastet.AiLite.OpenAi.Managers;

public partial class OpenAIService : IChatCompletionService
{
	public async Task<ChatCompletionCreateResponse> CreateCompletion(
		ChatCompletionCreateRequest chatCompletionCreateRequest,
		string modelId = null,
		CancellationToken cancellationToken = default)
	{
		chatCompletionCreateRequest.model = modelId == null ? modelId : _options.DefaultModel;
		return await _httpClient.PostAndReadAsAsync<ChatCompletionCreateResponse>(
			_endpointProvider.ChatCompletionCreate(), chatCompletionCreateRequest, cancellationToken);
	}

	public async IAsyncEnumerable<ChatCompletionCreateResponse> CreateCompletionAsStream(
		ChatCompletionCreateRequest chatCompletionCreateRequest, string modelId = null,
		bool justDataMode = true, [EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		// Mark the request as streaming
		chatCompletionCreateRequest.stream = true;

		// Send the request to the CompletionCreate endpoint
		chatCompletionCreateRequest.model = modelId == null ? modelId : _options.DefaultModel;

		using var response = _httpClient.PostAsStreamAsync(_endpointProvider.ChatCompletionCreate(),
			chatCompletionCreateRequest, cancellationToken);

		if (!response.IsSuccessStatusCode)
		{
			yield return await response.HandleResponseContent<ChatCompletionCreateResponse>(cancellationToken);
			yield break;
		}

		await foreach (var baseResponse in response.AsStream<ChatCompletionCreateResponse>(
			               cancellationToken: cancellationToken)) yield return baseResponse;
	}
}