using BetfairDotNet.Enums.Account;

namespace BetfairDotNet.Interfaces;


internal interface ILoginResponse {

    public string SessionToken { get; init; }
    public LoginStatusEnum Status { get; init; }
}
