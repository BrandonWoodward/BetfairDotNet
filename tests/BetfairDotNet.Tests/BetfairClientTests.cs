using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests;

public class BetfairClientTests {

    [Fact]
    public void BetfairClient_CreatesServicesCorrectly() {
        // Arrange
        var apiKey = "YourApiKey";
        var username = "YourUsername";
        var password = "YourPassword";
        string? certPath = null;

        // Act
        var betfairClient = new BetfairClient(apiKey, username, password, certPath);

        // Assert
        betfairClient.Login.Should().NotBeNull("Login service should be initialized");
        betfairClient.Account.Should().NotBeNull("Account service should be initialized");
        betfairClient.Betting.Should().NotBeNull("Betting service should be initialized");
        betfairClient.Heartbeat.Should().NotBeNull("Heartbeat service should be initialized");
        betfairClient.Streaming.Should().NotBeNull("Streaming service should be initialized");
    }
}
