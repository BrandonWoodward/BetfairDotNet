using BetfairDotNet.Converters;
using BetfairDotNet.Models.Betting;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Betting;

public class RunnerIdTests {

    [Fact]
    public void RunnerId_ShouldDeserializeCorrectly() {
        // Arrange
        var runnerId = new RunnerId {
            MarketId = "some-market-id",
            SelectionId = 123456,
            Handicap = 0.0
        };

        // Act
        var json = JsonConvert.Serialize(runnerId);
        var deserializedRunnerId = JsonConvert.Deserialize<RunnerId>(json);

        // Assert
        deserializedRunnerId.Should().BeEquivalentTo(runnerId);
    }
}
