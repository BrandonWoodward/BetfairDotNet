using BetfairDotNet.Converters;
using BetfairDotNet.Models.Account;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.AccountModelTests;
public class CurrentRateTests {

    [Fact]
    public void CurrencyRate_ShouldSerializeCorrectly() {
        // Arrange
        var currencyRate = new CurrencyRate {
            CurrencyCode = "USD",
            Rate = 1.2
        };

        // Act
        var serialized = JsonConvert.Serialize(currencyRate);

        // Alternatively or additionally, you could assert against the expected JSON string
        var expectedJson = "{\"currencyCode\":\"USD\",\"rate\":1.2}";
        serialized.Should().Be(expectedJson);
    }
}
