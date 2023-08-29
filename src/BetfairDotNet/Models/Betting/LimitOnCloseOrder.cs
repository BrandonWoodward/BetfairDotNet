using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Place a new LIMIT_ON_CLOSE bet
/// </summary>
public sealed class LimitOnCloseOrder {

    /// <summary>
    /// The size of the bet. 
    /// </summary>
    [JsonPropertyName("liability"), JsonRequired]
    public required double Liability { get; set; }

    /// <summary>
    /// The limit price of the bet if LOC.
    /// </summary>
    [JsonPropertyName("price"), JsonRequired]
    public required double Price { get; set; }
}
