using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Account;


public sealed record AccountDetailsResponse {

    /// <summary>
    /// Default user currency Code. See Currency Parameters for minimum bet sizes relating to each currency.
    /// </summary>
    [JsonPropertyName("currencyCode")]
    public string CurrencyCode { get; init; } = string.Empty;

    /// <summary>
    /// First Name.
    /// </summary>
    [JsonPropertyName("firstName")]
    public string FirstName { get; init; } = string.Empty;

    /// <summary>
    /// Last name.
    /// </summary>
    [JsonPropertyName("lastName")]
    public string LastName { get; init; } = string.Empty;

    /// <summary>
    /// Locale code.
    /// </summary>
    [JsonPropertyName("localeCode")]
    public string LocaleCode { get; init; } = string.Empty;

    /// <summary>
    /// Region based on users zip/postcode (ISO 3166-1 alpha-3 format). 
    /// Defaults to GBR if zip/postcode cannot be identified.
    /// </summary>
    [JsonPropertyName("region")]
    public string Region { get; init; } = string.Empty;

    /// <summary>
    /// User Time Zone.
    /// </summary>
    [JsonPropertyName("timezone")]
    public string Timezone { get; init; } = string.Empty;

    /// <summary>
    /// User Discount Rate. 
    /// Betfair AUS/NZ customers should not rely on this to determine their
    /// discount rates which are now applied at the account level.
    /// </summary>
    [JsonPropertyName("discountRate")]
    public double DiscountRate { get; init; }

    /// <summary>
    /// The Betfair points balance.
    /// </summary>
    [JsonPropertyName("pointsBalance")]
    public int PointsBalance { get; init; }

    /// <summary>
    /// The customer's country of residence (ISO 2 Char format)
    /// </summary>
    [JsonPropertyName("countryCode")]
    public string CountryCode { get; init; } = string.Empty;
}
