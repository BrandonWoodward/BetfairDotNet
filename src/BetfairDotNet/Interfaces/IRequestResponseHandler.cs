using BetfairDotNet.Models.Account;

namespace BetfairDotNet.Interfaces;

internal interface IRequestResponseHandler {
    Task<LoginResponse> Authenticate(string url, string certificatePath, Dictionary<string, string> credentials);
    Task<T> Request<T>(string url, string method, Dictionary<string, object?>? data = null);
}