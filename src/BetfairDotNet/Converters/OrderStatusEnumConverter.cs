using System.Text.Json;
using System.Text.Json.Serialization;
using BetfairDotNet.Enums.Betting;

namespace BetfairDotNet.Converters;

internal sealed class OrderStatusEnumConverter : JsonConverter<OrderStatusEnum>
{
    public override OrderStatusEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return value switch
        {
            "E" or "EXECUTABLE" => OrderStatusEnum.EXECUTABLE,
            "EC" or "EXECUTION_COMPLETE" => OrderStatusEnum.EXECUTION_COMPLETE,
            "PENDING" => OrderStatusEnum.PENDING,
            "EXPIRED" => OrderStatusEnum.EXPIRED,
            _ => throw new ArgumentException("OrderTypeEnum Type not specified")
        };
    }

    public override void Write(Utf8JsonWriter writer, OrderStatusEnum value, JsonSerializerOptions options)
    {
        var result = value switch
        {
            OrderStatusEnum.EXECUTABLE => "EXECUTABLE",
            OrderStatusEnum.EXECUTION_COMPLETE => "EXECUTION_COMPLETE",
            OrderStatusEnum.PENDING => "PENDING",
            OrderStatusEnum.EXPIRED => "EXPIRED",
            _ => throw new ArgumentException("Invalid OrderStatusEnum value")
        };
        writer.WriteStringValue(result);
    }
}