using BetfairDotNet.Converters;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Enums.Betting;


[JsonConverter(typeof(CustomStringToEnumConverter<BetStatusEnum>))]
public enum BetStatusEnum {

    /// <summary>
    /// The bet is still active and has not been settled.
    /// </summary>
    ACTIVE,

    /// <summary>
    /// A matched bet that was settled normally.
    /// </summary>
    SETTLED,

    /// <summary>
    /// A matched bet that was subsequently voided by Betfair, before, during or after settlement.
    /// </summary>
    VOIDED,

    /// <summary>
    /// Unmatched bet that was cancelled by Betfair (for example at turn in play).
    /// </summary>
    LAPSED,

    /// <summary>
    /// Unmatched bet that was cancelled by an explicit customer action.
    /// </summary>
    CANCELLED,
}
