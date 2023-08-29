using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Contains some text which may be useful to render a betting history view. 
/// It offers no long-term warranty as to the correctness of the text.
/// </summary>
public sealed record ItemDescription {

    /// <summary>
    /// The event type name, translated into the requested locale. Available at EVENT_TYPE groupBy or lower.
    /// </summary>
    [JsonPropertyName("eventTypeDesc")]
    public string EventTypeDesc { get; set; } = string.Empty;

    /// <summary>
    /// The eventName, or openDate + venue, translated into the requested locale. Available at EVENT groupBy or lower.
    /// </summary>
    [JsonPropertyName("eventDesc")]
    public string EventDesc { get; set; } = string.Empty;

    /// <summary>
    /// The market name or racing market type ("Win", "To Be Placed (2 places)", "To Be Placed (5 places)" etc) 
    /// translated into the requested locale. Available at MARKET groupBy or lower.
    /// </summary>
    [JsonPropertyName("marketDesc")]
    public string MarketDesc { get; set; } = string.Empty;

    /// <summary>
    /// The market type e.g. MATCH_ODDS, PLACE, WIN etc.
    /// </summary>
    [JsonPropertyName("marketType")]
    public string MarketType { get; set; } = string.Empty;

    /// <summary>
    /// The start time of the market (in ISO-8601 format, not translated).
    /// Available at MARKET groupBy or lower.
    /// </summary>
    [JsonPropertyName("marketStartTime")]
    public DateTime MarketStartTime { get; set; }

    /// <summary>
    /// The runner name, maybe including the handicap, translated into the requested locale.
    /// Available at BET groupBy.
    /// </summary>
    [JsonPropertyName("runnerDesc")]
    public string RunnerDesc { get; set; } = string.Empty;

    /// <summary>
    /// The number of winners on a market. Available at BET groupBy.
    /// </summary>
    [JsonPropertyName("numberOfWinners")]
    public int NumberOfWinners { get; set; }

    /// <summary>
    /// The divisor is returned for the marketType EACH_WAY only and refers to the fraction of the 
    /// win odds at which the place portion of an each way bet is settled
    /// </summary>
    [JsonPropertyName("eachWayDivisor")]
    public double EachWayDivisor { get; set; }
}
