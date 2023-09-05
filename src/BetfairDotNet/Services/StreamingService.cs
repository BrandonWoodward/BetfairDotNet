using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Exceptions;
using BetfairDotNet.Models.Streaming;

namespace BetfairDotNet.Services;


/// <summary>
/// The service for the Betfair Streaming API.
/// </summary>
public sealed class StreamingService {

    private readonly IStreamSubscriptionHandler _streamSubscriptionHandler;
    private readonly string _apiKey;

    private AuthenticationMessage? _authenticationMessage;
    private MarketSubscription? _marketSubscription;
    private OrderSubscription? _orderSubscription;


    internal StreamingService(
        IStreamSubscriptionHandler streamSubscriptionHandler,
        string apiKey) {

        _streamSubscriptionHandler = streamSubscriptionHandler;
        _apiKey = apiKey;
    }


    /// <summary>
    /// Create a stream. Sets the authentication message.
    /// </summary>
    /// <param name="sessionToken"></param>
    /// <returns></returns>
    public StreamingService CreateStream(string sessionToken) {
        if(string.IsNullOrWhiteSpace(sessionToken)) {
            throw new ArgumentException("Session token not provided.");
        }
        _authenticationMessage = new AuthenticationMessage(sessionToken, _apiKey);
        return this;
    }


    /// <summary>
    /// Set the subscription criteria for market changes.
    /// </summary>
    /// <param name="marketSubscription"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public StreamingService WithMarketSubscription(MarketSubscription marketSubscription) {
        _marketSubscription = marketSubscription ?? throw new ArgumentNullException(nameof(marketSubscription));
        return this;
    }


    /// <summary>
    /// Set the subscription criteria for order changes.
    /// </summary>
    /// <param name="orderSubscription"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public StreamingService WithOrderSubscription(OrderSubscription orderSubscription) {
        _orderSubscription = orderSubscription ?? throw new ArgumentNullException(nameof(orderSubscription));
        return this;
    }


    /// <summary>
    /// Subscribe to the stream and begin receiving messages.
    /// </summary>
    /// <param name="onMarketChange"></param>
    /// <param name="onOrderChange"></param>
    /// <param name="onException"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task Subscribe(
        Action<MarketSnapshot>? onMarketChange = null,
        Action<OrderMarketSnapshot>? onOrderChange = null,
        Action<BetfairESAException>? onException = null) {

        if(_authenticationMessage == null) {
            throw new InvalidOperationException("Session token not provided.");
        }
        if(_marketSubscription == null && _orderSubscription == null) {
            throw new InvalidOperationException("No subscription criteria provided.");
        }
        await _streamSubscriptionHandler.Subscribe(
            _authenticationMessage,
            _marketSubscription,
            _orderSubscription,
            onMarketChange,
            onOrderChange,
            onException
        );
    }


    public async Task Resubscribe() {
        if(_marketSubscription == null && _orderSubscription == null) {
            throw new InvalidOperationException("No subscriptions set.");
        }
        if(_authenticationMessage == null) {
            throw new InvalidOperationException("Session token not provided.");
        }
        await _streamSubscriptionHandler.Resubscribe(
            _authenticationMessage,
            _marketSubscription,
            _orderSubscription
        );
    }
}
