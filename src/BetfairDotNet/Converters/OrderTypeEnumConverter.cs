using System.Text.Json;
using System.Text.Json.Serialization;
using BetfairDotNet.Enums.Betting;

namespace BetfairDotNet.Converters;

internal sealed class OrderTypeEnumConverter : JsonConverter<OrderTypeEnum>
{
    public override OrderTypeEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return value switch
        {
            "L" or "LIMIT" => OrderTypeEnum.LIMIT,
            "LOC" or "LIMIT_ON_CLOSE" => OrderTypeEnum.LIMIT_ON_CLOSE,
            "MOC" or "MARKET_ON_CLOSE" => OrderTypeEnum.MARKET_ON_CLOSE,
            _ => throw new ArgumentException("OrderTypeEnum Type not specified")
        };
    }

    public override void Write(Utf8JsonWriter writer, OrderTypeEnum value, JsonSerializerOptions options)
    {
        var result = value switch
        {
            OrderTypeEnum.LIMIT => "LIMIT",
            OrderTypeEnum.LIMIT_ON_CLOSE => "LIMIT_ON_CLOSE",
            OrderTypeEnum.MARKET_ON_CLOSE => "MARKET_ON_CLOSE",
            _ => throw new ArgumentException("Invalid OrderTypeEnum value")
        };
        writer.WriteStringValue(result);
    }
}