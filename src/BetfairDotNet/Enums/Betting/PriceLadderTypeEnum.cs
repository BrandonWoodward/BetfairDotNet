namespace BetfairDotNet.Enums.Betting;


public enum PriceLadderTypeEnum {

    /// <summary>
    /// Price ladder increments traditionally used for Odds Markets.
    /// </summary>
    CLASSIC,

    /// <summary>
    /// Price ladder with the finest available increment, traditionally used for 
    /// Asian Handicap markets.
    /// </summary>
    FINEST,

    /// <summary>
    /// Price ladder used for LINE markets. Refer to MarketLineRangeInfo for more details.
    /// </summary>
    LINE_RANGE
}
