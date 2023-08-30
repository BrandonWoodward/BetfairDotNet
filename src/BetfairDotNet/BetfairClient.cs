using BetfairDotNet.Adapters;
using BetfairDotNet.Endpoints;
using BetfairDotNet.Handlers;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Services;

namespace BetfairDotNet;

/// <summary>
/// The top level client containing the services for the different Betfair APIs.
/// </summary>
public sealed class BetfairClient : IBetfairClient {

    /// <summary>
    /// Provides login functionality for interactive and non-interactive workflows.
    /// </summary>
    public LoginService Login { get; private set; }

    /// <summary>
    /// Provides functionality for the Betfair Account API
    /// such as getting account details, funds, statements, etc.
    /// </summary>
    public AccountService Account { get; private set; }

    /// <summary>
    /// Provides functionality for the Betfair Betting API.
    /// Operations include listing markets, placing bets, cancelling bets, etc.
    /// </summary>
    public BettingService Betting { get; private set; }

    /// <summary>
    /// Provides functionality for the Betfair Heartbeat API
    /// which can perform an action when connection to the Betfair API is lost.
    /// </summary>
    public HeartbeatService Heartbeat { get; private set; }

    /// <summary>
    /// Provides a fluent interface for subscribing to the Betfair Streaming API.
    /// </summary>
    public StreamingService Streaming { get; private set; }


    /// <summary>
    /// Create a new BetfairClient instance, which is the top level client for all operations.
    /// This should be instantiated once per application lifetime. If using a DI container,
    /// register this as a singleton.
    /// </summary>
    /// <param name="apiKey"></param>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="certPath"></param>
    public BetfairClient(string apiKey, string username, string password, string? certPath = null) {
        // These are abstracted away so that they can be mocked for unit testing.
        var httpClient = new HttpClientAdapter(apiKey, 5000);
        var tcpClient = new TcpClientAdapter();
        var sslStream = new SslStreamAdapter(tcpClient.GetStream());

        var requestHandler = new RequestResponseHandler(httpClient);
        var socketHandler = new SslSocketHandler(tcpClient, sslStream, StreamingEndpoints.Production);

        Login = new LoginService(requestHandler, username, password, certPath);
        Account = new AccountService(requestHandler);
        Betting = new BettingService(requestHandler);
        Heartbeat = new HeartbeatService(requestHandler);
        Streaming = new StreamingService(socketHandler, apiKey);
    }
}
