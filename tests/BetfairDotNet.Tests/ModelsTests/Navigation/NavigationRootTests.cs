using BetfairDotNet.Converters;
using BetfairDotNet.Models.Navigation;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Navigation;

public class NavigationRootTests
{
    [Fact]
    public void NavigationRoot_DeserializesCorrectly()
    {
        // Arrange
        var originalObject = new NavigationRoot
        {
            Id = 5,
            Name = "SomeName",
            EventTypes = new()
            {
                new() { Id = "et1", Name = "EventType1" },
                new() { Id = "et2", Name = "EventType2" }
            }
        };

        // Act
        var serializedString = JsonConvert.Serialize(originalObject);
        var deserializedObject = JsonConvert.Deserialize<NavigationRoot>(serializedString);

        // Assert
        deserializedObject.Should().BeEquivalentTo(originalObject);
    }
}