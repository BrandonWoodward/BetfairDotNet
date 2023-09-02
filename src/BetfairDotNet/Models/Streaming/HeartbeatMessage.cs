using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Streaming;

/// <summary>
/// Sent to verify the connection is still alive when no other messages are being sent.
/// </summary>
// This can't inherit from BaseMessage due to some serialization issues
internal sealed record HeartbeatMessage {

    /// <summary>
    /// The operation type
    /// </summary>
    [JsonPropertyName("op"), JsonRequired]
    public string Operation { get; init; } = "heartbeat";

    /// <summary>
    /// Client generated unique id to link request with response (like json rpc)    
    /// /// </summary>
    [JsonPropertyName("id")]
    public int? Id { get; init; }
}
