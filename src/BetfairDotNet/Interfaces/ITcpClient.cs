namespace BetfairDotNet.Interfaces;

internal interface ITcpClient {
    bool Connected { get; }
    void Close();
    void Dispose();
    Task ConnectAsync(string hostname, int port);
}