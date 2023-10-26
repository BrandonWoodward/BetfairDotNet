namespace BetfairDotNet.Models.Streaming;


/// <summary>
/// An immutable atomic snapshot of a given market.
/// </summary>
public sealed record MarketSnapshot {
    
    /// <summary>
    /// The time the update was sent, according to Betfair.
    /// </summary>
    public long Timestamp { get; init; }

    /// <summary>
    /// The unique identifier for the market.
    /// </summary>
    public string MarketId { get; init; } = string.Empty;

    /// <summary>
    /// The market metadata
    /// </summary>
    public MarketDefinition? MarketDefinition { get; init; }

    /// <summary>
    /// The snapshots of each runner in the market.
    /// </summary>
    public Dictionary<long, RunnerSnapshot> RunnerSnapshots { get; init; } = new();
}
