namespace BetfairDotNet.Models.Streaming;

/// <summary>
/// Contains the options for a stream subscription including the session token
/// and parameters for resubscription.
/// </summary>
public sealed class StreamConfiguration 
{
    /// <summary>
    /// Internal use only, this will be set for you.
    /// </summary>
    internal string ApiKey { get; set; } = string.Empty;

    /// <summary>
    /// The session token obtained from a successful login.
    /// </summary>
    public string SessionToken { get; set; } = string.Empty;

    /// <summary>
    /// The amount of downtime on the socket needed to trigger a recovery.
    /// </summary>
    public int RecoveryThresholdMs { get; set; } = 3_000;

    /// <summary>
    /// In the event of loss of connection, this is maximum
    /// amount of time the stream will wait for recovery before
    /// unsubscribing and closing the socket. Defaults to 2 minutes.
    /// </summary>
    public int MaxRecoveryWaitMs { get; set; } = 120_000;
}
