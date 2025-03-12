using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bastet.AiLite.OpenAi.Model.Chat;

namespace Bastet.AiLite.OpenAi.Managers;

public partial class OpenAIService : IChatCompletionService
{
	public Task<ChatCompletionCreateResponse> CreateCompletion(ChatCompletionCreateRequest chatCompletionCreate, string modelId = null,
		CancellationToken cancellationToken = default)
	{
		throw new System.NotImplementedException();
	}

	public IAsyncEnumerable<ChatCompletionCreateResponse> CreateCompletionAsStream(ChatCompletionCreateRequest chatCompletionCreate, string modelId = null,
		bool justDataMode = true, CancellationToken cancellationToken = default)
	{
		throw new System.NotImplementedException();
	}
}