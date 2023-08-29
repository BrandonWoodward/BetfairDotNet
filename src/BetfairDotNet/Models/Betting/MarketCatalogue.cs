using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Information about a market
/// </summary>
public sealed record MarketCatalogue {

    /// <summary>
    /// The unique identifier for the market. MarketId's are prefixed with '1.'
    /// </summary>
    [JsonPropertyName("marketId"), JsonRequired]
    public required string MarketId { get; init; }

    /// <summary>
    /// The name of the market
    /// </summary>
    [JsonPropertyName("marketName"), JsonRequired]
    public required string MarketName { get; init; }

    /// <summary>
    /// The time this market starts at.
    /// Only returned when the MARKET_START_TIME enum is passed in the marketProjections
    /// </summary>
    [JsonPropertyName("marketStartTime")]
    public DateTime? MarketStartTime { get; init; }

    /// <summary>
    /// Details about the market.
    /// </summary>
    [JsonPropertyName("description")]
    public MarketDescription? Description { get; init; }

    /// <summary>
    /// The total amount of money matched on the market.  
    /// The returned value is cached. For the live total matched value use listMarketBook.
    /// </summary>
    [JsonPropertyName("totalMatched")]
    public double TotalMatched { get; init; }

    /// <summary>
    /// The runners (selections) contained in the market.
    /// </summary>
    [JsonPropertyName("runners")]
    public List<RunnerCatalog> Runners { get; init; } = new();

    /// <summary>
    /// The Event Type the market is contained within.
    /// </summary>
    [JsonPropertyName("eventType")]
    public EventType? EventType { get; init; }

    /// <summary>
    /// The competition the market is contained within. 
    /// Usually only applies to Football competitions
    /// </summary>
    [JsonPropertyName("competition")]
    public Competition? Competition { get; init; }

    /// <summary>
    /// The event the market is contained within.
    /// </summary>
    [JsonPropertyName("event")]
    public Event? Event { get; init; }
}
