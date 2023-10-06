using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Navigation;

/// <summary>
/// Only applies to Horse Racing and Greyhound Racing.
/// </summary>
public sealed record NavigationRace : NavigationItem
{
    /// <summary>
    /// The scheduled start time of the event.
    /// </summary>
    [JsonPropertyName("startTime")]
    public DateTime? StartTime { get; init; }
    
    /// <summary>
    /// The course or track.
    /// </summary>
    [JsonPropertyName("venue")]
    public string Venue { get; init; } = string.Empty;
    
    /// <summary>
    /// US specific information about race numbers.
    /// </summary>
    [JsonPropertyName("raceNumber")]
    public string RaceNumber { get; init; } = string.Empty;
    
    /// <summary>
    /// Always RACE
    /// </summary>
    [JsonPropertyName("type")]
    public override string Type { get; } = "RACE";
    
    /// <summary>
    /// The country in which the event is taking place.
    /// </summary>
    [JsonPropertyName("countryCode")]
    public string CountryCode { get; init; } = string.Empty;
}