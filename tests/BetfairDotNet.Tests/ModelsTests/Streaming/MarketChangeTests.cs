using BetfairDotNet.Converters;
using BetfairDotNet.Models.Streaming;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Streaming;
public class MarketChangeTests {


    [Fact]
    public void MarketChange_ShouldDeserializeCorrectly() {
        // Arrange
        var marketChange = new MarketChange() {
            Id = "1.234",
            MarketDefinition = new MarketDefinition(),
            RunnerChanges = new List<RunnerChange>(),
            IsImage = true,
            IsConflated = true,
            TotalVolume = 12345.67
        };

        // Act
        var json = JsonConvert.Serialize(marketChange);
        var result = JsonConvert.Deserialize<MarketChange>(json);

        // Assert
        marketChange.Should().BeEquivalentTo(result);
    }
}
