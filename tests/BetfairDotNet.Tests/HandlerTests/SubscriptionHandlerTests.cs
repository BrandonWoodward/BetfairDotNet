using BetfairDotNet.Handlers;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Streaming;
using NSubstitute;
using Xunit;

namespace BetfairDotNet.Tests.HandlerTests;

public class StreamSubscriptionHandlerTests {

    private readonly ISslSocketHandler _socketHandler = Substitute.For<ISslSocketHandler>();
    private readonly IChangeMessageHandler _changeMessageHandler = Substitute.For<IChangeMessageHandler>();
    private readonly ISubject _changeMessageSubject = Substitute.For<ISubject>();


    [Fact]
    public async Task Subscribe_ShouldAuthenticateConnection_WithAuthenticationMessage() {
        // Arrange
        var socketHandler = Substitute.For<ISslSocketHandler>();
        var changeMessageHandler = Substitute.For<IChangeMessageHandler>();
        var changeMessageSubject = Substitute.For<ISubject>();

        var handler = new StreamSubscriptionHandler(socketHandler, changeMessageHandler, changeMessageSubject);

        var authMessage = new AuthenticationMessage("token", "apiKey");

        // Act
        await handler.Subscribe(authMessage);

        // Assert
        await socketHandler.Received().Start();
        await socketHandler.Received().SendLine(authMessage);
    }


    [Fact]
    public async Task Subscribe_ShouldSubscribeToMarket_WhenMarketSubscriptionAndActionAreProvided() {
        // Arrange
        var socketHandler = Substitute.For<ISslSocketHandler>();
        var changeMessageHandler = Substitute.For<IChangeMessageHandler>();
        var changeMessageSubject = Substitute.For<ISubject>();
        var handler = new StreamSubscriptionHandler(socketHandler, changeMessageHandler, changeMessageSubject);
        var marketSubscription = new MarketSubscription(new StreamingMarketFilter(), new StreamingMarketDataFilter());
        Action<MarketSnapshot> action = _ => { };

        // Act
        await handler.Subscribe(new AuthenticationMessage("token", "apiKey"), marketSubscription, null, action);

        // Assert
        changeMessageSubject.Received().SubscribeMarket(action);
        await socketHandler.Received().SendLine(marketSubscription);
    }


    [Fact]
    public async Task Resubscribe_ShouldReauthenticateAndResubscribe_WithClocks() {
        // Arrange
        var socketHandler = Substitute.For<ISslSocketHandler>();
        var changeMessageHandler = Substitute.For<IChangeMessageHandler>();
        var changeMessageSubject = Substitute.For<ISubject>();

        var handler = new StreamSubscriptionHandler(socketHandler, changeMessageHandler, changeMessageSubject);

        var marketSubscription = new MarketSubscription(new StreamingMarketFilter(), new StreamingMarketDataFilter());
        var orderSubscription = new OrderSubscription(new OrderFilter());

        changeMessageHandler.GetClocks().Returns(Tuple.Create("initialClk1", "initialClk2", "clk1", "clk2"));

        // Act
        await handler.Resubscribe(new AuthenticationMessage("token", "apiKey"), marketSubscription, orderSubscription);

        // Assert
        await socketHandler.Received().SendLine(Arg.Is<MarketSubscription>(x => x.InitialClk == "initialClk1" && x.Clk == "clk1"));
        await socketHandler.Received().SendLine(Arg.Is<OrderSubscription>(x => x.InitialClk == "initialClk2" && x.Clk == "clk2"));
    }
}
