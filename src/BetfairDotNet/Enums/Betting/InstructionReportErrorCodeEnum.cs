namespace BetfairDotNet.Enums.Betting;


public enum InstructionReportErrorCodeEnum {

    /// <summary>
    /// The bet placed succesfully.
    /// </summary>
    NONE,

    /// <summary>
    /// The bet size was invalid.
    /// </summary>
    INVALID_BET_SIZE,

    /// <summary>
    /// The specified runner does not exist or is no longer applicable for the market.
    /// </summary>
    INVALID_RUNNER,

    /// <summary>
    /// The bet has been taken or has lapsed.
    /// </summary>
    BET_TAKEN_OR_LAPSED,

    /// <summary>
    /// The bet is in progress.
    /// </summary>
    BET_IN_PROGRESS,

    /// <summary>
    /// The runner has been removed from the market.
    /// </summary>
    RUNNER_REMOVED,

    /// <summary>
    /// The market is not currently open for betting.
    /// </summary>
    MARKET_NOT_OPEN_FOR_BETTING,

    /// <summary>
    /// The requested action would exceed the user's loss limit.
    /// </summary>
    LOSS_LIMIT_EXCEEDED,

    /// <summary>
    /// The market is not open for BSP betting.
    /// </summary>
    MARKET_NOT_OPEN_FOR_BSP_BETTING,

    /// <summary>
    /// Attempt to edit a bet with an invalid price.
    /// </summary>
    INVALID_PRICE_EDIT,

    /// <summary>
    /// The odds are invalid.
    /// </summary>
    INVALID_ODDS,

    /// <summary>
    /// The account has insufficient funds.
    /// </summary>
    INSUFFICIENT_FUNDS,

    /// <summary>
    /// The persistence type is invalid.
    /// </summary>
    INVALID_PERSISTENCE_TYPE,

    /// <summary>
    /// There was an error in the matcher component.
    /// </summary>
    ERROR_IN_MATCHER,

    /// <summary>
    /// Invalid combination of back and lay.
    /// </summary>
    INVALID_BACK_LAY_COMBINATION,

    /// <summary>
    /// There was an error with the order.
    /// </summary>
    ERROR_IN_ORDER,

    /// <summary>
    /// The bid type was invalid.
    /// </summary>
    INVALID_BID_TYPE,

    /// <summary>
    /// The bet ID was invalid.
    /// </summary>
    INVALID_BET_ID,

    /// <summary>
    /// The bet was cancelled; it was not placed.
    /// </summary>
    CANCELLED_NOT_PLACED,

    /// <summary>
    /// A related action, such as an order, failed.
    /// </summary>
    RELATED_ACTION_FAILED,

    /// <summary>
    /// No action was required.
    /// </summary>
    NO_ACTION_REQUIRED,

    /// <summary>
    /// You may only specify a time in force on either the place request 
    /// OR on individual limit order instructions (not both), 
    /// since the implied behaviors are incompatible.
    /// </summary>
    TIME_IN_FORCE_CONFLICT,

    /// <summary>
    /// You have specified a persistence type for a FILL_OR_KILL order,
    /// which is nonsensical because no umatched portion 
    /// can remain after the order has been placed.
    /// </summary>
    UNEXPECTED_PERSISTENCE_TYPE,

    /// <summary>
    /// You have specified a time in force of FILL_OR_KILL, but have included a non-LIMIT order type.
    /// </summary>
    INVALID_ORDER_TYPE,

    /// <summary>
    /// You have specified a minFillSize on a limit order, where the limit order's time in force is not FILL_OR_KILL. 
    /// Using minFillSize is not supported where the time in force of the request (as opposed to an order) is FILL_OR_KILL.
    /// </summary>
    UNEXPECTED_MIN_FILL_SIZE,

    /// <summary>
    /// The supplied customer order reference is too long.
    /// </summary>
    INVALID_CUSTOMER_ORDER_REF,

    /// <summary>
    /// The minFillSize must be greater than zero and less than or equal to the order's size. 
    /// The minFillSize cannot be less than the minimum bet size for your currency
    /// </summary>
    INVALID_MIN_FILL_SIZE,

    /// <summary>
    /// Your bet is lapsed. There is better odds than requested available in the market, but your 
    /// preferences don't allow the system to match your bet against better odds. Change your betting 
    /// preferences to accept better odds if you don't want to receive this error.
    /// </summary>
    BET_LAPSED_PRICE_IMPROVEMENT_TOO_LARGE,
}
