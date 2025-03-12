using Newtonsoft.Json;

namespace Bastet.AiLite.OpenAi.Model.Common;

public class ToolCall
{
	public int index { get; set; }
	
	public string id { get; set; }
	
	public string type { get; set; }
	
	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public FunctionCall function { get; set; }
}