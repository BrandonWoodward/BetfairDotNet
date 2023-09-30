using System.Text.Json;
using BetfairDotNet.Converters;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ConverterTests;

public class EmptyStringToEnumConverterTests
{
    private readonly JsonSerializerOptions _options = new()
    {
        Converters = { new EmptyStringToEnumConverter<StubEnum>() }
    };

    public enum StubEnum { None, FirstValue, SecondValue }

    [Fact]
    public void Deserialize_EmptyString_ReturnsFirstEnumValue()
    {
        // Arrange
        var json = "\"\"";

        // Act
        var result = JsonSerializer.Deserialize<StubEnum>(json, _options);

        // Assert
        result.Should().Be(StubEnum.None);
    }

    [Fact]
    public void Deserialize_ValidEnumString_ReturnsEnumValue()
    {
        // Arrange
        var json = "\"firstvalue\"";

        // Act
        var result = JsonSerializer.Deserialize<StubEnum>(json, _options);

        // Assert
        result.Should().Be(StubEnum.FirstValue);
    }

    [Fact]
    public void Deserialize_InvalidString_ReturnsFirstEnumValue()
    {
        // Arrange
        var json = "\"foo\"";

        // Act
        var result = JsonSerializer.Deserialize<StubEnum>(json, _options);

        // Assert
        result.Should().Be(StubEnum.None);
    }

    [Fact]
    public void Serialize_EnumValue_ReturnsStringValue()
    {
        // Arrange
        var value = StubEnum.SecondValue;

        // Act
        var json = JsonSerializer.Serialize(value, _options);

        // Assert
        json.Should().Be("\"SecondValue\"");
    }
}