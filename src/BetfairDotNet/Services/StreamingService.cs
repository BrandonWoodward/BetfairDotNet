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

    private MarketSubscription? _marketSubscription;
    private OrderSubscription? _orderSubscription;
    private StreamConfiguration? _streamConfiguration;


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
    public StreamingService CreateStream(StreamConfiguration streamConfiguration) {
        if(streamConfiguration is null) throw new ArgumentNullException(nameof(streamConfiguration));
        streamConfiguration.ApiKey = _apiKey;
        _streamConfiguration = streamConfiguration;
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

        if(_streamConfiguration == null) {
            throw new InvalidOperationException("No stream configuration set.");
        }
        if(_marketSubscription == null && _orderSubscription == null) {
            throw new InvalidOperationException("No subscription criteria provided.");
        }

        await _streamSubscriptionHandler.Subscribe(
            _streamConfiguration,
            _marketSubscription,
            _orderSubscription,
            onMarketChange,
            onOrderChange,
            onException
        );
    }
}
