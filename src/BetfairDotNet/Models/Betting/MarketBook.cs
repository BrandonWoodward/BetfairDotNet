using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// The dynamic data in a market.
/// </summary>
public sealed record MarketBook {

    /// <summary>
    /// The unique identifier for the market. 
    /// MarketId's are prefixed with '1.'
    /// </summary>
    [JsonPropertyName("marketId"), JsonRequired]
    public required string MarketId { get; init; }

    /// <summary>
    /// True if the data returned by listMarketBook will be delayed. 
    /// The data may be delayed because you are not logged in with a funded account 
    /// or you are using an Application Key that does not allow up to date data.
    /// </summary>
    [JsonPropertyName("isMarketDataDelayed"), JsonRequired]
    public required bool IsMarketDataDelayed { get; init; }

    /// <summary>
    /// The status of the market, e.g OPEN, SUSPENDED, CLOSED (settled), etc.
    /// </summary>
    [JsonPropertyName("status")]
    public MarketStatusEnum Status { get; init; }

    /// <summary>
    /// The number of seconds an order is held until it is submitted into the market. 
    /// Orders are usually delayed when the market is in-play
    /// </summary>
    [JsonPropertyName("betDelay")]
    public int BetDelay { get; init; }

    /// <summary>
    /// True if the market starting price has been reconciled.
    /// </summary>
    [JsonPropertyName("bspReconciled")]
    public bool IsBspReconciled { get; init; }

    /// <summary>
    /// If false, runners may be added to the market.
    /// </summary>
    [JsonPropertyName("complete")]
    public bool IsComplete { get; init; }

    /// <summary>
    /// True if the market is currently in play.
    /// </summary>
    [JsonPropertyName("inplay")]
    public bool IsInplay { get; init; }

    /// <summary>
    /// The number of selections that could be inittled as winners.
    /// </summary>
    [JsonPropertyName("numberOfWinners")]
    public int NumberOfWinners { get; init; }

    /// <summary>
    /// The number of runners in the market.
    /// </summary>
    [JsonPropertyName("numberOfRunners")]
    public int NumberOfRunners { get; init; }

    /// <summary>
    /// The number of runners that are currently active. 
    /// An active runner is a selection available for betting
    /// </summary>
    [JsonPropertyName("numberOfActiveRunners")]
    public int NumberOfActiveRunners { get; init; }

    /// <summary>
    /// The most recent time an order was executed.
    /// </summary>
    [JsonPropertyName("lastMatchTime")]
    public DateTime? LastMatchTime { get; init; }

    /// <summary>
    /// The total amount matched.
    /// </summary>
    [JsonPropertyName("totalMatched")]
    public double TotalMatched { get; init; }

    /// <summary>
    /// The total amount of orders that remain unmatched.
    /// </summary>
    [JsonPropertyName("totalAvailable")]
    public double TotalAvailable { get; init; }

    /// <summary>
    /// True if cross matching is enabled for this market.
    /// </summary>
    [JsonPropertyName("crossMatching")]
    public bool IsCrossMatching { get; init; }

    /// <summary>
    /// True if runners in the market can be voided. 
    /// This doesn't include horse racing markets under which bets are 
    /// voided on non-runners with any applicable reduction factor applied
    /// </summary>
    [JsonPropertyName("runnersVoidable")]
    public bool IsRunnersVoidable { get; init; }

    /// <summary>
    /// The version of the market. 
    /// The version increments whenever the market status changes, e.g, turning in-play, 
    /// or suspended when a goal is scored.
    /// </summary>
    [JsonPropertyName("version")]
    public long Version { get; init; }

    /// <summary>
    /// Information about the runners (selections) in the market.
    /// </summary>
    [JsonPropertyName("runners")]
    public List<RunnerBook> Runners { get; init; } = new();
}
