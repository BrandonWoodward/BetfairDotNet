using BetfairDotNet.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Net.Security;
using System.Net.Sockets;

namespace BetfairDotNet.Adapters;


[ExcludeFromCodeCoverage]
internal sealed class SslStreamAdapter : IDisposable, ISslStream {

    private readonly SslStream _sslStream;


    internal SslStreamAdapter(NetworkStream networkStream) {
        _sslStream = new SslStream(networkStream);
    }


    public async Task AuthenticateAsClientAsync(string targetHost) {
        await _sslStream.AuthenticateAsClientAsync(targetHost);
    }


    public async Task WriteAsync(byte[] buffer) {
        await _sslStream.WriteAsync(buffer);
        await _sslStream.FlushAsync();
    }


    public int Read(byte[] buffer, int offset, int count) {
        return _sslStream.Read(buffer, offset, count);
    }


    public void Close() {
        _sslStream.Close();
    }


    public void Dispose() {
        _sslStream.Dispose();
    }
}
