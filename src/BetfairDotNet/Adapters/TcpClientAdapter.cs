using BetfairDotNet.Interfaces;
using System.Net.Sockets;

namespace BetfairDotNet.Adapters;


internal sealed class TcpClientAdapter : IDisposable, ITcpClient {

    private readonly TcpClient _tcpClient = new();
    public bool Connected => _tcpClient.Connected;


    public NetworkStream GetStream() {
        return _tcpClient.GetStream();
    }


    public async Task ConnectAsync(string hostname, int port) {
        await _tcpClient.ConnectAsync(hostname, port);
    }


    public void Close() {
        _tcpClient.Close();
    }


    public void Dispose() {
        _tcpClient.Dispose();
    }
}
