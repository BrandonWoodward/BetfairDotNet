using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// A list of KeyLineSelection objects describing the key line for the market.
/// </summary>
public sealed record KeyLineDescription {

    /// <summary>
    /// A list of <see cref=" KeyLineSelection"/> objects.
    /// </summary>
    [JsonPropertyName("keyLine")]
    public List<KeyLineSelection>? KeyLine { get; init; }
}
