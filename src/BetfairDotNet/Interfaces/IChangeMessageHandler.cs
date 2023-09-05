namespace BetfairDotNet.Interfaces;

internal interface IChangeMessageHandler {
    Tuple<string?, string?, string?, string?> GetClocks();
    void HandleException(Exception ex);
    void HandleMessage(ReadOnlyMemory<byte> message);
}