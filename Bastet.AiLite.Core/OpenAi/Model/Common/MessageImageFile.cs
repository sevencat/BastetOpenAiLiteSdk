using Newtonsoft.Json;

namespace Bastet.AiLite.OpenAi.Model.Common;

public class MessageImageFile
{
	public string file_id { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public string detail { get; set; }
}