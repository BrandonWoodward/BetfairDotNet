using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Account;


public sealed record AccountFundsResponse {

    /// <summary>
    /// Amount available to bet.
    /// </summary>
    [JsonPropertyName("availableToBetBalance")]
    public double Balance { get; init; }

    /// <summary>
    /// Current exposure.
    /// </summary>
    [JsonPropertyName("exposure")]
    public double Exposure { get; init; }

    /// <summary>
    /// Sum of retained commission.
    /// </summary>
    [JsonPropertyName("retainedCommission")]
    public double RetainedCommission { get; init; }

    /// <summary>
    /// Exposure limit.
    /// </summary>
    [JsonPropertyName("exposureLimit")]
    public double ExposureLimit { get; init; }

    /// <summary>
    /// User Discount Rate. 
    /// Betfair AUS/NZ customers should not rely on this to determine 
    /// their discount rates which are now applied at the account level.
    /// </summary>
    [JsonPropertyName("discountRate")]
    public double DiscountRate { get; init; }

    /// <summary>
    /// The Betfair points balance.  
    /// </summary>
    [JsonPropertyName("pointsBalance")]
    public int PointsBalance { get; init; }
}
