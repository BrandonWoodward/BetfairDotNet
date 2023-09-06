
using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Streaming;


public sealed record MarketSubscription : BaseMessage {

    /// <summary>
    /// Token value delta (received in MarketChangeMessage) that should be passed to resume a subscription
    /// </summary>
    [JsonPropertyName("clk")]
    public string? Clk { get; set; }

    /// <summary>
    /// The heartbeat rate (looped back on initial image after validation: bounds are 500 to 30000).
    /// Internal use only.
    /// </summary>
    [JsonPropertyName("heartbeatMs"), JsonRequired]
    public int HeartbeatMs { get; set; } = 500;

    /// <summary>
    /// Token value (received in initial MarketChangeMessage) that should be passed to resume a subscription
    /// </summary>
    [JsonPropertyName("initialClk")]
    public string? InitialClk { get; set; }

    /// <summary>
    /// Gets or Sets MarketFilter
    /// </summary>
    [JsonPropertyName("marketFilter"), JsonRequired]
    public StreamingMarketFilter MarketFilter { get; set; }

    /// <summary>
    /// The conflation rate (looped back on initial image after validation: bounds are 0 to 120000)
    /// </summary>
    [JsonPropertyName("conflateMs"), JsonRequired]
    public long ConflateMs { get; set; }

    /// <summary>
    /// Filter for fields to include
    /// </summary>
    [JsonPropertyName("marketDataFilter"), JsonRequired]
    public StreamingMarketDataFilter MarketDataFilter { get; set; }


    public MarketSubscription(
        StreamingMarketFilter marketFilter,
        StreamingMarketDataFilter marketDataFilter,
        long conflateMs = 0) : base("marketSubscription") {

        MarketFilter = marketFilter;
        MarketDataFilter = marketDataFilter;
        ConflateMs = conflateMs;
    }
}
