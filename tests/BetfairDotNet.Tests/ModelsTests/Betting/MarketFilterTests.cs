using BetfairDotNet.Converters;
using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Models.Betting;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Betting;


public class MarketFilterTests {

    [Fact]
    public void MarketFilter_ShouldSerializeProperly() {
        // Arrange
        var marketFilter = new MarketFilter {
            TextQuery = "Football",
            EventTypeIds = new List<string> { "1", "2" },
            EventIds = new List<string> { "3", "4" },
            CompetitionIds = new List<string> { "5", "6" },
            MarketIds = new List<string> { "7", "8" },
            Venues = new List<string> { "Venue1", "Venue2" },
            BspOnly = true,
            TurnInPlayEnabled = false,
            InPlayOnly = true,
            MarketBettingTypes = new List<MarketBettingTypeEnum> { MarketBettingTypeEnum.ODDS },
            MarketCountries = new List<string> { "GB", "US" },
            MarketTypeCodes = new List<string> { "MATCH_ODDS" },
            MarketStartTime = null,
            WithOrders = new List<OrderStatusEnum> { OrderStatusEnum.EXECUTABLE },
            RaceTypes = new List<string> { "Flat", "Hurdle" }
        };

        var expectedJObject = new JObject
        {
            { "textQuery", "Football" },
            { "eventTypeIds", new JArray("1", "2") },
            { "eventIds", new JArray("3", "4") },
            { "competitionIds", new JArray("5", "6") },
            { "marketIds", new JArray("7", "8") },
            { "venues", new JArray("Venue1", "Venue2") },
            { "bspOnly", true },
            { "turnInPlayEnabled", false },
            { "inPlayOnly", true },
            { "marketBettingTypes", new JArray("ODDS") },
            { "marketCountries", new JArray("GB", "US") },
            { "marketTypeCodes", new JArray("MATCH_ODDS") },
            { "withOrders", new JArray("EXECUTABLE") },
            { "raceTypes", new JArray("Flat", "Hurdle") }
        };

        // Act
        var serializedJson = JsonConvert.Serialize(marketFilter);
        var serializedJObject = JObject.Parse(serializedJson);


        // Assert
        serializedJObject.Should().BeEquivalentTo(expectedJObject);
    }
}
