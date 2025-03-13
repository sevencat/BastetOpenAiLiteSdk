using System;
using System.Collections.Generic;
using Bastet.AiLite.OpenAi.Model.Common;

namespace Bastet.AiLite.OpenAi.Model.Chat;

public class ChatCompletionCreateResponse : BaseResponse
{
	public string model { get; set; }

	public List<ChatChoiceResponse> choices { get; set; }

	public UsageResponse usage { get; set; }

	public string system_fingerprint { get; set; }

	public string service_tier { get; set; }

	public long created { get; set; }

	public DateTimeOffset CreatedAt => DateTimeOffset.FromUnixTimeSeconds(created);

	public string id { get; set; }
}