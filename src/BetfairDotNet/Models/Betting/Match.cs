using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// An individual bet Match, or rollup by price or avg price. Rollup depends on the requested MatchProjection.
/// </summary>
public sealed record Match {

    /// <summary>
    /// Unique identifier for the bet. Only present if no rollup.
    /// </summary>
    [JsonPropertyName("betId")]
    public string BetId { get; init; } = string.Empty;

    /// <summary>
    /// Unique identifier for a match. Only present if no rollup.
    /// </summary>
    [JsonPropertyName("matchId")]
    public string MatchId { get; init; } = string.Empty;

    /// <summary>
    /// BACK or LAY.
    /// </summary>
    [JsonPropertyName("side"), JsonRequired]
    public required SideEnum Side { get; init; }

    /// <summary>
    /// Either actual match price or avg match price depending on rollup. 
    /// This value is not meaningful for activity on LINE markets and is 
    /// not guaranteed to be returned or maintained for these markets. 
    /// </summary>
    [JsonPropertyName("price"), JsonRequired]
    public required double Price { get; init; }

    /// <summary>
    /// Size matched at in this fragment, or at this price or avg price depending on rollup
    /// </summary>
    [JsonPropertyName("size"), JsonRequired]
    public required double Size { get; init; }

    /// <summary>
    /// DateTime of the match. Only present if no rollup.
    /// </summary>
    [JsonPropertyName("matchDate")]
    public DateTime MatchDate { get; init; }
}
