using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Streaming;

/// <summary>
/// Message sent for Betfair ESA order subscription operation.
/// </summary>
public sealed record OrderSubscription : BaseMessage {

    /// <summary>
    /// Gets or Sets OrderFilter
    /// </summary>
    [JsonPropertyName("orderFilter")]
    public OrderFilter OrderFilter { get; set; }

    /// <summary>
    /// Token value delta (received in MarketChangeMessage) that should be passed to resume a subscription
    /// </summary>
    [JsonPropertyName("clk")]
    public string? Clk { get; set; }

    /// <summary>
    /// The heartbeat rate (looped back on initial image after validation: bounds are 500 to 30000)
    /// Internal use only
    /// </summary>
    [JsonPropertyName("heartbeatMs"), JsonRequired]
    public int HeartbeatMs { get; set; } = 500;

    /// <summary>
    /// Token value (received in initial MarketChangeMessage) that should be passed to resume a subscription
    /// </summary>
    [JsonPropertyName("initialClk")]
    public string? InitialClk { get; set; }

    /// <summary>
    /// The conflation rate (looped back on initial image after validation: bounds are 0 to 120000)
    /// </summary>
    [JsonPropertyName("conflateMs")]
    public long ConflateMs { get; set; }


    public OrderSubscription(
        OrderFilter orderFilter,
        long conflateMs = 0) : base("orderSubscription") {

        OrderFilter = orderFilter;
        ConflateMs = conflateMs;
    }
}
