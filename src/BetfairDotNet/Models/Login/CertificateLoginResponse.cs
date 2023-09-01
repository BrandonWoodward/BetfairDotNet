using BetfairDotNet.Enums.Account;
using BetfairDotNet.Interfaces;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Login;


/// <summary>
/// The response from the certificate login request.
/// </summary>
public sealed record CertificateLoginResponse : ILoginResponse {

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
