using BetfairDotNet.Enums.Streaming;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Exceptions;
using BetfairDotNet.Models.Streaming;

namespace BetfairDotNet.Handlers;

internal class ChangeMessageHandler : IChangeMessageHandler {

    private readonly IChangeMessageFactory _changeMessageFactory;
    private readonly IMarketSnapshotFactory _marketSnapshotFactory;
    private readonly IOrderSnapshotFactory _orderSnapshotFactory;
    private readonly ISubject _changeMessageSubject;


    public ChangeMessageHandler(
        IChangeMessageFactory changeMessageFactory,
        IMarketSnapshotFactory marketSnapshotFactory,
        IOrderSnapshotFactory orderSnapshotFactory,
        ISubject changeMessageSubject) {

        _changeMessageFactory = changeMessageFactory;
        _marketSnapshotFactory = marketSnapshotFactory;
        _orderSnapshotFactory = orderSnapshotFactory;
        _changeMessageSubject = changeMessageSubject;
    }


    public void HandleMessage(ReadOnlyMemory<byte> message) {
        if(message.IsEmpty) return; // Should only happen in testing
        var changeMessage = _changeMessageFactory.Process(message);
        // TODO handle ConnectionMessage and log ConnectionId
        switch(changeMessage) {
            case StatusMessage status:
                HandleStatus(status);
                break;
            case MarketChangeMessage marketChange:
                HandleMarket(marketChange);
                break;
            case OrderChangeMessage orderChange:
                HandleOrder(orderChange);
                break;
        }
    }


    public void HandleException(Exception ex) {
        if(ex is BetfairESAException esaEx) {
            _changeMessageSubject.OnExceptionNext(esaEx);
        }
    }


    private void HandleStatus(StatusMessage statusChange) {
        if(statusChange.StatusCode == StatusCodeEnum.SUCCESS) return;
        var message = $"{statusChange.ErrorCode}: {statusChange.ErrorMessage}";
        var exception = new BetfairESAException(false, message);
        _changeMessageSubject.OnExceptionNext(exception);
    }


    private void HandleMarket(MarketChangeMessage marketChange) {
        var marketSnaps = _marketSnapshotFactory.GetSnapshots(marketChange);
        foreach(var marketSnapshot in marketSnaps) {
            _changeMessageSubject.OnMarketNext(marketSnapshot);
        }
    }


    private void HandleOrder(OrderChangeMessage orderChange) {
        var orderSnaps = _orderSnapshotFactory.GetSnapshots(orderChange);
        foreach(var orderSnap in orderSnaps) {
            _changeMessageSubject.OnOrderNext(orderSnap);
        }
    }
}
