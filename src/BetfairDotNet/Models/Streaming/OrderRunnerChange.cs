using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Streaming;


/// <summary>
/// 
/// </summary>
internal sealed record OrderRunnerChange {

    /// <summary>
    /// Matched amounts by distinct matched price on the Back side for this runner (selection)
    /// </summary>
    [JsonPropertyName("mb")]
    public List<List<double>> MatchedBacks { get; init; } = new();

    /// <summary>
    /// Orders on this runner (selection) that are not fully matched
    /// </summary>
    [JsonPropertyName("uo")]
    public List<ESAOrder> UnmatchedOrders { get; init; } = new();

    /// <summary>
    /// Selection Id - the id of the runner (selection)
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; init; }

    /// <summary>
    /// The handicap of the runner (selection) (null if not applicable)
    /// </summary>
    [JsonPropertyName("hc")]
    public double? Handicap { get; init; }

    /// <summary>
    /// Replace cache on full image
    /// </summary>
    [JsonPropertyName("fullImage")]
    public bool FullImage { get; init; }

    /// <summary>
    /// Matched amounts by distinct matched price on the Lay side for this runner (selection)
    /// </summary>
    [JsonPropertyName("ml")]
    public List<List<double>> MatchedLays { get; init; } = new();
}
