using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Models.Betting;
using FluentAssertions;
using System.Text.Json;

namespace BetfairDotNet.Tests.ModelsTests.BettingModelTests;

public class PlaceInstructionReportTests {

    public void PlaceInstructionReport_ShouldDeserializeCorrectly() {
        var placeInstructionReport = new PlaceInstructionReport {
            Status = InstructionReportStatusEnum.SUCCESS,
            ErrorCode = InstructionReportErrorCodeEnum.ERROR_IN_MATCHER,
            OrderStatus = OrderStatusEnum.EXECUTABLE,
            Instruction = new PlaceInstruction {
                OrderType = OrderTypeEnum.LIMIT,
                SelectionId = 123456,
                Side = SideEnum.BACK,
            },
            BetId = "some-bet-id",
            PlacedDate = DateTime.UtcNow,
            AveragePriceMatched = 1.5,
            SizeMatched = 100.0
        };

        var json = JsonSerializer.Serialize(placeInstructionReport);
        var deserializedPlaceInstructionReport = JsonSerializer.Deserialize<PlaceInstructionReport>(json);

        // Your assertions here to compare 'placeInstructionReport' and 'deserializedPlaceInstructionReport'
        placeInstructionReport.Should().BeEquivalentTo(deserializedPlaceInstructionReport);
    }
}
