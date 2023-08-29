namespace BetfairDotNet.Enums.Streaming;



public enum MarketPriceFilterEnum {

    /// <summary>
    /// Depth based ladders, including virtual prices. This is what you will see on the exchange website.
    /// </summary>
    EX_BEST_OFFERS_DISP,

    /// <summary>
    /// Depth based ladders, no virtual bets.
    /// </summary>
    EX_BEST_OFFERS,

    /// <summary>
    /// Price based ladders, no virtual bets.
    /// </summary>
    EX_ALL_OFFERS,

    /// <summary>
    /// Traded volume by distinct price. 
    /// </summary>
    EX_TRADED,

    /// <summary>
    /// 
    /// </summary>
    EX_TRADED_VOL,

    /// <summary>
    /// Last traded price.
    /// </summary>
    EX_LTP,

    /// <summary>
    /// Metadata about the market including status of the runners etc.
    /// </summary>
    EX_MARKET_DEF,

    /// <summary>
    /// Traded volume by distinct price for the SP auction.
    /// </summary>
    SP_TRADED,

    /// <summary>
    /// 
    /// </summary>
    SP_PROJECTED
}
