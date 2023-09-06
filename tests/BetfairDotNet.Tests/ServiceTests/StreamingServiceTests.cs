using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Streaming;
using BetfairDotNet.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace BetfairDotNet.Tests.ServiceTests;


public class StreamingServiceTests {


    private readonly IStreamSubscriptionHandler _streamSubscriptionHandler = Substitute.For<IStreamSubscriptionHandler>();
    private readonly string _apiKey = "TestApiKey";
    private readonly StreamConfiguration _streamConfiguration = new() { SessionToken = "" };
    private readonly MarketSubscription _marketSubscription = new(new StreamingMarketFilter(), new StreamingMarketDataFilter());
    private readonly OrderSubscription _orderSubscription = new(new OrderFilter());


    [Fact]
    public void CreateStream_ShouldSetStreamConfiguration() {
        // Arrange
        var service = new StreamingService(_streamSubscriptionHandler, _apiKey);

        // Act
        service.CreateStream(_streamConfiguration);

        // Assert
        // Here, verify that _streamConfiguration.ApiKey was set to _apiKey
        _streamConfiguration.ApiKey.Should().Be(_apiKey);
    }


    [Fact]
    public void CreateStream_ShouldThrowArgumentNullException_WhenStreamConfigurationIsNull() {
        // Arrange
        var service = new StreamingService(_streamSubscriptionHandler, _apiKey);

        // Act
        Action act = () => service.CreateStream(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }


    [Fact]
    public void WithMarketSubscription_ShouldThrowArgumentNullException_WhenMarketSubscriptionIsNull() {
        // Arrange
        var service = new StreamingService(_streamSubscriptionHandler, _apiKey);

        // Act
        Action act = () => service.WithMarketSubscription(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }


    [Fact]
    public void WithOrderSubscription_ShouldThrowArgumentNullException_WhenOrderSubscriptionIsNull() {
        // Arrange
        var service = new StreamingService(_streamSubscriptionHandler, _apiKey);

        // Act
        Action act = () => service.WithOrderSubscription(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }


    [Fact]
    public async Task Subscribe_ShouldThrowInvalidOperationException_WhenNoStreamConfigurationSet() {
        // Arrange
        var service = new StreamingService(_streamSubscriptionHandler, _apiKey);

        // Act
        Func<Task> act = async () => await service.Subscribe();

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("No stream configuration set.");
    }


    [Fact]
    public async Task Subscribe_ShouldThrowInvalidOperationException_WhenNoSubscriptionCriteria() {
        // Arrange
        var service = new StreamingService(_streamSubscriptionHandler, _apiKey).CreateStream(_streamConfiguration);

        // Act
        Func<Task> act = async () => await service.Subscribe();

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("No subscription criteria provided.");
    }


    [Fact]
    public async Task Subscribe_ShouldCallStreamSubscriptionHandlerSubscribe_WhenMarketSubscriptionSet() {
        // Arrange
        var service = new StreamingService(_streamSubscriptionHandler, _apiKey)
            .CreateStream(_streamConfiguration)
            .WithMarketSubscription(_marketSubscription);

        // Act
        await service.Subscribe();

        // Assert
        await _streamSubscriptionHandler.Received().Subscribe(
            _streamConfiguration,
            _marketSubscription,
            null,
            null,
            null,
            null
        );
    }


    [Fact]
    public async Task Subscribe_ShouldCallStreamSubscriptionHandlerSubscribe_WhenOrderSubscriptionSet() {
        // Arrange
        var service = new StreamingService(_streamSubscriptionHandler, _apiKey)
            .CreateStream(_streamConfiguration)
            .WithOrderSubscription(_orderSubscription);

        // Act
        await service.Subscribe();

        // Assert
        await _streamSubscriptionHandler.Received().Subscribe(
            _streamConfiguration,
            null,
            _orderSubscription,
            null,
            null,
            null
        );
    }
}
