using BetfairDotNet.Models.Betting;
using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.BettingModelTests;

public class CountryCodeResultTests
{

    [Fact]
    public void CountryCodeResult_ShouldDeserializeCorrectly()
    {
        // Arrange
        var json = @"{
                ""countryCode"": ""GB"",
                ""marketCount"": 50
            }";

        var expectedCountryCodeResult = new CountryCodeResult
        {
            CountryCode = "GB",
            MarketCount = 50
        };

        // Act
        var deserializedCountryCodeResult = JsonSerializer.Deserialize<CountryCodeResult>(json);

        // Assert
        deserializedCountryCodeResult.Should().BeEquivalentTo(expectedCountryCodeResult);
    }
}
