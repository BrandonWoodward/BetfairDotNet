using BetfairDotNet.Converters;
using BetfairDotNet.Models.Betting;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Betting;
public class TimeRangeResultTests {

    [Fact]
    public void TimeRangeResult_ShouldDeserializeCorrectly() {
        // Arrange
        var timeRangeResult = new TimeRangeResult() {
            MarketCount = 3,
            TimeRange = new() { From = DateTime.Now, To = DateTime.Now.AddDays(1) }
        };

        // Act
        var json = JsonConvert.Serialize(timeRangeResult);
        var deserialized = JsonConvert.Deserialize<TimeRangeResult>(json);

        // Assert
        deserialized.Should().BeEquivalentTo(timeRangeResult);
    }
}
