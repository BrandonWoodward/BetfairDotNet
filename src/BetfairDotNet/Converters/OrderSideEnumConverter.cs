using System.Text.Json;
using System.Text.Json.Serialization;
using BetfairDotNet.Enums.Betting;

namespace BetfairDotNet.Converters;

internal sealed class OrderSideEnumConverter : JsonConverter<SideEnum>
{
    public override SideEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return value switch
        {
            "B" or "BACK" => SideEnum.BACK,
            "L" or "LAY" => SideEnum.LAY,
            _ => throw new ArgumentException("SideEnum Type not specified")
        };
    }

    public override void Write(Utf8JsonWriter writer, SideEnum value, JsonSerializerOptions options)
    {
        var result = value switch
        {
            SideEnum.BACK => "BACK",
            SideEnum.LAY => "LAY",
            _ => throw new ArgumentException("Invalid OrderSideEnum value")
        };
        writer.WriteStringValue(result);
    }
}





