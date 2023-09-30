using BetfairDotNet.Converters;
using BetfairDotNet.Models.Navigation;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Navigation;

public class NavigationRaceTests
{
    [Fact]
    public void NavigationRace_DeserializesCorrectly()
    {
        // Arrange
        var originalObject = new NavigationRace
        {
            Id = "5",
            Name = "SomeName",
            StartTime = new DateTime(2023, 1, 1),
            Venue = "SomeVenue",
            RaceNumber = "2",
            Children = new() { new NavigationGroup { Id = "1", Name = "Child1" } },
        };

        // Act
        var serializedString = JsonConvert.Serialize(originalObject);
        var deserializedObject = JsonConvert.Deserialize<NavigationRace>(serializedString);

        // Assert
        deserializedObject.Should().BeEquivalentTo(originalObject);
    }
}