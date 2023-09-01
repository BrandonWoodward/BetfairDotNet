using BetfairDotNet.Converters;
using BetfairDotNet.Models.Heartbeat;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Heartbeat;

public class HeartbeatReportTests {

    [Fact]
    public void HeartbeatReport_ShouldDeserializeCorrectly() {
        // Arrange
        var heartbeatReport = new HeartbeatReport() {
            ActionPerformed = "action",
            ActualTimeoutSeconds = 1
        };

        // Act
        var json = JsonConvert.Serialize(heartbeatReport);
        var deserialized = JsonConvert.Deserialize<HeartbeatReport>(json);

        // Assert
        deserialized.Should().BeEquivalentTo(heartbeatReport);
    }
}
