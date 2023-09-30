namespace BetfairDotNet.Models.Exceptions;


/// <summary>
/// Exception thrown when an error occurs relating to the Betfair API-NG.
/// </summary>
public sealed class BetfairNGException : Exception 
{
    /// <summary>
    /// The time when the exception occurred.
    /// </summary>
    public DateTime Timestamp { get; init; }

    /// <summary>
    /// The full url of the request that threw the exception.
    /// </summary>
    public string Endpoint { get; init; }

    /// <summary>
    /// The original request if applicable.
    /// </summary>
    public BetfairServerRequest? OriginalRequest { get; init; }
    
    internal BetfairNGException(string endpoint, string message)
        : base(message) 
    {
        Timestamp = DateTime.UtcNow;
        Endpoint = endpoint;
    }
    
    internal BetfairNGException(string endpoint, string message, Exception innerException)
        : base(message, innerException) 
    {
        Timestamp = DateTime.UtcNow;
        Endpoint = endpoint;
    }
    
    internal BetfairNGException(string endpoint, BetfairServerRequest? request, string message, Exception innerException)
        : base(message, innerException) 
    {
        Timestamp = DateTime.UtcNow;
        Endpoint = endpoint;
        OriginalRequest = request;
    }
    
    internal BetfairNGException(string endpoint, BetfairServerRequest? request, BetfairServerException ex)
        : base($"The request to {endpoint} threw an exception: {(ex.BetfairError?.ExceptionDetails?.ErrorCode ?? ex.JsonRPCErrorCode)}", ex) 
    {
        Timestamp = DateTime.UtcNow;
        Endpoint = endpoint;
        OriginalRequest = request;
    }
}
