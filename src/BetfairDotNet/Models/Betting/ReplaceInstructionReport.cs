using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Response for a replace instructions request.
/// </summary>
public sealed record ReplaceInstructionReport {

    /// <summary>
    /// Whether the command succeeded or failed.
    /// </summary>
    [JsonPropertyName("status"), JsonRequired]
    public required InstructionReportStatusEnum Status { get; init; }

    /// <summary>
    /// Cause of failure, or null if command succeeds
    /// </summary>
    [JsonPropertyName("errorCode")]
    public InstructionReportErrorCodeEnum ErrorCode { get; init; }

    /// <summary>
    /// Cancelation report for the original order.
    /// </summary>
    [JsonPropertyName("cancelInstructionReport")]
    public CancelExecutionReport? CancelInstructionReport { get; init; }

    /// <summary>
    /// Placement report for the new order.
    /// </summary>
    [JsonPropertyName("placeInstructionReport")]
    public PlaceInstructionReport? PlaceInstructionReport { get; init; }
}
