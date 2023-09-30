using System.Text.Json;
using BetfairDotNet.Converters;
using BetfairDotNet.Models.Navigation;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ConverterTests;

public class NavigationItemConverterTests
{
    private readonly JsonSerializerOptions _options = new()
    {
        Converters = { new NavigationItemConverter() }
    };

    [Fact]
    public void Deserialize_GroupType_ReturnsNavigationGroup()
    {
        // Arrange
        var json = "{\"id\":\"1\", \"name\":\"TestGroup\", \"type\":\"GROUP\", \"children\": []}";

        // Act
        var result = JsonSerializer.Deserialize<NavigationItem>(json, _options);

        // Assert
        result.Should().BeOfType<NavigationGroup>();
    }

    [Fact]
    public void Deserialize_EventType_ReturnsNavigationEvent()
    {
        // Arrange
        var json = "{\"id\":\"2\", \"name\":\"TestEvent\", \"countryCode\":\"US\", \"type\":\"EVENT\", \"children\": []}";

        // Act
        var result = JsonSerializer.Deserialize<NavigationItem>(json, _options);

        // Assert
        result.Should().BeOfType<NavigationEvent>();
    }

    [Fact]
    public void Deserialize_MarketType_ReturnsNavigationMarket()
    {
        // Arrange
        var json = "{\"id\":\"3\", \"exchangeId\":\"E1\", \"name\":\"TestMarket\", \"type\":\"MARKET\"}";

        // Act
        var result = JsonSerializer.Deserialize<NavigationItem>(json, _options);

        // Assert
        result.Should().BeOfType<NavigationMarket>();
    }
    
    [Fact]
    public void Deserialize_ReturnsNavigationRace_WhenTypeIsRace()
    {
        // Arrange
        var json = "{\"id\":\"3\", \"exchangeId\":\"E1\", \"name\":\"TestMarket\", \"type\":\"RACE\"}";

        // Act
        var result = JsonSerializer.Deserialize<NavigationItem>(json, _options);

        // Assert
        result.Should().BeOfType<NavigationRace>();
    }

    [Fact]
    public void Deserialize_UnknownType_ThrowsJsonException()
    {
        // Arrange
        var json = "{\"id\":\"4\", \"name\":\"Unknown\", \"type\":\"UNKNOWN\"}";

        // Act & Assert
        Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<NavigationItem>(json, _options));
    }

    [Fact]
    public void Serialize_NavigationGroup_ReturnsCorrectJson()
    {
        // Arrange
        var group = new NavigationGroup { Id = "1", Name = "TestGroup" };

        // Act
        var json = JsonSerializer.Serialize<NavigationItem>(group, _options);

        // Assert
        json.Should().Contain("\"type\":\"GROUP\"");
    }
}