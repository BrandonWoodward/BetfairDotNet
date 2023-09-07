using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Streaming;
using BetfairDotNet.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace BetfairDotNet.Tests.ServiceTests;


public class StreamingServiceTests {


    private readonly IStreamSubscriptionHandler _streamSubscriptionHandler;
    private readonly StreamConfiguration _streamConfiguration;
    private readonly MarketSubscription _marketSubscription;
    private readonly OrderSubscription _orderSubscription;
    private readonly string _apiKey;
    private readonly StreamingService _sut;


    public StreamingServiceTests() {
        _streamSubscriptionHandler = Substitute.For<IStreamSubscriptionHandler>();
        _streamConfiguration = new() { SessionToken = "" };
        _marketSubscription = new(new StreamingMarketFilter(), new StreamingMarketDataFilter());
        _orderSubscription = new(new OrderFilter());
        _apiKey = "TestApiKey";
        _sut = new(_streamSubscriptionHandler, _apiKey);
    }


    [Fact]
    public void CreateStream_ShouldSetStreamConfiguration() {
        // Act
        _sut.CreateStream(_streamConfiguration);

        // Assert
        // Here, verify that _streamConfiguration.ApiKey was set to _apiKey
        _streamConfiguration.ApiKey.Should().Be(_apiKey);
    }


    [Fact]
    public void CreateStream_ShouldThrowArgumentNullException_WhenStreamConfigurationIsNull() {
        // Act
        var act = () => _sut.CreateStream(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }


    [Fact]
    public void WithMarketSubscription_ShouldThrowArgumentNullException_WhenMarketSubscriptionIsNull() {
        // Act
        var act = () => _sut.WithMarketSubscription(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }


    [Fact]
    public void WithOrderSubscription_ShouldThrowArgumentNullException_WhenOrderSubscriptionIsNull() {
        // Act
        var act = () => _sut.WithOrderSubscription(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }


    [Fact]
    public async Task Subscribe_ShouldThrowInvalidOperationException_WhenNoStreamConfigurationSet() {
        // Act
        var act = async () => await _sut.Subscribe();

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("No stream configuration set.");
    }


    [Fact]
    public async Task Subscribe_ShouldThrowInvalidOperationException_WhenNoSubscriptionCriteria() {
        // Arrange
        _sut.CreateStream(_streamConfiguration);

        // Act
        var act = async () => await _sut.Subscribe();

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>().WithMessage("No subscription criteria provided.");
    }


    [Fact]
    public async Task Subscribe_ShouldCallStreamSubscriptionHandlerSubscribe_WhenMarketSubscriptionSet() {
        // Arrange
        _sut.CreateStream(_streamConfiguration).WithMarketSubscription(_marketSubscription);

        // Act
        await _sut.Subscribe();

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
        _sut.CreateStream(_streamConfiguration).WithOrderSubscription(_orderSubscription);

        // Act
        await _sut.Subscribe();

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
