using System.Text.Json.Serialization;

namespace BetfairDotNet.Enums.Streaming;

/// <summary>
/// The distinct operations for the streaming api
/// </summary>
public enum OperationTypeEnum {

    // Request

    /// <summary>
    /// Indicates the connection is still alive when no updates are sent.
    /// </summary>
    [JsonPropertyName("heartbeat")]
    HEARTBEAT,

    /// <summary>
    /// Authenticate the connection.
    /// </summary>
    [JsonPropertyName("authentication")]
    AUTHENTICATION,

    /// <summary>
    /// Subscribe to market changes.
    /// </summary>
    [JsonPropertyName("marketSubscription")]
    MARKET_SUBSCRIPTION,

    /// <summary>
    /// Subscribe to order changes
    /// </summary>
    [JsonPropertyName("orderSubscription")]
    ORDER_SUBSCRIPTION,

    // Response

    /// <summary>
    /// Operation returned in response to the connection operation.
    /// </summary>
    [JsonPropertyName("connection")]
    CONNECTION,

    /// <summary>
    /// Operation returned in response to an authentication operation.
    /// </summary>
    [JsonPropertyName("status")]
    STATUS,

    /// <summary>
    /// Operation returned in response to a market subscription.
    /// </summary>
    [JsonPropertyName("mcm")]
    MARKET_CHANGE_MESSAGE,

    /// <summary>
    /// Operation returned in response to an order subscription.
    /// </summary>
    [JsonPropertyName("ocm")]
    ORDER_CHANGE_MESSAGE
}
