namespace BetfairDotNet.Models.Streaming;


/// <summary>
/// An immutable atomic snapshot of a runner.
/// </summary>
public sealed record RunnerSnapshot {

    /// <summary>
    /// The unique identifier for the selection.
    /// </summary>
    public long SelectionId { get; init; }

    /// <summary>
    /// The selection's metadata if requested.
    /// </summary>
    public RunnerDefinition? RunnerDefinition { get; init; }

    /// <summary>
    /// Amounts available to lay by distinct price. Filtered as per <see cref="StreamingMarketDataFilter"/>
    /// </summary>
    public PriceLadder ToLay { get; init; }

    /// <summary>
    /// Amounts available to back by distinct price. Filtered as per <see cref="StreamingMarketDataFilter"/>
    /// </summary>
    public PriceLadder ToBack { get; init; }

    /// <summary>
    /// The traded volume by distinct price, if requested. See <see cref="StreamingMarketDataFilter"/>
    /// </summary>
    public PriceLadder Traded { get; init; }

    /// <summary>
    /// Last price traded on this selection. Only returned if requested with <see cref="StreamingMarketDataFilter"/>
    /// </summary>
    public double LastTradedPrice { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public double StartingPriceNear { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public double StartingPriceFar { get; init; }
}
