using BetfairDotNet.Models.Betting;
using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.BettingModelTests;

public class RunnerProfitAndLossTests {

    [Fact]
    public void RunnerProfitAndLoss_ShouldDeserializeCorrectly() {
        // Arrange
        var runnerProfitAndLoss = new RunnerProfitAndLoss {
            SelectionId = 12345,
            IfWin = 50.0,
            IfLose = -10.0,
            IfPlace = 25.0
        };

        // Act
        var json = JsonSerializer.Serialize(runnerProfitAndLoss);
        var deserializedRunnerProfitAndLoss = JsonSerializer.Deserialize<RunnerProfitAndLoss>(json);

        // Assert
        deserializedRunnerProfitAndLoss.Should().BeEquivalentTo(runnerProfitAndLoss);
    }
}
