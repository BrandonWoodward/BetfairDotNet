using BetfairDotNet.Enums.Streaming;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Betting;
using BetfairDotNet.Models.Streaming;
using System.Collections.Concurrent;

namespace BetfairDotNet.Factories;


internal class OrderSnapshotFactory : IOrderSnapshotFactory
{
    private readonly ConcurrentDictionary<string, OrderMarketSnapshot> _orderCache;

    public OrderSnapshotFactory(ConcurrentDictionary<string, OrderMarketSnapshot> orderCache)
    {
        _orderCache = orderCache;
    }

    public IEnumerable<OrderMarketSnapshot> GetSnapshots(OrderChangeMessage changeMessage)
    {
        if(changeMessage.ChangeType == ChangeTypeEnum.HEARTBEAT)
        {
            yield break; // Swallow heartbeat
        }

        if(changeMessage.ChangeType == ChangeTypeEnum.SUB_IMAGE)
        {
            _orderCache.Clear();
        }

        foreach(var change in changeMessage.OrderChanges)
        {
            yield return change.IsImage
                ? ProcessImage(change, changeMessage.PublishTime) // Replace in cache
                : ProcessDelta(change, changeMessage.PublishTime); // Merge with cache
        }
    }

    private static OrderMarketSnapshot ProcessImage(OrderMarketChange changeMessage, long timestamp)
    {
        var rnrSnaps = changeMessage.OrderRunnerChanges.ToDictionary(rnr => rnr.Id, ProcessRunnerImage);
        return new OrderMarketSnapshot
        {
            Timestamp = timestamp,
            MarketId = changeMessage.Id,
            OrderRunnerSnapshots = rnrSnaps
        };
    }

    private static OrderRunnerSnapshot ProcessRunnerImage(OrderRunnerChange omc)
    {
        var orderRunnerSnapshot = new OrderRunnerSnapshot(omc.Id);
        foreach(var order in omc.UnmatchedOrders)
        {
            orderRunnerSnapshot.UnmatchedOrders[order.Id] = order;
        }
        return orderRunnerSnapshot;
    }

    private OrderMarketSnapshot ProcessDelta(OrderMarketChange changeMessage, long timestamp)
    {
        _orderCache.TryGetValue(changeMessage.Id, out var cachedMarket);
        cachedMarket ??= new() { MarketId = changeMessage.Id };

        var updatedRunnerSnaps = new Dictionary<long, OrderRunnerSnapshot>(cachedMarket.OrderRunnerSnapshots);
        foreach(var runnerChange in changeMessage.OrderRunnerChanges)
        {
            cachedMarket.OrderRunnerSnapshots.TryGetValue(runnerChange.Id, out var runnerSnap);
            runnerSnap ??= new(runnerChange.Id);

            updatedRunnerSnaps[runnerChange.Id] = ProcessRunnerDelta(runnerChange, runnerSnap);
        }

        return _orderCache[changeMessage.Id] = cachedMarket with
        {
            Timestamp = timestamp,
            OrderRunnerSnapshots = updatedRunnerSnaps
        };
    }

    private static OrderRunnerSnapshot ProcessRunnerDelta(OrderRunnerChange orc, OrderRunnerSnapshot cachedRunner)
    {
        var newUnmatchedOrders = new Dictionary<string, ESAOrder>(cachedRunner.UnmatchedOrders);

        foreach(var order in orc.UnmatchedOrders)
        {
            newUnmatchedOrders[order.Id] = order;
        }

        return cachedRunner with
        {
            MatchedBacks = UpdateLadder(orc.MatchedBacks, cachedRunner.MatchedBacks),
            MatchedLays = UpdateLadder(orc.MatchedLays, cachedRunner.MatchedBacks),
            UnmatchedOrders = newUnmatchedOrders
        };
    }

    private static PriceLadder UpdateLadder(List<List<double>>? levels, PriceLadder ladder)
    {
        foreach(var level in levels ?? Enumerable.Empty<List<double>>())
        {
            var price = level[0];
            var size = level[1];
            if(size == 0)
            {
                ladder.RemoveLevel(price);
            }
            else
            {
                ladder.AddOrUpdateLevel(new(price, size));
            }
        }
        return ladder;
    }
}