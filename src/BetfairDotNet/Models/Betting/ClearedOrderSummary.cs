using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Betting;


/// <summary>
/// Summary of a cleared order (settled, voided, cancelled etc)
/// </summary>
public sealed record ClearedOrderSummary {

    /// <summary>
    /// The id of the event type bet on. 
    /// Available at EVENT_TYPE groupBy level or lower.
    /// </summary>
    [JsonPropertyName("eventTypeId")]
    public string EventTypeId { get; init; } = string.Empty;

    /// <summary>
    /// The id of the event bet on. 
    /// Available at EVENT groupBy level or lower.
    /// </summary>
    [JsonPropertyName("eventId")]
    public string EventType { get; init; } = string.Empty;

    /// <summary>
    /// The id of the market bet on. 
    /// Available at MARKET groupBy level or lower.
    /// </summary>
    [JsonPropertyName("marketId")]
    public string MarketId { get; init; } = string.Empty;

    /// <summary>
    /// The id of the selection bet on. 
    /// Available at RUNNER groupBy level or lower.
    /// </summary>
    [JsonPropertyName("selectionId")]
    public long SelectionId { get; init; }

    /// <summary>
    /// The handicap.  
    /// Enter the specific handicap value (returned by RUNNER in listMaketBook) 
    /// if the market is an Asian handicap market. 
    /// Available at MARKET groupBy level or lower.
    /// </summary>
    [JsonPropertyName("handicap")]
    public double Handicap { get; init; }

    /// <summary>
    /// The id of the bet. 
    /// Available at BET groupBy level.
    /// </summary>
    [JsonPropertyName("betId")]
    public string BetId { get; init; } = string.Empty;

    /// <summary>
    /// The date the bet order was placed by the customer. 
    /// Only available at BET groupBy level.
    /// </summary>
    [JsonPropertyName("placedDate")]
    public DateTime? PlacedDate { get; init; }

    /// <summary>
    /// The turn in play persistence state of the order at bet placement time. 
    /// This field will be empty or omitted on true SP bets. Only available at BET groupBy level.
    /// </summary>
    [JsonPropertyName("persistenceType")]
    public PersistenceTypeEnum PersistenceType { get; init; }

    /// <summary>
    /// The type of bet (e.g standard limited-liability Exchange bet (LIMIT), 
    /// a standard BSP bet (MARKET_ON_CLOSE), or a minimum-accepted-price BSP bet 
    /// (LIMIT_ON_CLOSE)). If the bet has a OrderType of MARKET_ON_CLOSE and a 
    /// persistenceType of MARKET_ON_CLOSE then it is a bet which has transitioned 
    /// from LIMIT to MARKET_ON_CLOSE. Only available at BET groupBy level.
    /// </summary>
    [JsonPropertyName("orderType")]
    public OrderTypeEnum OrderType { get; init; }

    /// <summary>
    /// Whether the bet was a back or lay bet. 
    /// Available at SIDE groupBy level or lower.
    /// </summary>
    [JsonPropertyName("side")]
    public SideEnum Side { get; init; }

    /// <summary>
    /// A container for all the ancillary data and localised text valid for this Item
    /// </summary>
    [JsonPropertyName("itemDescription")]
    public ItemDescription? ItemDescription { get; init; }

    /// <summary>
    /// The settlement outcome of the bet. 
    /// Tri-state (WIN/LOSE/PLACE) to account for Each Way bets where the place portion 
    /// of the bet won but the win portion lost. The profit/loss amount in this case could
    /// be positive or negative depending on the price matched at. Only available at BET groupBy level.
    /// </summary>
    [JsonPropertyName("betOutcome")]
    public string BetOutcome { get; init; } = string.Empty;

    /// <summary>
    /// The average requested price across all settled bet orders under this Item. Available at SIDE 
    /// groupBy level or lower. For LINE markets this is the line position requested. For LINE markets 
    /// this is the line position requested. 
    /// </summary>
    [JsonPropertyName("priceRequested")]
    public double Price { get; init; }

    /// <summary>
    /// The date and time the bet order was settled by Betfair. 
    /// Available at SIDE groupBy level or lower.
    /// </summary>
    [JsonPropertyName("settledDate")]
    public DateTime? SettledDate { get; init; }

    /// <summary>
    /// The date and time the last bet order was matched by Betfair. Available on Settled orders only.
    /// </summary>
    [JsonPropertyName("lastMatchedDate")]
    public DateTime? LastMatchedDate { get; init; }

    /// <summary>
    /// The number of actual bets within this grouping (will be 1 for BET groupBy)
    /// </summary>
    [JsonPropertyName("betCount")]
    public int BetCount { get; init; }

    /// <summary>
    /// The cumulative amount of commission paid by the customer across all bets under this Item, 
    /// in the account currency. Available at EXCHANGE, EVENT_TYPE, EVENT and MARKET level groupings only.
    /// </summary>
    [JsonPropertyName("commission")]
    public double Commission { get; init; }

    /// <summary>
    /// The average matched price across all settled bets or bet fragments under this Item. 
    /// Available at SIDE groupBy level or lower. For LINE markets this is the line position matched at. 
    /// </summary>
    [JsonPropertyName("priceMatched")]
    public double PriceMatched { get; init; }

    /// <summary>
    /// If true, then the matched price was affected by a reduction factor due to of a runner 
    /// removal from this Horse Racing market.
    /// </summary>
    [JsonPropertyName("priceReduced")]
    public bool PriceReduced { get; init; }

    /// <summary>
    /// The cumulative bet size that was settled as matched or voided under this Item, 
    /// in the account currency. Available at SIDE groupBy level or lower.
    /// </summary>
    [JsonPropertyName("sizeSettled")]
    public double SizeSettled { get; init; }

    /// <summary>
    /// The profit or loss (negative profit) gained on this line, in the account currency
    /// </summary>
    [JsonPropertyName("profit")]
    public double Profit { get; init; }

    /// <summary>
    /// The amount of the bet that was available to be matched, before cancellation or lapsing, in the account currency.
    /// </summary>
    [JsonPropertyName("sizeCancelled")]
    public double SizeCancelled { get; init; }

    /// <summary>
    /// The order reference defined by the customer for the bet order.
    /// </summary>
    [JsonPropertyName("customerOrderRef")]
    public string CustomerOrderRef { get; init; } = string.Empty;

    /// <summary>
    /// The strategy reference defined by the customer for the bet order.
    /// </summary>
    [JsonPropertyName("customerStrategyRef")]
    public string CustomerStrategyRef { get; init; } = string.Empty;

}
