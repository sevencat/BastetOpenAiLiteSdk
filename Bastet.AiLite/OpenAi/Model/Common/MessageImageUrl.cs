using Newtonsoft.Json;

namespace Bastet.AiLite.OpenAi.Model.Common;

public class MessageImageUrl
{
	public string url { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public string detail { get; set; }
}