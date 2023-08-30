using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Exceptions;
using BetfairDotNet.Models.Streaming;
using FluentAssertions;
using NSubstitute;
using System.Net.Sockets;
using Xunit;

namespace BetfairDotNet.Tests.HandlerTests;

public class SslSocketHandlerTests {


    private readonly ITcpClient _mockTcpClient;
    private readonly ISslStream _mockSslStream;
    private readonly SslSocketHandler _handler;


    public SslSocketHandlerTests() {
        _mockTcpClient = Substitute.For<ITcpClient>();
        _mockSslStream = Substitute.For<ISslStream>();
        _handler = new SslSocketHandler(_mockTcpClient, _mockSslStream, "test-endpoint");
    }


    [Fact]
    public async Task Start_ShouldConnectAndAuthenticate() {
        // Arrange
        _mockTcpClient.Connected.Returns(true);

        // Act
        await _handler.Start();

        // Assert
        await _mockTcpClient.Received().ConnectAsync("test-endpoint", 443);
        await _mockSslStream.Received().AuthenticateAsClientAsync("test-endpoint");
    }


    [Fact]
    public async Task Start_ShouldPropagateCustomException_WhenSocketExceptionIsThrown() {
        // Arrange
        var observedException = new TaskCompletionSource<Exception>();
        _mockTcpClient.When(x => x.ConnectAsync(Arg.Any<string>(), Arg.Any<int>())).Do(x => throw new SocketException());
        var handler = new SslSocketHandler(_mockTcpClient, _mockSslStream, "testEndpoint");
        handler.MessageStream.Subscribe(_ => { }, observedException.SetResult);

        // Act
        await handler.Start();

        // Assert
        var exception = await observedException.Task;
        exception.Should().NotBeNull();
        exception.Should().BeOfType<BetfairESAException>();
        ((BetfairESAException)exception).InnerException.Should().BeOfType<SocketException>();
    }


    [Fact]
    public async Task SendLine_ShouldSerializeAndWriteMessage() {
        // Arrange
        var message = new AuthenticationMessage("testToken", "testApiKey");

        // Act
        await _handler.Start(); // Assume this works based on the first test
        await _handler.SendLine(message);

        // Assert
        await _mockSslStream.Received().WriteAsync(Arg.Any<byte[]>());
    }


    [Fact]
    public async Task SendLine_ShouldPropagateCustomException_WhenIOExceptionIsThrown() {
        // Arrange
        var observedException = new TaskCompletionSource<Exception>();
        var message = new AuthenticationMessage("testToken", "testApiKey");
        _mockSslStream.When(x => x.WriteAsync(Arg.Any<byte[]>())).Do(x => throw new IOException());

        var handler = new SslSocketHandler(_mockTcpClient, _mockSslStream, "testEndpoint");

        handler.MessageStream.Subscribe(
            _ => { /* Do nothing on next */ },
            ex => observedException.SetResult(ex) // Set the exception when OnError is called
        );

        // Act
        await handler.SendLine(message);

        // Assert
        var exception = await observedException.Task;

        exception.Should().NotBeNull();
        exception.Should().BeOfType<BetfairESAException>();
        ((BetfairESAException)exception).InnerException.Should().BeOfType<IOException>();
    }


    [Fact]
    public void Stop_ShouldDisposeResources() {
        // Arrange
        _mockTcpClient.Connected.Returns(true);

        // Act
        _handler.Start().Wait();
        _handler.Stop();

        // Assert
        _mockTcpClient.Received().Close();
        _mockSslStream.Received().Close();
    }
}
