using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Betting;


/// <summary>
/// A container representing search results.
/// </summary>
public sealed record ClearedOrderSummaryReport {

    /// <summary>
    /// The list of cleared orders returned by your query. 
    /// This will be a valid list (i.e. empty or non-empty but never 'null').
    /// </summary>
    [JsonPropertyName("clearedOrders"), JsonRequired]
    public required List<ClearedOrderSummary> ClearedOrders { get; init; }

    /// <summary>
    /// Indicates whether there are further result items beyond this page. 
    /// Note that underlying data is highly time-dependent and the subsequent search orders query might return an empty result.
    /// </summary>
    [JsonPropertyName("moreAvailable"), JsonRequired]
    public required bool MoreAvailable { get; init; }
}
