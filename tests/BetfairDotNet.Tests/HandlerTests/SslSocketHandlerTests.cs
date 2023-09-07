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
    private readonly SslSocketHandler _sut;


    public SslSocketHandlerTests() {
        _mockSslSocket = Substitute.For<ISslSocket>();
        _sut = new SslSocketHandler(_mockSslSocket, "test-endpoint");
    }


    [Fact]
    public async Task Start_ShouldConnectAndAuthenticate() {
        // Arrange
        _mockSslSocket.IsConnected().Returns(true);

        // Act
        await _sut.Start(5_000, 5_000);

        // Assert
        await _mockSslSocket.Received().ConnectAsync("test-endpoint", 443, 5_000);
        await _mockSslSocket.Received().AuthenticateAsClientAsync("test-endpoint");

        // Teardown
        _sut.Stop();
    }


    [Fact]
    public async Task Start_ShouldPropagateCustomException_WhenSocketExceptionIsThrown() {
        // Arrange
        var observedException = new TaskCompletionSource<Exception>();

        _mockSslSocket
            .When(x => x.ConnectAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>()))
            .Do(x => throw new SocketException());

        _sut.MessageReceived.Subscribe(_ => { }, observedException.SetResult);

        // Act
        await _sut.Start(5_000, 5_000);

        // Assert
        var exception = await observedException.Task;
        exception.Should().NotBeNull();
        exception.Should().BeOfType<BetfairESAException>();
        ((BetfairESAException)exception).InnerException.Should().BeOfType<SocketException>();

        // Teardown
        _sut.Stop();
    }


    [Fact]
    public async Task SendLine_ShouldSerializeAndWriteMessage() {
        // Arrange
        var message = new AuthenticationMessage("testToken", "testApiKey");

        // Act
        await _sut.Start(5_000, 5_000); // Assume this works based on the first test
        await _sut.SendLine(message);

        // Assert
        await _mockSslSocket.Received().WriteAsync(Arg.Any<byte[]>());

        // Teardown
        _sut.Stop();
    }


    [Fact]
    public async Task SendLine_ShouldPropagateCustomException_WhenIOExceptionIsThrown() {
        // Arrange
        var observedException = new TaskCompletionSource<Exception>();
        var message = new AuthenticationMessage("testToken", "testApiKey");

        _mockSslSocket
            .When(x => x.WriteAsync(Arg.Any<byte[]>()))
            .Do(x => throw new IOException());


        _sut.MessageReceived.Subscribe(
            _ => { },
            observedException.SetResult
        );

        // Act
        await _sut.SendLine(message);

        // Assert
        var exception = await observedException.Task;
        exception.Should().NotBeNull();
        exception.Should().BeOfType<BetfairESAException>();
        ((BetfairESAException)exception).InnerException.Should().BeOfType<IOException>();
    }


    [Fact]
    public async Task SendLine_ShouldPropagateCustomException_WhenSocketExceptionIsThrown() {
        // Arrange
        var observedException = new TaskCompletionSource<Exception>();
        var message = new AuthenticationMessage("testToken", "testApiKey");

        _mockSslSocket
            .When(x => x.WriteAsync(Arg.Any<byte[]>()))
            .Do(x => throw new SocketException());


        _sut.MessageReceived.Subscribe(
            _ => { /* Do nothing on next */ },
            observedException.SetResult
        );

        // Act
        await _sut.SendLine(message);

        // Assert
        var exception = await observedException.Task;
        exception.Should().NotBeNull();
        exception.Should().BeOfType<BetfairESAException>();
        ((BetfairESAException)exception).InnerException.Should().BeOfType<SocketException>();
    }


    [Fact]
    public void Stop_ShouldDisposeResources() {
        // Arrange
        _mockSslSocket.IsConnected().Returns(true);

        // Act
        _sut.Start(5_000, 5_000).Wait();
        _sut.Stop();

        // Assert
        _mockSslSocket.Received().Close();
    }
}
