using BetfairDotNet.Converters;
using BetfairDotNet.Enums.Streaming;
using BetfairDotNet.Models.Streaming;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Streaming;
public class MarketChangeMessageTests {

    [Fact]
    public void MarketChangeMessage_ShouldDeserializeCorrectly() {
        // Arrange
        var marketChangeMessage = new MarketChangeMessage() {
            ChangeType = ChangeTypeEnum.RESUB_DELTA,
            SegmentType = SegmentTypeEnum.SEG_START,
            InitialClk = "123",
            Clk = "1234",
            ConflateMs = 1000,
            HeartbeatMs = 1000,
            PublishTime = 111222333444555,
            MarketChanges = new(),
        };

        // Act
        var json = JsonConvert.Serialize(marketChangeMessage);
        var result = JsonConvert.Deserialize<MarketChangeMessage>(json);

        // Assert
        result.Should().BeEquivalentTo(marketChangeMessage);
    }
}
