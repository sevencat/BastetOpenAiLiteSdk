using System.Collections.Generic;

namespace Bastet.AiLite.OpenAi.Model.Chat;

public class ChatChoiceResponse
{
	public ChatMessage delta
	{
		get => message;
		set => message = value;
	}

	public ChatMessage message { get; set; }

	public int? index { get; set; }

	public string finish_reason { get; set; }

	public FinishDetailsResponse finish_details { get; set; }

	public ChatLogProbsResponse logprobs { get; set; }

	public class FinishDetailsResponse
	{
		public string type { get; set; }

		public string stop { get; set; }
	}

	public class ChatLogProbsResponse
	{
		public List<ContentItem> content { get; set; }
	}

	public class ContentItemBase
	{
		public string token { get; set; }

		public double logprob { get; set; }

		public List<int> bytes { get; set; }
	}

	public class ContentItem : ContentItemBase
	{
		public List<ContentItemBase> top_logprobs { get; set; }
	}
}