using System.Text.Json;
using System.Text.Json.Serialization;
using BetfairDotNet.Enums.Betting;

namespace BetfairDotNet.Converters;

internal sealed class PersistenceTypeEnumConverter : JsonConverter<PersistenceTypeEnum>
{
    public override PersistenceTypeEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return value switch
        {
            "L" or "LAPSE" => PersistenceTypeEnum.LAPSE,
            "P" or "PERSIST" => PersistenceTypeEnum.PERSIST,
            "MOC" or "MARKET_ON_CLOSE" => PersistenceTypeEnum.MARKET_ON_CLOSE,
            _ => throw new ArgumentException("PersistenceTypeEnum Type not specified")
        };
    }

    public override void Write(Utf8JsonWriter writer, PersistenceTypeEnum value, JsonSerializerOptions options)
    {
        var result = value switch
        {
            PersistenceTypeEnum.LAPSE => "LAPSE",
            PersistenceTypeEnum.PERSIST => "PERSIST",
            PersistenceTypeEnum.MARKET_ON_CLOSE => "MARKET_ON_CLOSE",
            _ => throw new ArgumentException("Invalid PersistenceType value")
        };
        writer.WriteStringValue(result);
    }
}