namespace BetfairDotNet.Models.Streaming;


/// <summary>
/// An immutable, atomic snapshot of orders for a given market.
/// </summary>
public record OrderMarketSnapshot {
    
    /// <summary>
    /// The time the update was sent, according to Betfair.
    /// </summary>
    public long Timestamp { get; init; }

    /// <summary>
    /// The id of this market.
    /// </summary>
    public string MarketId { get; init; } = string.Empty;

    /// <summary>
    /// The order snapshots for each runner.
    /// </summary>
    public Dictionary<long, OrderRunnerSnapshot> OrderRunnerSnapshots { get; init; } = new();
}
