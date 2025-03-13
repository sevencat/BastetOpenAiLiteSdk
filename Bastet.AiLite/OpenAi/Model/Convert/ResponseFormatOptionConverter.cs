using System;
using Bastet.AiLite.OpenAi.Model.Common;
using Newtonsoft.Json;

namespace Bastet.AiLite.OpenAi.Model.Convert;

public class ResponseFormatOptionConverter : JsonConverter<ResponseFormatOneOfType>
{
	public override void WriteJson(JsonWriter writer, ResponseFormatOneOfType value, JsonSerializer serializer)
	{
		if (value?.AsString != null)
		{
			writer.WriteValue(value.AsString);
		}
		else if (value?.AsObject != null)
		{
			serializer.Serialize(writer, value.AsObject);
		}
		else
		{
			writer.WriteNull();
		}
	}

	public override ResponseFormatOneOfType ReadJson(JsonReader reader, Type objectType,
		ResponseFormatOneOfType existingValue,
		bool hasExistingValue, JsonSerializer serializer)
	{
		var tokentype = reader.TokenType;
		if (tokentype == JsonToken.String)
			return new ResponseFormatOneOfType(reader.ReadAsString());
		if (tokentype == JsonToken.StartObject)
		{
			var obj = serializer.Deserialize<ResponseFormat>(reader);
			return new ResponseFormatOneOfType(obj);
		}

		throw new JsonException();
	}
}