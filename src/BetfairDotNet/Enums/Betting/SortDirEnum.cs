﻿using BetfairDotNet.Converters;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Enums.Betting;


[JsonConverter(typeof(EmptyStringToEnumConverter<SortDirEnum>))]
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
