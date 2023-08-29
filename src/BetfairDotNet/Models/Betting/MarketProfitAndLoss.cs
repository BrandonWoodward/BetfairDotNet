using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Profit and loss in a market.
/// </summary>
public sealed record MarketProfitAndLoss {

    /// <summary>
    /// The unique identifier for the market.
    /// </summary>
    [JsonPropertyName("marketId")]
    public string MarketId { get; set; } = string.Empty;

    /// <summary>
    /// The commission rate applied to P&L values. Only returned if netOfCommision option is requested
    /// </summary>
    [JsonPropertyName("commissionApplied")]
    public double CommissionApplied { get; set; }

    /// <summary>
    /// Calculated profit and loss data.
    /// </summary>
    [JsonPropertyName("profitAndLosses")]
    public List<RunnerProfitAndLoss> ProfitAndLosses { get; set; } = new();
}
