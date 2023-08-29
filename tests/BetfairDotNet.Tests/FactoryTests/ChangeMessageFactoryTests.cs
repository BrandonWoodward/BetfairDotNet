using BetfairDotNet.Factories;
using BetfairDotNet.Models.Streaming;
using System.Text;
using Xunit;

namespace BetfairDotNet.Tests.FactoryTests;
public class ChangeMessageFactoryTests {


    private readonly ChangeMessageFactory _sut = new();


    [Fact]
    public void Process_ConnectionMessage_ReturnsCorrectType() {

        // Arrange
        var input = "{\"op\":\"connection\",\"connectionId\":\"002-230915140112-174\"}";
        var bytes = Encoding.UTF8.GetBytes(input);

        // Act
        var result = _sut.Process(bytes);

        // Assert
        Assert.IsType<ConnectionMessage>(result);
    }


    [Fact]
    public void Process_StatusMessage_ReturnsCorrectType() {

        // Arrange
        var input = "{\"op\":\"status\",\"id\":1,\"statusCode\":\"SUCCESS\",\"connectionClosed\":false,\"connectionsAvailable\":9}";
        var bytes = Encoding.UTF8.GetBytes(input);

        // Act
        var result = _sut.Process(bytes);

        // Assert
        Assert.IsType<StatusMessage>(result);
    }


    [Fact]
    public void Process_MarketChangeMessage_ReturnsCorrectType() {

        // Arrange
        var input = "{\"op\":\"mcm\",\"id\":2,\"initialClk\":\"xxz4183KItIcgfqW3yLNHMq0xOEi\",\"clk\":\"AAAAAAAA\",\"conflateMs\":0}";
        var bytes = Encoding.UTF8.GetBytes(input);

        // Act
        var result = _sut.Process(bytes.AsMemory());

        // Assert
        Assert.IsType<MarketChangeMessage>(result);
    }


    [Fact]
    public void Process_OrderChangeMessage_ReturnsCorrectType() {

        // Arrange
        var input = "{\"op\": \"ocm\",\"id\":2,\"initialClk\":\"xxz4183KItIcgfqW3yLNHMq0xOEi\",\"clk\":\"AAAAAAAA\",\"conflateMs\":0}";
        var bytes = Encoding.UTF8.GetBytes(input);

        // Act
        var result = _sut.Process(bytes);

        // Assert
        Assert.IsType<OrderChangeMessage>(result);
    }


    [Fact]
    public void Process_UnknownOperation_ThrowsInvalidOperationException() {

        // Arrange
        var input = "{\"op\": \"unknown\",\"id\":2,\"initialClk\":\"xxz4183KItIcgfqW3yLNHMq0xOEi\",\"clk\":\"AAAAAAAA\",\"conflateMs\":0}";
        var bytes = Encoding.UTF8.GetBytes(input);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _sut.Process(bytes));
    }


    [Fact]
    public void Process_NoOperationInMessage_ThrowsInvalidOperationException() {

        // Arrange
        var input = "{\"foo\": \"bar\", \"id\":2}";
        var bytes = Encoding.UTF8.GetBytes(input);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => _sut.Process(bytes));
    }
}
