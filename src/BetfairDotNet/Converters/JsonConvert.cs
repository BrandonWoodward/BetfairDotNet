using System.Text.Json;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Converters;

internal static class JsonConvert
{
    private static readonly JsonSerializerOptions _options;

    static JsonConvert()
    {
        _options = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            ReadCommentHandling = JsonCommentHandling.Skip,
        };
    }

    public static string Serialize<T>(T data)
    {
        var serialized = JsonSerializer.Serialize(data, _options);
        return serialized ?? throw new JsonException("Json serialization resulted in a null value.");
    }
    
    public static T Deserialize<T>(string json)
    {
        var deserialized = JsonSerializer.Deserialize<T>(json, _options);
        return deserialized ?? throw new JsonException("Json deserialization resulted in a null value.");
    }

    public static T Deserialize<T>(ReadOnlyMemory<byte> memory)
    {
        var deserialized = JsonSerializer.Deserialize<T>(memory.Span, _options);
        return deserialized ?? throw new JsonException("Json deserialization resulted in a null value.");
    }
}
