using BetfairDotNet.Converters;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Enums.Account;

[JsonConverter(typeof(CustomStringToEnumConverter<IncludeItemEnum>))]
public enum IncludeItemEnum {

    /// <summary>
    /// The default value.
    /// </summary>
    NONE,

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
