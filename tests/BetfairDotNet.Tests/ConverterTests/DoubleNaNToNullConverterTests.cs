using BetfairDotNet.Converters;
using FluentAssertions;
using System.Text;
using System.Text.Json;
using Xunit;

namespace BetfairDotNet.Tests.ConverterTests;


public class DoubleNaNToNullConverterTests {


    private DoubleNaNToNullConverter _sut = new();


    [Fact]
    public void ReadFromJson_ShouldReturnNull_WhenGivenNaN() {
        // Arrange
        var json = "\"NaN\"";
        _sut = new();

        // Act
        var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
        reader.Read();
        var value = _sut.Read(ref reader, typeof(double?), new JsonSerializerOptions());

        // Assert
        value.Should().BeNull();
    }


    [Fact]
    public void ReadFromJson_ShouldReturnDouble_WhenGivenNumericString() {
        // Arrange
        var json = "\"42.42\"";
        _sut = new();

        // Act
        var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
        reader.Read();
        var value = _sut.Read(ref reader, typeof(double?), new JsonSerializerOptions());

        // Assert
        value.Should().Be(42.42);
    }


    [Fact]
    public void ReadFromJson_ShouldReturnNull_WhenGivenNullToken() {
        // Arrange
        var json = "null";
        _sut = new();

        // Act
        var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
        reader.Read();
        var value = _sut.Read(ref reader, typeof(double?), new JsonSerializerOptions());

        // Assert
        value.Should().BeNull();
    }


    [Fact]
    public void ReadFromJson_ShouldThrowJsonException_WhenGivenInvalidString() {
        // Arrange
        var json = "\"invalid\"";
        _sut = new();

        // Act
        var action = () => {
            var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
            reader.Read();
            var value = _sut.Read(ref reader, typeof(double?), new JsonSerializerOptions());
        };

        // Assert
        action.Should().Throw<JsonException>();
    }


    [Fact]
    public void ReadFromJson_ShouldThrowJsonException_WhenGivenUnexpectedToken() {
        // Arrange
        var json = "{ }";
        _sut = new();

        // Act
        var action = () => {
            var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
            reader.Read();
            var value = _sut.Read(ref reader, typeof(double?), new JsonSerializerOptions());
        };

        // Assert
        action.Should().Throw<JsonException>();
    }


    [Fact]
    public void ReadFromJson_ShouldReturnDouble_WhenGivenNumericToken() {
        // Arrange
        var json = "42.42";
        _sut = new();

        // Act
        var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
        reader.Read();
        var value = _sut.Read(ref reader, typeof(double?), new JsonSerializerOptions());

        // Assert
        value.Should().Be(42.42);
    }


    [Fact]
    public void WriteToJson_ShouldWriteNull_WhenGivenNullValue() {
        // Arrange
        double? value = null;
        _sut = new();

        // Act
        var stream = new MemoryStream();
        var writer = new Utf8JsonWriter(stream);
        _sut.Write(writer, value, new JsonSerializerOptions());
        writer.Flush();
        var json = Encoding.UTF8.GetString(stream.ToArray());

        // Assert
        json.Should().Be("null");
    }


    [Fact]
    public void WriteToJson_ShouldWriteNumber_WhenGivenNumberValue() {
        // Arrange
        var value = 42.42;
        _sut = new();

        // Act
        var stream = new MemoryStream();
        var writer = new Utf8JsonWriter(stream);
        _sut.Write(writer, value, new JsonSerializerOptions());
        writer.Flush();
        var json = Encoding.UTF8.GetString(stream.ToArray());

        // Assert
        json.Should().Be("42.42");
    }
}
