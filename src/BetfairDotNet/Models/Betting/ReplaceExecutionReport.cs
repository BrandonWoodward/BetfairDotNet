using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Response to a replace instruction.
/// </summary>
public sealed record ReplaceExecutionReport {

    /// <summary>
    /// Echo of the customerRef if passed.
    /// </summary>
    [JsonPropertyName("customerRef")]
    public string CustomerRef { get; init; } = string.Empty;

    /// <summary>
    /// See <see cref="InstructionReportStatusEnum"/> for more details.
    /// </summary>
    [JsonPropertyName("status"), JsonRequired]
    public required ExecutionReportStatusEnum Status { get; init; }

    /// <summary>
    /// See <see cref="InstructionReportErrorCodeEnum"/> for more details.
    /// </summary>
    [JsonPropertyName("errorCode")]
    public ExecutionReportErrorCodeEnum ErrorCode { get; init; }

    /// <summary>
    /// Echo of marketId passed.
    /// </summary>
    [JsonPropertyName("marketId")]
    public string MarketId { get; init; } = string.Empty;

    /// <summary>
    /// Responses for each replace instruction.
    /// </summary>
    [JsonPropertyName("instructionReports")]
    public List<ReplaceInstructionReport>? InstructionReports { get; init; }
}
