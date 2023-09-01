using BetfairDotNet.Handlers;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Streaming;
using BetfairDotNet.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace BetfairDotNet.Tests.ServiceTests;


public class StreamingServiceTests {

    private readonly ISslSocketHandler _mockSslSocketHandler = Substitute.For<ISslSocketHandler>();


    [Fact]
    public async Task CreateStream_ShouldReturnSubscriptionHandler_WhenSessionTokenAndMarketSubscriptionProvided() {
        // Arrange
        var sut = new StreamingService(_mockSslSocketHandler, "testApiKey");
        var sessionToken = "sessionToken";
        var marketSubscription = new MarketSubscription(new StreamingMarketFilter(), new StreamingMarketDataFilter());

        // Act
        var result = await sut.CreateStream(sessionToken, marketSubscription);

        // Assert
        await _mockSslSocketHandler.Received().Start();
        await _mockSslSocketHandler.Received().SendLine(Arg.Is<AuthenticationMessage>(msg
            => msg.SessionToken == sessionToken && msg.ApiKey == "testApiKey"));
        await _mockSslSocketHandler.Received().SendLine(marketSubscription);
        result.Should().NotBeNull().And.BeOfType<StreamSubscriptionHandler>();
    }


    [Fact]
    public async Task CreateStream_ShouldReturnSubscriptionHandler_WhenSessionTokenAndOrderSubscriptionProvided() {
        // Arrange
        var sut = new StreamingService(_mockSslSocketHandler, "testApiKey");
        var sessionToken = "sessionToken";
        var orderSubscription = new OrderSubscription(new OrderFilter());

        // Act
        var result = await sut.CreateStream(sessionToken, orderSubscription);

        // Assert
        await _mockSslSocketHandler.Received().Start();
        await _mockSslSocketHandler.Received().SendLine(Arg.Is<AuthenticationMessage>(msg
            => msg.SessionToken == sessionToken && msg.ApiKey == "testApiKey"));
        await _mockSslSocketHandler.Received().SendLine(orderSubscription);
        result.Should().NotBeNull().And.BeOfType<StreamSubscriptionHandler>();

        // Teardown
        result.Dispose();
    }


    [Fact]
    public async Task CreateStream_ShouldReturnSubscriptionHandler_WhenSessionTokenAndBothSubscriptionsProvided() {
        // Arrange
        var sut = new StreamingService(_mockSslSocketHandler, "testApiKey");
        var sessionToken = "sessionToken";
        var marketSubscription = new MarketSubscription(new StreamingMarketFilter(), new StreamingMarketDataFilter());
        var orderSubscription = new OrderSubscription(new OrderFilter());

        // Act
        var result = await sut.CreateStream(sessionToken, marketSubscription, orderSubscription);

        // Assert
        await _mockSslSocketHandler.Received().Start();
        await _mockSslSocketHandler.Received().SendLine(Arg.Is<AuthenticationMessage>(msg
            => msg.SessionToken == sessionToken && msg.ApiKey == "testApiKey"));
        await _mockSslSocketHandler.Received().SendLine(marketSubscription);
        await _mockSslSocketHandler.Received().SendLine(orderSubscription);
        result.Should().NotBeNull().And.BeOfType<StreamSubscriptionHandler>();

        // Teardown
        result.Dispose();
    }


    [Fact]
    public async Task CreateStream_ShouldThrowArgumentException_WhenSessionTokenNotProvided() {
        // Arrange
        var _mockSslSocketHandler = Substitute.For<ISslSocketHandler>();
        var sut = new StreamingService(_mockSslSocketHandler, "testApiKey");
        var sessionToken = "";
        var marketSubscription = new MarketSubscription(new StreamingMarketFilter(), new StreamingMarketDataFilter());

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async ()
            => await sut.CreateStream(sessionToken, marketSubscription));
    }
}
