using BetfairDotNet.Adapters;
using BetfairDotNet.Endpoints;
using BetfairDotNet.Factories;
using BetfairDotNet.Handlers;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Streaming;
using BetfairDotNet.Services;
using System.Collections.Concurrent;

namespace BetfairDotNet;

/// <summary>
/// The top level client containing the services for the different Betfair APIs.
/// </summary>
public sealed class BetfairClient : IBetfairClient {

    private readonly Lazy<LoginService> _login;
    private readonly Lazy<AccountService> _account;
    private readonly Lazy<BettingService> _betting;
    private readonly Lazy<HeartbeatService> _heartbeat;
    private readonly Lazy<StreamingService> _streaming;

    /// <summary>
    /// Provides login functionality for interactive and non-interactive workflows.
    /// </summary>
    public LoginService Login => _login.Value;

    /// <summary>
    /// Provides functionality for the Betfair Account API
    /// such as getting account details, funds, statements, etc.
    /// </summary>
    public AccountService Account => _account.Value;

    /// <summary>
    /// Provides functionality for the Betfair Betting API.
    /// Operations include listing markets, placing bets, cancelling bets, etc.
    /// </summary>
    public BettingService Betting => _betting.Value;

    /// <summary>
    /// Provides functionality for the Betfair Heartbeat API
    /// which can perform an action when connection to the Betfair API is lost.
    /// </summary>
    public HeartbeatService Heartbeat => _heartbeat.Value;

    /// <summary>
    /// Provides a fluent interface for subscribing to the Betfair Streaming API.
    /// </summary>
    public StreamingService Streaming => _streaming.Value;


    /// <summary>
    /// Create a new BetfairClient instance, which is the top level client for all operations.
    /// This should be instantiated once per application lifetime.
    /// </summary>
    /// <param name="apiKey"></param>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <param name="certPath"></param>
    public BetfairClient(string apiKey, string username, string password, string? certPath = null) {
        // Shared services
        var httpClient = new HttpClientAdapter(apiKey, 5000);
        var requestHandler = new RequestResponseHandler(httpClient);

        _login = new Lazy<LoginService>(() => new LoginService(requestHandler, username, password, certPath));
        _account = new Lazy<AccountService>(() => new AccountService(requestHandler));
        _betting = new Lazy<BettingService>(() => new BettingService(requestHandler));
        _heartbeat = new Lazy<HeartbeatService>(() => new HeartbeatService(requestHandler));

        _streaming = new Lazy<StreamingService>(() => {

            // Wraps Tcp and Ssl operations
            var sslSocket = new SslSocketAdapter();

            // Wraps Rx operations
            var changeMessageSubject = new SubjectAdapter();

            // Caches for delta merging change messages
            var marketCache = new ConcurrentDictionary<string, MarketSnapshot>();
            var orderCache = new ConcurrentDictionary<string, OrderMarketSnapshot>();

            // Factories for creating snapshots
            var marketSnapshotFactory = new MarketSnapshotFactory(marketCache);
            var orderSnapshotFactory = new OrderSnapshotFactory(orderCache);
            var changeMessageFactory = new ChangeMessageFactory();

            // Handler for processing raw messages
            var changeMessageHandler = new ChangeMessageHandler(
                changeMessageFactory,
                marketSnapshotFactory,
                orderSnapshotFactory,
                changeMessageSubject
            );

            // Defines the logic that consumes the stream
            var socketHandler = new SslSocketHandler(sslSocket, StreamingEndpoints.Production);
            var streamSubscriptionHandler = new StreamSubscriptionHandler(changeMessageHandler, socketHandler.MessageStream, changeMessageSubject);
            return new StreamingService(socketHandler, streamSubscriptionHandler, apiKey);
        });
    }
}
