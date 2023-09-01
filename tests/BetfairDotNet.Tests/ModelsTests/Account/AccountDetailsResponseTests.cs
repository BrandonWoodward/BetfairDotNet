using BetfairDotNet.Converters;
using BetfairDotNet.Models.Account;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Account;

public class AccountDetailsResponseTests {

    [Fact]
    public void AccountDetailsResponse_ShouldDeserializeCorrectly() {
        // Arrange
        var json = @" 
        {
            ""currencyCode"": ""USD"",
            ""firstName"": ""John"",
            ""lastName"": ""Doe"",
            ""localeCode"": ""en-US"",
            ""region"": ""USA"",
            ""timezone"": ""PST"",
            ""discountRate"": 0.15,
            ""pointsBalance"": 100,
            ""countryCode"": ""US""
        }";

        // Act
        var deserialized = JsonConvert.Deserialize<AccountDetailsResponse>(json);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized.Should().BeEquivalentTo(new {
            CurrencyCode = "USD",
            FirstName = "John",
            LastName = "Doe",
            LocaleCode = "en-US",
            Region = "USA",
            Timezone = "PST",
            DiscountRate = 0.15,
            PointsBalance = 100,
            CountryCode = "US"
        });
    }
}
