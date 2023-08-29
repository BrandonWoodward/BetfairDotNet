using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Description of the price ladder type and any related data.
/// </summary>
public sealed record PriceLadderDescription {

    /// <summary>
    /// The type of price ladder.
    /// </summary>
    [JsonPropertyName("type")]
    public PriceLadderTypeEnum Type { get; init; }
}
