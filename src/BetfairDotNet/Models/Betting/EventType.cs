using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// The event type.
/// </summary>
public sealed record EventType {

    /// <summary>
    /// The unique identifier for the event type.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// The name of the event type.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
}
