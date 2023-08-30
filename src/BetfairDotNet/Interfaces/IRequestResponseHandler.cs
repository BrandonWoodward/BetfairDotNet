using BetfairDotNet.Models.Login;

namespace BetfairDotNet.Interfaces;

internal interface IRequestResponseHandler {
    Task<T> Authenticate<T>(string url, Dictionary<string, string> credentials, string? certificatePath = null) where T : ILoginResponse;
    Task<T> Request<T>(string url, string method, Dictionary<string, object?>? data = null);
}