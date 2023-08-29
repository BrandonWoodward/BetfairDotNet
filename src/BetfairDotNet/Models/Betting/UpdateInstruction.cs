using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Instruction to update LIMIT bet's persistence of an order that do not affect exposure.
/// </summary>
public sealed class UpdateInstruction {

    /// <summary>
    /// Unique identifier for the bet.
    /// </summary>
    [JsonPropertyName("betId"), JsonRequired]
    public required string BetId { get; set; }

    /// <summary>
    /// The new persistence type to update this bet to.
    /// </summary>
    [JsonPropertyName("newPersistenceType"), JsonRequired]
    public required PersistenceTypeEnum NewPersistenceType { get; set; }
}
