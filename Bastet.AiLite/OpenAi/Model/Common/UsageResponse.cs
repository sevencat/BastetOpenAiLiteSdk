﻿namespace Bastet.AiLite.OpenAi.Model.Common;

public class UsageResponse
{
	public int prompt_tokens { get; set; }

	public int? completion_tokens { get; set; }

	public int total_tokens { get; set; }

	public CompletionTokensDetails completion_tokens_details { get; set; }

	public PromptTokensDetails prompt_tokens_details { get; set; }
}