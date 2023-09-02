using BetfairDotNet.Handlers;
using BetfairDotNet.Models.Streaming;

namespace BetfairDotNet.Interfaces;
public interface IStreamSubscriptionHandler
{
    void Dispose();
    StreamSubscriptionHandler FilterMarkets(Func<MarketSnapshot, bool> predicate);
    StreamSubscriptionHandler FilterOrders(Func<OrderMarketSnapshot, bool> predicate);
    IDisposable Subscribe(Action<MarketSnapshot> onMarketChange, Action<Exception>? onException = null);
    IDisposable Subscribe(Action<MarketSnapshot> onMarketChange, Action<OrderMarketSnapshot> onOrderChange, Action<Exception>? onException = null);
    IDisposable Subscribe(Action<OrderMarketSnapshot> onOrderChange, Action<Exception>? onException = null);
}