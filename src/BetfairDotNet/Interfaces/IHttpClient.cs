namespace BetfairDotNet.Interfaces;

internal interface IHttpClient {
    void AddClientCertifcate(string certificatePath);
    void AddDefaultRequestHeader(string name, string value);

    Task<string> PostStringContent(string url, string content);
    Task<string> PostUrlEncodedContent(string url, FormUrlEncodedContent content);
}