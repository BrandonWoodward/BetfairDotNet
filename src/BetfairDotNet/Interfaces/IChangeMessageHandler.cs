namespace BetfairDotNet.Interfaces;

internal interface IChangeMessageHandler {
    void HandleException(Exception ex);
    void HandleMessage(ReadOnlyMemory<byte> message);
}