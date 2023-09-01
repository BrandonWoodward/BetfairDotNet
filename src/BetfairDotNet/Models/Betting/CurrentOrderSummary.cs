using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;


/// <summary>
/// Summary of a current order (unmatched etc)
/// </summary>
public sealed record CurrentOrderSummary {

    /// <summary>
    /// The bet ID of the original place order.
    /// </summary>
    [JsonPropertyName("betId"), JsonRequired]
    public required string BetId { get; init; }

    /// <summary>
    /// The market id the order is for.
    /// </summary>
    [JsonPropertyName("marketId"), JsonRequired]
    public required string MarketId { get; init; }

    /// <summary>
    /// The selection id the order is for.
    /// </summary>
    [JsonPropertyName("selectionId")]
    public long SelectionId { get; init; }

    /// <summary>
    /// The handicap associated with the runner in case of Asian handicap markets, null otherwise.
    /// </summary>
    [JsonPropertyName("handicap"), JsonRequired]
    public required double Handicap { get; init; }

    /// <summary>
    /// The price and size of the bet.
    /// </summary>
    [JsonPropertyName("priceSize"), JsonRequired]
    public required PriceSize PriceSize { get; init; }

    /// <summary>
    /// Not to be confused with size. This is the liability of a given BSP bet.
    /// </summary>
    [JsonPropertyName("bspLiability"), JsonRequired]
    public required double BspLiability { get; init; }

    /// <summary>
    /// BACK or LAY
    /// </summary>
    [JsonPropertyName("side"), JsonRequired]
    public required SideEnum Side { get; init; }

    /// <summary>
    /// Either EXECUTABLE (an unmatched amount remains) or EXECUTION_COMPLETE (no unmatched amount remains).
    /// </summary>
    [JsonPropertyName("status"), JsonRequired]
    public required OrderStatusEnum Status { get; init; }

    /// <summary>
    /// What to do with the order at turn-in-play.
    /// </summary>
    [JsonPropertyName("persistenceType"), JsonRequired]
    public required PersistenceTypeEnum PersistenceType { get; init; }

    /// <summary>
    /// BSP Order type.
    /// </summary>
    [JsonPropertyName("orderType"), JsonRequired]
    public required OrderTypeEnum OrderType { get; init; }

    /// <summary>
    /// The date, to the second, the bet was placed.
    /// </summary>
    [JsonPropertyName("placedDate"), JsonRequired]
    public required DateTime PlacedDate { get; init; }

    /// <summary>
    /// The date, to the second, of the last matched bet fragment (where applicable)
    /// </summary>
    [JsonPropertyName("matchedDate"), JsonRequired]
    public required DateTime MatchedDate { get; init; }

    /// <summary>
    /// The average price matched at. Voided match fragments are removed from this average calculation. 
    /// The price is automatically adjusted in the event of non runners being declared with applicable reduction factors.
    /// This value is not meaningful for activity on LINE markets and is not guaranteed to be returned or maintained for these markets. 
    /// </summary>
    [JsonPropertyName("averagePriceMatched")]
    public double AveragePriceMatched { get; init; }

    /// <summary>
    /// The current amount of this bet that was matched.
    /// </summary>
    [JsonPropertyName("sizeMatched")]
    public double SizeMatched { get; init; }

    /// <summary>
    /// The current amount of this bet that is unmatched.
    /// </summary>
    [JsonPropertyName("sizeRemaining")]
    public double SizeRemaining { get; init; }

    /// <summary>
    /// The current amount of this bet that was lapsed.
    /// </summary>
    [JsonPropertyName("sizeLapsed")]
    public double SizeLapsed { get; init; }

    /// <summary>
    /// The current amount of this bet that was cancelled.
    /// </summary>
    [JsonPropertyName("sizeCancelled")]
    public double SizeCancelled { get; init; }

    /// <summary>
    /// The current amount of this bet that was voided.
    /// </summary>
    [JsonPropertyName("sizeVoided")]
    public double SizeVoided { get; init; }

    /// <summary>
    /// The regulator authorisation code.
    /// </summary>
    [JsonPropertyName("regulatorAuthCode")]
    public string RegulatorAuthCode { get; init; } = string.Empty;

    /// <summary>
    /// The regulator Code.
    /// </summary>
    [JsonPropertyName("regulatorCode")]
    public string RegulatorCode { get; init; } = string.Empty;

    /// <summary>
    /// The order reference defined by the customer for this bet.
    /// </summary>
    [JsonPropertyName("customerOrderRef")]
    public string CustomerOrderRef { get; init; } = string.Empty;

    /// <summary>
    /// The strategy reference defined by the customer for this bet.
    /// </summary>
    [JsonPropertyName("customerStrategyRef")]
    public string CustomerStrategyRef { get; init; } = string.Empty;

    /// <summary>
    /// A container for all the ancillary data for this Item.
    /// </summary>
    [JsonPropertyName("currentItemDescription")]
    public string CurrentItemDescription { get; init; } = string.Empty;
}
