using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Betting;


/// <summary>
/// The response to a cancelInstructions request.
/// </summary>
public sealed record CancelInstructionReport {

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
    [JsonPropertyName("instruction")]
    public CancelInstruction? Instruction { get; init; }

    /// <summary>
    /// The proportion of the bet that was cancelled.
    /// </summary>
    [JsonPropertyName("sizeCancelled"), JsonRequired]
    public double SizeCancelled { get; init; }

    /// <summary>
    /// The time the bet was cancelled.
    /// </summary>
    [JsonPropertyName("cancelledDate")]
    public DateTime? CancelledDate { get; init; }
}
