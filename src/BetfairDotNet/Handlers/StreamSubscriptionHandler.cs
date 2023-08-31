using BetfairDotNet.Enums.Streaming;
using BetfairDotNet.Factories;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Exceptions;
using BetfairDotNet.Models.Streaming;
using System.Collections.Concurrent;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace BetfairDotNet.Handlers;


/// <summary>
/// Handles subscriptions to the Betfair Streaming API.
/// </summary>
public sealed class StreamSubscriptionHandler : IDisposable {


    private readonly Subject<MarketSnapshot> _marketSubject;
    private readonly Subject<OrderMarketSnapshot> _orderSubject;
    private readonly Subject<BetfairESAException> _exceptionSubject;
    private readonly IDisposable _messageSubscription;
    private readonly IChangeMessageFactory _changeMessageFactory;
    private readonly IMarketSnapshotFactory _marketSnapshotFactory;
    private readonly IOrderSnapshotFactory _orderSnapshotFactory;

    private Func<MarketSnapshot, bool>? _marketPredicate;
    private Func<OrderMarketSnapshot, bool>? _orderPredicate;


    internal StreamSubscriptionHandler(IObservable<ReadOnlyMemory<byte>> messageStream) {
        _marketSubject = new();
        _orderSubject = new();
        _exceptionSubject = new();

        var marketCache = new ConcurrentDictionary<string, MarketSnapshot>();
        var orderCache = new ConcurrentDictionary<string, OrderMarketSnapshot>();

        _changeMessageFactory = new ChangeMessageFactory();
        _marketSnapshotFactory = new MarketSnapshotFactory();
        _orderSnapshotFactory = new OrderSnapshotFactory(orderCache);

        _messageSubscription = messageStream.Subscribe(OnMessage, OnException);
    }


    private void OnMessage(ReadOnlyMemory<byte> message) {
        if(message.IsEmpty) return; // Should only happen in testing
        var changeMessage = _changeMessageFactory.Process(message);
        switch(changeMessage) {
            case ConnectionMessage connection:
                OnConnection(connection);
                break;
            case StatusMessage status:
                OnStatus(status);
                break;
            case MarketChangeMessage marketChange:
                OnMarket(marketChange);
                break;
            case OrderChangeMessage orderChange:
                OnOrder(orderChange);
                break;
        }
    }


    private void OnException(Exception ex) {
        if(ex is BetfairESAException esaEx) {
            _exceptionSubject.OnNext(esaEx);
        }
    }


    private void OnConnection(ConnectionMessage connection) {
        // TODO: log connection id
    }


    private void OnStatus(StatusMessage statusChange) {
        if(statusChange.StatusCode == StatusCodeEnum.SUCCESS) return;
        var message = $"{statusChange.ErrorCode}: {statusChange.ErrorMessage}";
        var exception = new BetfairESAException(false, message);
        _exceptionSubject.OnNext(exception);
    }


    private void OnOrder(OrderChangeMessage orderChange) {
        var orderSnaps = _orderSnapshotFactory.GetSnapshots(orderChange);
        foreach(var orderSnap in orderSnaps) {
            _orderSubject.OnNext(orderSnap);
        }
    }


    private void OnMarket(MarketChangeMessage marketChange) {
        var marketSnaps = _marketSnapshotFactory.GetSnapshots(marketChange);
        foreach(var marketSnap in marketSnaps) {
            _marketSubject.OnNext(marketSnap);
        }
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
    /// Filter market subscription events based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public StreamSubscriptionHandler FilterMarkets(Func<MarketSnapshot, bool> predicate) {
        _marketPredicate = predicate;
        return this;
    }


    /// <summary>
    /// Filter order subscription events based on a predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public StreamSubscriptionHandler FilterOrders(Func<OrderMarketSnapshot, bool> predicate) {
        _orderPredicate = predicate;
        return this;
    }


    private IDisposable SubscribeInternal(
        Action<MarketSnapshot>? onMarketChange,
        Action<OrderMarketSnapshot>? onOrderChange,
        Action<Exception>? onException) {

        var disposables = new List<IDisposable>();
        if(onMarketChange != null) {
            var marketObservable = _marketPredicate == null
                ? _marketSubject.AsObservable()
                : _marketSubject.Where(_marketPredicate);
            disposables.Add(marketObservable.Subscribe(onMarketChange));
        }
        if(onOrderChange != null) {
            var orderObservable = _orderPredicate == null
                ? _orderSubject.AsObservable()
                : _orderSubject.Where(_orderPredicate);
            disposables.Add(orderObservable.Subscribe(onOrderChange));
        }
        if(onException != null) {
            disposables.Add(_exceptionSubject.Subscribe(onException));
        }
        return new CompositeDisposable(disposables);
    }


    public void Dispose() {
        _messageSubscription.Dispose();
        _marketSubject.Dispose();
        _orderSubject.Dispose();
        _exceptionSubject.Dispose();
    }
}
