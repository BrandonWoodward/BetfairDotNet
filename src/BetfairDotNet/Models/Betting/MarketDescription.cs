using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Market definition.
/// </summary>
public sealed record MarketDescription {

    /// <summary>
    /// If 'true' the market supports 'Keep' bets if the market is to be turned in-play
    /// </summary>
    [JsonPropertyName("persistenceEnabled"), JsonRequired]
    public required bool IsPersistenceEnabled { get; init; }

    /// <summary>
    /// If 'true' the market supports Betfair SP betting.
    /// </summary>
    [JsonPropertyName("bspMarket"), JsonRequired]
    public required bool IsBspMarket { get; init; }

    /// <summary>
    /// The market start time. 
    /// This is the scheduled start time of the market e.g. horse race or football match etc.
    /// </summary>
    [JsonPropertyName("marketTime"), JsonRequired]
    public required DateTime MarketTime { get; init; }

    /// <summary>
    /// The market suspend time. 
    /// This is the next time the market will be suspended for betting and is normally the same as the marketTime.
    /// </summary>
    [JsonPropertyName("suspendTime"), JsonRequired]
    public required DateTime? SuspendTime { get; init; }

    /// <summary>
    /// The market settle time.
    /// </summary>
    [JsonPropertyName("settleTime")]
    public DateTime? SettleTime { get; init; }

    /// <summary>
    /// See <see cref="MarketBettingTypeEnum"/>
    /// </summary>
    [JsonPropertyName("bettingType"), JsonRequired]
    public required MarketBettingTypeEnum BettingType { get; init; }

    /// <summary>
    /// If 'true' the market is set to turn in-play
    /// </summary>
    [JsonPropertyName("turnInPlayEnabled"), JsonRequired]
    public required bool IsTurnInPlayEnabled { get; init; }

    /// <summary>
    /// Market base type.
    /// </summary>
    [JsonPropertyName("marketType"), JsonRequired]
    public required string MarketType { get; init; }

    /// <summary>
    /// The market regulator. Value include “GIBRALTAR REGULATOR" (.com), 
    /// MR_ESP (Betfair.es markets), MR_IT (Betfair.it). GIBRALTAR REGULATOR = MR_INT in the Stream API
    /// </summary>
    [JsonPropertyName("regulator"), JsonRequired]
    public required string Regulator { get; init; }

    /// <summary>
    /// The commission rate applicable to the market.
    /// </summary>
    [JsonPropertyName("marketBaseRate"), JsonRequired]
    public required double MarketBaseRate { get; init; }

    /// <summary>
    /// Indicates whether or not the user's discount rate is taken into account on this market. 
    /// If ‘false’ all users will be charged the same commission rate, regardless of discount rate.
    /// </summary>
    [JsonPropertyName("discountAllowed"), JsonRequired]
    public required bool IsDiscountAllowed { get; init; }

    /// <summary>
    /// The wallet to which the market belongs.
    /// </summary>
    [JsonPropertyName("wallet")]
    public string Wallet { get; init; } = string.Empty;

    /// <summary>
    /// The market rules.
    /// </summary>
    [JsonPropertyName("rules")]
    public string Rules { get; init; } = string.Empty;

    /// <summary>
    /// Indicates whether rules have a date included.
    /// </summary>
    [JsonPropertyName("rulesHasDate")]
    public bool RulesHasDate { get; init; }

    /// <summary>
    /// The divisor is returned for the marketType EACH_WAY only and refers to the 
    /// fraction of the win odds at which the place portion of an each way bet is settled
    /// </summary>
    [JsonPropertyName("eachWayDivisor")]
    public double EachWayDivisor { get; init; }

    /// <summary>
    /// Any additional information regarding the market
    /// </summary>
    [JsonPropertyName("clarifications")]
    public string Clarifications { get; init; } = string.Empty;

    /// <summary>
    /// Line range info for line markets.
    /// </summary>
    [JsonPropertyName("lineRangeInfo")]
    public MarketLineRangeInfo? LineRangeInfo { get; init; }

    /// <summary>
    /// An external identifier of a race type.
    /// </summary>
    [JsonPropertyName("raceType")]
    public string RaceType { get; init; } = string.Empty;

    /// <summary>
    /// Details about the price ladder in use for this market.
    /// </summary>
    [JsonPropertyName("priceLadderDescription")]
    public PriceLadderDescription? PriceLadderDescription { get; init; }
}
