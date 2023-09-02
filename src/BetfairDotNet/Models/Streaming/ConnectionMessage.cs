using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Streaming;


/// <summary>
/// The response received when first establishing a connection.
/// </summary>
internal sealed record ConnectionMessage : BaseMessage {

    /// <summary>
    /// The connection id
    /// </summary>
    [JsonPropertyName("connectionId"), JsonRequired]
    public required string ConnectionId { get; set; }

    public ConnectionMessage() : base("connection") { }
}
