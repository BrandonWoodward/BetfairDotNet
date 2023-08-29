using BetfairDotNet.Utils;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Streaming;


/// <summary>
/// Change message for a runner.
/// </summary>
internal sealed record RunnerChange {

    /// <summary>
    /// The total amount matched. This value is truncated at 2dp.
    /// </summary>
    [JsonPropertyName("tv")]
    public double? TotalVolume { get; init; }

    /// <summary>
    ///  Best Available To Back - LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove)
    /// </summary>
    [JsonPropertyName("batb")]
    public List<List<double>>? BestAvailableToBack { get; init; }

    /// <summary>
    /// Starting Price Back - PriceVol tuple delta of price changes (0 vol is remove)
    /// </summary>
    [JsonPropertyName("spb")]
    public List<List<double>>? StartingPriceBack { get; init; }

    /// <summary>
    /// Best Display Available To Lay (includes virtual prices)- LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove)
    /// </summary>
    [JsonPropertyName("bdatl")]
    public List<List<double>>? BestAvailableToLayVirtual { get; init; }

    /// <summary>
    /// Traded - PriceVol tuple delta of price changes (0 vol is remove)
    /// </summary>
    [JsonPropertyName("trd")]
    public List<List<double>>? TradedVolume { get; init; }

    /// <summary>
    /// Starting Price Far - The far starting price (or null if un-changed)
    /// </summary>
    [JsonPropertyName("spf")]
    [JsonConverter(typeof(DoubleNaNToNullConverter))]
    // Sometimes API return "NaN" which deserializers as a string, causing exception
    public double? StartingPriceFar { get; init; }

    /// <summary>
    /// Last Traded Price - The last traded price (or null if un-changed)
    /// </summary>
    [JsonPropertyName("ltp")]
    public double? LastTradedPrice { get; init; }

    /// <summary>
    /// Available To Back - PriceVol tuple delta of price changes (0 vol is remove)
    /// </summary>
    [JsonPropertyName("atb")]
    public List<List<double>>? AvailableToBack { get; init; }

    /// <summary>
    /// Starting Price Lay - PriceVol tuple delta of price changes (0 vol is remove)
    /// </summary>
    [JsonPropertyName("spl")]
    public List<List<double>>? StartingPriceLay { get; init; }

    /// <summary>
    /// Starting Price Near - The near starting price (or null if un-changed)
    /// </summary>
    // Sometimes API return "NaN" which deserializers as a string, causing exception
    [JsonPropertyName("spn")]
    [JsonConverter(typeof(DoubleNaNToNullConverter))]
    public double? StartingPriceNear { get; init; }

    /// <summary>
    /// Available To Lay - PriceVol tuple delta of price changes (0 vol is remove)
    /// </summary>
    [JsonPropertyName("atl")]
    public List<List<double>>? AvailableToLay { get; init; }

    /// <summary>
    /// Best Available To Lay - LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove)
    /// </summary>
    [JsonPropertyName("batl")]
    public List<List<double>>? BestAvailableToLay { get; init; }

    /// <summary>
    /// Selection Id - the id of the runner (selection)
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; init; }

    /// <summary>
    /// Handicap - the handicap of the runner (selection) (null if not applicable)
    /// </summary>
    [JsonPropertyName("hc")]
    public double? Handicap { get; init; }

    /// <summary>
    /// Best Display Available To Back (includes virtual prices)- LevelPriceVol triple delta of price changes, keyed by level (0 vol is remove)
    /// </summary>
    [JsonPropertyName("bdatb")]
    public List<List<double>>? BestAvailableToBackVirtual { get; init; }
}
