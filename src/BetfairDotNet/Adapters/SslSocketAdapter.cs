using BetfairDotNet.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Net.Security;
using System.Net.Sockets;

namespace BetfairDotNet.Adapters;


[ExcludeFromCodeCoverage]
internal sealed class SslSocketAdapter : IDisposable, ISslSocket {

    private TcpClient? _tcpClient;
    private SslStream? _sslStream;


    public async Task ConnectAsync(string hostname, int port) {
        _tcpClient = new TcpClient();
        await _tcpClient.ConnectAsync(hostname, port);
    }


    public async Task AuthenticateAsClientAsync(string targetHost) {
        if(_tcpClient is null) throw new InvalidOperationException("TcpClient not initialized.");
        _sslStream = new SslStream(_tcpClient.GetStream());
        await _sslStream.AuthenticateAsClientAsync(targetHost);
    }


    public async Task WriteAsync(byte[] buffer) {
        if(_sslStream is null) throw new InvalidOperationException("TcpClient not initialized.");
        await _sslStream.WriteAsync(buffer);
        await _sslStream.FlushAsync();
    }


    public int Read(byte[] buffer, int offset, int count) {
        if(_sslStream is null) throw new InvalidOperationException("TcpClient not initialized.");
        return _sslStream.Read(buffer, offset, count);
    }


    public bool IsConnected() {
        return _tcpClient is not null && _tcpClient.Connected;
    }


    public void Close() {
        _tcpClient?.Close();
        _sslStream?.Close();
    }


    public void Dispose() {
        _tcpClient?.Dispose();
        _sslStream?.Dispose();
    }
}
