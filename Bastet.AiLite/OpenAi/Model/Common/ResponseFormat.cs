using Bastet.AiLite.OpenAi.Model.Convert;
using Newtonsoft.Json;

namespace Bastet.AiLite.OpenAi.Model.Common;

/// <summary>
///     An object specifying the format that the model must output.
///     Used to enable JSON mode.
/// </summary>
public class ResponseFormat
{
	/// <summary>
	///     Setting to json_object enables JSON mode.
	///     This guarantees that the message the model generates is valid JSON.
	///     Note that the message content may be partial if finish_reason="length",
	///     which indicates the generation exceeded max_tokens or the conversation exceeded the max context length.
	/// </summary>

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public string type { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public JsonSchema json_schema { get; set; }
}

public class JsonSchema
{
	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public string description { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public string name { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public bool? strict { get; set; }

	[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
	public PropertyDefinition schema { get; set; }
}

[JsonConverter(typeof(ResponseFormatOptionConverter))]
public class ResponseFormatOneOfType
{
	public ResponseFormatOneOfType()
	{
	}

	public ResponseFormatOneOfType(string asString)
	{
		AsString = asString;
	}

	public ResponseFormatOneOfType(ResponseFormat asObject)
	{
		AsObject = asObject;
	}

	[JsonIgnore]
	public string AsString { get; set; }

	[JsonIgnore]
	public ResponseFormat AsObject { get; set; }
}