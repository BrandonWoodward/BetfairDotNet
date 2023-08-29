using BetfairDotNet.Enums.Account;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models;
using BetfairDotNet.Models.Account;
using BetfairDotNet.Models.Exceptions;
using BetfairDotNet.Utils;
using System.Security.Cryptography;
using System.Text.Json;

namespace BetfairDotNet.Handlers;


internal class RequestResponseHandler : IRequestResponseHandler {


    private readonly IBetfairHttpClient _httpClient;


    public RequestResponseHandler(IBetfairHttpClient httpClient) {
        _httpClient = httpClient;
    }


    public async Task<T> Request<T>(string url, string method, Dictionary<string, object?>? data = null) {

        var requestBody = new BetfairServerRequest() { Method = method, Params = data };
        var requestJson = JsonConvert.Serialize(requestBody);

        try {
            var rawResponse = await _httpClient.PostStringContent(url, requestJson);
            var response = JsonConvert.Deserialize<BetfairServerResponse<T>>(rawResponse);

            return response.Error is not null
                ? throw new BetfairNGException(url + method, data, requestJson, response.Error)
                : response.Response!;
        }
        catch(HttpRequestException e) {
            throw new BetfairNGException(url + method, data, requestJson, $"NETWORK_ERROR ({e.StatusCode})", e);
        }
        catch(TaskCanceledException e) when(!e.CancellationToken.IsCancellationRequested) {
            throw new BetfairNGException(url + method, data, requestJson, "NETWORK_ERROR (Timeout)", e);
        }
        catch(JsonException e) {
            throw new BetfairNGException(url + method, data, requestJson, "JSON_SERIALIZATION_ERROR", e);
        }
    }


    public async Task<LoginResponse> Authenticate(string url, string certificatePath, Dictionary<string, string> credentials) {

        using var stringContent = new FormUrlEncodedContent(credentials);

        // Bit of a hack to get the credentials in the expected format for the exception
        var objCredentials = credentials.ToDictionary(kvp => kvp.Key, kvp => (object?)kvp.Value);
        var stringCredentials = await stringContent.ReadAsStringAsync();

        try {
            _httpClient.AddClientCertifcate(certificatePath);
            var httpResponse = await _httpClient.PostUrlEncodedContent(url, stringContent);
            var response = JsonConvert.Deserialize<LoginResponse>(httpResponse);

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
            throw new BetfairNGException(url, objCredentials, stringCredentials, $"NETWORK_ERROR ({e.StatusCode})", e);
        }
        catch(TaskCanceledException e) when(!e.CancellationToken.IsCancellationRequested) {
            throw new BetfairNGException(url, objCredentials, stringCredentials, $"NETWORK_ERROR (Timeout)", e);
        }
        catch(JsonException e) {
            throw new BetfairNGException(url, objCredentials, stringCredentials, "JSON_SERIALIZATION_ERROR", e);
        }
    }
}
