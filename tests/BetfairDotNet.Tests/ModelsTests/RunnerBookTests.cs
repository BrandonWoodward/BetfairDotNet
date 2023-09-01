using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Models.Betting;
using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests;
public class RunnerBookTests {

    [Fact]
    public void TestRunnerBookSerialization() {
        var runnerBook = new RunnerBook {
            SelectionId = 123,
            Handicap = 1.2,
            Status = RunnerStatusEnum.PLACED,
            AdjustmentFactor = 0.9,
            LastPriceTraded = 2.1,
            TotalMatched = 200,
            RemovalDate = DateTime.Now,
            StartingPrices = new StartingPrices {
                NearPrice = 1.5,
                FarPrice = 1.6,
                BackStakeTaken = new List<PriceSize>(),
                LayLiabilityTaken = new List<PriceSize>(),
                ActualSP = 1.7
            },
            ExchangePrices = new ExchangePrices {
                AvailableToBack = new List<PriceSize>(),
                AvailableToLay = new List<PriceSize>(),
                TradedVolume = new List<PriceSize>()
            },
            Orders = new List<Order> {
                new Order
                {
                    BetId = "bet123",
                    OrderType = OrderTypeEnum.LIMIT_ON_CLOSE,
                    Status = OrderStatusEnum.EXECUTABLE,
                    PersistenceType = PersistenceTypeEnum.MARKET_ON_CLOSE,
                    Side = SideEnum.LAY,
                    Price = 1.8,
                    Size = 50,
                    BspLiability = 100,
                    PlacedDate = DateTime.Now,
                    AvgPriceMatched = 1.9,
                    SizeMatched = 50,
                    SizeRemaining = 0,
                    SizeLapsed = 0,
                    SizeCancelled = 0,
                    SizeVoided = 0,
                    CustomerOrderRef = "ref1",
                    CustomerStrategyRef = "strat1"
                }
            },
            Matches = new List<Match> {
                new Match {
                    BetId = "bet123",
                    MatchId = "match123",
                    Side = SideEnum.BACK,
                    Price = 1.9,
                    Size = 40,
                    MatchDate = DateTime.Now
                }
            },
            MatchesByStrategy = new Dictionary<string, List<Match>>() {
                { "strategy1", new List<Match>() { new() {
                    BetId = "123",
                    MatchId = "345",
                    MatchDate = DateTime.Now,
                    Price = 1.23,
                    Side = SideEnum.BACK,
                    Size = 345  } } }
            }
        };

        // Act
        var json = JsonSerializer.Serialize(runnerBook);
        var deserializedRunnerBook = JsonSerializer.Deserialize<RunnerBook>(json);

        // Assert
        runnerBook.Should().BeEquivalentTo(deserializedRunnerBook);
    }
}
