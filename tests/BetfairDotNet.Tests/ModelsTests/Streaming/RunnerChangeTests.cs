using BetfairDotNet.Converters;
using BetfairDotNet.Models.Streaming;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Streaming;

public class RunnerChangeTests {

    [Fact]
    public void RunnerChange_ShouldDeserializeCorrectly() {
        // Arrange
        var runnerChange = new RunnerChange {
            TotalVolume = 1.23,
            BestAvailableToBack = new List<List<double>> { new() { 1.11, 2.22 }, new() { 3.33, 4.44 } },
            StartingPriceBack = new List<List<double>> { new() { 5.55, 6.66 }, new() { 7.77, 8.88 } },
            BestAvailableToLayVirtual = new List<List<double>> { new() { 9.99, 10.10 }, new() { 11.11, 12.12 } },
            TradedVolume = new List<List<double>> { new() { 13.13, 14.14 }, new() { 15.15, 16.16 } },
            StartingPriceFar = 17.17,
            LastTradedPrice = 18.18,
            AvailableToBack = new List<List<double>> { new() { 19.19, 20.20 }, new() { 21.21, 22.22 } }
        };

        // Act
        var json = JsonConvert.Serialize(runnerChange);
        var result = JsonConvert.Deserialize<RunnerChange>(json);

        // Assert
        result.Should().BeEquivalentTo(runnerChange);
    }
}
