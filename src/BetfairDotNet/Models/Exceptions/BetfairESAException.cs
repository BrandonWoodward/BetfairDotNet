namespace BetfairDotNet.Models.Exceptions;


/// <summary>
/// Exception thrown when an error occurs relating to the Betfair Exchange Streaming API.
/// </summary>
public sealed class BetfairESAException : Exception {
    /// <summary>
    /// The time at which the error was thrown
    /// </summary>
    public DateTime Timestamp { get; init; }


    /// <summary>
    /// Indicates whether a reconnection should be attempted.
    /// </summary>
    public bool ShouldReconnect { get; init; }


    public BetfairESAException(bool shouldReconnect, string message, Exception innerException) : base(message, innerException) {
        Timestamp = DateTime.UtcNow;
        ShouldReconnect = shouldReconnect;
    }

    public BetfairESAException(bool shouldReconnect, string message) : base(message) {
        Timestamp = DateTime.UtcNow;
        ShouldReconnect = shouldReconnect;
    }
}
