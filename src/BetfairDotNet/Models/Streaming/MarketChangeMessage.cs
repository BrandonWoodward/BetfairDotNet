using BetfairDotNet.Enums.Streaming;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Streaming;

/// <summary>
/// Response from a MarketSubscription.
/// </summary>
internal sealed record MarketChangeMessage : BaseMessage {

    /// <summary>
    /// Set to indicate the type of change)
    /// </summary>
    [JsonPropertyName("ct")]
    public ChangeTypeEnum ChangeType { get; init; }

    /// <summary>
    /// If the change is split into multiple segments, this denotes the beginning and end of a change, and segments in between. 
    /// Will be NONE if data is not segmented
    /// </summary>
    [JsonPropertyName("segmentType")]
    public SegmentTypeEnum SegmentType { get; init; }

    /// <summary>
    /// Token value (non-null) should be stored and passed in a MarketSubscriptionMessage 
    /// to resume subscription (in case of disconnect)
    /// </summary>
    [JsonPropertyName("clk")]
    public string Clk { get; init; } = string.Empty;

    /// <summary>
    /// Heartbeat Milliseconds - the heartbeat rate (may differ from requested: bounds are 500 to 30000)
    /// </summary>
    [JsonPropertyName("heartbeatMs")]
    public long? HeartbeatMs { get; init; }

    /// <summary>
    /// Publish Time (in millis since epoch) that the changes were generated
    /// </summary>
    [JsonPropertyName("pt")]
    public long? PublishTime { get; init; }

    /// <summary>
    /// Token value (non-null) should be stored and passed in a MarketSubscriptionMessage to resume subscription (in case of disconnect)
    /// </summary>
    [JsonPropertyName("initialClk")]
    public string InitialClk { get; init; } = string.Empty;

    /// <summary>
    /// MarketChanges - the modifications to markets (will be null on a heartbeat
    /// </summary>
    [JsonPropertyName("mc")]
    public List<MarketChange> MarketChanges { get; init; } = new();

    /// <summary>
    /// The conflation rate (may differ from that requested if subscription is delayed)
    /// </summary>
    [JsonPropertyName("conflateMs")]
    public long? ConflateMs { get; set; }
}
