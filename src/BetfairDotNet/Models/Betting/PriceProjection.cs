using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Selection criteria of the returning price data.
/// </summary>
public sealed class PriceProjection {

    /// <summary>
    /// The basic price data you want to receive in the response.
    /// </summary>
    [JsonPropertyName("priceData")]
    public List<PriceDataEnum>? PriceData { get; set; }

    /// <summary>
    /// Options to alter the default representation of best offer prices Applicable to EX_BEST_OFFERS priceData selection
    /// </summary>
    [JsonPropertyName("exBestOffersOverrides")]
    public ExBestOffersOverrides? ExBestOffersOverrides { get; set; }

    /// <summary>
    /// Indicates if the returned prices should include virtual prices. 
    /// Applicable to EX_BEST_OFFERS and EX_ALL_OFFERS priceData selections, default value is false.
    /// <para>Should be set to true to replicate prices shown on the Betfair Exchange website.</para>
    /// </summary>
    [JsonPropertyName("virtualise")]
    public bool Virtualise { get; set; }

    /// <summary>
    /// Indicates if the volume returned at each price point should be the absolute value or a 
    /// cumulative sum of volumes available at the price and all better prices. If unspecified defaults to false. 
    /// <para>Applicable to EX_BEST_OFFERS and EX_ALL_OFFERS price projections. Not supported as yet.</para>
    /// </summary>
    [JsonPropertyName("rolloverStakes")]
    public bool RolloverStakes { get; set; }
}
