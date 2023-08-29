namespace BetfairDotNet.Models.Exceptions;


/// <summary>
/// Exception thrown when an error occurs relating to the Betfair API-NG.
/// </summary>
public sealed class BetfairNGException : Exception {

    /// <summary>
    /// The time when the exception occurred.
    /// </summary>
    public DateTime Timestamp { get; init; }

    /// <summary>
    /// The full url of the request that threw the exception.
    /// </summary>
    public string Endpoint { get; init; }

    /// <summary>
    /// The original parameters sent with the request.
    /// </summary>
    public Dictionary<string, object?>? RequestParameters { get; init; }

    /// <summary>
    /// The serialized content sent with the request.
    /// </summary>
    public string? RequestJson { get; init; }


    // Constructor for exceptions with only a message and request details
    public BetfairNGException(string endpoint, Dictionary<string, object?>? requestParams, string? requestJson, string message)
        : base(message) {
        Timestamp = DateTime.UtcNow;
        Endpoint = endpoint;
        RequestParameters = requestParams;
        RequestJson = requestJson;
    }

    // Constructor for exceptions with an inner exception and request details
    public BetfairNGException(string endpoint, Dictionary<string, object?>? requestParams, string? requestJson, string message, Exception innerException)
        : base(message, innerException) {
        Timestamp = DateTime.UtcNow;
        Endpoint = endpoint;
        RequestParameters = requestParams;
        RequestJson = requestJson;
    }

    // Constructor specific for the BetfairServerException
    public BetfairNGException(string endpoint, Dictionary<string, object?>? requestParams, string? requestJson, BetfairServerException ex)
        : base($"The request to {endpoint} threw an exception: {(ex.BetfairError?.ExceptionDetails?.ErrorCode ?? ex.JsonRPCErrorCode)}", ex) {
        Timestamp = DateTime.UtcNow;
        Endpoint = endpoint;
        RequestParameters = requestParams;
        RequestJson = requestJson;
    }
}
