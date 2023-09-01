using BetfairDotNet.Enums.Account;
using BetfairDotNet.Interfaces;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Login;


/// <summary>
/// The resposne from the interactive login request.
/// </summary>
public sealed record InteractiveLoginResponse : ILoginResponse {

    /// <summary>
    /// The session token if successful, otherwise empty.
    /// </summary>
    [JsonPropertyName("token")]
    public string SessionToken { get; init; } = string.Empty;


    /// <summary>
    /// SUCCESS or the error code if unsuccessful.
    /// </summary>
    [JsonPropertyName("error")]
    public LoginStatusEnum Status { get; init; }
}
