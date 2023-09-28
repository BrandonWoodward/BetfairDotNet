using BetfairDotNet.Converters;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Streaming;
using System.Text;
using System.Text.Json;

namespace BetfairDotNet.Factories;

internal class ChangeMessageFactory : IChangeMessageFactory {

    public BaseMessage Process(ReadOnlyMemory<byte> input) {
        var operation = GetOperation(input);
        return operation switch {
            "connection" => JsonConvert.Deserialize<ConnectionMessage>(input),
            "status" => JsonConvert.Deserialize<StatusMessage>(input),
            "mcm" => JsonConvert.Deserialize<MarketChangeMessage>(input),
            "ocm" => JsonConvert.Deserialize<OrderChangeMessage>(input),
            _ => throw new InvalidOperationException("Unknown operation"),
        };
    }


    private static string GetOperation(ReadOnlyMemory<byte> message) {
        var reader = new Utf8JsonReader(message.Span);
        while(reader.Read()) {
            if(reader.TokenType != JsonTokenType.PropertyName) continue;
            if (!reader.ValueTextEquals("op")) continue;
            reader.Read();
            return reader.GetString()!; // Only null when TokenType is null
        }
        throw new InvalidOperationException("Unknown stream operation");
    }
}
