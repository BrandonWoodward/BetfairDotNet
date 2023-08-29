using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Betting;


/// <summary>
/// Instruction to fully or partially cancel an order (only applies to LIMIT orders). 
/// The CancelInstruction report won't be returned for marketId level cancel instructions.
/// </summary>
public sealed class CancelInstruction {

    /// <summary>
    /// The unique identifier of the cancelled bet.
    /// </summary>
    [JsonPropertyName("betId")]
    public string? BetId { get; set; }

    /// <summary>
    /// If supplied then this is a partial cancel. Should be set to 'null' if no size reduction is required.
    /// </summary>
    [JsonPropertyName("sizeReduction")]
    public double? SizeReduction { get; set; }
}
