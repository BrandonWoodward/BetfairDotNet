using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Instruction to place a new order.
/// </summary>
public sealed class PlaceInstruction {

    /// <summary>
    /// See <see cref="OrderTypeEnum"/> for valid values."/>
    /// </summary>
    [JsonPropertyName("orderType"), JsonRequired]
    public OrderTypeEnum OrderType { get; set; }

    /// <summary>
    /// The selection id to place the bet on.
    /// </summary>
    [JsonPropertyName("selectionId"), JsonRequired]
    public long SelectionId { get; set; }

    /// <summary>
    /// The handicap associated with the runner in case of Asian handicap markets 
    /// (e.g. marketTypes ASIAN_HANDICAP_DOUBLE_LINE, ASIAN_HANDICAP_SINGLE_LINE) null otherwise.
    /// </summary>
    [JsonPropertyName("handicap")]
    public double? Handicap { get; set; }

    /// <summary>
    /// BACK or LAY
    /// </summary>
    [JsonPropertyName("side"), JsonRequired]
    public SideEnum Side { get; set; }

    /// <summary>
    /// A simple exchange bet for immediate execution.
    /// </summary>
    [JsonPropertyName("limitOrder")]
    public LimitOrder? LimitOrder { get; set; }

    /// <summary>
    /// Bets are matched if, and only if, the returned starting price is better than a specified price.
    /// <para>In the case of back bets, LOC bets are matched if the calculated starting price is greater 
    /// than the specified price. In the case of lay bets, LOC bets are matched if the starting price 
    /// is less than the specified price.</para>
    /// <para>If the specified limit is equal to the starting price, then it may be matched, partially matched, 
    /// or may not be matched at all, depending on how much is needed to balance all bets against each other 
    /// (MOC, LOC and normal exchange bets).</para>
    /// </summary>
    [JsonPropertyName("limitOnCloseOrder")]
    public LimitOnCloseOrder? LimitOnCloseOrder { get; set; }

    /// <summary>
    /// <para>Bets remain unmatched until the market is reconciled. </para> 
    /// <para>They are matched and settled at a price that is representative of the market at the 
    /// point the market is turned in-play.</para>
    /// <para>The market is reconciled to find a starting price and MOC bets are settled at whatever starting price is returned. /para> 
    /// <para>MOC bets are always matched and settled, unless a starting price is not available for the selection. Market on 
    /// Close bets can only be placed before the starting price is determined</para> 
    /// </summary>
    [JsonPropertyName("marketOnCloseOrder")]
    public MarketOnCloseOrder? MarketOnCloseOrder { get; set; }


    /// <summary>
    /// An optional reference customers can set to identify instructions. 
    /// No validation will be done on uniqueness and the string is limited to 32 characters.
    /// <para>If an empty string is provided it will be treated as null.</para>
    /// </summary>
    [JsonPropertyName("customerOrderRef")]
    public string CustomerOrderRef { get; set; } = string.Empty;
}
