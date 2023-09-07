using BetfairDotNet.Converters;
using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Models.Betting;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Betting;

public class UpdateInstructionReportTests {


    [Fact]
    public void UpdateInstructionReport_ShouldSerializeProperly() {
        // Arrange
        var updateInstructionReport = new UpdateInstructionReport {
            Status = InstructionReportStatusEnum.SUCCESS,
            ErrorCode = InstructionReportErrorCodeEnum.INVALID_BET_SIZE,
            Instruction = new UpdateInstruction {
                BetId = "some-bet-id",
                NewPersistenceType = PersistenceTypeEnum.LAPSE,
            }
        };

        // Act
        var json = JsonConvert.Serialize(updateInstructionReport);
        var deserializedUpdateExecutionReport = JsonConvert.Deserialize<UpdateInstructionReport>(json);

        // Assert
        deserializedUpdateExecutionReport.Should().BeEquivalentTo(updateInstructionReport);
    }
}
