using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Market rates.
/// </summary>
public sealed record MarketRates {

    /// <summary>
    /// Market base rate of commission.
    /// </summary>
    [JsonPropertyName("marketBaseRate"), JsonRequired]
    public required double MarketBaseRate { get; init; }

    /// <summary>
    /// Is discount allowed on market.
    /// </summary>
    [JsonPropertyName("discountAllowed"), JsonRequired]
    public required bool DiscountAllowed { get; init; }
}
