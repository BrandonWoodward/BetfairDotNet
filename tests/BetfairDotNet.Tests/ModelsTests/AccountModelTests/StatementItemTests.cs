using BetfairDotNet.Converters;
using BetfairDotNet.Enums.Account;
using BetfairDotNet.Models.Account;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.AccountModelTests;

public class StatementItemTests {

    [Fact]
    public void TestStatementItemSerialization() {
        // Arrange
        var statementItem = new StatementItem {
            RefId = "some-ref-id",
            ItemDate = DateTime.UtcNow,
            Amount = 50.0,
            Balance = 200.0,
            ItemClass = ItemClassEnum.UNKNOWN, // Replace with an actual enum value
            ItemClassData = new Dictionary<string, string>
            {
                    { "key1", "value1" },
                    { "key2", "value2" }
                }
        };

        // Act
        var json = JsonConvert.Serialize(statementItem);
        var deserializedStatementItem = JsonConvert.Deserialize<StatementItem>(json);

        // Assert
        deserializedStatementItem.Should().Be(statementItem);
    }
}
