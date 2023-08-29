using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Heartbeat;


/// <summary>
/// Response from heartbeat operation.
/// </summary>
public sealed record HeartbeatReport {

    /// <summary>
    /// The action performed since your last heartbeat request.
    /// </summary>
    [JsonPropertyName("actionPerformed"), JsonRequired]
    public required string ActionPerformed { get; init; }

    /// <summary>
    /// The actual timeout applied to your heartbeat request, see timeout request parameter description for details.
    /// </summary>
    [JsonPropertyName("actualTimeoutSeconds"), JsonRequired]
    public required int ActualTimeoutSeconds { get; init; }
}
