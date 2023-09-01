using BetfairDotNet.Converters;
using BetfairDotNet.Models.Betting;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Betting;

public class MarketRatesTests {

    [Fact]
    public void TestMarketRatesSerialization() {
        // Arrange
        var marketRates = new MarketRates {
            MarketBaseRate = 5.0,
            DiscountAllowed = true
        };

        // Act
        var json = JsonConvert.Serialize(marketRates);
        var deserializedMarketRates = JsonConvert.Deserialize<MarketRates>(json);

        // Assert
        deserializedMarketRates.Should().BeEquivalentTo(marketRates);
    }
}
