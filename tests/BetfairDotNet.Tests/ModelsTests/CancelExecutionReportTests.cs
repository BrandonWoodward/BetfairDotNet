using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Models.Betting;
using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests;

public class CancelExecutionReportTests {

    [Fact]
    public void CancelInstruction_ShouldSerializeProperly() {
        // Arrange
        var json = @"
        {
            ""customerRef"": ""SampleRef"",
            ""status"": ""SUCCESS"",
            ""errorCode"": ""ERROR_IN_MATCHER"",
            ""marketId"": ""Market123"",
            ""instruction"": [
                {
                    ""status"": ""SUCCESS"",
                    ""errorCode"": """",
                    ""instruction"": {
                        ""betId"": ""Bet1"",
                        ""sizeReduction"": 10.5
                    },
                    ""sizeCancelled"": 12.5,
                    ""cancelledDate"": ""2023-01-01T00:00:00""
                }
            ]
        }";

        // Act
        var deserialized = JsonSerializer.Deserialize<CancelExecutionReport>(json);

        // Assert
        var expected = new CancelExecutionReport {
            CustomerRef = "SampleRef",
            Status = InstructionReportStatusEnum.SUCCESS,
            ErrorCode = InstructionReportErrorCodeEnum.ERROR_IN_MATCHER,
            MarketId = "Market123",
            Instruction = new List<CancelInstructionReport>
            {
                    new CancelInstructionReport
                    {
                        Status = InstructionReportStatusEnum.SUCCESS,
                        ErrorCode = InstructionReportErrorCodeEnum.NONE,
                        Instruction = new CancelInstruction
                        {
                            BetId = "Bet1",
                            SizeReduction = 10.5
                        },
                        SizeCancelled = 12.5,
                        CancelledDate = new DateTime(2023, 1, 1, 0, 0, 0)
                    }
                }
        };
        deserialized.Should().BeEquivalentTo(expected);
    }
}
