using BetfairDotNet.Models.Betting;
using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Betting;

public class CompetitionResultTests {


    [Fact]
    public void CompetitionResult_ShouldDeserializeCorrectly() {
        // Arrange
        var json = @"{
                ""competition"": {
                    ""id"": ""comp1"",
                    ""name"": ""Premier League""
                },
                ""marketCount"": 50,
                ""competitionRegion"": ""UK""
            }";

        var expectedCompetitionResult = new CompetitionResult {
            Competition = new Competition {
                Id = "comp1",
                Name = "Premier League"
            },
            MarketCount = 50,
            CompetitionRegion = "UK"
        };

        // Act
        var deserializedCompetitionResult = JsonSerializer.Deserialize<CompetitionResult>(json);

        // Assert
        deserializedCompetitionResult.Should().BeEquivalentTo(expectedCompetitionResult);
    }
}
