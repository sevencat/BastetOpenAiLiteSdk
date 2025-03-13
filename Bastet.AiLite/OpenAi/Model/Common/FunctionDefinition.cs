using Newtonsoft.Json;

namespace Bastet.AiLite.OpenAi.Model.Common;

public class FunctionDefinition
{
	public string name { get; set; }

	/// <summary>
	///     A description of what the function does, used by the model to choose when and how to call the function.
	/// </summary>
	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public string description { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public PropertyDefinition parameters { get; set; }


	/// <summary>
	///     Whether to enable strict schema adherence when generating the function call. If set to true, the model will follow
	///     the exact schema defined in the parameters field. Only a subset of JSON Schema is supported when strict is true.
	///     Learn more about Structured Outputs in the <a href="https://platform.openai.com/docs/api-reference/chat/docs/guides/function-calling">function calling guide</a>.
	/// </summary>
	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public bool? strict { get; set; }
}