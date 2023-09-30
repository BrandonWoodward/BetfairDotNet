using BetfairDotNet.Interfaces;
using BetfairDotNet.Contexts;

namespace BetfairDotNet.Handlers;

internal sealed class RequestResponseHandler : IRequestResponseHandler 
{
    private readonly IHttpClient _httpClient;
    
    public RequestResponseHandler(IHttpClient httpClient) 
    {
        _httpClient = httpClient;
    }
    
    public async Task<T> Request<T>(string endpoint, string method, Dictionary<string, object?>? data = null)
    {
        return await new RequestResponseContext<T>()
            .WithEndpoint(endpoint)
            .WithRequestBody(method, data)
            .WithRequestAction(json => _httpClient.Post(endpoint, json!))
            .GetResponseOrThrow();
    }

    public async Task<T> Request<T>(string endpoint)
    {
        return await new RequestResponseContext<T>()
            .WithEndpoint(endpoint)
            .WithRequestAction(_ => _httpClient.Get(endpoint))
            .GetResponseOrThrow();
    }
    
    public async Task<T> Request<T>(string endpoint, Dictionary<string, string> credentials)
        where T : ILoginResponse
    {
        return await new AuthenticationContext<T>()
            .WithEndpoint(endpoint)
            .WithCredentials(credentials)
            .GetResponseOrThrow(_httpClient);
    }
    
    public async Task<T> Request<T>(string endpoint, Dictionary<string, string> credentials, string certPath)
        where T : ILoginResponse
    {
        return await new AuthenticationContext<T>()
            .WithEndpoint(endpoint)
            .WithCredentials(credentials)
            .WithCertificate(certPath)
            .GetResponseOrThrow(_httpClient);
    }
}
