﻿using BetfairDotNet.Converters;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Enums.Betting;


[JsonConverter(typeof(EmptyStringToEnumConverter<MarketProjectionEnum>))]
public enum MarketProjectionEnum {

    /// <summary>
    /// If not selected then the competition will not be returned with marketCatalogue
    /// </summary>
    COMPETITION,

    /// <summary>
    /// If not selected then the event will not be returned with marketCatalogue
    /// </summary>
    EVENT,

    /// <summary>
    /// If not selected then the eventType will not be returned with marketCatalogue
    /// </summary>
    EVENT_TYPE,

    /// <summary>
    /// If not selected then the runners will not be returned with marketCatalogue
    /// </summary>
    RUNNER_DESCRIPTION,

    /// <summary>
    /// If not selected then the start time will not be returned with marketCatalogue
    /// </summary>
    MARKET_START_TIME,

    /// <summary>
    /// If not selected then the description will not be returned with marketCatalogue
    /// </summary>
    MARKET_DESCRIPTION,

    /// <summary>
    /// If not selected then the runner metadata will not be returned with marketCatalogue. 
    /// If selected then RUNNER_DESCRIPTION will also be returned regardless of 
    /// whether it is included as a market projection.
    /// </summary>
    RUNNER_METADATA
}
