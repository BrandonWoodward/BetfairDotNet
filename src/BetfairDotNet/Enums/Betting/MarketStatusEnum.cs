using BetfairDotNet.Converters;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Enums.Betting;


[JsonConverter(typeof(EmptyStringToEnumConverter<MarketStatusEnum>))]
public enum MarketStatusEnum {

    /// <summary>
    /// An unknown market state.
    /// </summary>
    UNKNOWN,

    /// <summary>
    /// The market has been created but isn't yet available.
    /// </summary>
    INACTIVE,

    /// <summary>
    /// The market is open for betting.
    /// </summary>
    OPEN,

    /// <summary>
    /// The market is suspended and not available for betting.
    /// </summary>
    SUSPENDED,

    /// <summary>
    /// The market has been settled and is no longer available for betting.
    /// </summary>
    CLOSED
}
