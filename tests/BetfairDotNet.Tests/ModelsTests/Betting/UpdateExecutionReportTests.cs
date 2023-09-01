using BetfairDotNet.Converters;
using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Models.Betting;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Betting;

public class UpdateExecutionReportTests {

    [Fact]
    public void TestUpdateExecutionReportSerialization() {
        // Arrange
        var updateExecutionReport = new UpdateExecutionReport {
            CustomerRef = "some-customer-ref",
            Status = ExecutionReportStatusEnum.SUCCESS, // Replace with an actual enum value
            ErrorCode = ExecutionReportErrorCodeEnum.BET_ACTION_ERROR, // Replace with an actual enum value
            MarketId = "some-market-id",
            InstructionReports = new List<UpdateInstructionReport>()
        };

        // Act
        var json = JsonConvert.Serialize(updateExecutionReport);
        var deserializedUpdateExecutionReport = JsonConvert.Deserialize<UpdateExecutionReport>(json);

        // Assert
        deserializedUpdateExecutionReport.Should().BeEquivalentTo(updateExecutionReport);
    }
}
