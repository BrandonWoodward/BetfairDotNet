using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Enums.Streaming;
using BetfairDotNet.Factories;
using BetfairDotNet.Models.Betting;
using BetfairDotNet.Models.Streaming;
using FluentAssertions;
using System.Collections.Concurrent;
using Xunit;

namespace BetfairDotNet.Tests.FactoryTests;


public class MarketSnapshotFactoryTests {


    [Fact]
    public void ProcessImage_ShouldReturnEmpty_WhenMessageIsHeartbeat() {
        // Arrange
        var changeMessage = new MarketChangeMessage() { Id = 123, ChangeType = ChangeTypeEnum.HEARTBEAT };
        var cache = new ConcurrentDictionary<string, MarketSnapshot>();

        // Act
        var sut = new MarketSnapshotFactory(cache);
        var actual = sut.GetSnapshots(changeMessage);

        // Assert
        actual.Should().BeEmpty();
    }


    [Fact]
    public void ProcessImage_ShouldReturnMarketSnapshot_WhenMessageIsImage() {
        // Arrange
        var atl = new List<List<double>>() { new() { 1.23, 150 }, new() { 1.24, 300 } };
        var atb = new List<List<double>>() { new() { 1.25, 150 }, new() { 1.26, 300 } };
        var trd = new List<List<double>>() { new() { 1.27, 100 }, new() { 1.28, 100 } };
        var rnrChange = new RunnerChange() { Id = 12345, LastTradedPrice = 1.29, AvailableToBack = atb, AvailableToLay = atl, TradedVolume = trd };
        var rnrChanges = new List<RunnerChange> { rnrChange };
        var rnrDef = new RunnerDefinition() { Id = 12345, SortPriority = 1, Status = RunnerStatusEnum.ACTIVE };
        var rnrDefs = new List<RunnerDefinition> { rnrDef };
        var marketDef = new MarketDefinition() { Status = MarketStatusEnum.OPEN, Runners = rnrDefs };
        var marketChange = new MarketChange() { Id = "marketId", IsImage = true, RunnerChanges = rnrChanges, MarketDefinition = marketDef };
        var marketChanges = new List<MarketChange> { marketChange };
        var changeMessage = new MarketChangeMessage() { Id = 123, ChangeType = ChangeTypeEnum.SUB_IMAGE, MarketChanges = marketChanges };

        var expAtl = new PriceLadder(SideEnum.LAY, new List<PriceSize> { new(1.23, 150), new(1.24, 300) });
        var expAtb = new PriceLadder(SideEnum.BACK, new List<PriceSize> { new(1.25, 150), new(1.26, 300) });
        var expTrd = new PriceLadder(SideEnum.BACK, new List<PriceSize> { new(1.27, 100), new(1.28, 100) });
        var expRnrDef = new RunnerDefinition() { Id = 12345, SortPriority = 1, Status = RunnerStatusEnum.ACTIVE };
        var expRnrSnap = new RunnerSnapshot() { SelectionId = 12345, LastTradedPrice = 1.29, ToBack = expAtb, ToLay = expAtl, Traded = expTrd, RunnerDefinition = expRnrDef };
        var expRnrSnaps = new Dictionary<long, RunnerSnapshot> { [12345] = expRnrSnap };
        var expMarketDef = new MarketDefinition() { Status = MarketStatusEnum.OPEN, Runners = new List<RunnerDefinition> { expRnrDef } };
        var expectedSnapshot = new MarketSnapshot() { MarketId = "marketId", MarketDefinition = expMarketDef, RunnerSnapshots = expRnrSnaps };
        var cache = new ConcurrentDictionary<string, MarketSnapshot>();

        // Act
        var sut = new MarketSnapshotFactory(cache);
        var actual = sut.GetSnapshots(changeMessage).First();

        // Assert
        actual.Should().BeEquivalentTo(expectedSnapshot);
    }


