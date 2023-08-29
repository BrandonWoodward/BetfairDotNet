namespace BetfairDotNet.Endpoints;


/// <summary>
/// For pre-production (beta) releases, use the Integration endpoint, otherwise use Production.
/// </summary>
public static class StreamingEndpoints {
    public const string Production = "stream-api.betfair.com";
    public const string Integration = "stream-api-integration.betfair.com";
}
