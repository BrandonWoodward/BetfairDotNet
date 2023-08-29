namespace BetfairDotNet.Models.Streaming;


/// <summary>
/// An immutable atomic snapshot of a runner.
/// </summary>
public sealed record RunnerSnapshot {

    /// <summary>
    /// The unique identifier for the selection.
    /// </summary>
    public required long SelectionId { get; init; }

    /// <summary>
    /// The selection's metadata if requested.
    /// </summary>
    public required RunnerDefinition? RunnerDefinition { get; init; }

    /// <summary>
    /// Amounts available to lay by distinct price. Filtered as per <see cref="MarketDataFilter"/>
    /// </summary>
    public required PriceLadder ToLay { get; init; }

    /// <summary>
    /// Amounts available to back by distinct price. Filtered as per <see cref="MarketDataFilter"/>
    /// </summary>
    public required PriceLadder ToBack { get; init; }

    /// <summary>
    /// The traded volume by distinct price, if requested. See <see cref="MarketDataFilter"/>
    /// </summary>
    public required PriceLadder Traded { get; init; }

    /// <summary>
    /// Last price traded on this selection. Only returned if requested with <see cref="MarketDataFilter"/>
    /// </summary>
    public required double LastTradedPrice { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public required double StartingPriceNear { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public required double StartingPriceFar { get; init; }
}
