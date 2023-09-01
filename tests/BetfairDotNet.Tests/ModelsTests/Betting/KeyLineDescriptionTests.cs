using BetfairDotNet.Converters;
using BetfairDotNet.Models.Betting;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Betting;

public class KeyLineDescriptionTests {

    [Fact]
    public void TestKeyLineDescriptionSerialization() {
        // Arrange
        var keyLineDescription = new KeyLineDescription {
            KeyLine = new List<KeyLineSelection> {
                new KeyLineSelection { SelectionId = 12345, Handicap = 0.0 },
            }
        };

        // Act
        var json = JsonConvert.Serialize(keyLineDescription);
        var deserializedKeyLineDescription = JsonConvert.Deserialize<KeyLineDescription>(json);

        // Assert
        deserializedKeyLineDescription.Should().BeEquivalentTo(keyLineDescription);
    }
}
