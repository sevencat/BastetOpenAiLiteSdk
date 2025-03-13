using Newtonsoft.Json;

namespace Bastet.AiLite.OpenAi.Model.Common;

public class MessageContent
{
	public string type { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public string text { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public MessageImageUrl image_url { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public MessageImageFile image_file { get; set; }
}