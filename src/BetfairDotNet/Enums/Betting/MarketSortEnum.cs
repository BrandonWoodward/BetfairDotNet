using BetfairDotNet.Converters;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Enums.Betting;


[JsonConverter(typeof(EmptyStringToEnumConverter<MarketSortEnum>))]
public enum MarketSortEnum {

    /// <summary>
    /// Minimum traded volume.
    /// </summary>
    MINIMUM_TRADED,

    /// <summary>
    /// Maximum traded volume.
    /// </summary>
    MAXIMUM_TRADED,

    /// <summary>
    /// Minimum available to match.
    /// </summary>
    MINIMUM_AVAILABLE,

    /// <summary>
    /// Maximum available to match.
    /// </summary>
    MAXIMUM_AVAILABLE,

    /// <summary>
    /// The closest markets based on their expected start time.
    /// </summary>
    FIRST_TO_START,

    /// <summary>
    /// The most distant markets based on their expected start time.
    /// </summary>
    LAST_TO_START,
}
