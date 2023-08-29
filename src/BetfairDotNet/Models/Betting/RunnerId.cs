using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Contains the unique identifier for a runner.
/// </summary>
public sealed class RunnerId {

    /// <summary>
    /// The id of the market bet on.
    /// </summary>
    [JsonPropertyName("marketId")]
    public string? MarketId { get; set; }

    /// <summary>
    /// The id of the selection bet on.
    /// </summary>
    [JsonPropertyName("selectionId")]
    public long SelectionId { get; set; }

    /// <summary>
    /// The handicap associated with the runner in case of asian handicap markets, otherwise returns '0.0'.
    /// </summary>
    [JsonPropertyName("handicap")]
    public double Handicap { get; set; }
}
