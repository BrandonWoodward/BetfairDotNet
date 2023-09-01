using System.Text.Json;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Converters;


internal class DoubleNaNToNullConverter : JsonConverter<double?> {

    public override double? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        if(reader.TokenType == JsonTokenType.String) {
            var stringValue = reader.GetString();
            return stringValue == "NaN"
                ? null
                : double.TryParse(stringValue, out var value)
                ? value
                : throw new JsonException($"Unexpected value {stringValue} for double.");
        }
        else if(reader.TokenType == JsonTokenType.Number) {
            return reader.GetDouble();
        }
        else if(reader.TokenType == JsonTokenType.Null) {
            return null;
        }
        throw new JsonException($"Unexpected token type {reader.TokenType}.");
    }


    public override void Write(Utf8JsonWriter writer, double? value, JsonSerializerOptions options) {
        if(value.HasValue)
            writer.WriteNumberValue(value.Value);
        else
            writer.WriteNullValue();
    }
}
