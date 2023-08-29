using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Time range.
/// </summary>
public sealed class TimeRange {

    /// <summary>
    /// Time from.
    /// </summary>
    [JsonPropertyName("from")]
    public DateTime? From { get; set; }

    /// <summary>
    /// Time to.
    /// </summary>
    [JsonPropertyName("to")]
    public DateTime? To { get; set; }
}
