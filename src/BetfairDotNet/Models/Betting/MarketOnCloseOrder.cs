using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Place a new LIMIT_ON_CLOSE bet.
/// </summary>
public sealed class MarketOnCloseOrder {

    /// <summary>
    /// The size of the bet.
    /// </summary>
    [JsonPropertyName("liability"), JsonRequired]
    public required double Liability { get; set; }
}
