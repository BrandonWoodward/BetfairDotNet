namespace BetfairDotNet.Models.Streaming;


/// <summary>
/// An immutable atomic snapshot of a given market.
/// </summary>
public sealed record MarketSnapshot {

    /// <summary>
    /// The unique identifier for the market.
    /// </summary>
    public string MarketId { get; init; } = string.Empty;

    /// <summary>
    /// The clock value of the initial sub message. Use on reconnect.
    /// </summary>
    public string InitialClk { get; init; } = string.Empty;

    /// <summary>
    /// The clock value of this update. Use on reconnect.
    /// </summary>
    public string Clk { get; init; } = string.Empty;

    /// <summary>
    /// The market metadata
    /// </summary>
    public MarketDefinition? MarketDefinition { get; init; }

    /// <summary>
    /// The snapshots of each runner in the market.
    /// </summary>
    public Dictionary<long, RunnerSnapshot> RunnerSnapshots { get; init; } = new();
}
