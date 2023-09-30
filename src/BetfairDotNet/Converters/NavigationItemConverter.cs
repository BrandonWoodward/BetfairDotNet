using System.Text.Json;
using System.Text.Json.Serialization;
using BetfairDotNet.Models.Navigation;

namespace BetfairDotNet.Converters;

internal class NavigationItemConverter : JsonConverter<NavigationItem>
{
    public override NavigationItem Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var type = doc.RootElement.GetProperty("type").GetString() ?? string.Empty;

        return type switch
        {
            "GROUP" => JsonSerializer.Deserialize<NavigationGroup>(doc.RootElement.GetRawText()) ?? new(),
            "EVENT" => JsonSerializer.Deserialize<NavigationEvent>(doc.RootElement.GetRawText()) ?? new(),
            "MARKET" => JsonSerializer.Deserialize<NavigationMarket>(doc.RootElement.GetRawText()) ?? new(),
            "RACE" => JsonSerializer.Deserialize<NavigationRace>(doc.RootElement.GetRawText()) ?? new(),
            _ => throw new JsonException($"Unknown type {type}")
        };
    }

    public override void Write(Utf8JsonWriter writer, NavigationItem value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}