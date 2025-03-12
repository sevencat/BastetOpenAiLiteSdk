using Newtonsoft.Json;

namespace Bastet.AiLite.OpenAi.Model.Common;

public class FunctionCall
{
	public string name { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public string arguments { get; set; }
}