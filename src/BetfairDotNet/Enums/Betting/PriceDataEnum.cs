using BetfairDotNet.Converters;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Enums.Betting;


[JsonConverter(typeof(EmptyStringToEnumConverter<PriceDataEnum>))]
public enum PriceDataEnum {

    /// <summary>
    /// Amount available for the BSP auction.
    /// </summary>
    SP_AVAILABLE,

    /// <summary>
    /// Amount traded in the BSP auction.
    /// </summary>
    SP_TRADED,

    /// <summary>
    /// Only the best prices available for each runner, to requested price depth.
    /// </summary>
    EX_BEST_OFFERS,

    /// <summary>
    /// EX_ALL_OFFERS trumps EX_BEST_OFFERS if both settings are present.
    /// </summary>
    EX_ALL_OFFERS,

    /// <summary>
    /// Amount traded on the exchange.
    /// </summary>
    EX_TRADED
}
