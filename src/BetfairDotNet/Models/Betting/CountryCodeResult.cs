using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// CountryCode Result
/// </summary>
public sealed record CountryCodeResult {

    /// <summary>
    /// The ISO-2 code for the event.  
    /// </summary>
    [JsonPropertyName("countryCode")]
    public string CountryCode { get; init; } = string.Empty;

    /// <summary>
    /// Count of markets associated with this Country Code
    /// </summary>
    [JsonPropertyName("marketCount")]
    public int MarketCount { get; init; }
}
