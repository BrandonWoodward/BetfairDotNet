using BetfairDotNet.Converters;
using BetfairDotNet.Models.Navigation;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Navigation;

public class NavigationMarketTests
{
    [Fact]
    public void NavigationMarket_DeserializesCorrectly()
    {
        // Arrange
        var originalObject = new NavigationMarket
        {
            Id = "5",
            Name = "SomeName",
            ExchangeId = "123",
            MarketType = "WIN",
            MarketStartTime = new DateTime(2023, 1, 1),
            Children = new() { new NavigationGroup { Id = "1", Name = "Child1" } },
        };

        // Act
        var serializedString = JsonConvert.Serialize(originalObject);
        var deserializedObject = JsonConvert.Deserialize<NavigationMarket>(serializedString);

        // Assert
        deserializedObject.Should().BeEquivalentTo(originalObject);
    }
}