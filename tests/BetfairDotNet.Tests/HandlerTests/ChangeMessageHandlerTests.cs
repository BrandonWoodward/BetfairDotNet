using BetfairDotNet.Enums.Streaming;
using BetfairDotNet.Handlers;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Exceptions;
using BetfairDotNet.Models.Streaming;
using NSubstitute;
using Xunit;

namespace BetfairDotNet.Tests.HandlerTests;

public class ChangeMessageHandlerTests {

    private readonly ISslSocketHandler _socketHandler = Substitute.For<ISslSocketHandler>();
    private readonly IChangeMessageFactory _changeMessageFactory = Substitute.For<IChangeMessageFactory>();
    private readonly IMarketSnapshotFactory _marketSnapshotFactory = Substitute.For<IMarketSnapshotFactory>();
    private readonly IOrderSnapshotFactory _orderSnapshotFactory = Substitute.For<IOrderSnapshotFactory>();
    private readonly ISubject _changeMessageSubject = Substitute.For<ISubject>();


    [Fact]
    public void HandleMessage_ShouldNotCallFactories_WhenMessageEmpty() {
        // Arrange
        var sut = new ChangeMessageHandler(
            _socketHandler,
            _changeMessageFactory,
            _marketSnapshotFactory,
            _orderSnapshotFactory,
            _changeMessageSubject
        );

        // Act
        sut.HandleMessage(ReadOnlyMemory<byte>.Empty);

        // Assert
        _changeMessageFactory.DidNotReceive().Process(Arg.Any<ReadOnlyMemory<byte>>());
    }


    [Fact]
    public void HandleMessage_ShouldCallSubjectOnExceptionNext_WhenMessageIsStatusAndNotSuccess() {
        // Arrange
        var changeMessage = new StatusMessage() { StatusCode = StatusCodeEnum.FAILURE };
        _changeMessageFactory.Process(Arg.Any<ReadOnlyMemory<byte>>()).Returns(changeMessage);
        var sut = new ChangeMessageHandler(
            _socketHandler,
            _changeMessageFactory,
            _marketSnapshotFactory,
            _orderSnapshotFactory,
            _changeMessageSubject
        );

        // Act
        sut.HandleMessage(new ReadOnlyMemory<byte>(new byte[1]));

        // Assert
        _changeMessageSubject.Received(1).OnExceptionNext(Arg.Any<BetfairESAException>());
    }


    [Fact]
    public void HandleMessage_ShouldCallSubjectOnMarketNext_WhenMessageIsMarketChange() {
        // Arrange
        var changeMessage = new MarketChangeMessage();
        var marketSnapshot = new MarketSnapshot();
        _changeMessageFactory.Process(Arg.Any<ReadOnlyMemory<byte>>()).Returns(changeMessage);
        _marketSnapshotFactory.GetSnapshots(changeMessage).Returns(new List<MarketSnapshot> { marketSnapshot });
        var sut = new ChangeMessageHandler(
            _socketHandler,
            _changeMessageFactory,
            _marketSnapshotFactory,
            _orderSnapshotFactory,
            _changeMessageSubject
        );

        // Act
        sut.HandleMessage(new ReadOnlyMemory<byte>(new byte[1]));

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
        var sut = new ChangeMessageHandler(
            _socketHandler,
            _changeMessageFactory,
            _marketSnapshotFactory,
            _orderSnapshotFactory,
            _changeMessageSubject
        );

        // Act
        sut.HandleMessage(new ReadOnlyMemory<byte>(new byte[1]));

        // Assert
        _changeMessageSubject.Received(1).OnOrderNext(orderSnapshot);
    }


    [Fact]
    public void HandleException_ShouldCallSubjectOnExceptionNext_WhenBetfairESAExceptionThrown() {
        // Arrange
        var exception = new BetfairESAException("Some message");
        var sut = new ChangeMessageHandler(
            _socketHandler,
            _changeMessageFactory,
            _marketSnapshotFactory,
            _orderSnapshotFactory,
            _changeMessageSubject
        );

        // Act
        sut.HandleException(exception);

        // Assert
        _changeMessageSubject.Received(1).OnExceptionNext(exception);
    }
}
