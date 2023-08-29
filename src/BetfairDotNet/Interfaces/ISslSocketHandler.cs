using BetfairDotNet.Models.Streaming;

namespace BetfairDotNet.Interfaces;

internal interface ISslSocketHandler {
    IObservable<ReadOnlyMemory<byte>> MessageStream { get; }
    Task Start();
    void Stop();
    Task SendLine<T>(T message) where T : BaseMessage;
}