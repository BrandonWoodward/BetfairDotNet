using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Models.Betting;
using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Betting;
public class ClearedSummaryReportTests {

    [Fact]
    public void ClearedOrderSummaryReport_ShouldDeserializeProperly() {
        // Arrange
        var json = @"
        {
            ""clearedOrders"": [
                {
                    ""eventTypeId"": ""1"",
                    ""eventId"": ""2"",
                    ""marketId"": ""3"",
                    ""selectionId"": 4,
                    ""handicap"": 0,
                    ""betId"": ""5"",
                    ""placedDate"": ""2023-01-01T00:00:00"",
                    ""persistenceType"": ""LAPSE"",
                    ""orderType"": ""LIMIT"",
                    ""side"": ""BACK"",
                    ""itemDescription"": null,
                    ""betOutcome"": ""WIN"",
                    ""priceRequested"": 1.1,
                    ""settledDate"": ""2023-01-02T00:00:00"",
                    ""lastMatchedDate"": ""2023-01-03T00:00:00"",
                    ""betCount"": 1,
                    ""commission"": 0.1,
                    ""priceMatched"": 1.0,
                    ""priceReduced"": false,
                    ""sizeSettled"": 100,
                    ""profit"": 10,
                    ""sizeCancelled"": 0,
                    ""customerOrderRef"": ""order1"",
                    ""customerStrategyRef"": ""strategy1""
                }
            ],
            ""moreAvailable"": true
        }";

        var expectedReport = new ClearedOrderSummaryReport {
            ClearedOrders = new List<ClearedOrderSummary> {
                new ClearedOrderSummary {
                        EventTypeId = "1",
                        EventType = "2",
                        MarketId = "3",
                        SelectionId = 4,
                        Handicap = 0,
                        BetId = "5",
                        PlacedDate = new DateTime(2023, 1, 1, 0, 0, 0),
                        PersistenceType = PersistenceTypeEnum.LAPSE,
                        OrderType = OrderTypeEnum.LIMIT,
                        Side = SideEnum.BACK,
                        ItemDescription = null,
                        BetOutcome = "WIN",
                        Price = 1.1,
                        SettledDate = new DateTime(2023, 1, 2, 0, 0, 0),
                        LastMatchedDate = new DateTime(2023, 1, 3, 0, 0, 0),
                        BetCount = 1,
                        Commission = 0.1,
                        PriceMatched = 1.0,
                        PriceReduced = false,
                        SizeSettled = 100,
                        Profit = 10,
                        SizeCancelled = 0,
                        CustomerOrderRef = "order1",
                        CustomerStrategyRef = "strategy1"
                    }
                },
            MoreAvailable = true
        };

        // Act
        var deserializedReport = JsonSerializer.Deserialize<ClearedOrderSummaryReport>(json);

        // Assert
        deserializedReport.Should().BeEquivalentTo(expectedReport);
    }
}
