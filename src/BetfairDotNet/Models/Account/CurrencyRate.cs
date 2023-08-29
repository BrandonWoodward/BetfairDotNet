using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Account;

/// <summary>
/// Represents a currency rate.
/// </summary>
public sealed record CurrencyRate {

    /// <summary>
    /// Three letter ISO 4217 code
    /// </summary>
    [JsonPropertyName("currencyCode")]
    public string CurrencyCode { get; init; } = string.Empty;

    /// <summary>
    /// Exchange rate for the currency specified in the request.
    /// </summary>
    [JsonPropertyName("rate")]
    public double Rate { get; init; }
}
