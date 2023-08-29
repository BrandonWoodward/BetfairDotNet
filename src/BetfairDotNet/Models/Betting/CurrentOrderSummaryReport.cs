using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// A container representing search results.
/// </summary>
public sealed record CurrentOrderSummaryReport {

    /// <summary>
    /// The list of current orders returned by your query. This will be a valid list (i.e. empty or non-empty but never 'null').
    /// </summary>
    [JsonPropertyName("currentOrders"), JsonRequired]
    public required List<CurrentOrderSummary> CurrentOrders { get; init; }

    /// <summary>
    /// Indicates whether there are further result items beyond this page. Note that underlying data is 
    /// highly time-dependent and the subsequent search orders query might return an empty result.
    /// </summary>
    [JsonPropertyName("moreAvailable"), JsonRequired]
    public required bool MoreAvailable { get; init; }
}
