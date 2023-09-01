using BetfairDotNet.Converters;
using BetfairDotNet.Models.Account;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.AccountModelTests;
public class AccountStatementReportTests {

    [Fact]
    public void AccountStatementReport_ShouldDeserializeCorrectly() {
        // Arrange
        var accountStatementReport = new AccountStatementReport {
            AccountStatement = new List<StatementItem>(),
            MoreAvailable = true
        };

        // Act
        var json = JsonConvert.Serialize(accountStatementReport);
        var deserializedAccountStatementReport = JsonConvert.Deserialize<AccountStatementReport>(json);

        // Assert
        deserializedAccountStatementReport.Should().Be(accountStatementReport);
    }
}
