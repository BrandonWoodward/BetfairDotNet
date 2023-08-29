using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Response for a updateOrders request.
/// </summary>
public sealed record UpdateExecutionReport {

    /// <summary>
    /// Echo of the customerRef if passed.
    /// </summary>
    [JsonPropertyName("customerRef")]
    public string CustomerRef { get; init; } = string.Empty;

    /// <summary>
    /// See <see cref="InstructionReportStatusEnum"/> for more details.
    /// </summary>
    [JsonPropertyName("status"), JsonRequired]
    public ExecutionReportStatusEnum Status { get; init; }

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
    /// Responses for each update instruction.
    /// </summary>
    [JsonPropertyName("instructionReports")]
    public List<UpdateInstructionReport> InstructionReports { get; init; } = new();
}
