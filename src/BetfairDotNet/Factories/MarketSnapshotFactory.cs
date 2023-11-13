using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Enums.Streaming;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Betting;
using BetfairDotNet.Models.Streaming;
using System.Collections.Concurrent;

namespace BetfairDotNet.Factories;

internal class MarketSnapshotFactory : IMarketSnapshotFactory
{


    private readonly ConcurrentDictionary<string, MarketSnapshot> _marketCache;


    public MarketSnapshotFactory(ConcurrentDictionary<string, MarketSnapshot> marketCache)
    {
        _marketCache = marketCache;
    }


    public IEnumerable<MarketSnapshot> GetSnapshots(MarketChangeMessage changeMessage)
    {
        if(changeMessage.ChangeType == ChangeTypeEnum.HEARTBEAT)
        {
            yield break; // Swallow heartbeat
        }
        if(changeMessage.ChangeType == ChangeTypeEnum.SUB_IMAGE)
        {
            _marketCache.Clear();
        }
        foreach(var change in changeMessage.MarketChanges)
        { // ignore settled markets
            var snapshot = change.IsImage
                ? ProcessImage(change, changeMessage.PublishTime)
                : ProcessDelta(change, changeMessage.PublishTime);
            _marketCache[snapshot.MarketId] = snapshot; // Update cache
            yield return snapshot;
        }
    }


    private static MarketSnapshot ProcessImage(MarketChange changeMessage, long timestamp)
    {
        return new()
        {
            Timestamp = timestamp,
            MarketId = changeMessage.Id,
            MarketDefinition = changeMessage.MarketDefinition,
            RunnerSnapshots = ProcessRunnersImage(changeMessage)
        };
    }


    private static Dictionary<long, RunnerSnapshot> ProcessRunnersImage(MarketChange changeMessage)
    {
        var snapshots = new Dictionary<long, RunnerSnapshot>();
        foreach(var runner in changeMessage.RunnerChanges)
        {
            var runnerSnapshot = new RunnerSnapshot
            {
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


    private MarketSnapshot ProcessDelta(MarketChange mc, long timestamp)
    {
        var cachedMarket = _marketCache[mc.Id];
        return cachedMarket with
        {
            Timestamp = timestamp,
            MarketDefinition = mc.MarketDefinition ?? cachedMarket.MarketDefinition, // Sent in full if changed
            RunnerSnapshots = ProcessRunnersDelta(mc, cachedMarket.RunnerSnapshots)
        };
    }


    private static Dictionary<long, RunnerSnapshot> ProcessRunnersDelta(
        MarketChange mc,
        IDictionary<long, RunnerSnapshot> cachedRunners)
    {

        var rnrSnaps = new Dictionary<long, RunnerSnapshot>(cachedRunners);
        foreach(var rnr in mc.RunnerChanges)
        {
            var cached = cachedRunners[rnr.Id];
            var updatedRunnerSnapshot = cached with
            {
                //RunnerDefinition = mc.MarketDefinition?.Runners.First(r => r.Id == rnr.Id) ?? cached.RunnerDefinition,
                //LastTradedPrice = rnr.LastTradedPrice ?? cached.LastTradedPrice,
                //StartingPriceNear = rnr.StartingPriceNear ?? cached.StartingPriceNear,
                //StartingPriceFar = rnr.StartingPriceFar ?? cached.StartingPriceFar,
                //ToBack = UpdateLadder(rnr.AvailableToBack ?? rnr.BestAvailableToBack, cached.ToBack),
                //ToLay = UpdateLadder(rnr.AvailableToLay ?? rnr.BestAvailableToLay, cached.ToLay),
                Traded = UpdateLadder(rnr.TradedVolume, cached.Traded),
                TradedVolume = rnr.TotalVolume ?? cached.TradedVolume
            };
            rnrSnaps[rnr.Id] = updatedRunnerSnapshot;
        }
        return rnrSnaps;
    }


    private static PriceLadder CreateLadder(List<List<double>>? levels, SideEnum side)
    {
        var ladder = new PriceLadder(side);
        foreach(var level in levels ?? Enumerable.Empty<List<double>>())
        {
            if(level.Count == 3)
            {
                var ladderLevel = (int)level[0];
                var price = level[1];
                var size = level[2];
                if(price > 0 && size > 0)
                {
                    ladder.AddLevel(ladderLevel, new PriceSize(price, size));
                }
            }
            else if(level.Count == 2)
            {
                var price = level[0];
                var size = level[1];
                if(price > 0 && size > 0)
                {
                    ladder.AddLevel(price, new PriceSize(price, size));
                }
            }
        }
        return ladder;
    }


    private static PriceLadder UpdateLadder(List<List<double>>? levels, PriceLadder ladder)
    {
        foreach(var level in levels ?? Enumerable.Empty<List<double>>())
        {
            if(level.Count == 3)
            { // Depth based ladders
                var ladderLevel = (int)level[0];
                var price = level[1];
                var size = level[2];
                if(size == 0)
                {
                    ladder.RemoveLevelByDepth(ladderLevel);
                }
                else
                {
                    ladder.AddOrUpdateLevelByDepth(ladderLevel, new PriceSize(price, size));
                }
            }
            else if(level.Count == 2)
            { // Full-depth ladders
                var price = level[0];
                var size = level[1];
                if(size == 0)
                {
                    ladder.RemoveLevelByPrice(price);
                }
                else
                {
                    ladder.AddOrUpdateLevelByPrice(price, new PriceSize(price, size));
                }
            }
        }
        return ladder;
    }
}
