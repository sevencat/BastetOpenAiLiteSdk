namespace Bastet.AiLite.OpenAi.Managers;

public partial class OpenAIService : IOpenAIService
{
	public IFileService Files => this;

	public IChatCompletionService ChatCompletion => this;
}