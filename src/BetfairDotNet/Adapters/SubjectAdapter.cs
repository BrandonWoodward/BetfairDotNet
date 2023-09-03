using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Exceptions;
using BetfairDotNet.Models.Streaming;
using System.Diagnostics.CodeAnalysis;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace BetfairDotNet.Adapters;


[ExcludeFromCodeCoverage]
internal class SubjectAdapter : ISubject {

    private readonly Subject<MarketSnapshot> _marketSubject;
    private readonly Subject<OrderMarketSnapshot> _orderSubject;
    private readonly Subject<BetfairESAException> _exceptionSubject;


    public SubjectAdapter() {
        _marketSubject = new();
        _orderSubject = new();
        _exceptionSubject = new();
    }


    public void OnMarketNext(MarketSnapshot marketSnapshot) {
        _marketSubject.OnNext(marketSnapshot);
    }


    public void OnOrderNext(OrderMarketSnapshot orderMarketSnapshot) {
        _orderSubject.OnNext(orderMarketSnapshot);
    }


    public void OnExceptionNext(BetfairESAException exception) {
        _exceptionSubject.OnNext(exception);
    }


    public IDisposable SubscribeMarket(Action<MarketSnapshot> onMarketChange, Func<MarketSnapshot, bool>? predicate = null) {
        var marketObservable = predicate == null
            ? _marketSubject.AsObservable()
            : _marketSubject.Where(predicate);
        return marketObservable.Subscribe(onMarketChange);
    }


    public IDisposable SubscribeOrder(Action<OrderMarketSnapshot> onOrderChange, Func<OrderMarketSnapshot, bool>? predicate = null) {
        var orderObservable = predicate == null
            ? _orderSubject.AsObservable()
            : _orderSubject.Where(predicate);
        return orderObservable.Subscribe(onOrderChange);
    }


    public IDisposable SubscribeException(Action<BetfairESAException> onException) {
        return _exceptionSubject.Subscribe(onException);
    }


    public void Dispose() {
        _marketSubject.Dispose();
        _orderSubject.Dispose();
        _exceptionSubject.Dispose();
    }
}
