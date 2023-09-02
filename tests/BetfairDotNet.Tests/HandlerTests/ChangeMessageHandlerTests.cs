﻿using BetfairDotNet.Handlers;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Exceptions;
using BetfairDotNet.Models.Streaming;
using NSubstitute;
using Xunit;

namespace BetfairDotNet.Tests.HandlerTests;

public class ChangeMessageHandlerTests {

    private readonly IChangeMessageFactory _changeMessageFactory = Substitute.For<IChangeMessageFactory>();
    private readonly IMarketSnapshotFactory _marketSnapshotFactory = Substitute.For<IMarketSnapshotFactory>();
    private readonly IOrderSnapshotFactory _orderSnapshotFactory = Substitute.For<IOrderSnapshotFactory>();
    private readonly ISubject _changeMessageSubject = Substitute.For<ISubject>();


    [Fact]
    public void HandleMessage_ShouldNotCallFactories_WhenMessageEmpty() {
        // Arrange
        var handler = new ChangeMessageHandler(
            _changeMessageFactory,
            _marketSnapshotFactory,
            _orderSnapshotFactory,
            _changeMessageSubject
        );

        // Act
        handler.HandleMessage(ReadOnlyMemory<byte>.Empty);

        // Assert
        _changeMessageFactory.DidNotReceive().Process(Arg.Any<ReadOnlyMemory<byte>>());
    }


    [Fact]
    public void HandleMessage_ShouldCallSubjectOnMarketNext_WhenMessageIsMarketChange() {
        // Arrange
        var changeMessage = new MarketChangeMessage();
        var marketSnapshot = new MarketSnapshot();
        _changeMessageFactory.Process(Arg.Any<ReadOnlyMemory<byte>>()).Returns(changeMessage);
        _marketSnapshotFactory.GetSnapshots(changeMessage).Returns(new List<MarketSnapshot> { marketSnapshot });
        var handler = new ChangeMessageHandler(
            _changeMessageFactory,
            _marketSnapshotFactory,
            _orderSnapshotFactory,
            _changeMessageSubject
        );

        // Act
        handler.HandleMessage(new ReadOnlyMemory<byte>(new byte[1]));

        // Assert
        _changeMessageSubject.Received(1).OnMarketNext(marketSnapshot);
    }


    [Fact]
    public void HandleMessage_ShouldCallSubjectOnOrderNext_WhenMessageIsOrderChange() {
        // Arrange
        var changeMessage = new OrderChangeMessage();
        var orderSnapshot = new OrderMarketSnapshot();
        _changeMessageFactory.Process(Arg.Any<ReadOnlyMemory<byte>>()).Returns(changeMessage);
        _orderSnapshotFactory.GetSnapshots(changeMessage).Returns(new List<OrderMarketSnapshot> { orderSnapshot });
        var handler = new ChangeMessageHandler(
            _changeMessageFactory,
            _marketSnapshotFactory,
            _orderSnapshotFactory,
            _changeMessageSubject
        );

        // Act
        handler.HandleMessage(new ReadOnlyMemory<byte>(new byte[1]));

        // Assert
        _changeMessageSubject.Received(1).OnOrderNext(orderSnapshot);
    }


    [Fact]
    public void HandleException_ShouldCallSubjectOnExceptionNext_WhenBetfairESAExceptionThrown() {
        // Arrange
        var exception = new BetfairESAException(false, "Some message");
        var handler = new ChangeMessageHandler(
            _changeMessageFactory,
            _marketSnapshotFactory,
            _orderSnapshotFactory,
            _changeMessageSubject
        );

        // Act
        handler.HandleException(exception);

        // Assert
        _changeMessageSubject.Received(1).OnExceptionNext(exception);
    }
}
