using System.Text.Json;
using BetfairDotNet.Converters;
using BetfairDotNet.Models;
using BetfairDotNet.Models.Exceptions;

namespace BetfairDotNet.Contexts;

internal class RequestResponseContext<T>
{
    private string? _endpoint;
    private string? _json;
    private Func<Task<string>>? _requestAction;
    private BetfairServerRequest? _request;
    private BetfairServerResponse<T>? _response;

    public RequestResponseContext<T> WithEndpoint(string endpoint)
    {
        _endpoint = endpoint;
        return this;
    }

    public RequestResponseContext<T> WithRequestBody(string method, Dictionary<string, object?>? data = null)
    {
        _request = new() { Method = method, Params = data };
        _json = JsonConvert.Serialize(_request);
        return this;
    }

    public RequestResponseContext<T> WithRequestAction(Func<string?, Task<string>> action)
    {
        _requestAction = () => action(_json);
        return this;
    }

    public async Task<T> GetResponseOrThrow()
    {
        try
        {
            var rawResponse = await _requestAction!.Invoke();
            
            // If the request was a GET, the response is not wrapped in a BetfairServerResponse.
            if (_request is null)
            {
                return JsonConvert.Deserialize<T>(rawResponse);
            }
            
            _response = JsonConvert.Deserialize<BetfairServerResponse<T>>(rawResponse);
            
            if (_response.Error is not null)
            {
                throw new BetfairNGException(_endpoint!, _request, _response.Error);
            }

            return _response.Response!;
        }
        catch (HttpRequestException ex)
        {
            throw new BetfairNGException(_endpoint!, _request, $"NETWORK_ERROR ({ex.StatusCode})", ex);
        }
        catch (TaskCanceledException ex)
        {
            throw new BetfairNGException(_endpoint!, _request, "NETWORK_ERROR (Timeout)", ex);
        }
    }
}