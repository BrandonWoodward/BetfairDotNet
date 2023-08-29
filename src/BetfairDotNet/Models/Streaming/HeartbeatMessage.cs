namespace BetfairDotNet.Models.Streaming;

/// <summary>
/// Sent to verify the connection is still alive when no other messages are being sent.
/// </summary>
internal sealed record HeartbeatMessage : BaseMessage {

    internal HeartbeatMessage() {
        Operation = "heartbeat";
    }
}
