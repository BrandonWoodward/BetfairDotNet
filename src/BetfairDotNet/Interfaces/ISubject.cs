using BetfairDotNet.Models.Exceptions;
using BetfairDotNet.Models.Streaming;

namespace BetfairDotNet.Interfaces;
internal interface ISubject
{
    void Dispose();
    void OnExceptionNext(BetfairESAException exception);
    void OnMarketNext(MarketSnapshot marketSnapshot);
    void OnOrderNext(OrderMarketSnapshot orderMarketSnapshot);
    IDisposable SubscribeException(Action<BetfairESAException> onException);
    IDisposable SubscribeMarket(Action<MarketSnapshot> onMarketChange);
    IDisposable SubscribeOrder(Action<OrderMarketSnapshot> onOrderChange);
}