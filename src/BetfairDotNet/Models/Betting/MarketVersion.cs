using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Version of a market that changes on the turn in-play or suspend.
/// </summary>
public sealed class MarketVersion {

    /// <summary>
    /// A non-monotonically increasing number indicating market changes
    /// </summary>
    [JsonPropertyName("version"), JsonRequired]
    public required long Version { get; init; }
}
