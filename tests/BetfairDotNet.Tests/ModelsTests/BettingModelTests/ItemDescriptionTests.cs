using BetfairDotNet.Models.Betting;
using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.BettingModelTests;

public class ItemDescriptionTests {

    [Fact]
    public void ItemDescription_ShouldSerializeCorrectly() {
        // Arrange
        var itemDescription = new ItemDescription {
            EventTypeDesc = "EventType Description",
            EventDesc = "Event Description",
            MarketDesc = "Market Description",
            MarketType = "Market Type",
            MarketStartTime = DateTime.UtcNow,
            RunnerDesc = "Runner Description",
            NumberOfWinners = 2,
            EachWayDivisor = 4.0
        };

        // Act
        var json = JsonSerializer.Serialize(itemDescription);
        var deserializedItemDescription = JsonSerializer.Deserialize<ItemDescription>(json);

        // Assert
        itemDescription.Should().BeEquivalentTo(deserializedItemDescription);
    }
}
