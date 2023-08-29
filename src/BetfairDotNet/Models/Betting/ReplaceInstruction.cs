using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Instruction to replace a LIMIT or LIMIT_ON_CLOSE order at a new price. 
/// Original order will be cancelled and a new order placed at the new price for the remaining stake.
/// </summary>
public sealed class ReplaceInstruction {

    /// <summary>
    /// Unique identifier for the bet.
    /// </summary>
    [JsonPropertyName("betId"), JsonRequired]
    public required string BetId { get; set; }

    /// <summary>
    /// The price to replace the bet at.
    /// </summary>
    [JsonPropertyName("newPrice"), JsonRequired]
    public required double NewPrice { get; set; }
}
