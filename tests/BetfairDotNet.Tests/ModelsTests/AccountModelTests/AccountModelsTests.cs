using BetfairDotNet.Enums.Account;
using BetfairDotNet.Models.Account;
using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.AccountModelTests;
public class AccountModelsTests
{

    [Fact]
    public void AccountDetailsResponse_ShouldDeserializeCorrectly()
    {
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
        var deserialized = JsonSerializer.Deserialize<AccountDetailsResponse>(json);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized.Should().BeEquivalentTo(new
        {
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


    [Fact]
    public void AccountFundsResponse_ShouldDeserializeCorrectly()
    {
        // Arrange
        var json = @"
        {
            ""availableToBetBalance"": 100.5,
            ""exposure"": 20.5,
            ""retainedCommission"": 5.0,
            ""exposureLimit"": 2000.0,
            ""discountRate"": 0.2,
            ""pointsBalance"": 50
        }";

        // Act
        var deserialized = JsonSerializer.Deserialize<AccountFundsResponse>(json);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized.Should().BeEquivalentTo(new
        {
            Balance = 100.5,
            Exposure = 20.5,
            RetainedCommission = 5.0,
            ExposureLimit = 2000.0,
            DiscountRate = 0.2,
            PointsBalance = 50
        });
    }


    [Fact]
    public void AccountStatementReport_ShouldDeserializeCorrectly()
    {
        // Arrange
        var json = @"
        {
            ""accountStatement"": [
                {
                    ""refId"": ""Ref1"",
                    ""itemDate"": ""2023-01-01T12:00:00"",
                    ""amount"": 100.0,
                    ""balance"": 200.0,
                    ""itemClassData"": {
                        ""key1"": ""value1"",
                        ""key2"": ""value2""
                    }
                }
            ],
            ""moreAvailable"": true
        }";

        // Act
        var deserialized = JsonSerializer.Deserialize<AccountStatementReport>(json);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized.Should().BeEquivalentTo(new AccountStatementReport
        {
            AccountStatement = new List<StatementItem> {
                new StatementItem {
                    RefId = "Ref1",
                    ItemDate = DateTime.Parse("2023-01-01T12:00:00"),
                    Amount = 100.0,
                    Balance = 200.0,
                    ItemClass = ItemClassEnum.UNKNOWN,
                    ItemClassData = new Dictionary<string, string> {
                        { "key1", "value1" },
                        { "key2", "value2" }
                    }
                },
            },
            MoreAvailable = true
        });
    }


    [Fact]
    public void CurrencyRate_ShouldSerializeCorrectly()
    {
        // Arrange
        var currencyRate = new CurrencyRate
        {
            CurrencyCode = "USD",
            Rate = 1.2
        };

        // Act
        var serialized = JsonSerializer.Serialize(currencyRate);

        // Alternatively or additionally, you could assert against the expected JSON string
        var expectedJson = "{\"currencyCode\":\"USD\",\"rate\":1.2}";
        serialized.Should().Be(expectedJson);
    }
}
