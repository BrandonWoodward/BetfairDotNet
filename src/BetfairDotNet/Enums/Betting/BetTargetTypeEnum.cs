using BetfairDotNet.Converters;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Enums.Betting;


[JsonConverter(typeof(EmptyStringToEnumConverter<BetTargetTypeEnum>))]
public enum BetTargetTypeEnum {

    /// <summary>
    /// The payout requested minus the calculated size at which this LimitOrder is to be placed. BetTargetType bets are invalid for LINE markets.
    /// i.e Fixed Liability
    /// </summary>
    BACKERS_PROFIT,

    /// <summary>
    /// The total payout requested on a LimitOrder.
    /// </summary>
    PAYOUT,
}
