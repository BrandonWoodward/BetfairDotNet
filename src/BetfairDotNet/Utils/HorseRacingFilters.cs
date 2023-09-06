using BetfairDotNet.Models.Betting;

namespace BetfairDotNet.Utils;

public static class HorseRacingFilters {

    /// <summary>
    /// All UK and Irish horse racing markets for today.
    /// </summary>
    /// <returns></returns>
    public static MarketFilter GBAndIRE(int days) {
        return new MarketFilter {
            EventTypeIds = new List<string> { "7" },
            MarketCountries = new List<string> { "GB", "IE" },
            MarketStartTime = new TimeRange {
                From = DateTime.UtcNow,
                To = DateTime.UtcNow.Date.AddDays(days).AddTicks(-1)
            }
        };
    }


    /// <summary>
    /// All UK and Irish horse racing markets for today (Win Only).
    /// </summary>
    /// <returns></returns>
    public static MarketFilter GBAndIREWinOnly(int days) {
        return new MarketFilter {
            EventTypeIds = new List<string> { "7" },
            MarketCountries = new List<string> { "GB", "IE" },
            MarketTypeCodes = new List<string> { "WIN" },
            MarketStartTime = new TimeRange {
                From = DateTime.UtcNow,
                To = DateTime.UtcNow.Date.AddDays(days).AddTicks(-1)
            }
        };
    }
}
