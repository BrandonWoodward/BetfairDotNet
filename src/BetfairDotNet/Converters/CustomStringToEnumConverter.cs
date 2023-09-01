using System.Text.Json;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Converters;


internal class CustomStringToEnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : Enum {

    public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        var str = reader.GetString();
        if(string.IsNullOrEmpty(str)) {
            return (TEnum)Enum.GetValues(typeToConvert).GetValue(0)!;
        }
        if(Enum.TryParse(typeToConvert, str, true, out var result)) {
            return (TEnum)result;
        }
        return (TEnum)Enum.GetValues(typeToConvert).GetValue(0)!;
    }


    public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options) {
        writer.WriteStringValue(value.ToString());
    }
}
