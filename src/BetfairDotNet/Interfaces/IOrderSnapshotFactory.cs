using BetfairDotNet.Models.Streaming;

namespace BetfairDotNet.Interfaces;


internal interface IOrderSnapshotFactory {
    IEnumerable<OrderMarketSnapshot> GetSnapshots(OrderChangeMessage changeMessage);
}