using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Exceptions;

/// <summary>
/// The details for the <see cref="BetfairServerExceptionData"/>.
/// </summary>
public sealed class BetfairServerExceptionDetails {

    // TODO what is this?
    [JsonPropertyName("requestUUID")]
    public string? RequestUUID { get; init; }

    /// <summary>
    /// The error code.
    /// </summary>
    [JsonPropertyName("errorCode")]
    public string? ErrorCode { get; init; }

    /// <summary>
    /// The stack trace of the error.
    /// </summary>
    [JsonPropertyName("errorDetails")]
    public string? ErrorDetails { get; init; }
}
