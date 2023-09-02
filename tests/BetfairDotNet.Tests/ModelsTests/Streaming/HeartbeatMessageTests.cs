using BetfairDotNet.Converters;
using BetfairDotNet.Models.Streaming;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Streaming;
public class HeartbeatMessageTests {

    [Fact]
    public void HeartbeatMessage_ShouldDeserializeCorrectly() {

        // Arrange
        var heartbeatMessage = new HeartbeatMessage { Id = 123 };

        // Act
        var json = JsonConvert.Serialize(heartbeatMessage);
        var result = JsonConvert.Deserialize<HeartbeatMessage>(json);

        // Assert
        heartbeatMessage.Should().BeEquivalentTo(result);
    }
}
