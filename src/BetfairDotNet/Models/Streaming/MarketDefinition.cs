using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Streaming;


/// <summary>
/// The market metadata.
/// </summary>
public sealed record MarketDefinition {

    /// <summary>
    /// The type of betting offered by this market.
    /// </summary>
    [JsonPropertyName("bettingType")]
    public MarketBettingTypeEnum BettingType { get; init; }

    /// <summary>
    /// The status of the market.
    /// </summary>
    [JsonPropertyName("status")]
    public MarketStatusEnum Status { get; init; }

    /// <summary>
    /// Venue at which the market is taking place.
    /// </summary>
    [JsonPropertyName("venue")]
    public string Venue { get; init; } = string.Empty;

    /// <summary>
    /// The time the market was settled.
    /// </summary>
    [JsonPropertyName("settledTime")]
    public DateTime? SettledTime { get; init; }

    /// <summary>
    /// The timezone in which the market is taking place.
    /// </summary>
    [JsonPropertyName("timezone")]
    public string Timezone { get; init; } = string.Empty;

    /// <summary>
    /// Each way divisor used for calculating place bets. 
    /// This value is effective for marketTypes EACH_WAY only.
    /// </summary>
    [JsonPropertyName("eachWayDivisor")]
    public double EachWayDivisor { get; init; }

    /// <summary>
    /// The market regulators.
    /// </summary>
    [JsonPropertyName("regulators")]
    public List<string> Regulators { get; init; } = new();

    // TODO what is this? Should this be an enum?
    /// <summary>
    /// Gets or Sets MarketType
    /// </summary>
    [JsonPropertyName("marketType")]
    public string MarketType { get; init; } = string.Empty;

    /// <summary>
    /// The base rate of commission applied to this market.
    /// </summary>
    [JsonPropertyName("marketBaseRate")]
    public double MarketBaseRate { get; init; }

    /// <summary>
    /// Number of winners for this market.
    /// </summary>
    [JsonPropertyName("numberOfWinners")]
    public int NumberOfWinners { get; init; }

    /// <summary>
    /// The code of the country where the market is taking place.
    /// </summary>
    [JsonPropertyName("countryCode")]
    public string CountryCode { get; init; } = string.Empty;

    /// <summary>
    /// Is the market in play or not?
    /// </summary>
    [JsonPropertyName("inPlay")]
    public bool InPlay { get; init; }

    /// <summary>
    /// Gets or Sets BetDelay
    /// </summary>
    [JsonPropertyName("betDelay")]
    public int BetDelay { get; init; }

    /// <summary>
    /// Is the BSP offered in this market?
    /// </summary>
    [JsonPropertyName("bspMarket")]
    public bool BspMarket { get; init; }

    /// <summary>
    /// The number of runners (selections) active in the market.
    /// </summary>
    [JsonPropertyName("numberOfActiveRunners")]
    public int NumberOfActiveRunners { get; init; }

    /// <summary>
    /// Gets or Sets EventId
    /// </summary>
    [JsonPropertyName("eventId")]
    public string EventId { get; init; } = string.Empty;

    /// <summary>
    /// Is the cross matcher active currently?
    /// </summary>
    [JsonPropertyName("crossMatching")]
    public bool CrossMatching { get; init; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("runnersVoidable")]
    public bool RunnersVoidable { get; init; }

    /// <summary>
    /// Is in play betting offered on this market?
    /// </summary>
    [JsonPropertyName("turnInPlayEnabled")]
    public bool TurnInPlayEnabled { get; init; }

    /// <summary>
    /// The time of suspension for this market.
    /// </summary>
    [JsonPropertyName("suspendTime")]
    public DateTime? SuspendTime { get; init; }

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("discountAllowed")]
    public bool DiscountAllowed { get; init; }

    /// <summary>
    /// Keep bets allowed?
    /// </summary>
    [JsonPropertyName("persistenceEnabled")]
    public bool PersistenceEnabled { get; init; }

    /// <summary>
    /// Infomation about individual runners (selections) in the market.
    /// </summary>
    [JsonPropertyName("runners")]
    public List<RunnerDefinition> Runners { get; init; } = new();

    /// <summary>
    /// A non-monotically increasing value indicating changes to the market.
    /// </summary>
    [JsonPropertyName("version")]
    public long Version { get; init; }

    /// <summary>
    /// The Event Type the market is contained within.
    /// </summary>
    [JsonPropertyName("eventTypeId")]
    public string EventTypeId { get; init; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("complete")]
    public bool Complete { get; init; }

    /// <summary>
    /// The date this market opened.
    /// </summary>
    [JsonPropertyName("openDate")]
    public DateTime? OpenDate { get; init; }

    /// <summary>
    /// The time of this market.
    /// </summary>
    [JsonPropertyName("marketTime")]
    public DateTime? MarketTime { get; init; }

    /// <summary>
    /// Has the BSP been resolved?
    /// </summary>
    [JsonPropertyName("bspReconciled")]
    public bool BspReconciled { get; init; }
}
