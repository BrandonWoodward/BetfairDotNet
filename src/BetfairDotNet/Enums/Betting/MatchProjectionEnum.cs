using BetfairDotNet.Converters;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Enums.Betting;


[JsonConverter(typeof(CustomStringToEnumConverter<MatchProjectionEnum>))]
public enum MatchProjectionEnum {

    /// <summary>
    /// No rollup, return raw fragments
    /// </summary>
    NO_ROLLUP,

    /// <summary>
    /// Rollup matched amounts by distinct matched prices per side.
    /// </summary>
    ROLLED_UP_BY_PRICE,

    /// <summary>
    /// Rollup matched amounts by average matched price per side
    /// </summary>
    ROLLED_UP_BY_AVG_PRICE
}
