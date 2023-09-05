using BetfairDotNet.Enums.Streaming;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Exceptions;
using BetfairDotNet.Models.Streaming;

namespace BetfairDotNet.Handlers;

internal class ChangeMessageHandler : IChangeMessageHandler {

    private readonly ISslSocketHandler _socketHandler;
    private readonly IChangeMessageFactory _changeMessageFactory;
    private readonly IMarketSnapshotFactory _marketSnapshotFactory;
    private readonly IOrderSnapshotFactory _orderSnapshotFactory;
    private readonly ISubject _changeMessageSubject;

    private string? _marketInitialClk;
    private string? _orderInitialClk;
    private string? _marketClk;
    private string? _orderClk;


    public ChangeMessageHandler(
        ISslSocketHandler socketHandler,
        IChangeMessageFactory changeMessageFactory,
        IMarketSnapshotFactory marketSnapshotFactory,
        IOrderSnapshotFactory orderSnapshotFactory,
        ISubject changeMessageSubject) {

        _socketHandler = socketHandler;
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
            _socketHandler.Stop(); // Close socket
            _changeMessageSubject.OnExceptionNext(esaEx);
        }
    }


    public Tuple<string?, string?, string?, string?> GetClocks() {
        return Tuple.Create(_marketInitialClk, _orderInitialClk, _marketClk, _orderClk);
    }


    private void HandleStatus(StatusMessage statusChange) {
        if(statusChange.StatusCode == StatusCodeEnum.SUCCESS) return;
        var message = $"{statusChange.ErrorCode}: {statusChange.ErrorMessage}";
        var exception = new BetfairESAException(false, message);
        _socketHandler.Stop(); // Close socket
        _changeMessageSubject.OnExceptionNext(exception);
    }


    private void HandleMarket(MarketChangeMessage marketChange) {
        if(marketChange.InitialClk != null) {
            _marketInitialClk = marketChange.InitialClk;
        }
        if(marketChange.Clk != null) {
            _marketClk = marketChange.Clk;
        }
        var marketSnaps = _marketSnapshotFactory.GetSnapshots(marketChange);
        foreach(var marketSnapshot in marketSnaps) {
            _changeMessageSubject.OnMarketNext(marketSnapshot);
        }
    }


    private void HandleOrder(OrderChangeMessage orderChange) {
        if(orderChange.InitialClk != null) {
            _orderInitialClk = orderChange.InitialClk;
        }
        if(orderChange.Clk != null) {
            _orderClk = orderChange.Clk;
        }
        var orderSnaps = _orderSnapshotFactory.GetSnapshots(orderChange);
        foreach(var orderSnap in orderSnaps) {
            _changeMessageSubject.OnOrderNext(orderSnap);
        }
    }
}
