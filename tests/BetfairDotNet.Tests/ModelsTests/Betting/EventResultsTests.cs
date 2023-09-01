using BetfairDotNet.Models.Betting;
using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Betting;


public class EventResultsTests {

    [Fact]
    public void EventResult_ShouldDeserializeCorrectly() {
        // Arrange
        var eventResult = new EventResult {
            Event = new Event {
                Id = "event1",
                Name = "Football",
                CountryCode = "GB",
                Timezone = "GMT",
                Venue = "Wembley",
                OpenDate = DateTime.Now
            },
            MarketCount = 3
        };

        // Act
        var json = JsonSerializer.Serialize(eventResult);
        var deserializedEventResult = JsonSerializer.Deserialize<EventResult>(json);

        // Assert
        eventResult.Should().BeEquivalentTo(deserializedEventResult);
    }
}
