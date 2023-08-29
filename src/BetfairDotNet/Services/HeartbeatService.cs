using BetfairDotNet.Endpoints;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models;
using BetfairDotNet.Models.Heartbeat;

namespace BetfairDotNet.Services;

/// <summary>
/// A service that provides functionalities for sending heartbeat messages.
/// </summary>
public sealed class HeartbeatService {


    private readonly IRequestResponseHandler _networkService;


    internal HeartbeatService(IRequestResponseHandler networkService) {
        _networkService = networkService;
    }


    /// <summary>
    /// Sends a heartbeat to manage positions automatically in case of lost connectivity to the Betfair API.
    /// </summary>
    /// <remarks>
    /// The heartbeat operation helps customers manage their positions if their API clients lose connectivity. 
    /// If a heartbeat is missed within a specific time frame, Betfair will try to cancel all 'LIMIT' type bets 
    /// for the given customer on the exchange. However, there's no guarantee all bets will be cancelled, 
    /// as certain circumstances might prevent cancellation.
    /// 
    /// Manual intervention is strongly advised if connectivity is lost to ensure positions are managed correctly. 
    /// If the heartbeat service becomes unavailable, the heartbeat will be unregistered automatically. In this case, 
    /// positions should be managed manually until the service resumes. Note: heartbeat data might be lost if node failures occur, 
    /// potentially leading to unmanaged positions until the next heartbeat.
    /// </remarks>
    /// <param name="preferredTimeoutSeconds">
    /// The maximum allowed period (in seconds) without a heartbeat before an automatic cancellation request is submitted. 
    /// Passing a value of 0 unregisters your heartbeat (or ignores if none is registered)
    /// Errors during registration result in an UNEXPECTED_ERROR. If the timeout is outside the min-max range, it will default to the closest boundary. 
    /// Timeouts might change, so clients should use the returned actualTimeoutSeconds for subsequent heartbeats.
    /// </param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="BetfairServerResponse{T}"/> 
    /// with the <see cref="HeartbeatReport"/>.</returns>
    public Task<HeartbeatReport> Heartbeat(int preferredTimeoutSeconds) {
        var args = new Dictionary<string, object?> {
            ["preferredTimeoutSeconds"] = preferredTimeoutSeconds
        };
        return _networkService.Request<HeartbeatReport>(
            HeartbeatEndpoints.BaseUrl,
            HeartbeatEndpoints.Heartbeat,
            args
        );
    }
}
