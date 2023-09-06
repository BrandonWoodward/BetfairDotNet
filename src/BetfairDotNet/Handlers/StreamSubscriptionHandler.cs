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
    private readonly IDisposable _recoverySubscription;

    private AuthenticationMessage? _authenticationMessage;
    private StreamConfiguration _streamConfiguration;
    private MarketSubscription? _marketSubscription;
    private OrderSubscription? _orderSubscription;

    private bool _exceptionFlag;


    internal StreamSubscriptionHandler(
        ISslSocketHandler socketHandler,
        IChangeMessageHandler changeMessageHandler,
        ISubject changeMessageSubject) {

        _socketHandler = socketHandler;
        _changeMessageHandler = changeMessageHandler;
        _changeMessageSubject = changeMessageSubject;

        _messageSubscription = _socketHandler.MessageReceived.Subscribe(
            ForwardMessageToHandler,
            ForwardExceptionToHandler
        );
        _recoverySubscription = _socketHandler.SocketRecovered.Subscribe(
            async _ => await CheckResubscribe()
        );
    }


    public async Task Subscribe(
        StreamConfiguration streamConfiguration,
        MarketSubscription? marketSubscription = null,
        OrderSubscription? orderSubscription = null,
        Action<MarketSnapshot>? onMarketChange = null,
        Action<OrderMarketSnapshot>? onOrderChange = null,
        Action<BetfairESAException>? onException = null) {

        _streamConfiguration = streamConfiguration;
        _marketSubscription = marketSubscription;
        _orderSubscription = orderSubscription;
        _authenticationMessage = new AuthenticationMessage(
            streamConfiguration.SessionToken,
            streamConfiguration.ApiKey
        );

        // Need to subscribe to the observable before sending subscription messages
        // in order to catch exceptions
        if(onMarketChange != null) _changeMessageSubject.SubscribeMarket(onMarketChange);
        if(onOrderChange != null) _changeMessageSubject.SubscribeOrder(onOrderChange);
        if(onException != null) _changeMessageSubject.SubscribeException(onException);

        await SendSubscriptionMessages();
    }


    public void Unsubscribe() {
        _socketHandler.Stop();
        _messageSubscription.Dispose();
        _recoverySubscription.Dispose();
        _changeMessageSubject.Dispose();
    }


    private async Task SendSubscriptionMessages() {
        // Clocks will be null on initial subscription, that's ok;
        var (marketInitialClk, orderInitialClk, marketClk, orderClk) = _changeMessageHandler.GetClocks();
        await AuthenticateConnection();
        await SubscribeMarket(marketInitialClk, marketClk);
        await SubscribeOrder(orderInitialClk, orderClk);
    }


    private async Task AuthenticateConnection() {
        var heartbeatMs = _marketSubscription?.HeartbeatMs ?? _orderSubscription?.HeartbeatMs;
        await _socketHandler.Start(
            _streamConfiguration.RecoveryThresholdMs + heartbeatMs!.Value, // Account for heartbeat interval
            _streamConfiguration.MaxRecoveryWaitMs
        );
        if(_exceptionFlag) return; // Don't continue on exception
        await _socketHandler.SendLine(_authenticationMessage!); // Not null here, set in Subscribe()
    }


    private async Task SubscribeMarket(string? initialClk, string? clk) {
        if(_marketSubscription == null || _exceptionFlag) return; // Don't continue on exception
        _marketSubscription = _marketSubscription with { InitialClk = initialClk, Clk = clk };
        await _socketHandler.SendLine(_marketSubscription);
    }


    private async Task SubscribeOrder(string? initialClk, string? clk) {
        if(_orderSubscription == null || _exceptionFlag) return; // Don't continue on exception
        _orderSubscription = _orderSubscription with { InitialClk = initialClk, Clk = clk };
        await _socketHandler.SendLine(_orderSubscription);
    }


    private async Task CheckResubscribe() {
        _socketHandler.Stop();
        await SendSubscriptionMessages();
    }


    private void ForwardExceptionToHandler(Exception exception) {
        _exceptionFlag = true;
        _changeMessageHandler.HandleException(exception);
    }


    private void ForwardMessageToHandler(ReadOnlyMemory<byte> message) {
        if(_exceptionFlag) _exceptionFlag = false; // Message received, must be ok
        _changeMessageHandler.HandleMessage(message);
    }
}
