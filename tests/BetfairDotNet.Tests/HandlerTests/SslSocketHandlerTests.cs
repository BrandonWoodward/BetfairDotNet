using BetfairDotNet.Handlers;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Exceptions;
using BetfairDotNet.Models.Streaming;
using FluentAssertions;
using NSubstitute;
using System.Net.Sockets;
using Xunit;

namespace BetfairDotNet.Tests.HandlerTests;

public class SslSocketHandlerTests {


    private readonly ISslSocket _mockSslSocket;
    private readonly SslSocketHandler _handler;


    public SslSocketHandlerTests() {
        _mockSslSocket = Substitute.For<ISslSocket>();
        _handler = new SslSocketHandler(_mockSslSocket, "test-endpoint");
    }


    [Fact]
    public async Task Start_ShouldConnectAndAuthenticate() {
        // Arrange
        _mockSslSocket.IsConnected().Returns(true);

        // Act
        await _handler.Start();

        // Assert
        await _mockSslSocket.Received().ConnectAsync("test-endpoint", 443);
        await _mockSslSocket.Received().AuthenticateAsClientAsync("test-endpoint");
    }


    [Fact]
    public async Task Start_ShouldPropagateCustomException_WhenSocketExceptionIsThrown() {
        // Arrange
        var observedException = new TaskCompletionSource<Exception>();
        _mockSslSocket.When(x => x.ConnectAsync(Arg.Any<string>(), Arg.Any<int>())).Do(x => throw new SocketException());
        var handler = new SslSocketHandler(_mockSslSocket, "testEndpoint");
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
        await _mockSslSocket.Received().WriteAsync(Arg.Any<byte[]>());
    }


    [Fact]
    public async Task SendLine_ShouldPropagateCustomException_WhenIOExceptionIsThrown() {
        // Arrange
        var observedException = new TaskCompletionSource<Exception>();
        var message = new AuthenticationMessage("testToken", "testApiKey");
        _mockSslSocket.When(x => x.WriteAsync(Arg.Any<byte[]>())).Do(x => throw new IOException());

        var handler = new SslSocketHandler(_mockSslSocket, "testEndpoint");

        handler.MessageStream.Subscribe(
            _ => { /* Do nothing on next */ },
            observedException.SetResult // Set the exception when OnError is called
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
        _mockSslSocket.IsConnected().Returns(true);

        // Act
        _handler.Start().Wait();
        _handler.Stop();

        // Assert
        _mockSslSocket.Received().Close();
        _mockSslSocket.Received().Close();
    }
}
