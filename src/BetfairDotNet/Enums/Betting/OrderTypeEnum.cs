namespace BetfairDotNet.Enums.Betting;


public enum OrderTypeEnum {

    /// <summary>
    /// A normal exchange limit order for immediate execution.
    /// </summary>
    LIMIT,

    /// <summary>
    /// Limit order for the auction (SP)
    /// </summary>
    LIMIT_ON_CLOSE,

    /// <summary>
    /// Market order for the auction (SP)
    /// </summary>
    MARKET_ON_CLOSE
}
