namespace BetfairDotNet.Interfaces;

internal interface IHttpClient 
{
    void AddClientCertificate(string certificatePath);
    void AddDefaultRequestHeader(string name, string value);

    Task<string> Get(string url);
    Task<string> Post(string url, string content);
    Task<string> Post(string url, FormUrlEncodedContent content);
}