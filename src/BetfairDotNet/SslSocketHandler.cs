using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Exceptions;
using BetfairDotNet.Models.Streaming;
using BetfairDotNet.Utils;
using System.Buffers;
using System.Net.Security;
using System.Net.Sockets;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace BetfairDotNet;


internal sealed class SslSocketHandler : ISslSocketHandler {


    private readonly Subject<ReadOnlyMemory<byte>> _messageSubject = new();
    private readonly object _stopLock = new();
    private readonly string _endpoint;
    private readonly int _port = 443;

    private int _incrementId;
    private TcpClient _client;
    private SslStream _sslStream;
    private Thread _listener;
    private CancellationTokenSource _cts;


    public IObservable<ReadOnlyMemory<byte>> MessageStream
        => _messageSubject.AsObservable();


    internal SslSocketHandler(string endpoint) {
        _endpoint = endpoint;
    }


    public async Task Start() {
        _client = new();
        _cts = new(); // used to shutdown background thread
        try {
            await _client.ConnectAsync(_endpoint, _port);
            _sslStream = new SslStream(_client.GetStream(), false);
            await _sslStream.AuthenticateAsClientAsync(_endpoint);
            _listener = new Thread(() => ReceiveLines(_cts.Token));
            _listener.Start();
        }
        catch(SocketException ex) {
            _cts.Cancel();
            _messageSubject.OnError(
                new BetfairESAException(true, ex.Message, ex)
            );
        }
    }


    public void Stop() {
        lock(_stopLock) { // stop race condition
            _cts.Cancel();
            _cts.Dispose();
            _sslStream?.Close();
            _sslStream?.Dispose();
            _client?.Close();
            _client?.Dispose();
        }
    }


    public async Task SendLine<T>(T message) where T : BaseMessage {
        try {
            message.Id = Interlocked.Increment(ref _incrementId);
            var messageJson = JsonConvert.Serialize(message);
            var messageBytes = Encoding.UTF8.GetBytes(messageJson + "\r\n"); // CRLF is required
            await _sslStream.WriteAsync(messageBytes);
            await _sslStream.FlushAsync();
        }
        catch(IOException ex) {
            _messageSubject.OnError(
                new BetfairESAException(true, ex.Message, ex)
            );
        }
    }


    private void ReceiveLines(CancellationToken token) {
        var buffer = ArrayPool<byte>.Shared.Rent(1024 * 256); // Unsure of optimal size
        var delimiter = "\r\n"u8.ToArray(); // Separate each message to processs individually
        var bufferOffset = 0;
        try {
            while(_client.Connected && !token.IsCancellationRequested) {
                var bytesRead = _sslStream.Read(buffer, bufferOffset, buffer.Length - bufferOffset);
                bufferOffset += bytesRead;
                var foundIndex = 0;
                while((foundIndex = buffer.AsSpan(0, bufferOffset).IndexOf(delimiter)) != -1) {
                    _messageSubject.OnNext(buffer.AsMemory(0, foundIndex));
                    bufferOffset -= foundIndex + delimiter.Length;
                    buffer.AsSpan().Slice(foundIndex + delimiter.Length, bufferOffset).CopyTo(buffer);
                }
            }
        }
        catch(SocketException ex) {
            _messageSubject.OnError(
                new BetfairESAException(true, ex.Message, ex)
            );
        }
        finally {
            ArrayPool<byte>.Shared.Return(buffer); // Avoid memory leak
        }
    }
}