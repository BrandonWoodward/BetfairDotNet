using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Streaming;


public sealed record AuthenticationMessage : BaseMessage {

    /// <summary>
    /// The session token from a successful login
    /// </summary>
    [JsonPropertyName("session"), JsonRequired]
    public string SessionToken { get; set; }

    /// <summary>
    /// The api key from user's betfair account
    /// </summary>
    [JsonPropertyName("appKey"), JsonRequired]
    public string ApiKey { get; set; }


    internal AuthenticationMessage(string sessionToken, string apiKey) : base("authentication") {
        SessionToken = sessionToken;
        ApiKey = apiKey;
    }
}
