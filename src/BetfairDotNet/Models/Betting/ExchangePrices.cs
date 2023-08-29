using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Contains the prices and traded volumes at both exchange and SP for a given selection.
/// </summary>
public sealed record ExchangePrices {

    /// <summary>
    /// Best prices available for the Back bets.
    /// </summary>
    [JsonPropertyName("availableToBack")]
    public List<PriceSize>? AvailableToBack { get; init; }

    /// <summary>
    /// Best prices available for the Lay bets.
    /// </summary>
    [JsonPropertyName("availableToLay")]
    public List<PriceSize>? AvailableToLay { get; init; }

    /// <summary>
    /// Traded volume by price.
    /// </summary>
    [JsonPropertyName("tradedVolume")]
    public List<PriceSize>? TradedVolume { get; init; }
}
