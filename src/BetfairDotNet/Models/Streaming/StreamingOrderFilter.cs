using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Streaming;


/// <summary>
/// Filter orders by accounts.
/// </summary>
public sealed record StreamingOrderFilter 
{
    /// <summary>
    /// Returns overall / net position
    /// </summary>
    [JsonPropertyName("includeOverallPosition")]
    public bool IncludeOverallPosition { get; set; }

    /// <summary>
    /// Restricts to specified customerStrategyRefs (specified in placeOrders).
    /// This will filter orders and StrategyMatchChanges accordingly (Note: overall position is not filtered)
    /// </summary>
    [JsonPropertyName("customerStrategyRefs")]
    public List<string>? CustomerStrategyRefs { get; set; }

    /// <summary>
    /// Returns strategy positions
    /// </summary>
    [JsonPropertyName("partitionMatchedByStrategyRef")]
    public bool PartitionMatchedByStrategyRef { get; set; }
}
