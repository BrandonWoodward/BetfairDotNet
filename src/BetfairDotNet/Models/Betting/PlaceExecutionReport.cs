using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// The return type for the placeOrders request.
/// </summary>
public sealed record PlaceExecutionReport {

    /// <summary>
    /// Echo of the customerRef if passed.
    /// </summary>
    [JsonPropertyName("customerRef")]
    public string CustomerRef { get; init; } = string.Empty;

    /// <summary>
    /// See <see cref="ExecutionReportStatusEnum"/> for more details."
    /// </summary>
    [JsonPropertyName("status"), JsonRequired]
    public required ExecutionReportStatusEnum Status { get; init; }

    /// <summary>
    /// See <see cref="ExecutionReportErrorCodeEnum"/> for more details."
    /// </summary>
    [JsonPropertyName("errorCode")]
    public ExecutionReportErrorCodeEnum ErrorCode { get; init; }

    /// <summary>
    /// Echo of marketId passed
    /// </summary>
    [JsonPropertyName("marketId")]
    public string MarketId { get; init; } = string.Empty;

    /// <summary>
    /// List of responses to the PlaceInstruction(s).
    /// </summary>
    [JsonPropertyName("instructionReports")]
    public List<PlaceInstructionReport> InstructionReports { get; init; } = new();

}
