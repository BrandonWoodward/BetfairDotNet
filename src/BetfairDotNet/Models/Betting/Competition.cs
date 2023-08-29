using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Betting;


/// <summary>
/// The competition associated with an event.
/// </summary>
public sealed record Competition {

    /// <summary>
    /// Unique identifier for the competition.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// The name of the competition.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
}

