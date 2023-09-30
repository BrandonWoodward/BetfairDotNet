using System.Text.Json;
using BetfairDotNet.Converters;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ConverterTests;

public class NumberOrEmptyStringConverterTests
{
    private readonly JsonSerializerOptions _options = new()
    {
        Converters = { new NumberOrEmptyStringConverter() }
    };
    
    [Fact]
    public void Deserialize_ReturnsDefault_WhenEmptyString()
    {
        // Arrange
        var json = "\"\"";
        
        // Act
        var result = JsonSerializer.Deserialize<int>(json, _options);
        
        // Assert
        result.Should().Be(0);
    }

    [Fact]
    public void Deserialize_ReturnsNumber_WhenValidNumber()
    {
        // Arrange
        var json = "42";
        
        // Act
        var result = JsonSerializer.Deserialize<int>(json, _options);
        
        // Assert
        result.Should().Be(42);
    }

    [Fact]
    public void Deserialize_ThrowsJsonException_WhenInvalidString()
    {
        // Arrange
        var json = "\"foo\"";
        
        // Act
        Action act = () => JsonSerializer.Deserialize<int>(json, _options);
        
        // Assert
        act.Should().Throw<JsonException>();
    }
    

    [Fact]
    public void Serialize_ReturnsNumber_WhenValidNumber()
    {
        // Arrange
        var value = 42;
        
        // Act
        var json = JsonSerializer.Serialize(value, _options);
        
        // Assert
        json.Should().Be("42");
    }
}