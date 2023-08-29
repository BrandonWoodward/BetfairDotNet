using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// The dynamic data about runners in a market.
/// </summary>
public sealed record RunnerBook {

    /// <summary>
    /// The unique id of the runner (selection). 
    /// The same selectionId and runnerName pairs are used accross all Betfair markets which contain them.
    /// </summary>
    [JsonPropertyName("selectionId"), JsonRequired]
    public required long SelectionId { get; init; }

    /// <summary>
    /// The handicap. 
    /// Enter the specific handicap value (returned by RUNNER in listMaketBook) 
    /// if the market is an Asian handicap market.
    /// </summary>
    [JsonPropertyName("handicap"), JsonRequired]
    public required double Handicap { get; init; }

    /// <summary>
    /// The status of the selection (i.e., ACTIVE, REMOVED, WINNER, PLACED, LOSER, HIDDEN) 
    /// Runner status information is available for 90 days following market settlement.
    /// </summary>
    [JsonPropertyName("status"), JsonRequired]
    public required RunnerStatusEnum Status { get; init; }

    /// <summary>
    /// The adjustment factor applied if the selection is removed.
    /// </summary>
    [JsonPropertyName("adjustmentFactor")]
    public double? AdjustmentFactor { get; init; }

    /// <summary>
    /// The price of the most recent bet matched on this selection.
    /// </summary>
    [JsonPropertyName("lastPriceTraded")]
    public double? LastPriceTraded { get; init; }

    /// <summary>
    /// The total amount matched on this runner.
    /// </summary>
    [JsonPropertyName("totalMatched")]
    public double? TotalMatched { get; init; }

    /// <summary>
    /// If date and time the runner was removed.
    /// </summary>
    [JsonPropertyName("removalDate")]
    public DateTime? RemovalDate { get; init; }

    /// <summary>
    /// The BSP related prices for this runner.
    /// </summary>
    [JsonPropertyName("sp")]
    public StartingPrices? StartingPrices { get; init; }

    /// <summary>
    /// The Exchange prices available for this runner.
    /// </summary>
    [JsonPropertyName("ex")]
    public ExchangePrices? ExchangePrices { get; init; }

    /// <summary>
    /// List of orders in the market.
    /// </summary>
    [JsonPropertyName("orders")]
    public List<Order> Orders { get; init; } = new();

    /// <summary>
    /// List of matches (i.e, orders that have been fully or partially executed)
    /// </summary>
    [JsonPropertyName("matches")]
    public List<Match> Matches { get; init; } = new();

    /// <summary>
    /// List of matches for each strategy, ordered by matched data.
    /// </summary>
    [JsonPropertyName("matchesByStrategy")]
    public Dictionary<string, List<Match>> MatchesByStrategy { get; init; } = new();
}
