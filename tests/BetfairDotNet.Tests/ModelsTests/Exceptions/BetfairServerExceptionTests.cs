using BetfairDotNet.Converters;
using BetfairDotNet.Models.Exceptions;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Exceptions;

public class BetfairServerExceptionTests {


    [Fact]
    public void BetfairServerException_ShouldDeserializeCorrectly() {
        // Arrange
        var betfairServerException = new BetfairServerException() {
            JsonRPCErrorCode = "12345",
            JsonRPCErrorMessage = "JsonRPCErrorMessage",
            BetfairError = new BetfairServerExceptionData()
        };

        // Act
        var json = JsonConvert.Serialize(betfairServerException);
        var deserialized = JsonConvert.Deserialize<BetfairServerException>(json);

        // Assert
        deserialized.Should().BeEquivalentTo(betfairServerException);
    }
}
