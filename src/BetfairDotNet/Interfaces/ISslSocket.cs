namespace BetfairDotNet.Interfaces;

internal interface ISslSocket {
    Task AuthenticateAsClientAsync(string targetHost);
    void Close();
    Task ConnectAsync(string hostname, int port);
    void Dispose();
    bool IsConnected();
    int Read(byte[] buffer, int offset, int count);
    Task WriteAsync(byte[] buffer);
}