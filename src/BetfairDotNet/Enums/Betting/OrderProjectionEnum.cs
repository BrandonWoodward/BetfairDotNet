using BetfairDotNet.Converters;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Enums.Betting;


[JsonConverter(typeof(CustomStringToEnumConverter<OrderProjectionEnum>))]
public enum OrderProjectionEnum {

    /// <summary>
    /// EXECUTABLE and EXECUTION_COMPLETE orders
    /// </summary>
    ALL,

    /// <summary>
    /// An order that has a remaining unmatched portion. This is either a fully unmatched or partially matched bet (order).
    /// </summary>
    EXECUTABLE,

    /// <summary>
    /// An order that does not have any remaining unmatched portion.  This is a fully matched bet (order).
    /// </summary>
    EXECUTION_COMPLETE
}
