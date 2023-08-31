using BetfairDotNet.Handlers;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Streaming;

namespace BetfairDotNet.Services;


/// <summary>
/// The service for the Betfair Streaming API.
/// </summary>
public sealed class StreamingService {


    private readonly ISslSocketHandler _tcpClient;
    private readonly string _apiKey;


    internal StreamingService(ISslSocketHandler tcpClient, string apiKey) {
        _tcpClient = tcpClient;
        _apiKey = apiKey;
    }


    /// <summary>
    /// Create a stream for the given market subscription only.
    /// </summary>
    /// <param name="sessionToken"></param>
    /// <param name="marketSubscription"></param>
    /// <returns></returns>
    public async Task<StreamSubscriptionHandler> CreateStream(
        string sessionToken,
        MarketSubscription marketSubscription) {

        CheckSessionToken(sessionToken);
        await AuthenticateConnection(sessionToken);
        await _tcpClient.SendLine(marketSubscription);
        return new StreamSubscriptionHandler(_tcpClient.MessageStream);
    }


    /// <summary>
    /// Create a stream for the given order subscription only.
    /// </summary>
    /// <param name="sessionToken"></param>
    /// <param name="marketSubscription"></param>
    /// <param name="orderSubscription"></param>
    /// <returns></returns>
    public async Task<StreamSubscriptionHandler> CreateStream(
        string sessionToken,
        OrderSubscription orderSubscription) {

        CheckSessionToken(sessionToken);
        await AuthenticateConnection(sessionToken);
        await _tcpClient.SendLine(orderSubscription);
        return new StreamSubscriptionHandler(_tcpClient.MessageStream);
    }


    /// <summary>
    /// Create a stream for the given market and order subscriptions.
    /// </summary>
    /// <param name="sessionToken"></param>
    /// <param name="marketSubscription"></param>
    /// <param name="orderSubscription"></param>
    /// <returns></returns>
    public async Task<StreamSubscriptionHandler> CreateStream(
        string sessionToken,
        MarketSubscription marketSubscription,
        OrderSubscription orderSubscription) {

        CheckSessionToken(sessionToken);
        await AuthenticateConnection(sessionToken);
        await _tcpClient.SendLine(orderSubscription);
        await _tcpClient.SendLine(marketSubscription);
        return new StreamSubscriptionHandler(_tcpClient.MessageStream);
    }


    private static void CheckSessionToken(string sessionToken) {
        if(string.IsNullOrWhiteSpace(sessionToken)) {
            throw new ArgumentException("Session token not provided.");
        }
    }


    private async Task AuthenticateConnection(string sessionToken) {
        await _tcpClient.Start();
        await _tcpClient.SendLine(new AuthenticationMessage(sessionToken, _apiKey));
    }
}
