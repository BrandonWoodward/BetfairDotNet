using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Models.Betting;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.BettingModelTests;

public class PriceProjectionTests
{

    [Fact]
    public void PriceProjection_ShouldSerializeCorrectly()
    {
        // Arrange
        var priceProjection = new PriceProjection
        {
            PriceData = new List<PriceDataEnum> {
                PriceDataEnum.EX_BEST_OFFERS,
                PriceDataEnum.EX_TRADED
            },
            ExBestOffersOverrides = new ExBestOffersOverrides
            {
                BestPricesDepth = 1,
                RollUpModel = RollupModelEnum.PAYOUT,
                RollUpLimit = 2,
                RollUpLiabilityThreshold = 3,
                RollUpLiabilityFactor = 4
            },
            Virtualise = true,
            RolloverStakes = true
        };

        var expectedJson = @"
        {
            ""priceData"":[""EX_BEST_OFFERS"",""EX_TRADED""],
            ""exBestOffersOverrides"":
            {
                ""bestPricesDepth"":1,
                ""rollupModel"":""PAYOUT"",
                ""rollupLimit"":2,
                ""rollupLiabilityThreshold"":3,
                ""rollupLiabilityFactor"":4
            },   
            ""virtualise"":true,
            ""rolloverStakes"":true
        }";

        // Act
        var serializedPriceProjection = JsonSerializer.Serialize(priceProjection);
        var expectedJObject = JObject.Parse(expectedJson);
        var actualJObject = JObject.Parse(serializedPriceProjection);

        // Assert
        actualJObject.Should().BeEquivalentTo(expectedJObject);
    }
}
