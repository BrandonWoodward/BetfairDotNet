namespace BetfairDotNet.Enums.Betting;


public enum GroupByEnum {

    /// <summary>
    /// A roll up of settled P&L, commission paid and number of bet orders, on a specified event type.
    /// </summary>
    EVENT_TYPE,

    /// <summary>
    /// A roll up of settled P&L, commission paid and number of bet orders, on a specified event
    /// </summary>
    EVENT,

    /// <summary>
    /// A roll up of settled P&L, commission paid and number of bet orders, on a specified market
    /// </summary>
    MARKET,

    /// <summary>
    /// An averaged roll up of settled P&L, and number of bets, on the specified side of a 
    /// specified selection within a specified market, that are either settled or voided
    /// </summary>
    SIDE,

    /// <summary>
    /// The P&L, side and regulatory information etc, about each individual bet order.
    /// </summary>
    BET
}
