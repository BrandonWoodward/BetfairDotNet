using BetfairDotNet.Handlers;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Streaming;
using NSubstitute;
using Xunit;

namespace BetfairDotNet.Tests.HandlerTests;

public class StreamSubscriptionHandlerTests {

    private readonly StreamSubscriptionHandler _sut;
    private readonly ISslSocketHandler _socketHandler;
    private readonly IChangeMessageHandler _changeMessageHandler;
    private readonly ISubject _subject;

    public StreamSubscriptionHandlerTests() {
        _socketHandler = Substitute.For<ISslSocketHandler>();
        _changeMessageHandler = Substitute.For<IChangeMessageHandler>();
        _subject = Substitute.For<ISubject>();
        _sut = new StreamSubscriptionHandler(_socketHandler, _changeMessageHandler, _subject);
    }


    [Fact]
    public async Task Subscribe_ShouldCallSocketHandlerStart_WhenStreamConfigured() {
        // Arrange
        var streamConfiguration = new StreamConfiguration { SessionToken = "" };
        var marketSubscription = new MarketSubscription(new StreamingMarketFilter(), new StreamingMarketDataFilter());
        var orderSubscription = new OrderSubscription(new OrderFilter());
        _changeMessageHandler.GetClocks().Returns(Tuple.Create<string?, string?, string?, string?>(null, null, null, null));

        // Act
        await _sut.Subscribe(streamConfiguration, marketSubscription);

        // Assert
        await _socketHandler.Received().Start(Arg.Any<int>(), Arg.Any<int>());
    }


    [Fact]
    public void Unsubscribe_ShouldCallSocketHandlerStop() {
        // Act
        _sut.Unsubscribe();

        // Assert
        _socketHandler.Received().Stop();
        _subject.Received().Dispose();
    }
}
