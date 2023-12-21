using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Streaming;


/// <summary>
/// Filter to select desired markets when subscribing to market changes.
/// </summary>
public sealed class StreamingMarketFilter 
{
    /// <summary>
    /// The countries in which the markets are taking place.
    /// </summary>
    [JsonPropertyName("countryCodes")]
    public List<string>? CountryCodes { get; set; }

    /// <summary>
    /// The betting types to filter on. If empty then returns all markets. 
    /// </summary>
    [JsonPropertyName("bettingTypes")]
    public List<MarketBettingTypeEnum>? BettingTypes { get; set; }

    /// <summary>
    /// Filter on markets that will turn in-play
    /// </summary>
    [JsonPropertyName("turnInPlayEnabled")]
    public bool? TurnInPlayEnabled { get; set; }

    /// <summary>
    /// Filter on market types.
    /// </summary>
    [JsonPropertyName("marketTypes")]
    public List<string>? MarketTypes { get; set; }

    /// <summary>
    /// The venue to filter on.
    /// </summary>
    [JsonPropertyName("venues")]
    public List<string>? Venues { get; set; }

    /// <summary>
    /// Filter on specific market ids.
    /// </summary>
    [JsonPropertyName("marketIds")]
    public List<string>? MarketIds { get; set; }

    /// <summary>
    /// Filter on event type ids.
    /// </summary>
    [JsonPropertyName("eventTypeIds")]
    public List<string>? EventTypeIds { get; set; }

    /// <summary>
    /// Filter on event ids.
    /// </summary>
    [JsonPropertyName("eventIds")]
    public List<string>? EventIds { get; set; }

    /// <summary>
    /// Filter on whether the market offers bsp.
    /// </summary>
    [JsonPropertyName("bspMarket")]
    public bool? BspMarket { get; set; }
}
