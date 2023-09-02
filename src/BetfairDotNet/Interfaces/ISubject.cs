using BetfairDotNet.Models.Exceptions;
using BetfairDotNet.Models.Streaming;

namespace BetfairDotNet.Interfaces;
internal interface ISubject
{
    void Dispose();
    void OnExceptionNext(BetfairESAException exception);
    void OnMarketNext(MarketSnapshot marketSnapshot);
    void OnOrderNext(OrderMarketSnapshot orderMarketSnapshot);
    IDisposable SubscribeException(Action<Exception> onException);
    IDisposable SubscribeMarket(Action<MarketSnapshot> onMarketChange, Func<MarketSnapshot, bool>? predicate = null);
    IDisposable SubscribeOrder(Action<OrderMarketSnapshot> onOrderChange, Func<OrderMarketSnapshot, bool>? predicate = null);
}