using BetfairDotNet.Converters;
using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Models.Betting;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Betting;

public class UpdateExecutionReportTests {

    [Fact]
    public void UpdateExecutionReport_ShouldSerializeProperly() {
        // Arrange
        var updateExecutionReport = new UpdateExecutionReport {
            CustomerRef = "some-customer-ref",
            Status = ExecutionReportStatusEnum.SUCCESS,
            ErrorCode = ExecutionReportErrorCodeEnum.BET_ACTION_ERROR,
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
