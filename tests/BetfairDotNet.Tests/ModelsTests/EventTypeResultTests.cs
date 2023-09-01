using BetfairDotNet.Models.Betting;
using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests;
public class EventTypeResultTests {

    [Fact]
    public void EventTypeResult_ShouldDeserializeCorrectly() {
        // Arrange
        var json = @"
        {
            ""eventType"": {
                ""id"": ""1"",
                ""name"": ""Football""
            },
            ""marketCount"": 50
        }";

        var expectedEventTypeResult = new EventTypeResult {
            EventType = new EventType {
                Id = "1",
                Name = "Football"
            },
            MarketCount = 50
        };

        // Act
        var deserializedEventTypeResult = JsonSerializer.Deserialize<EventTypeResult>(json);

        // Assert
        deserializedEventTypeResult.Should().BeEquivalentTo(expectedEventTypeResult);
    }
}
