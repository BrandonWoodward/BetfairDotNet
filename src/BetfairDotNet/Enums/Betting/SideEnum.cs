using BetfairDotNet.Converters;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Enums.Betting;


[JsonConverter(typeof(EmptyStringToEnumConverter<SideEnum>))]
public enum SideEnum {

    /// <summary>
    /// To back a team, horse or outcome is to bet on the selection to win. 
    /// For LINE markets a Back bet refers to a SELL line. 
    /// A SELL line will win if the outcome is LESS THAN the taken line (price)  
    /// </summary>
    BACK,

    /// <summary>
    /// To lay a team, horse, or outcome is to bet on the selection to lose. 
    /// For LINE markets a Lay bet refers to a BUY line. 
    /// A BUY line will win if the outcome is MORE THAN the taken line (price) 
    /// </summary>
    LAY
}
