using BetfairDotNet.Factories;
using BetfairDotNet.Models.Streaming;
using FluentAssertions;
using System.Text;
using Xunit;

namespace BetfairDotNet.Tests.FactoryTests;
public class ChangeMessageFactoryTests {


    private ChangeMessageFactory _sut = new();


    [Fact]
    public void Process__ReturnsConnectionMessage_WhenOpIsConnection() {
        // Arrange
        _sut = new();
        var input = "{\"op\":\"connection\",\"connectionId\":\"002-230915140112-174\"}";
        var bytes = Encoding.UTF8.GetBytes(input);

        // Act
        var result = _sut.Process(bytes);

        // Assert
        result.Should().BeOfType<ConnectionMessage>();
    }


    [Fact]
    public void Process__ReturnsStatusMessage_WhenOpIsStatus() {
        // Arrange
        _sut = new();
        var input = "{\"op\":\"status\",\"id\":1,\"statusCode\":\"SUCCESS\",\"connectionClosed\":false,\"connectionsAvailable\":9}";
        var bytes = Encoding.UTF8.GetBytes(input);

        // Act
        var result = _sut.Process(bytes);

        // Assert
        result.Should().BeOfType<StatusMessage>();
    }


    [Fact]
    public void Process_ReturnsMarketChangeMessage_WhenOpIsMcm() {
        // Arrange
        _sut = new();
        var input = "{\"op\":\"mcm\",\"id\":2,\"initialClk\":\"xxz4183KItIcgfqW3yLNHMq0xOEi\",\"clk\":\"AAAAAAAA\",\"conflateMs\":0}";
        var bytes = Encoding.UTF8.GetBytes(input);

        // Act
        var result = _sut.Process(bytes.AsMemory());

        // Assert
        result.Should().BeOfType<MarketChangeMessage>();
    }


    [Fact]
    public void Process_ReturnsOrderChangeMessage_WhenOpIsOcm() {
        // Arrange
        _sut = new();
        var input = "{\"op\": \"ocm\",\"id\":2,\"initialClk\":\"xxz4183KItIcgfqW3yLNHMq0xOEi\",\"clk\":\"AAAAAAAA\",\"conflateMs\":0}";
        var bytes = Encoding.UTF8.GetBytes(input);

        // Act
        var result = _sut.Process(bytes);

        // Assert
        result.Should().BeOfType<OrderChangeMessage>();
    }


    [Fact]
    public void Process_ThrowsInvalidOperationException_WhenOpUnknown() {
        // Arrange
        _sut = new();
        var input = "{\"op\": \"unknown\",\"id\":2,\"initialClk\":\"xxz4183KItIcgfqW3yLNHMq0xOEi\",\"clk\":\"AAAAAAAA\",\"conflateMs\":0}";
        var bytes = Encoding.UTF8.GetBytes(input);

        // Act & Assert
        FluentActions.Invoking(() => _sut.Process(bytes)).Should().Throw<InvalidOperationException>();
    }


    [Fact]
    public void Process_ThrowsInvalidOperationException_WhenNoOp() {
        // Arrange
        _sut = new();
        var input = "{\"foo\": \"bar\", \"id\":2}";
        var bytes = Encoding.UTF8.GetBytes(input);

        // Act & Assert
        FluentActions.Invoking(() => _sut.Process(bytes)).Should().Throw<InvalidOperationException>();
    }
}
