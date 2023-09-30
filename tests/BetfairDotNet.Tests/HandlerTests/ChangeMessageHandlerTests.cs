using BetfairDotNet.Enums.Streaming;
using BetfairDotNet.Handlers;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Exceptions;
using BetfairDotNet.Models.Streaming;
using NSubstitute;
using Xunit;

namespace BetfairDotNet.Tests.HandlerTests;

public class ChangeMessageHandlerTests {

    private readonly ISslSocketHandler _socketHandler;
    private readonly IChangeMessageFactory _changeMessageFactory;
    private readonly IMarketSnapshotFactory _marketSnapshotFactory;
    private readonly IOrderSnapshotFactory _orderSnapshotFactory;
    private readonly ISubject _changeMessageSubject;
    private readonly ChangeMessageHandler _sut;


    public ChangeMessageHandlerTests() {
        _socketHandler = Substitute.For<ISslSocketHandler>();
        _changeMessageFactory = Substitute.For<IChangeMessageFactory>();
        _marketSnapshotFactory = Substitute.For<IMarketSnapshotFactory>();
        _orderSnapshotFactory = Substitute.For<IOrderSnapshotFactory>();
        _changeMessageSubject = Substitute.For<ISubject>();
        _sut = new(
            _socketHandler,
            _changeMessageFactory,
            _marketSnapshotFactory,
            _orderSnapshotFactory,
            _changeMessageSubject
        );
    }


    [Fact]
    public void HandleMessage_ShouldNotCallFactories_WhenMessageEmpty() {
        // Act
        _sut.HandleMessage(ReadOnlyMemory<byte>.Empty);

        // Assert
        _changeMessageFactory.DidNotReceive().Process(Arg.Any<ReadOnlyMemory<byte>>());
    }


    [Fact]
    public void HandleMessage_ShouldCallSubjectOnExceptionNext_WhenMessageIsStatusAndNotSuccess() {
        // Arrange
        var changeMessage = new StatusMessage { StatusCode = StatusCodeEnum.FAILURE };
        _changeMessageFactory.Process(Arg.Any<ReadOnlyMemory<byte>>()).Returns(changeMessage);

        // Act
        _sut.HandleMessage(new(new byte[1]));

        // Assert
        _changeMessageSubject.Received(1).OnExceptionNext(Arg.Any<BetfairESAException>());
    }


    [Fact]
    public void HandleMessage_ShouldCallSubjectOnMarketNext_WhenMessageIsMarketChange() {
        // Arrange
        var changeMessage = new MarketChangeMessage { InitialClk = "123", Clk = "123" };
        var marketSnapshot = new MarketSnapshot();
        _changeMessageFactory.Process(Arg.Any<ReadOnlyMemory<byte>>()).Returns(changeMessage);
        _marketSnapshotFactory.GetSnapshots(changeMessage).Returns(new List<MarketSnapshot> { marketSnapshot });

        // Act
        _sut.HandleMessage(new(new byte[1]));

        // Assert
        _changeMessageSubject.Received(1).OnMarketNext(marketSnapshot);
    }


    [Fact]
    public void HandleMessage_ShouldCallSubjectOnOrderNext_WhenMessageIsOrderChange() {
        // Arrange
        var changeMessage = new OrderChangeMessage { InitialClk = "123", Clk = "123" };
        var orderSnapshot = new OrderMarketSnapshot();
        _changeMessageFactory.Process(Arg.Any<ReadOnlyMemory<byte>>()).Returns(changeMessage);
        _orderSnapshotFactory.GetSnapshots(changeMessage).Returns(new List<OrderMarketSnapshot> { orderSnapshot });

        // Act
        _sut.HandleMessage(new(new byte[1]));

        // Assert
        _changeMessageSubject.Received(1).OnOrderNext(orderSnapshot);
    }


    [Fact]
    public void HandleException_ShouldCallSubjectOnExceptionNext_WhenBetfairESAExceptionThrown() {
        // Arrange
        var exception = new BetfairESAException("Some message");

        // Act
        _sut.HandleException(exception);

        // Assert
        _changeMessageSubject.Received(1).OnExceptionNext(exception);
    }
}
