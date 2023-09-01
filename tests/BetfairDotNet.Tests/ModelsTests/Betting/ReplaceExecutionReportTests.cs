using BetfairDotNet.Converters;
using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Models.Betting;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Betting;

public class ReplaceExecutionReportTests {

    [Fact]
    public void ReplaceExecutionReport_ShouldDeserializeCorrectly() {
        // Arrange
        var replaceExecutionReport = new ReplaceExecutionReport {
            CustomerRef = "some-customer-ref",
            Status = ExecutionReportStatusEnum.FAILURE,
            ErrorCode = ExecutionReportErrorCodeEnum.PERMISSION_DENIED,
            MarketId = "some-market-id",
            InstructionReports = new List<ReplaceInstructionReport> {
                new ReplaceInstructionReport {
                    Status = InstructionReportStatusEnum.SUCCESS,

                },
            }
        };

        // Act
        var json = JsonConvert.Serialize(replaceExecutionReport);
        var deserializedReplaceExecutionReport = JsonConvert.Deserialize<ReplaceExecutionReport>(json);

        // Assert
        deserializedReplaceExecutionReport.Should().BeEquivalentTo(replaceExecutionReport);
    }
}
