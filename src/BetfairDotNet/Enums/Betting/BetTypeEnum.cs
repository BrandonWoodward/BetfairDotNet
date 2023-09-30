using BetfairDotNet.Converters;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Enums.Betting;


[JsonConverter(typeof(EmptyStringToEnumConverter<BetTypeEnum>))]
public enum BetTypeEnum {
    /// <summary>
    /// Order by market id, then placed time, then bet id.
    /// </summary>
    BY_MARKET,

    /// <summary>
    /// Order by time of last matched fragment (if any), then placed time, then bet id. 
    /// Filters out orders which have no matched date. 
    /// The dateRange filter (if specified) is applied to the matched date.
    /// </summary>
    BY_MATCH_TIME,

    /// <summary>
    /// Order by placed time, then bet id. 
    /// This is an alias of to be deprecated BY_BET. 
    /// The dateRange filter (if specified) is applied to the placed date.
    /// </summary>
    BY_PLACE_TIME,

    /// <summary>
    /// Order by time of last settled fragment (if any due to partial market settlement), 
    /// then by last match time, then placed time, then bet id.
    /// Filters out orders which have not been settled. 
    /// The dateRange filter (if specified) is applied to the settled date.
    /// </summary>
    BY_SETTLED_TIME,

    /// <summary>
    /// Order by time of last voided fragment (if any), then by last match time, then placed time, then bet id. 
    /// Filters out orders which have not been voided. 
    /// The dateRange filter (if specified) is applied to the voided date.
    /// </summary>
    BY_VOID_TIME,
}
