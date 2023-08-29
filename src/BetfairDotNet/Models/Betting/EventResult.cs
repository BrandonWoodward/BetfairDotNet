using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// The result of a completed event.
/// </summary>
public sealed record EventResult {

    /// <summary>
    /// The event on which the result applies.
    /// </summary>
    [JsonPropertyName("event")]
    public Event? Event { get; init; }

    /// <summary>
    /// Count of markets associated with this event.
    /// </summary>
    [JsonPropertyName("marketCount")]
    public int MarketCount { get; init; }
}
