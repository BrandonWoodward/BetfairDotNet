using BetfairDotNet.Enums.Streaming;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Streaming;


internal sealed record StatusMessage : BaseMessage {

    /// <summary>
    /// The type of error in case of a failure
    /// </summary>
    [JsonPropertyName("errorCode")]
    public StatusErrorCodeEnum ErrorCode { get; init; }

    /// <summary>
    /// The status of the last request
    /// </summary>
    [JsonPropertyName("statusCode")]
    public StatusCodeEnum StatusCode { get; init; }

    /// <summary>
    /// Additional message in case of a failure
    /// </summary>
    [JsonPropertyName("errorMessage")]
    public string ErrorMessage { get; init; } = string.Empty;

    /// <summary>
    /// The connection id
    /// </summary>
    [JsonPropertyName("connectionId")]
    public string ConnectionId { get; init; } = string.Empty;

    /// <summary>
    /// Is the connection now closed
    /// </summary>
    [JsonPropertyName("connectionClosed")]
    public bool ConnectionClosed { get; init; }

    public StatusMessage() : base("status") { }
}
