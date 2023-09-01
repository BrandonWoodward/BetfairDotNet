using BetfairDotNet.Models.Betting;
using System.Diagnostics.CodeAnalysis;

namespace BetfairDotNet.Utils;


/// <summary>
/// A collection of common market filters.
/// </summary>
[ExcludeFromCodeCoverage]
public static class MarketFilterHelpers {

    /// <summary>
    /// A filter for today's horse racing markets in Great Britain and Ireland.
    /// </summary>
    public static MarketFilter TodaysGBAndIREHorseRacing() {
        return new MarketFilter {
            EventTypeIds = new List<string> { "7" },
            MarketCountries = new List<string> { "GB", "IE" },
            MarketStartTime = new TimeRange {
                From = DateTime.UtcNow,
                To = DateTime.UtcNow.Date.AddDays(1).AddTicks(-1)
            }
        };
    }


    /// <summary>
    /// A filter for today's horse racing in Great Britain and Ireland (Win Only).
    /// </summary>
    public static MarketFilter TodaysGBAndIREHorseRacingWinOnly() {
        return new MarketFilter {
            EventTypeIds = new List<string> { "7" },
            MarketCountries = new List<string> { "GB", "IE" },
            MarketTypeCodes = new List<string> { "WIN" },
            MarketStartTime = new TimeRange {
                From = DateTime.UtcNow,
                To = DateTime.UtcNow.Date.AddDays(1).AddTicks(-1)
            }
        };
    }
}
