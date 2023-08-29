namespace BetfairDotNet.Enums.Betting;


public enum SortDirEnum {

    /// <summary>
    /// Order from earliest value to latest e.g. lowest betId is first in the results.
    /// </summary>
    EARLIEST_TO_LATEST,

    /// <summary>
    /// Order from the latest value to the earliest e.g. highest betId is first in the results.
    /// </summary>
    LATEST_TO_EARLIEST
}
