using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Betting;


/// <summary>
/// Response to a CancelInstruction.
/// </summary>
public sealed record CancelExecutionReport {

    /// <summary>
    /// Echo of the customerRef if passed.
    /// </summary>
    [JsonPropertyName("customerRef")]
    public string CustomerRef { get; init; } = string.Empty;

    /// <summary>
    /// See <see cref="InstructionReportStatusEnum"/> for more details."
    /// </summary>
    [JsonPropertyName("status"), JsonRequired]
    public required InstructionReportStatusEnum Status { get; init; }

    /// <summary>
    /// See <see cref="InstructionReportErrorCodeEnum"/> for more details."
    /// </summary>
    [JsonPropertyName("errorCode")]
    public InstructionReportErrorCodeEnum ErrorCode { get; init; }

    /// <summary>
    /// Echo of marketId passed.
    /// </summary>
    [JsonPropertyName("marketId")]
    public string MarketId { get; init; } = string.Empty;

    /// <summary>
    /// The list of responses per cancel instruction.
    /// </summary>
    [JsonPropertyName("instruction")]
    public List<CancelInstructionReport> Instruction { get; init; } = new();
}
