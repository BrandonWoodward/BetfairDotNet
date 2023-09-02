using BetfairDotNet.Converters;
using BetfairDotNet.Models.Streaming;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Streaming;

public class OrderFilterTests {

    [Fact]
    public void OrderFilter_ShouldSerializeCorrectly() {
        // Arrange
        var orderFilter = new OrderFilter {
            IncludeOverallPosition = true,
            CustomerStrategyRefs = new List<string> { "strategy1", "strategy2" },
            PartitionMatchedByStrategyRef = false
        };

        // Act
        var json = JsonConvert.Serialize(orderFilter);
        var result = JsonConvert.Deserialize<OrderFilter>(json);

        // Assert
        result.Should().BeEquivalentTo(orderFilter);
    }
}
