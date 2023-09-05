using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Streaming;
using BetfairDotNet.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace BetfairDotNet.Tests.ServiceTests;


public class StreamingServiceTests {


    private readonly IStreamSubscriptionHandler _mockHandler = Substitute.For<IStreamSubscriptionHandler>();


    [Fact]
    public void CreateStream_ThrowsException_WhenSessionTokenIsNullOrWhiteSpace() {
        // Arrange
        var service = new StreamingService(_mockHandler, "apiKey");

        // Act
        Action act = () => service.CreateStream("  "); // Pass a whitespace string to trigger the error condition.

        // Assert
        act.Should().Throw<ArgumentException>().WithMessage("Session token not provided.");
    }


    [Fact]
    public async Task Subscribe_ThrowsException_WhenNoSessionTokenProvided() {
        // Arrange
        var service = new StreamingService(_mockHandler, "apiKey");

        // Act
        Func<Task> act = async () => await service.Subscribe(); // Do not call CreateStream before Subscribe.

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Session token not provided.");
    }


    [Fact]
    public async Task Subscribe_ThrowsException_WhenNoSubscriptionCriteriaProvided() {
        // Arrange
        var service = new StreamingService(_mockHandler, "apiKey").CreateStream("token");

        // Act
        Func<Task> act = async () => await service.Subscribe();

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("No subscription criteria provided.");
    }


    [Fact]
    public async Task Subscribe_ShouldCallStreamSubscriptionHandler_WithCorrectParameters() {
        // Arrange
        var marketSubscription = new MarketSubscription(new StreamingMarketFilter(), new StreamingMarketDataFilter());
        var orderSubscription = new OrderSubscription(new OrderFilter());

        var service = new StreamingService(_mockHandler, "apiKey")
            .CreateStream("token")
            .WithMarketSubscription(marketSubscription)
            .WithOrderSubscription(orderSubscription);

        // Act
        await service.Subscribe();

        // Assert
        await _mockHandler.Received().Subscribe(
            Arg.Is<AuthenticationMessage>(x => x.SessionToken == "token" && x.ApiKey == "apiKey"),
            marketSubscription,
            orderSubscription,
            null,
            null,
            null);
    }


    [Fact]
    public async Task Resubscribe_ThrowsException_WhenNoSubscriptionsSet() {
        // Arrange
        var service = new StreamingService(_mockHandler, "apiKey").CreateStream("token");

        // Act
        Func<Task> act = service.Resubscribe;

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("No subscriptions set.");
    }
}
