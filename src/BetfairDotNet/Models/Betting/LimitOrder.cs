using BetfairDotNet.Enums.Betting;
using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Place a new LIMIT order (simple exchange bet for immediate execution)
/// </summary>
public sealed class LimitOrder {

    /// <summary>
    /// The size of the bet. 
    /// For market type EACH_WAY. The total stake = size x 2
    /// </summary>
    [JsonPropertyName("size"), JsonRequired]
    public required double Size { get; set; }

    /// <summary>
    /// The limit price. 
    /// <para>For LINE markets, the price at which the bet is settled and struck will always be 2.0 (Evens). 
    /// On these bets, the Price field is used to indicate the line value which is being bought or sold.</para>
    /// </summary>
    [JsonPropertyName("price"), JsonRequired]
    public required double Price { get; set; }

    /// <summary>
    /// What to do with the order at turn-in-play.
    /// </summary>
    [JsonPropertyName("persistenceType"), JsonRequired]
    public required PersistenceTypeEnum PersistenceType { get; set; }

    /// <summary>
    /// The type of TimeInForce value to use. This value takes precedence over any PersistenceType value chosen. 
    /// <para> If this attribute is populated along with the PersistenceType field, then the PersistenceType will be ignored.</para> 
    /// <para> When using FILL_OR_KILL for a Line market the Volume Weighted Average Price(VWAP) functionality is disabled. </para> 
    /// </summary>
    [JsonPropertyName("timeInForce")]
    public TimeInForceEnum? TimeInForce { get; set; }

    /// <summary>
    /// An optional field used if the TimeInForce attribute is populated.
    /// If specified without TimeInForce then this field is ignored.
    /// <para>If no minFillSize is specified, the order is killed unless the entire size can be matched. </para>
    /// <para>If minFillSize is specified, the order is killed unless at least the minFillSize can be matched.
    /// The minFillSize cannot be greater than the order's size. </para> 
    /// <para>If specified for a BetTargetType and FILL_OR_KILL order, then this value will be ignored</para> 
    /// </summary>
    [JsonPropertyName("minFillSize")]
    public double? MinFillSize { get; set; }

    /// <summary>
    /// An optional field to allow betting to a targeted PAYOUT or BACKERS_PROFIT. 
    /// It's invalid to specify both a Size and BetTargetType 
    /// <para>Matching provides best execution at the requested price or better up to the payout or profit.
    /// If the bet is not matched completely and immediately, the remaining portion enters the unmatched pool of bets on the exchange.</para>
    /// BetTargetType bets are invalid for LINE markets
    /// </summary>
    [JsonPropertyName("betTargetType")]
    public BetTargetTypeEnum? BetTargetType { get; set; }

    /// <summary>
    /// An optional field which MUST be specified if BetTargetType is specified for this order 
    /// <para> The requested outcome size of either the payout or profit.
    /// This is named from the backer's perspective. For Lay bets the profit represents the bet's liability.</para>
    /// </summary>
    [JsonPropertyName("betTargetSize")]
    public double? BetTargetSize { get; set; }
}
