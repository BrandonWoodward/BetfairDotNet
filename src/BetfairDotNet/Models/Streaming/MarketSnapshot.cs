namespace BetfairDotNet.Models.Streaming;


/// <summary>
/// An immutable atomic snapshot of a given market.
/// </summary>
public sealed record MarketSnapshot {

    /// <summary>
    /// The unique identifier for the market.
    /// </summary>
    public required string MarketId { get; init; }

    /// <summary>
    /// The market metadata
    /// </summary>
    public required MarketDefinition? MarketDefinition { get; init; }

    /// <summary>
    /// The snapshots of each runner in the market.
    /// </summary>
    public required Dictionary<long, RunnerSnapshot> RunnerSnapshots { get; init; }
}
