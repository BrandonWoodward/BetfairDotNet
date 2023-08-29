using BetfairDotNet.Models.Betting;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Streaming;


public sealed record MarketSubscription : BaseMessage {

    /// <summary>
    /// Allow the server to send large sets of data in segments, instead of a single block
    /// </summary>
    [JsonPropertyName("segmentationEnabled"), JsonRequired]
    public bool SegmentationEnabled { get; set; }

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
    /// Gets or Sets MarketFilter
    /// </summary>
    [JsonPropertyName("marketFilter"), JsonRequired]
    public MarketFilter MarketFilter { get; set; }

    /// <summary>
    /// The conflation rate (looped back on initial image after validation: bounds are 0 to 120000)
    /// </summary>
    [JsonPropertyName("conflateMs"), JsonRequired]
    public long ConflateMs { get; set; }

    /// <summary>
    /// Filter for fields to include
    /// </summary>
    [JsonPropertyName("marketDataFilter"), JsonRequired]
    public MarketDataFilter MarketDataFilter { get; set; }


    public MarketSubscription(
        MarketFilter marketFilter,
        MarketDataFilter marketDataFilter,
        long heartbeatMs = 30000,
        long conflateMs = 0) {

        Operation = "marketSubscription";
        MarketFilter = marketFilter;
        MarketDataFilter = marketDataFilter;
        HeartbeatMs = heartbeatMs;
        ConflateMs = conflateMs;
        SegmentationEnabled = true; // always set for best performance
    }
}
