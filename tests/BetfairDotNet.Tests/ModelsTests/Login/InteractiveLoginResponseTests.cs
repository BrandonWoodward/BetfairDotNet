using BetfairDotNet.Converters;
using BetfairDotNet.Enums.Account;
using BetfairDotNet.Models.Login;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Login;

public class InteractiveLoginResponseTests {

    [Fact]
    public void InteractiveLoginResponse_ShouldDeserializeCorrectly() {
        // Arrange
        var interactiveLoginResponse = new InteractiveLoginResponse() {
            SessionToken = "sessionToken",
            Status = LoginStatusEnum.ACTIONS_REQUIRED,
        };

        // Act
        var json = JsonConvert.Serialize(interactiveLoginResponse);
        var deserialized = JsonConvert.Deserialize<InteractiveLoginResponse>(json);

        // Assert
        deserialized.Should().BeEquivalentTo(interactiveLoginResponse);
    }
}
