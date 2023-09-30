using System.Security.Cryptography;
using BetfairDotNet.Converters;
using BetfairDotNet.Enums.Account;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Exceptions;

namespace BetfairDotNet.Contexts;

internal sealed class AuthenticationContext<T> where T : ILoginResponse
{
    private string? _endpoint;
    private string? _certPath;
    private FormUrlEncodedContent? _credentials;
    
    public AuthenticationContext<T> WithEndpoint(string endpoint)
    {
        _endpoint = endpoint;
        return this;
    }

    public AuthenticationContext<T> WithCredentials(Dictionary<string, string> credentials)
    {
        _credentials = new(credentials);
        return this;
    }

    public AuthenticationContext<T> WithCertificate(string certPath)
    {
        _certPath = certPath;
        return this;
    }

    public async Task<T> GetResponseOrThrow(IHttpClient httpClient)
    {
        try
        {
            if(_certPath is not null) httpClient.AddClientCertificate(_certPath);
            var httpResponse = await httpClient.Post(_endpoint!, _credentials!);
            var response = JsonConvert.Deserialize<T>(httpResponse);
            if (response.Status is not LoginStatusEnum.SUCCESS)
            {
                throw new BetfairNGException(_endpoint!, $"Authentication failed ({response.Status}).");
            }

            httpClient.AddDefaultRequestHeader("X-Authentication", response.SessionToken);
            return response;
        }
        catch (CryptographicException ex)
        {
            throw new BetfairNGException(_endpoint!, "Certificate not found or was invalid.", ex);
        }
        catch (HttpRequestException ex)
        {
            throw new BetfairNGException(_endpoint!, $"A network error occurred ({ex.StatusCode}).", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new BetfairNGException(_endpoint!, "A network error occurred (Timeout).", ex);
        }
        finally
        {
            _credentials?.Dispose();
        }
    }
}