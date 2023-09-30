namespace BetfairDotNet.Interfaces;

internal interface IRequestResponseHandler 
{
    Task<T> Request<T>(string endpoint, string method, Dictionary<string, object?>? data = null);
    
    Task<T> Request<T>(string endpoint);

    Task<T> Request<T>(string endpoint, Dictionary<string, string> credentials)
        where T : ILoginResponse;

    Task<T> Request<T>(string endpoint, Dictionary<string, string> credentials, string certPath)
        where T : ILoginResponse;
}