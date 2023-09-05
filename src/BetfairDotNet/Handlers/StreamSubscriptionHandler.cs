using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Exceptions;
using BetfairDotNet.Models.Streaming;
using System.Reactive.Linq;

namespace BetfairDotNet.Handlers;


/// <summary>
/// Handles subscriptions to the Betfair Streaming API.
/// </summary>
public sealed class StreamSubscriptionHandler : IStreamSubscriptionHandler {

    private readonly ISslSocketHandler _socketHandler;
    private readonly IChangeMessageHandler _changeMessageHandler;
    private readonly ISubject _changeMessageSubject;
    private readonly IDisposable _messageSubscription;


    internal StreamSubscriptionHandler(
        ISslSocketHandler socketHandler,
        IChangeMessageHandler changeMessageHandler,
        ISubject changeMessageSubject) {

        _socketHandler = socketHandler;
        _changeMessageHandler = changeMessageHandler;
        _changeMessageSubject = changeMessageSubject;
        _messageSubscription = _socketHandler.MessageStream.Subscribe(
            changeMessageHandler.HandleMessage,
            changeMessageHandler.HandleException
        );
    }


    public async Task Subscribe(
        AuthenticationMessage authenticationMessage,
        MarketSubscription? marketSubscription = null,
        OrderSubscription? orderSubscription = null,
        Action<MarketSnapshot>? onMarketChange = null,
        Action<OrderMarketSnapshot>? onOrderChange = null,
        Action<BetfairESAException>? onException = null) {

        await AuthenticateConnection(authenticationMessage);
        await Task.WhenAll(
            SubscribeMarket(marketSubscription, onMarketChange),
            SubscribeOrder(orderSubscription, onOrderChange)
        );
        SubscribeException(onException);
    }


    public async Task Resubscribe(
        AuthenticationMessage authenticationMessage,
        MarketSubscription? marketSubscription = null,
        OrderSubscription? orderSubscription = null) {

        var (marketInitialClk, orderInitialClk, marketClk, orderClk) = _changeMessageHandler.GetClocks();
        if(marketSubscription != null) {
            marketSubscription = marketSubscription with { InitialClk = marketInitialClk, Clk = marketClk };
        }
        if(orderSubscription != null) {
            orderSubscription = orderSubscription with { InitialClk = orderInitialClk, Clk = orderClk };
        }
        await AuthenticateConnection(authenticationMessage);
        if(marketSubscription != null) {
            await _socketHandler.SendLine(marketSubscription);
        }
        if(orderSubscription != null) {
            await _socketHandler.SendLine(orderSubscription);
        }
    }


    public void Unsubscribe() {
        _socketHandler.Stop();
        _messageSubscription.Dispose();
        _changeMessageSubject.Dispose();
    }


    private async Task AuthenticateConnection(AuthenticationMessage message) {
        await _socketHandler.Start();
        await _socketHandler.SendLine(message);
    }


    private async Task SubscribeMarket(MarketSubscription? message, Action<MarketSnapshot>? action) {
        if(message == null || action == null) return;
        _changeMessageSubject.SubscribeMarket(action);
        await _socketHandler.SendLine(message);
    }


    private async Task SubscribeOrder(OrderSubscription? message, Action<OrderMarketSnapshot>? action) {
        if(message == null || action == null) return;
        _changeMessageSubject.SubscribeOrder(action);
        await _socketHandler.SendLine(message);
    }


    private void SubscribeException(Action<BetfairESAException>? action) {
        if(action == null) return;
        _changeMessageSubject.SubscribeException(action);
    }
}
