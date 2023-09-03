using BetfairDotNet.Enums.Account;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Heartbeat;

/// <summary>
/// The response from the keep alive request
/// </summary>
public sealed record KeepAliveResponse {

    [JsonPropertyName("error")]
    public LoginStatusEnum Status { get; init; }

}
