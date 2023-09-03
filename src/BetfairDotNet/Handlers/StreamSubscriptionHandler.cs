using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Streaming;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace BetfairDotNet.Handlers;


/// <summary>
/// Handles subscriptions to the Betfair Streaming API.
/// </summary>
public sealed class StreamSubscriptionHandler : IStreamSubscriptionHandler {

    private readonly ISslSocketHandler _socketHandler;
    private readonly ISubject _changeMessageSubject;
    private readonly IDisposable _messageSubscription;

    private Func<MarketSnapshot, bool>? _marketPredicate;
    private Func<OrderMarketSnapshot, bool>? _orderPredicate;


    internal StreamSubscriptionHandler(
        ISslSocketHandler socketHandler,
        IChangeMessageHandler changeMessageHandler,
        IObservable<ReadOnlyMemory<byte>> messageStream,
        ISubject changeMessageSubject) {

        _socketHandler = socketHandler;
        _changeMessageSubject = changeMessageSubject;
        _messageSubscription = messageStream.Subscribe(
            changeMessageHandler.HandleMessage,
            changeMessageHandler.HandleException
        );
    }


    /// <summary>
    /// Subscribe to market change stream events, with the provided callbacks.
    /// </summary>
    /// <param name="onMarketChange"></param>
    /// <param name="onException"></param>
    public IDisposable Subscribe(
        Action<MarketSnapshot> onMarketChange,
        Action<Exception>? onException = null) {

        if(onMarketChange == null) throw new ArgumentNullException(nameof(onMarketChange));
        return SubscribeInternal(onMarketChange, null, onException);
    }


    /// <summary>
    /// Subscribe to order change stream events, with the provided callbacks.
    /// </summary>
    /// <param name="onOrderChange"></param>
    /// <param name="onException"></param>
    public IDisposable Subscribe(
        Action<OrderMarketSnapshot> onOrderChange,
        Action<Exception>? onException = null) {

        if(onOrderChange == null) throw new ArgumentNullException(nameof(onOrderChange));
        return SubscribeInternal(null, onOrderChange, onException);
    }


    /// <summary>
    /// Subscribe to the market and order stream events using with the provided callbacks.
    /// </summary>
    /// <param name="onMarketChange"></param>
    /// <param name="onOrderChange"></param>
    /// <param name="onException"></param>
    public IDisposable Subscribe(
        Action<MarketSnapshot> onMarketChange,
        Action<OrderMarketSnapshot> onOrderChange,
        Action<Exception>? onException = null) {

        if(onMarketChange == null) throw new ArgumentNullException(nameof(onMarketChange));
        if(onOrderChange == null) throw new ArgumentNullException(nameof(onOrderChange));
        return SubscribeInternal(onMarketChange, onOrderChange, onException);
    }


    /// <summary>
    /// End the stream. Closes the socket and disposes of the subscription.
    /// </summary>
    public void Unsubscribe() {
        _socketHandler.Stop();
        _messageSubscription.Dispose();
        _changeMessageSubject.Dispose();
    }


    /// <summary>
    /// Filter market subscription events based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public StreamSubscriptionHandler WithMarkets(Func<MarketSnapshot, bool> predicate) {
        _marketPredicate = predicate;
        return this;
    }


    /// <summary>
    /// Filter order subscription events based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public StreamSubscriptionHandler WithOrders(Func<OrderMarketSnapshot, bool> predicate) {
        _orderPredicate = predicate;
        return this;
    }


    private IDisposable SubscribeInternal(
        Action<MarketSnapshot>? onMarketChange,
        Action<OrderMarketSnapshot>? onOrderChange,
        Action<Exception>? onException) {

        var disposables = new List<IDisposable>();
        if(onMarketChange != null) {
            disposables.Add(_changeMessageSubject.SubscribeMarket(onMarketChange, _marketPredicate));
        }
        if(onOrderChange != null) {
            disposables.Add(_changeMessageSubject.SubscribeOrder(onOrderChange, _orderPredicate));
        }
        if(onException != null) {
            disposables.Add(_changeMessageSubject.SubscribeException(onException));
        }
        return new CompositeDisposable(disposables);
    }
}
