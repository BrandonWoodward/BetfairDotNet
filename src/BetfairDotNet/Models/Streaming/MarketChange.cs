using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Streaming;


/// <summary>
/// The changes to a given market.
/// </summary>
internal sealed record MarketChange {

    /// <summary>
    /// Runner Changes - a list of changes to runners (or null if un-changed)
    /// </summary>
    [JsonPropertyName("rc")]
    public List<RunnerChange> RunnerChanges { get; init; } = new();

    /// <summary>
    /// Image - replace existing prices / data with the data supplied: it is not a delta (or null if delta)
    /// </summary>
    [JsonPropertyName("img")]
    public bool IsImage { get; init; }

    /// <summary>
    /// The total amount matched across the market. This value is truncated at 2dp (or null if un-changed)
    /// </summary>
    [JsonPropertyName("tv")]
    public double? TotalVolume { get; init; }

    /// <summary>
    /// Conflated - have more than a single change been combined (or null if not conflated)
    /// </summary>
    /// <value>Conflated - have more than a single change been combined (or null if not conflated)</value>
    [JsonPropertyName("con")]
    public bool IsConflated { get; init; }

    /// <summary>
    /// The definition of the market, sent in full or null if un-changed.
    /// </summary>
    [JsonPropertyName("marketDefinition")]
    public MarketDefinition? MarketDefinition { get; init; }

    /// <summary>
    /// Market Id - the id of the market
    /// </summary>
    /// <value>Market Id - the id of the market</value>
    [JsonPropertyName("id"), JsonRequired]
    public required string Id { get; init; }
}
