using BetfairDotNet.Handlers;
using BetfairDotNet.Models.Exceptions;
using BetfairDotNet.Models.Streaming;

namespace BetfairDotNet.Interfaces;

public interface IStreamSubscriptionHandler
{
    Task Subscribe(AuthenticationMessage authenticationMessage, MarketSubscription? marketSubscription = null, OrderSubscription? orderSubscription = null, Action<MarketSnapshot>? onMarketChange = null, Action<OrderMarketSnapshot>? onOrderChange = null, Action<BetfairESAException>? onException = null);
    Task Resubscribe(AuthenticationMessage authenticationMessage, MarketSubscription? marketSubscription = null, OrderSubscription? orderSubscription = null);
    void Unsubscribe();
}