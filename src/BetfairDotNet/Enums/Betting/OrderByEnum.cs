namespace BetfairDotNet.Enums.Betting;



public enum OrderByEnum {

    /// <summary>
    /// Represents the ordering by market ID.
    /// Orders are prioritized by:
    /// <list type="bullet">
    /// <item><description>Market ID</description></item>
    /// <item><description>Placement time</description></item>
    /// <item><description>Bet ID</description></item>
    /// </list>
    /// </summary>
    BY_MARKET,

    /// <summary>
    /// Represents the ordering by the time of the last matched fragment.
    /// Orders are prioritized by:
    /// <list type="bullet">
    /// <item><description>Time of last matched fragment (if any)</description></item>
    /// <item><description>Placement time</description></item>
    /// <item><description>Bet ID</description></item>
    /// </list>
    /// Note: Orders without a matched date are filtered out. If a date range filter is specified, it is applied to the matched date.
    /// </summary>
    BY_MATCH_TIME,

    /// <summary>
    /// Represents the ordering by placement time (alias for the deprecated "BY_BET").
    /// Orders are prioritized by:
    /// <list type="bullet">
    /// <item><description>Placement time</description></item>
    /// <item><description>Bet ID</description></item>
    /// </list>
    /// Note: If a date range filter is specified, it is applied to the placed date.
    /// </summary>
    BY_PLACE_TIME,

    /// <summary>
    /// Represents the ordering by the time of the last settled fragment.
    /// Orders are prioritized by:
    /// <list type="bullet">
    /// <item><description>Time of last settled fragment (if partially settled)</description></item>
    /// <item><description>Last match time</description></item>
    /// <item><description>Placement time</description></item>
    /// <item><description>Bet ID</description></item>
    /// </list>
    /// Note: Orders not yet settled are filtered out. If a date range filter is specified, it is applied to the settled date.
    /// </summary>
    BY_SETTLED_TIME,

    /// <summary>
    /// Represents the ordering by the time of the last voided fragment.
    /// Orders are prioritized by:
    /// <list type="bullet">
    /// <item><description>Time of last voided fragment (if any)</description></item>
    /// <item><description>Last match time</description></item>
    /// <item><description>Placement time</description></item>
    /// <item><description>Bet ID</description></item>
    /// </list>
    /// Note: Orders not voided are filtered out. If a date range filter is specified, it is applied to the voided date.
    /// </summary>
    BY_VOID_TIME,
}
