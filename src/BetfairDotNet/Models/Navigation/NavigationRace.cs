using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Navigation;

public sealed record NavigationRace : NavigationItem
{
    [JsonPropertyName("startTime")]
    public DateTime? StartTime { get; init; }
    
    [JsonPropertyName("venue")]
    public string Venue { get; init; } = string.Empty;
    
    [JsonPropertyName("raceNumber")]
    public string RaceNumber { get; init; }
    
    [JsonPropertyName("type")]
    public override string Type { get; } = "RACE";
}