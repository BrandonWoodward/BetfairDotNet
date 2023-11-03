using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Streaming;


/// <summary>
/// 
/// </summary>
public sealed record ESAOrder 
{
    /// <summary>
    /// BACK or LAY
    /// </summary>
    [JsonPropertyName("side")]
    public SideEnum Side { get; init; }

    /// <summary>
    /// Persistence Type - whether the order will persist at in play or not (L = LAPSE, P = PERSIST, MOC = Market On Close)
    /// </summary>
    [JsonPropertyName("pt")]
    public PersistenceTypeEnum PersistenceType { get; init; }

    /// <summary>
    /// Order Type - the type of the order (L = LIMIT, MOC = MARKET_ON_CLOSE, LOC = LIMIT_ON_CLOSE)
    /// </summary>
    [JsonPropertyName("ot")]
    public OrderTypeEnum OrderType { get; init; }

    /// <summary>
    /// The status of the order (E = EXECUTABLE, EC = EXECUTION_COMPLETE)
    /// </summary>
    [JsonPropertyName("status")]
    public OrderStatusEnum Status { get; init; }

    /// <summary>
    /// The amount of the order that has been voided
    /// </summary>
    [JsonPropertyName("sv")]
    public double SizeVoided { get; init; }

    /// <summary>
    /// The original placed price of the order
    /// </summary>
    [JsonPropertyName("p")]
    public double Price { get; init; }

    /// <summary>
    /// The amount of the order that has been cancelled
    /// </summary>
    [JsonPropertyName("sc")]
    public double? SizeCancelled { get; init; }

    /// <summary>
    /// The regulator of the order
    /// </summary>
    [JsonPropertyName("rc")]
    public string Regulator { get; init; } = string.Empty;

    /// <summary>
    /// The original placed size of the order
    /// </summary>
    [JsonPropertyName("s")]
    public double Size { get; init; }

    /// <summary>
    /// The date the order was placed, returned in ms since epoch
    /// </summary>
    [JsonPropertyName("pd")]
    public long PlacedDate { get; init; }

    /// <summary>
    /// The auth code returned by the regulator
    /// </summary>
    [JsonPropertyName("rac")]
    public string RegulatorAuthCode { get; init; } = string.Empty;

    /// <summary>
    /// The date the order was matched (null if the order is not matched)
    /// </summary>
    [JsonPropertyName("md")]
    public long? MatchedDate { get; init; }

    /// <summary>
    /// The amount of the order that has been lapsed
    /// </summary>
    [JsonPropertyName("sl")]
    public double? SizeLapsed { get; init; }

    /// <summary>
    /// The average price the order was matched at (null if the order is not matched)
    /// </summary>
    [JsonPropertyName("avp")]
    public double? AveragePriceMatched { get; init; }

    /// <summary>
    /// The amount of the order that has been matched
    /// </summary>
    [JsonPropertyName("sm")]
    public double? SizeMatched { get; init; }

    /// <summary>
    /// The id of the order
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// The BSP liability of the order (null if the order is not a BSP order)
    /// </summary>
    [JsonPropertyName("bsp")]
    public double? BspLiability { get; init; }

    /// <summary>
    /// The amount of the order that is remaining unmatched
    /// </summary>
    [JsonPropertyName("sr")]
    public double? SizeRemaining { get; init; }

    /// <summary>
    /// The date the order was cancelled (null if the order is not cancelled)
    /// </summary>
    [JsonPropertyName("cd")]
    public long? CancelledDate { get; init; }
    
    /// <summary>
    /// The customer supplied strategy reference used to group orders together.
    /// </summary>
    [JsonPropertyName("rfs")]
    public string CustomerRefStrategy { get; init; } = string.Empty;
    
    /// <summary>
    /// The customer supplied reference used to identify Order. 
    /// </summary>
    [JsonPropertyName("rfo")]
    public string CustomerOrderReference { get; init; } = string.Empty;
}
