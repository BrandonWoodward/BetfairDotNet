using BetfairDotNet.Converters;
using BetfairDotNet.Enums.Streaming;
using BetfairDotNet.Models.Streaming;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Streaming;

public class StreamingMarketDataFilterTests {

    [Fact]
    public void StreamingMarketDataFilter_ShouldSerializeAndDeserializeCorrectly() {
        // Arrange
        var streamingMarketDataFilter = new StreamingMarketDataFilter {
            LadderLevels = 3,
            Fields = new List<MarketDataFilterEnum> { MarketDataFilterEnum.SP_TRADED, MarketDataFilterEnum.EX_BEST_OFFERS_DISP }
        };

        // Act
        var json = JsonConvert.Serialize(streamingMarketDataFilter);
        var deserializedStreamingMarketDataFilter = JsonConvert.Deserialize<StreamingMarketDataFilter>(json);

        // Assert
        deserializedStreamingMarketDataFilter.Should().BeEquivalentTo(streamingMarketDataFilter);
    }
}
