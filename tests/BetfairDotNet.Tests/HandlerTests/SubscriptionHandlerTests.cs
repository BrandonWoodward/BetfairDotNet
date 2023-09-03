using BetfairDotNet.Handlers;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Streaming;
using NSubstitute;
using Xunit;

namespace BetfairDotNet.Tests.HandlerTests;
public class StreamSubscriptionHandlerTests {

    private readonly ISslSocketHandler _socketHandler = Substitute.For<ISslSocketHandler>();
    private readonly IChangeMessageHandler _changeMessageHandler = Substitute.For<IChangeMessageHandler>();
    private readonly IObservable<ReadOnlyMemory<byte>> _messageStream = Substitute.For<IObservable<ReadOnlyMemory<byte>>>();
    private readonly ISubject _changeMessageSubject = Substitute.For<ISubject>();
    private StreamSubscriptionHandler _streamSubscriptionHandler;


    public StreamSubscriptionHandlerTests() {
        _streamSubscriptionHandler = new StreamSubscriptionHandler(_socketHandler, _changeMessageHandler, _messageStream, _changeMessageSubject);
    }


    [Fact]
    public void Subscribe_MarketOnly_ShouldSubscribeMarketCallback() {
        // Arrange
        Action<MarketSnapshot> marketCallback = _ => { };

        // Act
        var disposable = _streamSubscriptionHandler.Subscribe(marketCallback);

        // Assert
        _changeMessageSubject.Received(1).SubscribeMarket(Arg.Is(marketCallback));
    }


    [Fact]
    public void Subscribe_OrderOnly_ShouldSubscribeOrderCallback() {
        // Arrange
        Action<OrderMarketSnapshot> orderCallback = _ => { };

        // Act
        var disposable = _streamSubscriptionHandler.Subscribe(orderCallback);

        // Assert
        _changeMessageSubject.Received(1).SubscribeOrder(Arg.Is(orderCallback));
    }


    [Fact]
    public void FilterMarkets_ShouldSetMarketPredicate() {
        // Arrange
        Action<MarketSnapshot> marketCallback = _ => { };
        Func<MarketSnapshot, bool> predicate = _ => true;

        // Act
        _streamSubscriptionHandler.WithMarkets(predicate).Subscribe(marketCallback);

        // Act & Assert
        _changeMessageSubject.Received().SubscribeMarket(
            Arg.Any<Action<MarketSnapshot>>(),
            Arg.Is<Func<MarketSnapshot, bool>>(p => (Func<MarketSnapshot, bool>)p != null));
    }


    [Fact]
    public void FilterOrders_ShouldSetOrderPredicate() {
        // Arrange
        Action<OrderMarketSnapshot> orderCallback = _ => { };
        Func<OrderMarketSnapshot, bool> predicate = _ => true;

        // Act
        _streamSubscriptionHandler.WithOrders(predicate).Subscribe(orderCallback);

        // Act & Assert
        _changeMessageSubject.Received().SubscribeOrder(
            Arg.Any<Action<OrderMarketSnapshot>>(),
            Arg.Is<Func<OrderMarketSnapshot, bool>>(p => (Func<OrderMarketSnapshot, bool>)p != null)
        );
    }


    [Fact]
    public void Dispose_ShouldDisposeResources() {
        // Arrange & Act
        _streamSubscriptionHandler.Unsubscribe();

        // Assert
        _changeMessageSubject.Received(1).Dispose();
    }
}
