using BetfairDotNet.Adapters;
using BetfairDotNet.Converters;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Exceptions;
using BetfairDotNet.Models.Streaming;
using System.Buffers;
using System.Net.Sockets;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace BetfairDotNet.Handlers;


internal sealed class SslSocketHandler : ISslSocketHandler
{

    private readonly Subject<ReadOnlyMemory<byte>> _messageSubject = new();
    private readonly Subject<Unit> _recoverySubject = new();
    private readonly string _endpoint;
    private readonly int _port = 443;

    private ISslSocket _socket;
    private int _incrementId;
    private Thread? _listener;
    private CancellationTokenSource? _cts;
    private TimeSpan? _recoveryThreshold;
    private DateTime? _lastReceivedTime;

    public IObservable<ReadOnlyMemory<byte>> MessageReceived
        => _messageSubject.AsObservable();

    public IObservable<Unit> SocketRecovered
        => _recoverySubject.AsObservable();


    internal SslSocketHandler(ISslSocket sslStream, string endpoint)
    {
        _socket = sslStream;
        _endpoint = endpoint;
    }


    public async Task Start(int recoveryThreshold, int maxRecoveryWait)
    {
        _recoveryThreshold = TimeSpan.FromMilliseconds(recoveryThreshold);
        _cts = new CancellationTokenSource();
        _lastReceivedTime = DateTime.UtcNow;

        try
        {
            await _socket.ConnectAsync(_endpoint, _port, maxRecoveryWait);
            await _socket.AuthenticateAsClientAsync(_endpoint);
            _listener = new Thread(ReceiveLines) { IsBackground = true };
            _listener.Start();
        }
        catch(SocketException ex)
        {
            var exception = new BetfairESAException("Failed to start stream. See InnerException", ex);
            _messageSubject.OnError(exception);
        }
    }


    public void Stop()
    {
        _cts?.Cancel();
        _cts?.Dispose();
        if(_socket.IsConnected()) _socket?.Close();
        _socket = new SslSocketAdapter(); // For reconnection               
    }


    public async Task SendLine<T>(T message) where T : BaseMessage
    {
        message.Id = Interlocked.Increment(ref _incrementId);
        var messageJson = JsonConvert.Serialize(message);
        var messageBytes = Encoding.UTF8.GetBytes(messageJson + "\r\n"); // CRLF is required

        try
        {
            await _socket.WriteAsync(messageBytes);
        }
        catch(IOException ex)
        {
            HandleException("Failed to send line. See InnerException", ex);
        }
        catch(SocketException ex)
        {
            HandleException("Failed to send line. See InnerException", ex);
        }
    }


    private void ReceiveLines()
    {
        var buffer = ArrayPool<byte>.Shared.Rent(2 * 1024 * 1024); // Unsure of optimal size
        var delimiter = "\r\n"u8.ToArray(); // Separate each message to processs individually
        var bufferOffset = 0;

        try
        {
            while(_socket.IsConnected() && !(_cts?.IsCancellationRequested ?? true))
            {
                _lastReceivedTime = DateTime.UtcNow;

                var bytesRead = _socket.Read(buffer, bufferOffset, buffer.Length - bufferOffset);

                // If we haven't received a message in the last heartbeat interval, then we have a problem
                if(_recoveryThreshold.HasValue && DateTime.UtcNow - _lastReceivedTime > _recoveryThreshold.Value)
                {
                    _recoverySubject.OnNext(Unit.Default); // Initiate recovery
                    break;
                }

                bufferOffset += bytesRead;
                int foundIndex;

                while((foundIndex = buffer.AsSpan(0, bufferOffset).IndexOf(delimiter)) != -1)
                {
                    _messageSubject.OnNext(buffer.AsMemory(0, foundIndex));
                    bufferOffset -= foundIndex + delimiter.Length;
                    buffer.AsSpan().Slice(foundIndex + delimiter.Length, bufferOffset).CopyTo(buffer);
                }
            }
        }
        catch(SocketException ex)
        {
            HandleException("Failed to receive line. See InnerException", ex);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(buffer); // Avoid memory leak
        }
    }


    private void HandleException(string message, Exception ex)
    {
        var exception = new BetfairESAException($"{message} See InnerException for details.", ex);
        _messageSubject.OnError(exception);
    }
}