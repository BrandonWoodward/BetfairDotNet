using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Streaming;


/// <summary>
/// 
/// </summary>
internal sealed record OrderMarketChange {

    /// <summary>
    /// Account Id for which the order changes apply.
    /// </summary>
    [JsonPropertyName("accountId")]
    public long AccountId { get; init; }

    /// <summary>
    /// Order Changes - a list of changes to orders on a selection
    /// </summary>
    /// <value>Order Changes - a list of changes to orders on a selection</value>
    [JsonPropertyName("orc")]
    public List<OrderRunnerChange> OrderRunnerChanges { get; init; } = new();

    /// <summary>
    /// TODO what is this?
    /// </summary>
    [JsonPropertyName("closed")]
    public bool Closed { get; init; }

    /// <summary>
    /// The order's corresponding market id.
    /// </summary>
    [JsonPropertyName("id"), JsonRequired]
    public required string Id { get; init; }

    /// <summary>
    /// Replace order cache on full image.
    /// </summary>
    [JsonPropertyName("fullImage")]
    public bool IsImage { get; init; }
}
