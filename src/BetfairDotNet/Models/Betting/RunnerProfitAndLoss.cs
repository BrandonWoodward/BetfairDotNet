using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Profit and loss if selection is wins or loses.
/// </summary>
public sealed record RunnerProfitAndLoss {


    /// <summary>
    /// The unique identifier for the selection.
    /// </summary>
    [JsonPropertyName("selectionId")]
    public long SelectionId { get; init; }

    /// <summary>
    /// Profit or loss for the market if this selection is the winner.
    /// </summary>
    [JsonPropertyName("ifWin")]
    public double IfWin { get; init; }

    /// <summary>
    /// Profit or loss for the market if this selection is the loser. Only returned for multi-winner odds markets.
    /// </summary>
    [JsonPropertyName("ifLose")]
    public double IfLose { get; init; }

    /// <summary>
    /// Profit or loss for the market if this selection is placed. Applies to marketType EACH_WAY only.
    /// </summary>
    [JsonPropertyName("ifPlace")]
    public double IfPlace { get; init; }
}
