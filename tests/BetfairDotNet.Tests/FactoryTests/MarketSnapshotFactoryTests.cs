using BetfairDotNet.Enums.Streaming;
using BetfairDotNet.Factories;
using BetfairDotNet.Models.Streaming;
using FluentAssertions;
using System.Collections.Concurrent;
using Xunit;

namespace BetfairDotNet.Tests.FactoryTests;


public class MarketSnapshotFactoryTests {


    [Fact]
    public void ProcessImage_ShouldReturnEmpty_WhenMessageIsHeartbeat() {
        // Arrange
        var changeMessage = new MarketChangeMessage() { Id = 123, ChangeType = ChangeTypeEnum.HEARTBEAT };
        var cache = new ConcurrentDictionary<string, MarketSnapshot>();

        // Act
        var sut = new MarketSnapshotFactory(cache);
        var actual = sut.GetSnapshots(changeMessage);

        // Assert
        actual.Should().BeEmpty();
    }
}
