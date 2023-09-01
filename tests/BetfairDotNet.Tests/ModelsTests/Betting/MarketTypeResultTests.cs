using BetfairDotNet.Converters;
using BetfairDotNet.Models.Betting;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Betting;
public class MarketTypeResultTests {

    [Fact]
    public void TestMarketTypeResultSerialization() {
        // Arrange
        var marketTypeResult = new MarketTypeResult {
            MarketType = "some-market-type",
            MarketCount = 42
        };

        // Act
        var json = JsonConvert.Serialize(marketTypeResult);
        var deserializedMarketTypeResult = JsonConvert.Deserialize<MarketTypeResult>(json);

        // Assert
        deserializedMarketTypeResult.Should().Be(marketTypeResult);
    }
}
