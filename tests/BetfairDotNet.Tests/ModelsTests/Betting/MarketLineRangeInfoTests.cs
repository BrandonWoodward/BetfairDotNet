using BetfairDotNet.Converters;
using BetfairDotNet.Models.Betting;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Betting;

public class MarketLineRangeInfoTests {

    [Fact]
    public void MarketLineRangInfo_ShouldDeserializeCorrectly() {
        // Arrange
        var marketLineRangeInfo = new MarketLineRangeInfo {
            MaxUnitValue = 100.0,
            MinUnitValue = 0.0,
            Interval = 0.5,
            MarketUnit = "runs"
        };

        // Act
        var json = JsonConvert.Serialize(marketLineRangeInfo);
        var deserializedMarketLineRangeInfo = JsonConvert.Deserialize<MarketLineRangeInfo>(json);

        // Assert
        deserializedMarketLineRangeInfo.Should().BeEquivalentTo(marketLineRangeInfo);
    }
}
