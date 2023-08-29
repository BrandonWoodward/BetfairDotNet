using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;


/// <summary>
/// Description of a markets key line selection, comprising the selectionId and handicap of the team it is applied to.
/// </summary>
public sealed record KeyLineSelection {

    /// <summary>
    /// Selection ID of the runner in the key line handicap.
    /// </summary>
    [JsonPropertyName("selectionId")]
    public long SelectionId { get; set; }

    /// <summary>
    /// Handicap value of the key line.
    /// </summary>
    [JsonPropertyName("handicap")]
    public double Handicap { get; set; }
}
