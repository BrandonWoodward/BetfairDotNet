using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Models.Betting;
using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Betting;
public class CurrentOrderSummaryReportTests {

    [Fact]
    public void CurrentOrderSummaryReport_ShouldDeserializeProperly() {
        // Arrange
        var json = @"
        {
            ""currentOrders"": [
                {
                    ""betId"": ""1"",
                    ""marketId"": ""2"",
                    ""selectionId"": 3,
                    ""handicap"": 0,
                    ""priceSize"": {
                        ""price"": 1.1,
                        ""size"": 2.2
                    },
                    ""bspLiability"": 3.3,
                    ""side"": ""LAY"",
                    ""status"": ""EXECUTION_COMPLETE"",
                    ""persistenceType"": ""LAPSE"",
                    ""orderType"": ""LIMIT"",
                    ""placedDate"": ""2023-01-01T00:00:00"",
                    ""matchedDate"": ""2023-01-02T00:00:00"",
                    ""averagePriceMatched"": 4.4,
                    ""sizeMatched"": 5.5,
                    ""sizeRemaining"": 6.6,
                    ""sizeLapsed"": 7.7,
                    ""sizeCancelled"": 8.8,
                    ""sizeVoided"": 9.9,
                    ""regulatorAuthCode"": ""regulator1"",
                    ""regulatorCode"": ""regulator2"",
                    ""customerOrderRef"": ""order1"",
                    ""customerStrategyRef"": ""strategy1""
                }
            ],
            ""moreAvailable"": true
        }";

        var expectedReport = new CurrentOrderSummaryReport {
            CurrentOrders = new List<CurrentOrderSummary> {
                new CurrentOrderSummary {
                    BetId = "1",
                    MarketId = "2",
                    SelectionId = 3,
                    Handicap = 0,
                    PriceSize = new PriceSize(1.1, 2.2),
                    BspLiability = 3.3,
                    Side = SideEnum.LAY,
                    Status = OrderStatusEnum.EXECUTION_COMPLETE,
                    PersistenceType = PersistenceTypeEnum.LAPSE,
                    OrderType = OrderTypeEnum.LIMIT,
                    PlacedDate = new DateTime(2023, 1, 1, 0, 0, 0),
                    MatchedDate = new DateTime(2023, 1, 2, 0, 0, 0),
                    AveragePriceMatched = 4.4,
                    SizeMatched = 5.5,
                    SizeRemaining = 6.6,
                    SizeLapsed = 7.7,
                    SizeCancelled = 8.8,
                    SizeVoided = 9.9,
                    RegulatorAuthCode = "regulator1",
                    RegulatorCode = "regulator2",
                    CustomerOrderRef = "order1",
                    CustomerStrategyRef = "strategy1"
                }
            },
            MoreAvailable = true
        };

        // Act
        var deserializedReport = JsonSerializer.Deserialize<CurrentOrderSummaryReport>(json);

        // Assert
        deserializedReport.Should().BeEquivalentTo(expectedReport);
    }
}
