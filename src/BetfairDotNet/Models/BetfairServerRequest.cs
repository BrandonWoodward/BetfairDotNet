using System.Text.Json.Serialization;

namespace BetfairDotNet.Models;


public sealed class BetfairServerRequest 
{
    [JsonPropertyName("jsonrpc")]
    public string JsonRpc { get; private set; } = "2.0";

    [JsonPropertyName("method")]
    public required string Method { get; set; }

    [JsonPropertyName("params")]
    public Dictionary<string, object?>? Params { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; } = 1;
}
