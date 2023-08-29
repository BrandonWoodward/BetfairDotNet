using BetfairDotNet.Models.Exceptions;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Models;

internal sealed record BetfairServerResponse<T> {

    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("result")]
    public T? Response { get; init; }

    [JsonPropertyName("error")]
    public BetfairServerException? Error { get; init; }
}
