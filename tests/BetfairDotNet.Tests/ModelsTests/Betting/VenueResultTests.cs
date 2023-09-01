using BetfairDotNet.Converters;
using BetfairDotNet.Models.Betting;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Betting;
public class VenueResultTests {

    [Fact]
    public void VenueResult_ShouldDeserializeCorrectly() {
        // Arrange
        var venueResult = new VenueResult() {
            MarketCount = 3,
            Venue = "Venue"
        };

        // Act
        var json = JsonConvert.Serialize(venueResult);
        var deserialized = JsonConvert.Deserialize<VenueResult>(json);

        // Assert
        deserialized.Should().BeEquivalentTo(venueResult);
    }
}
