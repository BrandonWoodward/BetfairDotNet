using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Market Line and Range Info
/// </summary>
public sealed record MarketLineRangeInfo {

    /// <summary>
    /// maxPrice - Maximum value for the outcome, in market units for this market (eg 100 runs)
    /// </summary>
    [JsonPropertyName("maxUnitValue"), JsonRequired]
    public required double MaxUnitValue { get; init; }

    /// <summary>
    /// minPrice - Minimum value for the outcome, in market units for this market (eg 0 runs) 
    /// </summary>
    [JsonPropertyName("minUnitValue"), JsonRequired]
    public required double MinUnitValue { get; init; }

    /// <summary>
    /// interval - The odds ladder on this market will be between the range of minUnitValue and maxUnitValue, 
    /// in increments of the inverval value.e.g. If minUnitValue=10 runs, maxUnitValue=20 runs, 
    /// interval=0.5 runs, then valid odds include 10, 10.5, 11, 11.5 up to 20 runs.
    /// </summary>
    [JsonPropertyName("interval"), JsonRequired]
    public required double Interval { get; init; }

    /// <summary>
    /// The type of unit the lines are incremented in by the interval (e.g: runs, goals or seconds)
    /// </summary>
    [JsonPropertyName("marketUnit"), JsonRequired]
    public required string MarketUnit { get; init; }
}
