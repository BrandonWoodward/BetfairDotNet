namespace BetfairDotNet.Interfaces;

internal interface ISslStream {
    Task AuthenticateAsClientAsync(string targetHost);
    void Close();
    void Dispose();
    int Read(byte[] buffer, int offset, int count);
    Task WriteAsync(byte[] buffer);
}