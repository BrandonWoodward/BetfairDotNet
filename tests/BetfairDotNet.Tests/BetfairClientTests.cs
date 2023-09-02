using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests;

public class BetfairClientTests {

    [Fact]
    public void LoginService_ShouldBeInitialized() {
        // Arrange
        var apiKey = "someApiKey";
        var username = "someUsername";
        var password = "somePassword";

        // Act
        var client = new BetfairClient(apiKey, username, password);

        // Assert
        client.Login.Should().NotBeNull();
    }


    [Fact]
    public void AccountService_ShouldBeInitialized() {
        // Arrange
        var apiKey = "someApiKey";
        var username = "someUsername";
        var password = "somePassword";

        // Act
        var client = new BetfairClient(apiKey, username, password);

        // Assert
        client.Account.Should().NotBeNull();
    }


    [Fact]
    public void BettingService_ShouldBeInitialized() {
        // Arrange
        var apiKey = "someApiKey";
        var username = "someUsername";
        var password = "somePassword";

        // Act
        var client = new BetfairClient(apiKey, username, password);

        // Assert
        client.Betting.Should().NotBeNull();
    }


    [Fact]
    public void HeartbeatService_ShouldBeInitialized() {
        // Arrange
        var apiKey = "someApiKey";
        var username = "someUsername";
        var password = "somePassword";

        // Act
        var client = new BetfairClient(apiKey, username, password);

        // Assert
        client.Heartbeat.Should().NotBeNull();
    }


    [Fact]
    public void StreamingService_ShouldBeInitialized() {
        // Arrange
        var apiKey = "someApiKey";
        var username = "someUsername";
        var password = "somePassword";

        // Act
        var client = new BetfairClient(apiKey, username, password);

        // Assert
        client.Streaming.Should().NotBeNull();
    }
}
