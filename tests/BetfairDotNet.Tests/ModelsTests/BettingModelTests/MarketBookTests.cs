using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Models.Betting;
using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.BettingModelTests;

public class MarketBookTests
{

    [Fact]
    public void MarketBook_ShouldDeserializeCorrectly()
    {
        // Arrange
        var json = @"{
                ""marketId"": ""1.12345"",
                ""isMarketDataDelayed"": true,
                ""status"": ""OPEN"",
                ""betDelay"": 10,
                ""bspReconciled"": true,
                ""complete"": true,
                ""inplay"": false,
                ""numberOfWinners"": 1,
                ""numberOfRunners"": 5,
                ""numberOfActiveRunners"": 4,
                ""lastMatchTime"": ""2023-09-01T12:00:00"",
                ""totalMatched"": 1000.5,
                ""totalAvailable"": 200.5,
                ""crossMatching"": false,
                ""runnersVoidable"": true,
                ""version"": 2,
                ""runners"": []
            }";

        var expectedMarketBook = new MarketBook
        {
            MarketId = "1.12345",
            IsMarketDataDelayed = true,
            Status = MarketStatusEnum.OPEN,
            BetDelay = 10,
            IsBspReconciled = true,
            IsComplete = true,
            IsInplay = false,
            NumberOfWinners = 1,
            NumberOfRunners = 5,
            NumberOfActiveRunners = 4,
            LastMatchTime = DateTime.Parse("2023-09-01T12:00:00"),
            TotalMatched = 1000.5,
            TotalAvailable = 200.5,
            IsCrossMatching = false,
            IsRunnersVoidable = true,
            Version = 2,
            Runners = new List<RunnerBook>()
        };

        // Act
        var deserializedMarketBook = JsonSerializer.Deserialize<MarketBook>(json);

        // Assert
        deserializedMarketBook.Should().BeEquivalentTo(expectedMarketBook);
    }
}
