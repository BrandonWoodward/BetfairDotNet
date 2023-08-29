using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// The result associated with an EventType.
/// </summary>
public sealed record EventTypeResult {

    /// <summary>
    /// The EventType assocated with this result.
    /// </summary>
    [JsonPropertyName("eventType")]
    public EventType? EventType { get; init; }

    /// <summary>
    /// Count of markets associated with this eventType.
    /// </summary>
    [JsonPropertyName("marketCount")]
    public int MarketCount { get; init; }
}
