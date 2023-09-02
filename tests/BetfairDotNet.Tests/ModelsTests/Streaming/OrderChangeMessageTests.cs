using BetfairDotNet.Converters;
using BetfairDotNet.Enums.Streaming;
using BetfairDotNet.Models.Streaming;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Streaming;
public class OrderChangeMessageTests {

    [Fact]
    public void OrderChangeMessage_ShouldDeserializeCorrectly() {
        // Arrange
        var orderChangeMessage = new OrderChangeMessage() {
            ChangeType = ChangeTypeEnum.RESUB_DELTA,
            SegmentType = SegmentTypeEnum.SEG_START,
            InitialClk = "123",
            Clk = "1234",
            ConflateMs = 1000,
            HeartbeatMs = 1000,
            PublishTime = 111222333444555,
            OrderChanges = new(),
        };

        // Act
        var json = JsonConvert.Serialize(orderChangeMessage);
        var result = JsonConvert.Deserialize<OrderChangeMessage>(json);

        // Assert
        result.Should().BeEquivalentTo(orderChangeMessage);
    }
}
