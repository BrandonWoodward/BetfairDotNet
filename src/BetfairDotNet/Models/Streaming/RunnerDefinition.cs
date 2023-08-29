using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Streaming;


/// <summary>
/// Runner metadata.
/// </summary>
public sealed record RunnerDefinition {

    /// <summary>
    /// The status of the selection (i.e., ACTIVE, REMOVED, WINNER, PLACED, LOSER etc.)
    /// </summary>
    [JsonPropertyName("status")]
    public RunnerStatusEnum Status { get; init; }

    /// <summary>
    /// The order in which the selections are arranged on the Exchange website.
    /// </summary>
    [JsonPropertyName("sortPriority")]
    public int SortPriority { get; init; }

    /// <summary>
    /// The time this selection was removed. Null if active
    /// </summary>
    [JsonPropertyName("removalDate")]
    public DateTime? RemovalDate { get; init; }

    /// <summary>
    /// Unique identifier for the selection.
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; init; }

    /// <summary>
    /// The handicap of the runner (selection) (null if not applicable)
    /// </summary>
    [JsonPropertyName("hc")]
    public double? Handicap { get; init; }

    /// <summary>
    /// Adjustment factor resulting from removed selections.
    /// </summary>
    [JsonPropertyName("adjustmentFactor")]
    public double AdjustmentFactor { get; init; }

    /// <summary>
    /// The BSP of the runner, null if BSP not yet reconciled
    /// </summary>
    [JsonPropertyName("bsp")]
    public double Bsp { get; init; }
}
