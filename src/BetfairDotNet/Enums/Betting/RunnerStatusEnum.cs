namespace BetfairDotNet.Enums.Betting;


public enum RunnerStatusEnum {

    /// <summary>
    /// Active.
    /// </summary>
    ACTIVE,

    /// <summary>
    /// Winner.
    /// </summary>
    WINNER,

    /// <summary>
    /// Loser.
    /// </summary>
    LOSER,

    /// <summary>
    /// The runner was placed, applies to EACH_WAY marketTypes only.
    /// </summary>
    PLACED,

    /// <summary>
    /// REMOVED_VACANT applies to Greyhounds. 
    /// Greyhound markets always return a fixed number of runners (traps). 
    /// If a dog has been removed, the trap is shown as vacant.
    /// </summary>
    REMOVED_VACANT,

    /// <summary>
    /// Removed.
    /// </summary>
    REMOVED,

    /// <summary>
    /// The selection is hidden from the market.  
    /// This occurs in Horse Racing markets were runners is hidden when it is doesn’t hold an official entry following an entry stage. 
    /// This could be because the horse was never entered or because they have been scratched from a race at a declaration stage. 
    /// All matched customer bet prices are set to 1.0 even if there are later supplementary stages. 
    /// Should it appear likely that a specific runner may actually be supplemented into the race this runner will be 
    /// reinstated with all matched customer bets set back to the original prices.
    /// </summary>
    HIDDEN
}
