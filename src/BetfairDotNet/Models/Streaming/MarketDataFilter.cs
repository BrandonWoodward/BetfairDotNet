using BetfairDotNet.Enums.Streaming;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Streaming;


/// <summary>
/// Filter the market data returned from a market subscription.
/// </summary>
public sealed class MarketDataFilter {

    /// <summary>
    /// Number of best prices to return each side of the book.
    /// </summary>
    [JsonPropertyName("ladderLevels")]
    public int? LadderLevels { get; set; }

    /// <summary>
    /// The price filter fields.
    /// </summary>
    [JsonPropertyName("fields")]
    public List<MarketPriceFilterEnum>? Fields { get; set; }
}
