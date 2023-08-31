using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Enums.Streaming;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Betting;
using BetfairDotNet.Models.Streaming;
using System.Collections.Concurrent;

namespace BetfairDotNet.Factories;

internal class MarketSnapshotFactory : IMarketSnapshotFactory {


    private readonly ConcurrentDictionary<string, MarketSnapshot> _marketCache;


    public MarketSnapshotFactory(ConcurrentDictionary<string, MarketSnapshot> marketCache) {
        _marketCache = marketCache;
    }


    public IEnumerable<MarketSnapshot> GetSnapshots(MarketChangeMessage changeMessage) {
        if(changeMessage.ChangeType == ChangeTypeEnum.HEARTBEAT) {
            yield break; // Swallow heartbeat
        }
        if(changeMessage.ChangeType == ChangeTypeEnum.SUB_IMAGE) {
            _marketCache.Clear();
        }
        foreach(var change in changeMessage.MarketChanges) { // ignore settled markets
            var snapshot = change.IsImage ? ProcessImage(change) : ProcessDelta(change);
            _marketCache[snapshot.MarketId] = snapshot; // Update cache
            yield return snapshot;
        }
    }


    private static MarketSnapshot ProcessImage(MarketChange changeMessage) {
        return new MarketSnapshot() {
            MarketId = changeMessage.Id,
            MarketDefinition = changeMessage.MarketDefinition,
            RunnerSnapshots = ProcessRunnersImage(changeMessage)
        };
    }


    private static Dictionary<long, RunnerSnapshot> ProcessRunnersImage(MarketChange changeMessage) {
        var snapshots = new Dictionary<long, RunnerSnapshot>();
        foreach(var runner in changeMessage.RunnerChanges) {
            var runnerSnapshot = new RunnerSnapshot {
                SelectionId = runner.Id,
                RunnerDefinition = changeMessage.MarketDefinition?.Runners.First(r => r.Id == runner.Id),
                LastTradedPrice = runner.LastTradedPrice.GetValueOrDefault(),
                StartingPriceNear = runner.StartingPriceNear.GetValueOrDefault(),
                StartingPriceFar = runner.StartingPriceFar.GetValueOrDefault(),
                ToBack = CreateLadder(runner.AvailableToBack ?? runner.BestAvailableToBack, SideEnum.BACK),
                ToLay = CreateLadder(runner.AvailableToLay ?? runner.BestAvailableToLay, SideEnum.LAY),
                Traded = CreateLadder(runner.TradedVolume, SideEnum.BACK), // Side doesn't matter here
            };
            snapshots[runner.Id] = runnerSnapshot;
        }
        return snapshots;
    }


    private MarketSnapshot ProcessDelta(MarketChange mc) {
        var cachedMarket = _marketCache[mc.Id];
        return cachedMarket with { // MarketDefinition sent in full if changed
            MarketDefinition = mc.MarketDefinition ?? cachedMarket.MarketDefinition,
            RunnerSnapshots = ProcessRunnersDelta(mc, cachedMarket.RunnerSnapshots)
        };
    }


    private static Dictionary<long, RunnerSnapshot> ProcessRunnersDelta(
        MarketChange mc,
        IDictionary<long, RunnerSnapshot> cachedRunners) {

        var rnrSnaps = new Dictionary<long, RunnerSnapshot>(cachedRunners);
        foreach(var runner in mc.RunnerChanges) {
            var cachedRunner = cachedRunners[runner.Id];
            var updatedRunnerSnapshot = cachedRunner with {
                LastTradedPrice = runner.LastTradedPrice ?? cachedRunner.LastTradedPrice,
                StartingPriceNear = runner.StartingPriceNear ?? cachedRunner.StartingPriceNear,
                StartingPriceFar = runner.StartingPriceFar ?? cachedRunner.StartingPriceFar,
                ToBack = UpdateLadder(runner.AvailableToBack ?? runner.BestAvailableToBack, cachedRunner.ToBack),
                ToLay = UpdateLadder(runner.AvailableToLay ?? runner.BestAvailableToLay, cachedRunner.ToLay),
                Traded = UpdateLadder(runner.TradedVolume, cachedRunner.Traded),
            };
            rnrSnaps[runner.Id] = updatedRunnerSnapshot;
        }
        return rnrSnaps;
    }


    private static PriceLadder CreateLadder(List<List<double>>? levels, SideEnum side) {
        var ladder = new PriceLadder(side);
        foreach(var level in levels ?? Enumerable.Empty<List<double>>()) {
            if(level.Count == 3) {
                var ladderLevel = (int)level[0];
                var price = level[1];
                var size = level[2];
                if(price > 0 && size > 0) {
                    ladder.AddLevel(ladderLevel, new PriceSize(price, size));
                }
            }
            else if(level.Count == 2) {
                var price = level[0];
                var size = level[1];
                if(price > 0 && size > 0) {
                    ladder.AddLevel(price, new PriceSize(price, size));
                }
            }
        }
        return ladder;
    }


    private static PriceLadder UpdateLadder(List<List<double>>? levels, PriceLadder ladder) {
        foreach(var level in levels ?? Enumerable.Empty<List<double>>()) {
            if(level.Count == 3) { // Depth based ladders
                var ladderLevel = (int)level[0];
                var price = level[1];
                var size = level[2];
                if(size == 0) {
                    ladder.RemoveLevelByDepth(ladderLevel);
                }
                else {
                    ladder.AddOrUpdateLevelByDepth(ladderLevel, new PriceSize(price, size));
                }
            }
            else if(level.Count == 2) { // Full-depth ladders
                var price = level[0];
                var size = level[1];
                if(size == 0) {
                    ladder.RemoveLevelByPrice(price);
                }
                else {
                    ladder.AddOrUpdateLevelByPrice(price, new PriceSize(price, size));
                }
            }
        }
        return ladder;
    }
}
