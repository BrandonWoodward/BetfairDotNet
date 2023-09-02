using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Streaming;
using BetfairDotNet.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace BetfairDotNet.Tests.ServiceTests;


public class StreamingServiceTests {

    private readonly ISslSocketHandler _mockSslSocketHandler = Substitute.For<ISslSocketHandler>();
    private readonly IStreamSubscriptionHandler _mockStreamSubscriptionHandler = Substitute.For<IStreamSubscriptionHandler>();


    [Fact]
    public async Task CreateStream_ShouldReturnSubscriptionHandler_WhenSessionTokenAndMarketSubscriptionProvided() {
        // Arrange
        var apiKey = "someApiKey";
        var sessionToken = "someToken";
        var service = new StreamingService(_mockSslSocketHandler, _mockStreamSubscriptionHandler, apiKey);
        var marketSubscription = new MarketSubscription(new StreamingMarketFilter(), new StreamingMarketDataFilter());

        // Act
        var result = await service.CreateStream(sessionToken, marketSubscription);

        // Assert
        result.Should().Be(_mockStreamSubscriptionHandler);
        await _mockSslSocketHandler.Received(1).Start();
        await _mockSslSocketHandler.Received(1).SendLine(new AuthenticationMessage(sessionToken, apiKey));
        await _mockSslSocketHandler.Received(1).SendLine(marketSubscription);
    }


    [Fact]
    public async Task CreateStream_ShouldReturnSubscriptionHandler_WhenSessionTokenAndOrderSubscriptionProvided() {
        // Arrange
        var apiKey = "someApiKey";
        var sessionToken = "someToken";
        var service = new StreamingService(_mockSslSocketHandler, _mockStreamSubscriptionHandler, apiKey);
        var orderSubscription = new OrderSubscription(new OrderFilter());

        // Act
        var result = await service.CreateStream(sessionToken, orderSubscription);

        // Assert
        result.Should().Be(_mockStreamSubscriptionHandler);
        await _mockSslSocketHandler.Received(1).Start();
        await _mockSslSocketHandler.Received(1).SendLine(new AuthenticationMessage(sessionToken, apiKey));
        await _mockSslSocketHandler.Received(1).SendLine(orderSubscription);
    }


    [Fact]
    public async Task CreateStream_ShouldReturnSubscriptionHandler_WhenSessionTokenAndBothSubscriptionsProvided() {
        // Arrange
        var apiKey = "someApiKey";
        var sessionToken = "someToken";
        var service = new StreamingService(_mockSslSocketHandler, _mockStreamSubscriptionHandler, apiKey);
        var marketSubscription = new MarketSubscription(new StreamingMarketFilter(), new StreamingMarketDataFilter());
        var orderSubscription = new OrderSubscription(new OrderFilter());

        // Act
        var result = await service.CreateStream(sessionToken, marketSubscription, orderSubscription);

        // Assert
        result.Should().Be(_mockStreamSubscriptionHandler);
        await _mockSslSocketHandler.Received(1).Start();
        await _mockSslSocketHandler.Received(1).SendLine(new AuthenticationMessage(sessionToken, apiKey));
        await _mockSslSocketHandler.Received(1).SendLine(marketSubscription);
    }


    [Fact]
    public void CreateStream_ShouldThrowArgumentException_WhenSessionTokenNotProvided() {
        // Arrange
        var apiKey = "someApiKey";
        var sessionToken = "";
        var service = new StreamingService(_mockSslSocketHandler, _mockStreamSubscriptionHandler, apiKey);
        var marketSubscription = new MarketSubscription(new StreamingMarketFilter(), new StreamingMarketDataFilter());

        // Act & Assert
        Func<Task> act = async () => await service.CreateStream(sessionToken, marketSubscription);
        act.Should().ThrowAsync<ArgumentException>().WithMessage("Session token not provided.");
    }
}
