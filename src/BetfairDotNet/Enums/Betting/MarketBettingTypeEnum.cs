namespace BetfairDotNet.Enums.Betting;


public enum MarketBettingTypeEnum {

    /// <summary>
    /// Odds Market - Any market that doesn't fit any any of the below categories.
    /// </summary>
    ODDS,

    /// <summary>
    /// Line Market - LINE markets operate at even-money odds of 2.0. 
    /// However, price for these markets refers to the line positions available as
    /// defined by the markets min-max range and interval steps. 
    /// Customers either Buy a line (LAY bet, winning if outcome is greater than the 
    /// taken line (price)) or Sell a line (BACK bet, winning if outcome is less than the taken line (price)). 
    /// If settled outcome equals the taken line, stake is returned. 
    /// </summary>
    LINE,

    /// <summary>
    /// Asian Handicap Market - A traditional Asian handicap market. Can be identified by marketType ASIAN_HANDICAP
    /// </summary>
    ASIAN_HANDICAP_DOUBLE_LINE,

    /// <summary>
    /// Asian Single Line Market - A market in which there can be 0 or multiple winners. e,.g marketType TOTAL_GOALS
    /// </summary>
    ASIAN_HANDICAP_SINGLE_LINE,

    /// <summary>
    /// Sportsbook Odds Market. 
    /// This type is deprecated and will be removed in future releases, 
    /// when Sportsbook markets will be represented as ODDS market but with a different product type.
    /// </summary>
    FIXED_ODDS
}
