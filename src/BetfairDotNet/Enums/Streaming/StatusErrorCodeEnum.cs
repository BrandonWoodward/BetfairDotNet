using BetfairDotNet.Converters;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Enums.Streaming;


[JsonConverter(typeof(EmptyStringToEnumConverter<StatusErrorCodeEnum>))]
internal enum StatusErrorCodeEnum {

    /// <summary>
    /// No error.
    /// </summary>
    NONE,

    /// <summary>
    /// Api key not provided.
    /// </summary>
    NO_APP_KEY,

    /// <summary>
    /// Api key is invalid.
    /// </summary>
    INVALID_APP_KEY,

    /// <summary>
    /// No session token provided.
    /// </summary>
    NO_SESSION,

    /// <summary>
    /// Invalid session token provided.
    /// </summary>
    INVALID_SESSION_INFORMATION,

    /// <summary>
    /// 
    /// </summary>
    NOT_AUTHORIZED,

    /// <summary>
    /// 
    /// </summary>
    INVALID_INPUT,

    /// <summary>
    /// 
    /// </summary>
    INVALID_CLOCK,

    /// <summary>
    /// An internal error occured.
    /// </summary>
    UNEXPECTED_ERROR,

    /// <summary>
    /// 
    /// </summary>
    TIMEOUT,

    /// <summary>
    /// Tried subscribing to too many markets.
    /// </summary>
    SUBSCRIPTION_LIMIT_EXCEEDED,

    /// <summary>
    /// 
    /// </summary>
    INVALID_REQUEST,

    /// <summary>
    /// Connection of the socket failed
    /// </summary>
    CONNECTION_FAILED,

    /// <summary>
    /// Too many concurrent connections.
    /// </summary>
    MAX_CONNECTION_LIMIT_EXCEEDED
}
