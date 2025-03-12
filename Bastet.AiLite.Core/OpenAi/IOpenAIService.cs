namespace Bastet.AiLite.OpenAi;

public interface IOpenAIService
{
	public IFileService Files { get; }
	
	public IChatCompletionService ChatCompletion { get; }
}