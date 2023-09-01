using BetfairDotNet.Converters;
using BetfairDotNet.Models.Exceptions;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Exceptions;

public class BetfairExceptionDetailsTests {

    [Fact]
    public void BetfairServerExceptionDetails_ShouldDeserializeCorrectly() {
        // Arrange
        var betfairServerExceptionDetails = new BetfairServerExceptionDetails {
            RequestUUID = "some-uuid",
            ErrorCode = "some-error-code",
            ErrorDetails = "some-error-details"
        };

        // Act
        var json = JsonConvert.Serialize(betfairServerExceptionDetails);
        var deserializedBetfairServerExceptionDetails = JsonConvert.Deserialize<BetfairServerExceptionDetails>(json);

        // Assert
        deserializedBetfairServerExceptionDetails.Should().BeEquivalentTo(betfairServerExceptionDetails);
    }
}
