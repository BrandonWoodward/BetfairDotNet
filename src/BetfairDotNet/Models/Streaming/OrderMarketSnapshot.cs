namespace BetfairDotNet.Models.Streaming;


/// <summary>
/// An immutable, atomic snapshot of orders for a given market.
/// </summary>
public record OrderMarketSnapshot {

    /// <summary>
    /// The id of this market.
    /// </summary>
    public required string MarketId { get; init; }

    /// <summary>
    /// The order snapshots for each runner.
    /// </summary>
    public Dictionary<long, OrderRunnerSnapshot> OrderRunnerSnapshots { get; init; } = new();
}
