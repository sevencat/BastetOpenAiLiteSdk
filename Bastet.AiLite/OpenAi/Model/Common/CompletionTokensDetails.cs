namespace Bastet.AiLite.OpenAi.Model.Common;

public record CompletionTokensDetails
{
	public int reasoning_tokens { get; set; }
	public int audio_tokens { get; set; }
}