    [Fact]
    public void ProcessImage_ShouldReturnMarketSnapshot_WhenMessageIsDelta() {
        // Arrange
        var atl = new List<List<double>>() { new() { 1.23, 150 } };
        var atb = new List<List<double>>() { new() { 1.25, 75 }, new() { 1.26, 0 } };
        var trd = new List<List<double>>() { new() { 1.28, 100 } };
        var rnrChange = new RunnerChange() { Id = 12345, LastTradedPrice = 1.29, AvailableToBack = atb, AvailableToLay = atl, TradedVolume = trd };
        var rnrChanges = new List<RunnerChange> { rnrChange };
        var rnrDef = new RunnerDefinition() { Id = 12345, SortPriority = 1, Status = RunnerStatusEnum.ACTIVE };
        var rnrDefs = new List<RunnerDefinition> { rnrDef };
        var marketDef = new MarketDefinition() { Status = MarketStatusEnum.OPEN, Runners = rnrDefs };
        var marketChange = new MarketChange() { Id = "marketId", IsImage = false, RunnerChanges = rnrChanges, MarketDefinition = marketDef };
        var marketChanges = new List<MarketChange> { marketChange };
        var changeMessage = new MarketChangeMessage() { Id = 123, ChangeType = ChangeTypeEnum.DELTA, MarketChanges = marketChanges };

        var cachedAtl = new PriceLadder(SideEnum.LAY, new List<PriceSize> { new(1.24, 300) });
        var cachedAtb = new PriceLadder(SideEnum.BACK, new List<PriceSize> { new(1.26, 300), new(1.25, 75) });
        var cachedTrd = new PriceLadder(SideEnum.BACK, new List<PriceSize> { new(1.27, 100) });
        var cachedRnrDef = new RunnerDefinition() { Id = 12345, SortPriority = 1, Status = RunnerStatusEnum.ACTIVE };
        var cachedRnrSnap = new RunnerSnapshot() { SelectionId = 12345, LastTradedPrice = 1.28, ToBack = cachedAtb, ToLay = cachedAtl, Traded = cachedTrd, RunnerDefinition = cachedRnrDef };
        var cachedRnrSnaps = new Dictionary<long, RunnerSnapshot> { [12345] = cachedRnrSnap };
        var cachedMarketDef = new MarketDefinition() { Status = MarketStatusEnum.SUSPENDED, Runners = new List<RunnerDefinition> { cachedRnrDef } };
        var cachedSnapshot = new MarketSnapshot() { MarketId = "marketId", MarketDefinition = cachedMarketDef, RunnerSnapshots = cachedRnrSnaps };
        var cache = new ConcurrentDictionary<string, MarketSnapshot>() { ["marketId"] = cachedSnapshot };

        var expAtl = new PriceLadder(SideEnum.LAY, new List<PriceSize> { new(1.23, 150), new(1.24, 300) });
        var expAtb = new PriceLadder(SideEnum.BACK, new List<PriceSize> { new(1.25, 75) });
        var expTrd = new PriceLadder(SideEnum.BACK, new List<PriceSize> { new(1.28, 100), new(1.27, 100) });
        var expRnrDef = new RunnerDefinition() { Id = 12345, SortPriority = 1, Status = RunnerStatusEnum.ACTIVE };
        var expRnrSnap = new RunnerSnapshot() { SelectionId = 12345, LastTradedPrice = 1.29, ToBack = expAtb, ToLay = expAtl, Traded = expTrd, RunnerDefinition = expRnrDef };
        var expRnrSnaps = new Dictionary<long, RunnerSnapshot> { [12345] = expRnrSnap };
        var expMarketDef = new MarketDefinition() { Status = MarketStatusEnum.OPEN, Runners = new List<RunnerDefinition> { expRnrDef } };
        var expSnapshot = new MarketSnapshot() { MarketId = "marketId", MarketDefinition = expMarketDef, RunnerSnapshots = expRnrSnaps };

        // Act
        var sut = new MarketSnapshotFactory(cache);
        var actual = sut.GetSnapshots(changeMessage).First();

        // Assert
        actual.Should().BeEquivalentTo(expSnapshot);
    }
}
