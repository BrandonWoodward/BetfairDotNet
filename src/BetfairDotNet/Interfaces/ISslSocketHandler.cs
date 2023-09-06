using BetfairDotNet.Models.Streaming;
using System.Reactive;

namespace BetfairDotNet.Interfaces;

internal interface ISslSocketHandler {

    IObservable<ReadOnlyMemory<byte>> MessageReceived { get; }
    IObservable<Unit> SocketRecovered { get; }

    void Stop();
    Task SendLine<T>(T message) where T : BaseMessage;
    Task Start(int recoveryThreshold, int maxRecoveryWait);
}