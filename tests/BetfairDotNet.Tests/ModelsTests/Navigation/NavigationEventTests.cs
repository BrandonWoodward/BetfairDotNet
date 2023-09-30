using BetfairDotNet.Converters;
using BetfairDotNet.Models.Navigation;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Navigation;

public class NavigationEventTests
{
    [Fact]
    public void NavigationEvent_DeserializesCorrectly()
    {
        // Arrange
        var originalObject = new NavigationEvent
        {
            Id = "5",
            Name = "SomeName",
            CountryCode = "GB",
            Children = new() { new NavigationGroup { Id = "1", Name = "Child1" } },
        };

        // Act
        var serializedString = JsonConvert.Serialize(originalObject);
        var deserializedObject = JsonConvert.Deserialize<NavigationEvent>(serializedString);

        // Assert
        deserializedObject.Should().BeEquivalentTo(originalObject);
    }
}