using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Exceptions;

/// <summary>
/// The internal Betfair exception data related to a <see cref="BetfairServerException"/>.
/// </summary>
public sealed class BetfairServerExceptionData {

    // TODO this should be an enum
    /// <summary>
    /// The type of exception that was thrown
    /// </summary>
    [JsonPropertyName("exceptionname")]
    public string? ExceptionType { get; init; }

    /// <summary>
    /// The inner details of the exception
    /// </summary>
    [JsonIgnore]
    public BetfairServerExceptionDetails? ExceptionDetails { get; set; }


    // This is a hack to handle the API returning different property names for the same data
    // When calling the AccountAPI, the property name is "AccountAPINGException"
    // When calling the Betting/Heartbeat/Sports API, the property name is "APINGException"
    // ee https://stackoverflow.com/questions/43714050/multiple-jsonproperty-name-assigned-to-single-property
    // TODO these must be public for now, but from .NET 8 these can be private
    [JsonPropertyName("APINGException"), JsonInclude]
    public BetfairServerExceptionDetails? APINGException { set => ExceptionDetails = value; }

    [JsonPropertyName("AccountAPINGException"), JsonInclude]
    public BetfairServerExceptionDetails? AccountAPINGException { set => ExceptionDetails = value; }
}
