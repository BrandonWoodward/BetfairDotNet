using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Models.Betting;
using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.BettingModelTests;
public class PlaceExecutionReportTests
{

    [Fact]
    public void PlaceExecutionReport_ShouldDeserializeCorrectly()
    {
        //Arrange
        var placeExecutionReport = new PlaceExecutionReport
        {
            CustomerRef = "some-customer-ref",
            Status = ExecutionReportStatusEnum.PROCESSED_WITH_ERRORS,
            ErrorCode = ExecutionReportErrorCodeEnum.INSUFFICIENT_FUNDS,
            MarketId = "1.123456",
            InstructionReports = new List<PlaceInstructionReport>()
        };

        // Act
        var json = JsonSerializer.Serialize(placeExecutionReport);
        var deserializedPlaceExecutionReport = JsonSerializer.Deserialize<PlaceExecutionReport>(json);

        // Assert
        placeExecutionReport.Should().BeEquivalentTo(deserializedPlaceExecutionReport);
    }
}
