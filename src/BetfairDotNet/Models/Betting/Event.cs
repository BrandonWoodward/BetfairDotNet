using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// The event metadata.
/// </summary>
public sealed record Event {

    /// <summary>
    /// The unique id for the event.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// The name of the event.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// The ISO-2 code for the event.  
    /// Default value is 'GB' when the correct country code cannot be determined.
    /// </summary>
    [JsonPropertyName("countryCode")]
    public string CountryCode { get; init; } = string.Empty;

    /// <summary>
    /// This is timezone in which the event is taking place.
    /// </summary>
    [JsonPropertyName("timezone")]
    public string Timezone { get; init; } = string.Empty;

    /// <summary>
    /// The venue at which the event is taking place.
    /// </summary>
    [JsonPropertyName("venue")]
    public string Venue { get; init; } = string.Empty;

    /// <summary>
    /// The scheduled start date and time of the event. 
    /// This is Europe/London (GMT) by default.
    /// </summary>
    [JsonPropertyName("openDate")]
    public DateTime? OpenDate { get; init; }

}
