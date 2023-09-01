using BetfairDotNet.Converters;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Enums.Betting;


[JsonConverter(typeof(CustomStringToEnumConverter<RollupModelEnum>))]
public enum RollupModelEnum {

    /// <summary>
    /// The volumes will be rolled up to the minimum value which is >= rollupLimit.
    /// </summary>
    STAKE,

    /// <summary>
    /// The volumes will be rolled up to the minimum value where the payout( price * volume ) is >= rollupLimit. 
    /// On a LINE market, volumes will be rolled up where payout( 2.0 * volume ) is >= rollupLimit
    /// </summary>
    PAYOUT,

    /// <summary>
    /// The volumes will be rolled up to the minimum value which is >= rollupLimit, until a lay price threshold. 
    /// There after, the volumes will be rolled up to the minimum value such that the liability >= a minimum liability. 
    /// Not supported as yet.
    /// </summary>
    MANAGED_LIABILITY,

    /// <summary>
    /// No rollup will be applied. However the volumes will be filtered by currency specific 
    /// minimum stake unless overridden specifically for the channel.
    /// </summary>
    NONE
}
