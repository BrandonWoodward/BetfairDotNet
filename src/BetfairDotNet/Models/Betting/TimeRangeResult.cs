using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// The result of an associated TimeRange.
/// </summary>
public sealed record TimeRangeResult {

    /// <summary>
    /// The TimeRange associated with this result.
    /// </summary>
    [JsonPropertyName("timeRange")]
    public TimeRange? TimeRange { get; init; }

    /// <summary>
    /// Count of markets associated with this TimeRange
    /// </summary>
    [JsonPropertyName("marketCount")]
    public int MarketCount { get; init; }
}
