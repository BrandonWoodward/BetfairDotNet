using BetfairDotNet.Enums.Streaming;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Streaming;


internal sealed record OrderChangeMessage : BaseMessage {

    /// <summary>
    /// Set to indicate the type of change - if null this is a delta)
    /// </summary>
    [JsonPropertyName("ct")]
    public ChangeTypeEnum ChangeType { get; init; }

    /// <summary>
    /// If the change is split into multiple segments, this denotes the beginning and end of a change, 
    /// and segments in between. Will be null if data is not segmented.
    /// </summary>
    [JsonPropertyName("segmentType")]
    public SegmentTypeEnum SegmentType { get; init; }

    /// <summary>
    /// Token value (non-null) should be stored and passed in a MarketSubscriptionMessage to resume subscription (in case of disconnect)
    /// </summary>
    [JsonPropertyName("clk")]
    public string? Clk { get; init; }

    /// <summary>
    /// The heartbeat rate (may differ from requested: bounds are 500 to 30000)
    /// </summary>
    [JsonPropertyName("heartbeatMs")]
    public long HeartbeatMs { get; init; }

    /// <summary>
    /// Publish Time (in millis since epoch) that the changes were generated
    /// </summary>
    [JsonPropertyName("pt")]
    public long PublishTime { get; init; }

    /// <summary>
    /// OrderMarketChanges - the modifications to orders for given account (will be null on a heartbeat)
    /// </summary>
    [JsonPropertyName("oc")]
    public List<OrderMarketChange> OrderChanges { get; init; } = new();

    /// <summary>
    /// Token value (non-null) should be stored and passed in a MarketSubscriptionMessage to resume subscription (in case of disconnect)
    /// </summary>
    [JsonPropertyName("initialClk")]
    public string? InitialClk { get; init; }

    /// <summary>
    /// The conflation rate (may differ from that requested if subscription is delayed)
    /// </summary>
    [JsonPropertyName("conflateMs")]
    public long ConflateMs { get; init; }


    public OrderChangeMessage() : base("oc") { }
}
