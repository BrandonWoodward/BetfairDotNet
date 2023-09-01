using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Models.Betting;
using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.BettingModelTests;

public class MarketDescriptionTests
{

    [Fact]
    public void TestMarketDescriptionSerialization()
    {
        // Arrange
        var marketDescription = new MarketDescription
        {
            IsPersistenceEnabled = true,
            IsBspMarket = false,
            MarketTime = DateTime.Now,
            SuspendTime = DateTime.Now.AddMinutes(5),
            SettleTime = DateTime.Now.AddMinutes(10),
            BettingType = MarketBettingTypeEnum.FIXED_ODDS,
            IsTurnInPlayEnabled = true,
            MarketType = "SomeMarketType",
            Regulator = "GIBRALTAR REGULATOR",
            MarketBaseRate = 5.0,
            IsDiscountAllowed = true,
            Wallet = "Main",
            Rules = "Some rules",
            RulesHasDate = true,
            EachWayDivisor = 4.0,
            Clarifications = "Some clarifications",
            LineRangeInfo = null, // Replace with actual object if needed
            RaceType = "Horse",
            PriceLadderDescription = new PriceLadderDescription
            {
                Type = PriceLadderTypeEnum.FINEST
            }
        };

        // Act
        var json = JsonSerializer.Serialize(marketDescription);
        var deserializedMarketDescription = JsonSerializer.Deserialize<MarketDescription>(json);

        // Assert
        marketDescription.Should().BeEquivalentTo(deserializedMarketDescription);
    }
}
