using BetfairDotNet.Converters;
using BetfairDotNet.Models.Account;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Account;
public class AccountFundsResponseTests {

    [Fact]
    public void AccountFundsResponse_ShouldDeserializeCorrectly() {
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
        var deserialized = JsonConvert.Deserialize<AccountFundsResponse>(json);

        // Assert
        deserialized.Should().NotBeNull();
        deserialized.Should().BeEquivalentTo(new {
            Balance = 100.5,
            Exposure = 20.5,
            RetainedCommission = 5.0,
            ExposureLimit = 2000.0,
            DiscountRate = 0.2,
            PointsBalance = 50
        });
    }
}
