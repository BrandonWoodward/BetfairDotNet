using BetfairDotNet.Converters;
using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Models.Streaming;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Streaming;
public class StreamingMarketFilterTests {

    [Fact]
    public void StreamingMarketFilter_ShouldSerializeCorrectly() {
        // Arrange
        var streamingMarketFilter = new StreamingMarketFilter {
            CountryCodes = new List<string> { "US", "UK" },
            BettingTypes = new List<MarketBettingTypeEnum> { MarketBettingTypeEnum.ODDS, MarketBettingTypeEnum.LINE },
            TurnInPlayEnabled = true,
            MarketTypes = new List<string> { "MATCH_ODDS", "OVER_UNDER" },
            Venues = new List<string> { "Venue1", "Venue2" },
            MarketIds = new List<string> { "MKT12345", "MKT67890" },
            EventTypeIds = new List<string> { "ET123", "ET456" },
            EventIds = new List<string> { "EV123", "EV456" },
            BspMarket = true
        };

        // Act
        var json = JsonConvert.Serialize(streamingMarketFilter);
        var deserializedStreamingMarketFilter = JsonConvert.Deserialize<StreamingMarketFilter>(json);

        // Assert
        deserializedStreamingMarketFilter.Should().BeEquivalentTo(streamingMarketFilter);
    }
}
