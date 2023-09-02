using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Streaming;


/// <summary>
/// The base response message type.
/// </summary>
public abstract record BaseMessage {

    /// <summary>
    /// The operation type
    /// </summary>
    [JsonPropertyName("op"), JsonRequired]
    public string Operation { get; init; }

    /// <summary>
    /// Client generated unique id to link request with response (like json rpc)    
    /// /// </summary>
    [JsonPropertyName("id")]
    public int? Id { get; set; }


    public BaseMessage(string operation) {
        Operation = operation;
    }
}
