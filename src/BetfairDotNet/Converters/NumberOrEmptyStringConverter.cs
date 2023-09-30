using System.Text.Json;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Converters;

internal class NumberOrEmptyStringConverter : JsonConverter<int>
{
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var stringValue = reader.GetString();
            if (string.IsNullOrEmpty(stringValue)) return default;
            if (int.TryParse(stringValue, out var intValue)) return intValue;
            throw new JsonException($"Unexpected value '{stringValue}' encountered when parsing integer.");
        }
        if (reader.TokenType == JsonTokenType.Number)
        {
            return reader.GetInt32();
        }

        throw new JsonException($"Unexpected token {reader.TokenType} encountered when parsing integer.");
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
    {
        writer.WriteNumberValue(value);
    }
}