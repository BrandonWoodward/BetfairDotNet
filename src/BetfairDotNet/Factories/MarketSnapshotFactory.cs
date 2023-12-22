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
        foreach(var r in changeMessage.RunnerChanges)
        {
            var runnerSnapshot = new RunnerSnapshot
            {
                SelectionId = r.Id,
                RunnerDefinition = changeMessage.MarketDefinition?.Runners.First(sr => sr.Id == r.Id),
                LastTradedPrice = r.LastTradedPrice.GetValueOrDefault(),
                StartingPriceNear = r.StartingPriceNear.GetValueOrDefault(),
                StartingPriceFar = r.StartingPriceFar.GetValueOrDefault(),
                ToBack = UpdateLadder(r.AvailableToBack ?? r.BestAvailableToBack, new(SideEnum.BACK)),
                ToLay = UpdateLadder(r.AvailableToLay ?? r.BestAvailableToLay, new(SideEnum.LAY)),
                ToBackVirtual = UpdateLadder(r.BestAvailableToBackVirtual, new(SideEnum.BACK)),
                ToLayVirtual = UpdateLadder(r.BestAvailableToLayVirtual, new(SideEnum.LAY)),
                Traded = UpdateLadder(r.TradedVolume, new(SideEnum.BACK)),
            };
            snapshots[r.Id] = runnerSnapshot;
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
                RunnerDefinition = mc.MarketDefinition?.Runners.First(r => r.Id == rnr.Id) ?? cached.RunnerDefinition,
                LastTradedPrice = rnr.LastTradedPrice ?? cached.LastTradedPrice,
                StartingPriceNear = rnr.StartingPriceNear ?? cached.StartingPriceNear,
                StartingPriceFar = rnr.StartingPriceFar ?? cached.StartingPriceFar,
                ToBack = UpdateLadder(rnr.AvailableToBack ?? rnr.BestAvailableToBack, cached.ToBack),
                ToLay = UpdateLadder(rnr.AvailableToLay ?? rnr.BestAvailableToLay, cached.ToLay),
                ToBackVirtual = UpdateLadder(rnr.BestAvailableToBackVirtual, cached.ToBackVirtual),
                ToLayVirtual = UpdateLadder(rnr.BestAvailableToLayVirtual, cached.ToLayVirtual),
                Traded = UpdateLadder(rnr.TradedVolume, cached.Traded),
                TradedVolume = rnr.TotalVolume ?? cached.TradedVolume
            };
            rnrSnaps[rnr.Id] = updatedRunnerSnapshot;
        }
        return rnrSnaps;
    }

    private static PriceLadder UpdateLadder(List<List<double>>? levels, PriceLadder ladder)
    {
        if (levels is null || levels.Count == 0)
        {
            return ladder;
        }

        if (levels[0].Count == 2)
        {
            foreach (var level in levels)
            {
                ladder[level[0]] = new(level[0], level[1]);
            }
        }

        if (levels[0].Count == 3)
        {
            foreach (var level in levels.OrderBy(l => l[0]).ToList())
            {
                ladder[(int)level[0]] = new(level[1], level[2]);
            }
        }

        return ladder;
    }
}
