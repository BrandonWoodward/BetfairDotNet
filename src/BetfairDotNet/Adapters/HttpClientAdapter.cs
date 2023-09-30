using BetfairDotNet.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace BetfairDotNet.Adapters;

[ExcludeFromCodeCoverage]
internal sealed class HttpClientAdapter : IHttpClient 
{
    private readonly HttpClient _httpClient;
    private readonly HttpClientHandler _httpClientHandler;

    internal HttpClientAdapter(string apiKey, int timeoutMs) 
    {
        _httpClientHandler = CreateClientHandler();
        _httpClient = CreateHttpClient(_httpClientHandler, apiKey, timeoutMs);
    }
    
    public async Task<string> Get(string url) {
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }


    public async Task<string> Post(string url, string content) {
        using var stringContent = new StringContent(content);
        var response = await _httpClient.PostAsync(url, stringContent);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }


    public async Task<string> Post(string url, FormUrlEncodedContent content) {
        var response = await _httpClient.PostAsync(url, content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }


    public void AddDefaultRequestHeader(string name, string value) {
        if(_httpClient.DefaultRequestHeaders.Contains(name)) {
            _httpClient.DefaultRequestHeaders.Remove(name);
        }
        _httpClient.DefaultRequestHeaders.Add(name, value);
    }



    public void AddClientCertificate(string certificatePath) {
        var certificate = new X509Certificate2(certificatePath);
        _httpClientHandler.ClientCertificates.Add(certificate);
    }



    private static HttpClientHandler CreateClientHandler() {
        var handler = new HttpClientHandler {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13,
        };
        return handler;
    }


    private static HttpClient CreateHttpClient(HttpMessageHandler handler, string apiKey, int timeoutMs) {
        var httpClient = new HttpClient(handler) { Timeout = TimeSpan.FromMilliseconds(timeoutMs) };
        httpClient.DefaultRequestHeaders.Add("X-Application", apiKey);
        httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
        httpClient.DefaultRequestHeaders.Add("User-Agent", "BetfairDotNet/1.0");
        return httpClient;
    }
}
