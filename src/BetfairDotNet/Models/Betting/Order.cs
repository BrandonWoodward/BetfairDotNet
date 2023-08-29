using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// An order on the Exchange.
/// </summary>
public sealed record Order {

    /// <summary>
    /// Unique identifier for the bet.
    /// </summary>
    [JsonPropertyName("betId"), JsonRequired]
    public required string BetId { get; init; }

    /// <summary>
    /// BSP Order type.
    /// </summary>
    [JsonPropertyName("orderType"), JsonRequired]
    public required OrderTypeEnum OrderType { get; init; }

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
    /// Indicates if the bet is a BACK or a LAY.
    /// For LINE markets customers either Buy a line
    /// (LAY bet, winning if outcome is greater than the taken line (price)) 
    /// or Sell a line(BACK bet, winning if outcome is less than the taken line (price))
    /// </summary>
    [JsonPropertyName("side"), JsonRequired]
    public required SideEnum Side { get; init; }

    /// <summary>
    /// The price of the bet. LINE markets operate at even-money odds of 2.0. 
    /// However, price for these markets refers to the line positions available 
    /// as defined by the markets min-max range and interval steps 
    /// </summary>
    [JsonPropertyName("price"), JsonRequired]
    public required double Price { get; init; }

    /// <summary>
    /// The size of the bet.
    /// </summary>
    [JsonPropertyName("size"), JsonRequired]
    public required double Size { get; init; }

    /// <summary>
    /// Not to be confused with size. This is the liability of a given BSP bet.
    /// </summary>
    [JsonPropertyName("bspLiability"), JsonRequired]
    public required double BspLiability { get; init; }

    /// <summary>
    /// The date, to the second, the bet was placed.
    /// </summary>
    [JsonPropertyName("placedDate"), JsonRequired]
    public required DateTime PlacedDate { get; init; }

    /// <summary>
    /// The average price matched at. 
    /// Voided match fragments are removed from this average calculation. 
    /// For MARKET_ON_CLOSE BSP bets this reports the matched SP price following 
    /// the SP reconciliation process. This value is not meaningful for activity 
    /// on LINE markets and is not guaranteed to be returned or maintained for these markets.
    /// </summary>
    [JsonPropertyName("avgPriceMatched")]
    public double? AvgPriceMatched { get; init; }

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
    /// The customer order reference sent for this bet.
    /// </summary>
    [JsonPropertyName("customerOrderRef")]
    public string CustomerOrderRef { get; init; } = string.Empty;

    /// <summary>
    /// The customer strategy reference sent for this bet.
    /// </summary>
    [JsonPropertyName("customerStrategyRef")]
    public string CustomerStrategyRef { get; init; } = string.Empty;
}
