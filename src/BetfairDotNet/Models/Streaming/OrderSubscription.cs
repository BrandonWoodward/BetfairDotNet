using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Streaming;

/// <summary>
/// Message sent for Betfair ESA order subscription operation.
/// </summary>
public sealed record OrderSubscription : BaseMessage {

    /// <summary>
    /// Allow the server to send large sets of data in segments, instead of a single block
    /// </summary>
    [JsonPropertyName("segmentationEnabled"), JsonRequired]
    public bool SegmentationEnabled { get; set; }

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
    /// Heartbeat Milliseconds - the heartbeat rate (looped back on initial image after validation: bounds are 500 to 30000)
    /// </summary>
    [JsonPropertyName("heartbeatMs"), JsonRequired]
    public long HeartbeatMs { get; set; }

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
        long heartbeatMs = 30000,
        long conflateMs = 0) : base("orderSubscription") {

        OrderFilter = orderFilter;
        HeartbeatMs = heartbeatMs;
        ConflateMs = conflateMs;
    }
}
