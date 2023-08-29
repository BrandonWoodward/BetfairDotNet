using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// The result associated with a MarketType.
/// </summary>
public sealed record MarketTypeResult {

    /// <summary>
    /// The MarketType assocated with this result.
    /// </summary>
    [JsonPropertyName("marketType")]
    public string MarketType { get; init; } = string.Empty;

    /// <summary>
    /// The number of markets associated with this result.
    /// </summary>
    [JsonPropertyName("marketCount")]
    public int MarketCount { get; init; }
}
