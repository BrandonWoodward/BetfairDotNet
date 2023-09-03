using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Heartbeat;
using BetfairDotNet.Services;
using NSubstitute;
using Xunit;

namespace BetfairDotNet.Tests.ServiceTests;

/// <summary>
/// Unit tests for the <see cref="HeartbeatService"/>.
/// </summary>
public class HeartbeatServiceTests {

    private readonly IRequestResponseHandler _mockNetwork = Substitute.For<IRequestResponseHandler>();
    private readonly HeartbeatService _heartbeatService;

    public HeartbeatServiceTests() {
        _heartbeatService = new HeartbeatService(_mockNetwork);
    }


    [Fact]
    public async Task Heartbeat_SendsCorrectRequest() {

        // Arrange
        var preferredTimeoutSeconds = 10;

        // Act 
        await _heartbeatService.Heartbeat(preferredTimeoutSeconds);

        // Assert
        await _mockNetwork.Received().Request<HeartbeatReport>(
            "https://api.betfair.com/exchange/heartbeat/json-rpc/v1",
            "/HeartbeatAPING/v1.0/heartbeat",
            Arg.Is<Dictionary<string, object?>>(args =>
                (int?)args["preferredTimeoutSeconds"] == preferredTimeoutSeconds
            )
        );
    }


    [Fact]
    public async void KeepAlive_SendCorrectRequest() {
        // Arrange
        // Act
        await _heartbeatService.KeepAive();

        // Assert
        await _mockNetwork.Received().Request<KeepAliveResponse>(
            "https://identitysso.betfair.com/api/keepAlive"
        );
    }
}
