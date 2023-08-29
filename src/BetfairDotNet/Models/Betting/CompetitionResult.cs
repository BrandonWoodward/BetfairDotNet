using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Betting;


/// <summary>
/// The result of a competition.
/// </summary>
public sealed record CompetitionResult {

    /// <summary>
    /// The competition associated with this result.
    /// </summary>
    [JsonPropertyName("competition")]
    public Competition? Competition { get; init; }

    /// <summary>
    /// Count of markets associated with this competition.
    /// </summary>
    [JsonPropertyName("marketCount")]
    public int MarketCount { get; init; }

    /// <summary>
    /// The region in which this competition is happening.
    /// </summary>
    [JsonPropertyName("competitionRegion")]
    public string CompetitionRegion { get; init; } = string.Empty;
}
