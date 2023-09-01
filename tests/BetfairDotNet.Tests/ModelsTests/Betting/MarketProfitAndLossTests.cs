using BetfairDotNet.Converters;
using BetfairDotNet.Models.Betting;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Betting;
public class MarketProfitAndLossTests {

    [Fact]
    public void TestMarketProfitAndLossSerialization() {
        // Arrange
        var marketProfitAndLoss = new MarketProfitAndLoss {
            MarketId = "some-market-id",
            CommissionApplied = 0.05,
            ProfitAndLosses = new List<RunnerProfitAndLoss> {
                new RunnerProfitAndLoss()
            }
        };

        // Act
        var json = JsonConvert.Serialize(marketProfitAndLoss);
        var deserializedMarketProfitAndLoss = JsonConvert.Deserialize<MarketProfitAndLoss>(json);

        // Assert
        deserializedMarketProfitAndLoss.Should().BeEquivalentTo(marketProfitAndLoss);
    }
}
