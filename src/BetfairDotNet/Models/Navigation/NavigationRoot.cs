using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Navigation;

public sealed record NavigationRoot
{
    [JsonPropertyName("id")]
    public int Id { get; init; } = 0;
    
    [JsonPropertyName("name")]
    public string Name { get; init; } = "ROOT";
    
    [JsonPropertyName("type")]
    public string Type { get; init; } = "GROUP";
    
    [JsonPropertyName("children")]
    public List<NavigationEventType> EventTypes { get; init; } = new();
}