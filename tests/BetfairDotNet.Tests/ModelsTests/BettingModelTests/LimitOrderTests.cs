using BetfairDotNet.Converters;
using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Models.Betting;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.BettingModelTests;

public class LimitOrderTests {

    [Fact]
    public void LimitOrder_ShouldSerializeCorrectly() {
        // Arrange
        var limitOrder = new LimitOrder {
            Size = 100.0,
            Price = 1.5,
            PersistenceType = PersistenceTypeEnum.LAPSE,
            TimeInForce = null,
            MinFillSize = 50.0,
            BetTargetType = BetTargetTypeEnum.BACKERS_PROFIT,
            BetTargetSize = 100.0
        };

        var expectedJObject = new JObject
        {
            { "size", 100.0 },
            { "price", 1.5 },
            { "persistenceType", "LAPSE" },
            { "minFillSize", 50.0 },
            { "betTargetType", "BACKERS_PROFIT" },
            { "betTargetSize", 100.0 }
        };

        // Act
        var serializedJson = JsonConvert.Serialize(limitOrder);
        var serializedJObject = JObject.Parse(serializedJson);

        // Assert
        serializedJObject.Should().BeEquivalentTo(expectedJObject);
    }
}
