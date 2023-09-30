using BetfairDotNet.Converters;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Enums.Heartbeat;


[JsonConverter(typeof(EmptyStringToEnumConverter<ActionPerformedEnum>))]
public enum ActionPerformedEnum {

    /// <summary>
    /// No action was performed since last heartbeat, or this is the first heartbeat.
    /// </summary>
    NONE,

    /// <summary>
    /// A request to cancel all unmatched bets was submitted since last heartbeat.
    /// </summary>
    CANCELLATION_REQUEST_SUBMITTED,

    /// <summary>
    /// All unmatched bets were cancelled since last heartbeat.
    /// </summary>
    ALL_BETS_CANCELLED,

    /// <summary>
    /// Not all unmatched bets were cancelled since last heartbeat
    /// </summary>
    SOME_BETS_NOT_CANCELLED,

    /// <summary>
    /// There was an error requesting cancellation, no bets have been cancelled.
    /// </summary>
    CANCELLATION_REQUEST_ERROR,

    /// <summary>
    /// There was no response from requesting cancellation, cancellation status unknown.
    /// </summary>
    CANCELLATION_STATUS_UNKNOWN,
}
