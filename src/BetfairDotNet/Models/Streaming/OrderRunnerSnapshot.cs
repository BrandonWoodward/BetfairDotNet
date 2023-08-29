using BetfairDotNet.Enums.Betting;

namespace BetfairDotNet.Models.Streaming;


/// <summary>
/// An immutable atomic snapshot of orders for a given runner.
/// </summary>
public sealed record OrderRunnerSnapshot {

    /// <summary>
    /// The unique id of the runner or selection.
    /// </summary>
    public long SelectionId { get; init; }

    /// <summary>
    /// The matched backs on this runner including partially matched orders.
    /// </summary>
    public PriceLadder MatchedBacks { get; init; } = new PriceLadder(SideEnum.BACK);

    /// <summary>
    /// The matched lays on this runner including partially matched orders.
    /// </summary>
    public PriceLadder MatchedLays { get; init; } = new PriceLadder(SideEnum.LAY);

    /// <summary>
    /// The orders that are yet to be matched on this runner.
    /// </summary>
    public Dictionary<string, ESAOrder> UnmatchedOrders { get; init; } = new();


    public OrderRunnerSnapshot(long selectionId) {
        SelectionId = selectionId;
    }
}
