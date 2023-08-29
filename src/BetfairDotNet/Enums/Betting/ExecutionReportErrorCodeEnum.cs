namespace BetfairDotNet.Enums.Betting;


public enum ExecutionReportErrorCodeEnum {

    /// <summary>
    /// The matcher is not healthy. 
    /// Please note: The error will also be returned is you attempt concurrent 'cancel all' bets 
    /// requests using cancelOrders which isn't permitted.
    /// </summary>
    ERROR_IN_MATCHER,

    /// <summary>
    /// The order itself has been accepted, but at least one (possibly all) actions have generated errors.
    /// </summary>
    PROCESSED_WITH_ERRORS,

    /// <summary>
    /// There is an error with an action that has caused the entire order to be rejected. 
    /// Check the instructionReports errorCode for the reason for the rejection of the order.
    /// </summary>
    BET_ACTION_ERROR,

    /// <summary>
    /// Order rejected due to the account's status (suspended, inactive, dup cards).
    /// </summary>
    INVALID_ACCOUNT_STATE,

    /// <summary>
    /// Order rejected due to the account's wallet's status.
    /// </summary>
    INVALID_WALLET_STATUS,

    /// <summary>
    /// Account has exceeded its exposure limit or available to bet limit.
    /// </summary>
    INSUFFICIENT_FUNDS,

    /// <summary>
    /// The account has exceed the self imposed loss limit.
    /// </summary>
    LOSS_LIMIT_EXCEEDED,

    /// <summary>
    /// Market is suspended
    /// </summary>
    MARKET_SUSPENDED,

    /// <summary>
    /// Market is not open for betting. It is either not yet active, suspended or closed awaiting settlement.
    /// </summary>
    MARKET_NOT_OPEN_FOR_BETTING,

    /// <summary>
    /// Duplicate customer reference data submitted. 
    /// Please note: There is a time window associated with the de-duplication of duplicate submissions which is 60 second
    /// </summary>
    DUPLICATE_TRANSACTION,

    /// <summary>
    /// Order cannot be accepted by the matcher due to the combination of actions. 
    /// For example, bets being edited are not on the same market, or order includes both edits and placement
    /// </summary>
    INVALID_ORDER,

    /// <summary>
    /// Market does not exist
    /// </summary>
    INVALID_MARKET_ID,

    /// <summary>
    /// Business rules do not allow order to be placed. 
    /// You are either attempting to place the order using a Delayed Application Key 
    /// or from a restricted jurisdiction (i.e. USA)
    /// </summary>
    PERMISSION_DENIED,

    /// <summary>
    /// Duplicate bet ids found. 
    /// For example, you've included the same betId more than once in a single cancelOrders request.
    /// </summary>
    DUPLICATE_BETIDS,

    /// <summary>
    /// Order hasn't been passed to matcher as system detected there will be no state change.
    /// </summary>
    NO_ACTION_REQUIRED,

    /// <summary>
    /// The requested service is unavailable.
    /// </summary>
    SERVICE_UNAVAILABLE,

    /// <summary>
    /// The regulator rejected the order. On the Italian Exchange this error will occur 
    /// if more than 50 bets are sent in a single placeOrders request.
    /// </summary>
    REJECTED_BY_REGULATOR,

    /// <summary>
    /// A specific error code that relates to Spanish Exchange markets only which 
    /// indicates that the bet placed contravenes the Spanish regulatory rules relating to loss chasing.
    /// </summary>
    NO_CHASING,

    /// <summary>
    /// The underlying regulator service is not available.
    /// </summary>
    REGULATOR_IS_NOT_AVAILABLE,

    /// <summary>
    /// The amount of orders exceeded the maximum amount allowed to be executed.
    /// </summary>
    TOO_MANY_INSTRUCTIONS,

    /// <summary>
    /// The supplied market version is invalid. Max length allowed for market version is 12.
    /// </summary>
    INVALID_MARKET_VERSION,

    /// <summary>
    /// The order falls outside the permitted price and size combination.
    /// </summary>
    INVALID_PROFIT_RATIO,
}
