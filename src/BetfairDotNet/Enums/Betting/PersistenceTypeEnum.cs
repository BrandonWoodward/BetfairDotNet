using BetfairDotNet.Converters;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Enums.Betting;


[JsonConverter(typeof(PersistenceTypeEnumConverter))]
public enum PersistenceTypeEnum 
{
    /// <summary>
    /// Lapse (cancel) the order automatically when the market is turned in play if the bet is unmatched.
    /// </summary>
    LAPSE,

    /// <summary>
    /// Persist the unmatched order to in-play. 
    /// The bet will be placed automatically into the in-play market at the start of the event. 
    /// Once in play, the bet won't be cancelled by Betfair if a material event takes place 
    /// and will be available until matched or cancelled by the user.
    /// </summary>
    PERSIST,

    /// <summary>
    /// Put the order into the auction (SP) at turn-in-play
    /// </summary>
    MARKET_ON_CLOSE
}
