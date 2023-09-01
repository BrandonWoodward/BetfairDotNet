﻿using BetfairDotNet.Converters;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Enums.Betting;


[JsonConverter(typeof(CustomStringToEnumConverter<TimeGranularityEnum>))]
public enum TimeGranularityEnum {

    /// <summary>
    /// Days.
    /// </summary>
    DAYS,

    /// <summary>
    /// Hours.
    /// </summary>
    HOURS,

    /// <summary>
    /// Minutes.
    /// </summary>
    MINUTES
}
