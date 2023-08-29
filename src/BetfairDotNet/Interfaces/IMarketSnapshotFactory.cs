using BetfairDotNet.Models.Streaming;

namespace BetfairDotNet.Interfaces;


internal interface IMarketSnapshotFactory {
    IEnumerable<MarketSnapshot> GetSnapshots(MarketChangeMessage changeMessage);
}