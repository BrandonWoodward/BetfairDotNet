using BetfairDotNet.Models.Streaming;

namespace BetfairDotNet.Interfaces;


internal interface IChangeMessageFactory {
    BaseMessage Process(ReadOnlyMemory<byte> input);
}