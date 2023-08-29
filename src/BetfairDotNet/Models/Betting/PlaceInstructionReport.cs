using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Response to a PlaceInstruction
/// </summary>
public sealed record PlaceInstructionReport {

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
    /// The status of the order, if the instruction succeeded. 
    /// If the instruction was unsuccessful, no value is provided.
    /// </summary>
    [JsonPropertyName("orderStatus")]
    public OrderStatusEnum? OrderStatus { get; init; }

    /// <summary>
    /// The instruction that was requested.
    /// </summary>
    [JsonPropertyName("instruction"), JsonRequired]
    public required PlaceInstruction Instruction { get; init; }

    /// <summary>
    /// The bet ID of the new bet. Will be null on failure or if order was placed asynchronously.
    /// </summary>
    [JsonPropertyName("betId")]
    public string? BetId { get; init; }

    /// <summary>
    /// Will be null if order was placed asynchronously.
    /// </summary>
    [JsonPropertyName("placedDate")]
    public DateTime? PlacedDate { get; init; }

    /// <summary>
    /// Will be null if order was placed asynchronously. 
    /// This value is not meaningful for activity on LINE markets and is not guaranteed to be returned or maintained for these markets. 
    /// </summary>
    [JsonPropertyName("averagePriceMatched")]
    public double? AveragePriceMatched { get; init; }

    /// <summary>
    /// Will be null if order was placed asynchronously.
    /// </summary>
    [JsonPropertyName("sizeMatched")]
    public double SizeMatched { get; init; }
}

