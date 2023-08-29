using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Filter options for listMaketCatalogue.
/// </summary>
public sealed class MarketFilter {

    /// <summary>
    /// Restrict markets by any text associated with the Event name. 
    /// You can include a wildcard (*) character as long as it is not the first character. 
    /// The textQuery field doesn't evaluate market or selection names.
    /// </summary>
    [JsonPropertyName("textQuery")]
    public string? TextQuery { get; set; }

    /// <summary>
    /// Restrict markets by event type associated with the market. (i.e., Football, Hockey, etc)
    /// </summary>
    [JsonPropertyName("eventTypeIds")]
    public List<string>? EventTypeIds { get; set; }

    /// <summary>
    /// Restrict markets by the event id associated with the market.
    /// </summary>
    [JsonPropertyName("eventIds")]
    public List<string>? EventIds { get; set; }

    /// <summary>
    /// Restrict markets by the competitions associated with the market.
    /// </summary>
    [JsonPropertyName("competitionIds")]
    public List<string>? CompetitionIds { get; set; }

    /// <summary>
    /// Restrict markets by the market id associated with the market.
    /// </summary>
    [JsonPropertyName("marketIds")]
    public List<string>? MarketIds { get; set; }

    /// <summary>
    /// Restrict markets by the venue associated with the market. 
    /// Currently, only Horse & Greyhound racing markets have venues.
    /// </summary>
    [JsonPropertyName("venues")]
    public List<string>? Venues { get; set; }

    /// <summary>
    /// Restrict to bsp markets only, if True or non-bsp markets if False. 
    /// If not specified then returns both BSP and non-BSP markets
    /// </summary>
    [JsonPropertyName("bspOnly")]
    public bool? BspOnly { get; set; }

    /// <summary>
    /// Restrict to markets that will turn in play if True or will not turn in play if false. 
    /// If not specified, returns both.
    /// </summary>
    [JsonPropertyName("turnInPlayEnabled")]
    public bool? TurnInPlayEnabled { get; set; }

    /// <summary>
    /// Restrict to markets that are currently in play if True or are not currently in play if false. 
    /// If not specified, returns both.
    /// </summary>
    [JsonPropertyName("inPlayOnly")]
    public bool? InPlayOnly { get; set; }

    /// <summary>
    /// Restrict to markets that match the betting type of the market (i.e. Odds, Asian Handicap Singles, Asian Handicap Doubles or Line)
    /// </summary>
    [JsonPropertyName("marketBettingTypes")]
    public List<MarketBettingTypeEnum>? MarketBettingTypes { get; set; }

    /// <summary>
    /// Restrict to markets that are in the specified country or countries.  
    /// The default value is 'GB' when the correct country code cannot be determined.
    /// </summary>
    [JsonPropertyName("marketCountries")]
    public List<string>? MarketCountries { get; set; }

    /// <summary>
    /// Restrict to markets that match the type of the market (i.e., MATCH_ODDS, HALF_TIME_SCORE). 
    /// You should use this instead of relying on the market name as the market type codes are the same in all locales.
    /// </summary>
    [JsonPropertyName("marketTypeCodes")]
    public List<string>? MarketTypeCodes { get; set; }

    /// <summary>
    /// Restrict to markets with a market start time before or after the specified date
    /// </summary>
    [JsonPropertyName("marketStartTime")]
    public TimeRange? MarketStartTime { get; set; }

    /// <summary>
    /// Restrict to markets where you have one or more orders in these states.
    /// </summary>
    [JsonPropertyName("withOrders")]
    public List<OrderStatusEnum>? WithOrders { get; set; }

    /// <summary>
    /// Restrict to markets of a specific raceType. 
    /// Valid values are - Harness, Flat, Hurdle, Chase, Bumper, NH Flat, Steeple 
    /// and NO_VALUE (when no valid race type has been mapped). 
    /// </summary>
    [JsonPropertyName("raceTypes")]
    public List<string>? RaceTypes { get; set; }
}
