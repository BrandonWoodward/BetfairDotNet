using BetfairDotNet.Converters;
using BetfairDotNet.Enums.Account;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models;
using BetfairDotNet.Models.Exceptions;
using System.Security.Cryptography;
using System.Text.Json;

namespace BetfairDotNet.Handlers;


internal class RequestResponseHandler : IRequestResponseHandler {


    private readonly IHttpClient _httpClient;


    public RequestResponseHandler(IHttpClient httpClient) {
        _httpClient = httpClient;
    }


    public async Task<T> Request<T>(string url, string? method = null, Dictionary<string, object?>? data = null) {

        string requestJson;

        if(string.IsNullOrEmpty(method)) {
            // Serialize data directly if "method" is not defined
            requestJson = JsonConvert.Serialize(data);
        }
        else {
            var requestBody = new BetfairServerRequest() { Method = method, Params = data };
            requestJson = JsonConvert.Serialize(requestBody);
        }

        try {
            var rawResponse = await _httpClient.PostStringContent(url, requestJson);
            var response = JsonConvert.Deserialize<BetfairServerResponse<T>>(rawResponse);

            if(response == null) {
                throw new BetfairNGException($"{url}{method}", data, requestJson, "NULL_RESPONSE");
            }

            return response.Error is not null
                ? throw new BetfairNGException($"{url}{method}", data, requestJson, response.Error)
                : response.Response!;
        }
        catch(HttpRequestException e) {
            throw new BetfairNGException($"{url}{method}", data, requestJson, $"NETWORK_ERROR ({e.StatusCode})", e);
        }
        catch(TaskCanceledException e) when(!e.CancellationToken.IsCancellationRequested) {
            throw new BetfairNGException($"{url}{method}", data, requestJson, "NETWORK_ERROR (Timeout)", e);
        }
        catch(JsonException e) {
            throw new BetfairNGException($"{url}{method}", data, requestJson, "JSON_SERIALIZATION_ERROR", e);
        }
    }



    public async Task<T> Authenticate<T>(
        string url,
        Dictionary<string, string> credentials,
        string? certificatePath = null) where T : ILoginResponse {

        using var stringContent = new FormUrlEncodedContent(credentials);

        // Bit of a hack to get the credentials in the expected format for the exception
        var objCredentials = credentials.ToDictionary(kvp => kvp.Key, kvp => (object?)kvp.Value);
        var stringCredentials = await stringContent.ReadAsStringAsync();

        try {
            if(!string.IsNullOrEmpty(certificatePath)) _httpClient.AddClientCertifcate(certificatePath);
            var httpResponse = await _httpClient.PostUrlEncodedContent(url, stringContent);
            var response = JsonConvert.Deserialize<T>(httpResponse);

            // Throw exception if the login failed for user to handle
            if(response.Status is not LoginStatusEnum.SUCCESS) {
                throw new BetfairNGException(url, objCredentials, stringCredentials, response.Status.ToString());
            }

            if(string.IsNullOrWhiteSpace(response.SessionToken)) {
                throw new BetfairNGException(url, objCredentials, stringCredentials, "NO_SESSION_TOKEN_RETURNED");
            }

            // Set X-Authentication header for subsequent requests if login success
            _httpClient.AddDefaultRequestHeader("X-Authentication", response.SessionToken);
            return response;
        }
        catch(CryptographicException e) {
            throw new BetfairNGException(url, objCredentials, stringCredentials, "CERTIFICATE_NOT_FOUND", e);
        }
        catch(HttpRequestException e) {
            throw new BetfairNGException(url, objCredentials, stringCredentials, $"NETWORK_ERROR", e);
        }
        catch(TaskCanceledException e) when(!e.CancellationToken.IsCancellationRequested) {
            throw new BetfairNGException(url, objCredentials, stringCredentials, $"TIMEOUT", e);
        }
        catch(JsonException e) {
            throw new BetfairNGException(url, objCredentials, stringCredentials, "JSON_SERIALIZATION_ERROR", e);
        }
    }
}
