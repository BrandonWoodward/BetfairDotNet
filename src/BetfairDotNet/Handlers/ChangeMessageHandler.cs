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
        if(message.IsEmpty) return;
        var changeMessage = _changeMessageFactory.Process(message);
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
        _socketHandler.Stop();
        if(ex is BetfairESAException esaEx) {
            _changeMessageSubject.OnExceptionNext(esaEx);
        }
    }


    public Tuple<string?, string?, string?, string?> GetClocks() {
        return Tuple.Create(_marketInitialClk, _orderInitialClk, _marketClk, _orderClk);
    }


    private void HandleStatus(StatusMessage statusChange) {
        if(statusChange.StatusCode == StatusCodeEnum.SUCCESS) return;
        var message = $"{statusChange.ErrorCode}: {statusChange.ErrorMessage}";
        var exception = new BetfairESAException(message);
        _socketHandler.Stop(); // Close socket
        _changeMessageSubject.OnExceptionNext(exception);
    }


    private void HandleMarket(MarketChangeMessage mc) {
        if(mc.InitialClk != null) { _marketInitialClk = mc.InitialClk; }
        if(mc.Clk != null) { _marketClk = mc.Clk; }
        if(mc.ChangeType == ChangeTypeEnum.HEARTBEAT) return; // swallow heartbeat
        var marketSnaps = _marketSnapshotFactory.GetSnapshots(mc);
        foreach(var marketSnapshot in marketSnaps) {
            _changeMessageSubject.OnMarketNext(marketSnapshot);
        }
    }


    private void HandleOrder(OrderChangeMessage oc) {
        if(oc.InitialClk != null) { _orderInitialClk = oc.InitialClk; }
        if(oc.Clk != null) { _orderClk = oc.Clk; }
        if(oc.ChangeType == ChangeTypeEnum.HEARTBEAT) return; // swallow heartbeat
        var orderSnaps = _orderSnapshotFactory.GetSnapshots(oc);
        foreach(var orderSnap in orderSnaps) {
            _changeMessageSubject.OnOrderNext(orderSnap);
        }
    }
}
