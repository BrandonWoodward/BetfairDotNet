using BetfairDotNet.Enums.Account;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Account;


public sealed record LoginResponse {

    /// <summary>
    /// The session token used to set X-Authentication header.
    /// </summary>
    [JsonPropertyName("sessionToken")]
    public string SessionToken { get; init; } = string.Empty;

    /// <summary>
    /// The possible failure and exceptional return codes.
    /// </summary>
    [JsonPropertyName("loginStatus"), JsonRequired]
    public required LoginStatusEnum Status { get; init; }
}
