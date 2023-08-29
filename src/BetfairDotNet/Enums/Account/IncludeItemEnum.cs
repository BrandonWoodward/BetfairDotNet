namespace BetfairDotNet.Enums.Account;

public enum IncludeItemEnum {

    /// <summary>
    /// Include all items.
    /// </summary>
    ALL,

    /// <summary>
    /// Include payments only.
    /// </summary>
    DEPOSITS_WITHDRAWALS,

    /// <summary>
    /// Include exchange bets only.
    /// </summary>
    EXCHANGE,

    /// <summary>
    /// Include poker transactions only.
    /// </summary>
    POKER_ROOM,
}
