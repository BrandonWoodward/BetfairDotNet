
using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Enums.Streaming;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Betting;
using BetfairDotNet.Models.Streaming;
using System.Collections.Concurrent;

namespace BetfairDotNet.Factories;


internal class OrderSnapshotFactory : IOrderSnapshotFactory {


    private readonly ConcurrentDictionary<string, OrderMarketSnapshot> _orderCache;


    public OrderSnapshotFactory(ConcurrentDictionary<string, OrderMarketSnapshot> orderCache) {
        _orderCache = orderCache;
    }


    public IEnumerable<OrderMarketSnapshot> GetSnapshots(OrderChangeMessage changeMessage) {
        if(changeMessage.ChangeType == ChangeTypeEnum.HEARTBEAT) {
            yield break; // Swallow heartbeat
        }
        if(changeMessage.ChangeType == ChangeTypeEnum.SUB_IMAGE) { // New sub
            _orderCache.Clear();
        }
        foreach(var change in changeMessage.OrderChanges) {
            yield return change.IsImage
                ? ProcessImage(change, changeMessage.PublishTime) // Replace in cache
                : ProcessDelta(change, changeMessage.PublishTime); // Merge with cache
        }
    }


    private static OrderMarketSnapshot ProcessImage(OrderMarketChange changeMessage, long timestamp) {
        var rnrSnaps = changeMessage.OrderRunnerChanges.ToDictionary(rnr => rnr.Id, ProcessRunnerImage);
        return new OrderMarketSnapshot {
            Timestamp = timestamp,
            MarketId = changeMessage.Id,
            OrderRunnerSnapshots = rnrSnaps
        };
    }


    private static OrderRunnerSnapshot ProcessRunnerImage(OrderRunnerChange omc) {
        var orderRunnerSnapshot = new OrderRunnerSnapshot(omc.Id);
        foreach(var order in omc.UnmatchedOrders) {
            orderRunnerSnapshot.UnmatchedOrders[order.Id] = order;
        }
        return orderRunnerSnapshot;
    }


    private OrderMarketSnapshot ProcessDelta(OrderMarketChange changeMessage, long timestamp) {
        var cachedMarket = _orderCache[changeMessage.Id];
        var updatedRunnerSnaps = new Dictionary<long, OrderRunnerSnapshot>(cachedMarket.OrderRunnerSnapshots);
        foreach(var runnerChange in changeMessage.OrderRunnerChanges) {
            var cachedRunner = cachedMarket.OrderRunnerSnapshots[runnerChange.Id];
            updatedRunnerSnaps[runnerChange.Id] = ProcessRunnerDelta(runnerChange, cachedRunner);
        }
        var updatedMarketSnapshot = cachedMarket with
        {
            Timestamp = timestamp,
            OrderRunnerSnapshots = updatedRunnerSnaps
        };
        _orderCache[changeMessage.Id] = updatedMarketSnapshot; // Update cache
        return updatedMarketSnapshot;
    }


    private static OrderRunnerSnapshot ProcessRunnerDelta(OrderRunnerChange orc, OrderRunnerSnapshot cachedRunner) {
        var newUnmatchedOrders = new Dictionary<string, ESAOrder>(cachedRunner.UnmatchedOrders);
        foreach(var order in orc.UnmatchedOrders) {
            if(order.Status == OrderStatusEnum.EXECUTION_COMPLETE) {
                newUnmatchedOrders.Remove(order.Id);
            }
            else {
                newUnmatchedOrders[order.Id] = order;
            }
        }
        return cachedRunner with {
            MatchedBacks = UpdateLadder(orc.MatchedBacks, cachedRunner.MatchedBacks),
            MatchedLays = UpdateLadder(orc.MatchedLays, cachedRunner.MatchedBacks),
            UnmatchedOrders = newUnmatchedOrders
        };
    }


    private static PriceLadder UpdateLadder(List<List<double>>? levels, PriceLadder ladder) {
        foreach(var level in levels ?? Enumerable.Empty<List<double>>()) {
            var price = level[0];
            var size = level[1];
            if(size == 0) {
                ladder.RemoveLevelByPrice(price);
            }
            else {
                ladder.AddOrUpdateLevelByPrice(price, new PriceSize(price, size));
            }
        }
        return ladder;
    }
}