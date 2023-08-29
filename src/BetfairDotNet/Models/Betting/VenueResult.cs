using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// The result associated with a particular venue.
/// </summary>
public sealed record VenueResult {

    /// <summary>
    /// The venue name.
    /// </summary>
    [JsonPropertyName("venue")]
    public string Venue { get; init; } = string.Empty;

    /// <summary>
    /// Count of markets associated with this venue.
    /// </summary>
    [JsonPropertyName("marketCount")]
    public int MarketCount { get; init; }
}
