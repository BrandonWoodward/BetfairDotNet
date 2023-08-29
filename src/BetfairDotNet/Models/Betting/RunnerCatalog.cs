using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Information about the Runners (selections) in a market.
/// </summary>
public sealed record RunnerCatalog {

    /// <summary>
    /// The unique id for the selection. 
    /// The same selectionId and runnerName pairs are used accross all Betfair markets which contain them. 
    /// </summary>
    [JsonPropertyName("selectionId"), JsonRequired]
    public required long SelectionId { get; init; }

    /// <summary>
    /// The name of the runner.
    /// </summary>
    [JsonPropertyName("runnerName"), JsonRequired]
    public required string RunnerName { get; init; }

    /// <summary>
    /// The handicap applies to market with the MarketBettingType 
    /// ASIAN_HANDICAP_SINGLE_LINE & ASIAN_HANDICAP_DOUBLE_LINE only otherwise '0'
    /// </summary>
    [JsonPropertyName("handicap"), JsonRequired]
    public required double Handicap { get; init; }

    /// <summary>
    /// The sort priority of this runner. Indicates the order in which the runners are displayed on the Betfair Exchange website.
    /// </summary>
    [JsonPropertyName("sortPriority"), JsonRequired]
    public required int SortPriority { get; init; }

    /// <summary>
    /// Metadata associated with the runner.  
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, string> Metadata { get; init; } = new();
}
