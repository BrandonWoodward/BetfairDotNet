using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Exceptions;

/// <summary>
/// This is the outer JSON-RPC exception that is thrown when an operation fails.
/// </summary>
public sealed class BetfairServerException : Exception {

    /// <summary>
    /// The JSON-RPC error code
    /// </summary>
    [JsonPropertyName("code")]
    public string JsonRPCErrorCode { get; init; } = string.Empty;

    /// <summary>
    /// The JSON-RPC error message
    /// </summary>
    [JsonPropertyName("message")]
    public string? JsonRPCErrorMessage { get; set; }

    /// <summary>
    /// The Betfair internal error data
    /// </summary>
    [JsonPropertyName("data")]
    public BetfairServerExceptionData? BetfairError { get; init; }
}
