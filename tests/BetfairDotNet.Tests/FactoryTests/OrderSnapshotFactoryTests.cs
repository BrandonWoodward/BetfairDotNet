using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Enums.Streaming;
using BetfairDotNet.Factories;
using BetfairDotNet.Models.Betting;
using BetfairDotNet.Models.Streaming;
using FluentAssertions;
using System.Collections.Concurrent;
using System.Text.Json;
using Xunit;

namespace BetfairDotNet.Tests.FactoryTests;

// TODO clean up this mess
public class OrderSnapshotFactoryTests {

    [Fact]
    public void ProcessImage_ShouldReturnEmpty_WhenMessageIsHeartbeat() {
        // Arrange
        var changeMessage = new OrderChangeMessage() { Id = 123, ChangeType = ChangeTypeEnum.HEARTBEAT };
        var cache = new ConcurrentDictionary<string, OrderMarketSnapshot>();

        // Act
        var sut = new OrderSnapshotFactory(cache);
        var actual = sut.GetSnapshots(changeMessage);

        // Assert
        actual.Should().BeEmpty();
    }


    [Fact]
    public void ProcessImage_ShouldReturnOrderMarketSnapshot_WhenMessageIsImage() {
        // Arrange
        var uo = new List<ESAOrder> { new ESAOrder() { Id = "betId", Size = 100, Price = 6.78 } };
        var runnerChange = new OrderRunnerChange() { Id = 12345, UnmatchedOrders = uo };
        var runnersChanges = new List<OrderRunnerChange> { runnerChange };
        var marketChange = new OrderMarketChange() { Id = "marketId", IsImage = true, OrderRunnerChanges = runnersChanges };
        var marketChanges = new List<OrderMarketChange> { marketChange };
        var changeMessage = new OrderChangeMessage() { Id = 123, ChangeType = ChangeTypeEnum.SUB_IMAGE, OrderChanges = marketChanges };

        var expUo = new Dictionary<string, ESAOrder> { ["betId"] = new ESAOrder() { Id = "betId", Size = 100, Price = 6.78 } };
        var expRunnerSnapshot = new OrderRunnerSnapshot(12345) { UnmatchedOrders = expUo };
        var expRunnerSnapshots = new Dictionary<long, OrderRunnerSnapshot> { [12345] = expRunnerSnapshot };
        var expSnapshot = new OrderMarketSnapshot() { MarketId = "marketId", OrderRunnerSnapshots = expRunnerSnapshots };

        var cache = new ConcurrentDictionary<string, OrderMarketSnapshot>();

        // Act
        var sut = new OrderSnapshotFactory(cache);
        var actual = sut.GetSnapshots(changeMessage).First();

        // Assert
        Assert.Equal(JsonSerializer.Serialize(expSnapshot), JsonSerializer.Serialize(actual));
    }


    [Fact]
    public void ProcessImage_ShouldReturnOrderMarketSnapshot_WhenMessageIsDelta() {
        // Arrange
        var mb = new List<List<double>>() { new() { 1.23, 150 } };
        var ml = new List<List<double>>() { new() { 3.45, 300 } };
        var uo = new List<ESAOrder> {
            new ESAOrder() { Id = "betId1", Size = 200, Price = 6.78, SizeRemaining = 50 },
            new ESAOrder() { Id = "betId2", Size = 300, Price = 9.87, SizeRemaining = 0, Status = OrderStatusEnum.EXECUTION_COMPLETE }
        };
        var runnerChange = new OrderRunnerChange() { Id = 12345, MatchedBacks = mb, MatchedLays = ml, UnmatchedOrders = uo };
        var runnersChanges = new List<OrderRunnerChange> { runnerChange };
        var marketChange = new OrderMarketChange() { Id = "marketId", OrderRunnerChanges = runnersChanges };
        var marketChanges = new List<OrderMarketChange> { marketChange };
        var changeMessage = new OrderChangeMessage() { Id = 123, ChangeType = ChangeTypeEnum.DELTA, OrderChanges = marketChanges };

        var cacheMb = new PriceLadder(SideEnum.BACK, new List<PriceSize> { new PriceSize(1.23, 100) });
        var cacheMl = new PriceLadder(SideEnum.LAY, new List<PriceSize> { new PriceSize(3.45, 100) });
        var cacheUo = new Dictionary<string, ESAOrder> {
            ["betId1"] = new ESAOrder() { Id = "betId1", Size = 200, Price = 6.78, SizeRemaining = 100 },
            ["betId2"] = new ESAOrder() { Id = "betId2", Size = 100, Price = 9.87, SizeRemaining = 200 }
        };
        var cacheRunnerSnapshot = new OrderRunnerSnapshot(12345) { MatchedBacks = cacheMb, MatchedLays = cacheMl, UnmatchedOrders = cacheUo };
        var cacheRunnerSnapshots = new Dictionary<long, OrderRunnerSnapshot> { [12345] = cacheRunnerSnapshot };
        var cacheSnapshot = new OrderMarketSnapshot() { MarketId = "marketId", OrderRunnerSnapshots = cacheRunnerSnapshots };
        var cache = new ConcurrentDictionary<string, OrderMarketSnapshot>() { ["marketId"] = cacheSnapshot };

        var expMb = new PriceLadder(SideEnum.BACK, new List<PriceSize> { new PriceSize(1.23, 150) });
        var expMl = new PriceLadder(SideEnum.LAY, new List<PriceSize> { new PriceSize(3.45, 300) });
        var expUo = new Dictionary<string, ESAOrder> {
            ["betId1"] = new ESAOrder() { Id = "betId1", Size = 200, Price = 6.78, SizeRemaining = 50 },
        };
        var expRunnerSnapshot = new OrderRunnerSnapshot(12345) { MatchedBacks = expMb, MatchedLays = expMl, UnmatchedOrders = expUo };
        var expRunnerSnapshots = new Dictionary<long, OrderRunnerSnapshot> { [12345] = expRunnerSnapshot };
        var expSnapshot = new OrderMarketSnapshot() { MarketId = "marketId", OrderRunnerSnapshots = expRunnerSnapshots };

        // Act
        var sut = new OrderSnapshotFactory(cache);
        var actual = sut.GetSnapshots(changeMessage).First();

        // Assert
        // Assert.Equal(JsonSerializer.Serialize(expSnapshot), JsonSerializer.Serialize(actual));
    }
}
