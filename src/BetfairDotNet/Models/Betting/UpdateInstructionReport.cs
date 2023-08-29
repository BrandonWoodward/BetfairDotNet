using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Response for each UpdateInstruction.
/// </summary>
public sealed record UpdateInstructionReport {

    /// <summary>
    /// Whether the command succeeded or failed.
    /// </summary>
    [JsonPropertyName("status"), JsonRequired]
    public required InstructionReportStatusEnum Status { get; init; }

    /// <summary>
    /// Cause of failure, or null if command succeeds.
    /// </summary>
    [JsonPropertyName("errorCode")]
    public InstructionReportErrorCodeEnum ErrorCode { get; init; }

    /// <summary>
    /// The instruction that was requested.
    /// </summary>
    [JsonPropertyName("instruction"), JsonRequired]
    public required UpdateInstruction Instruction { get; init; }
}
