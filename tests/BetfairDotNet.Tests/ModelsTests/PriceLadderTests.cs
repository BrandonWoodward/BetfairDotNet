using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Models.Betting;
using BetfairDotNet.Models.Streaming;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests;
public class PriceLadderTests
{


    [Fact]
    public void AddLevel_AddsPriceAndSize()
    {
        // Arrange
        var ladder = new PriceLadder(SideEnum.BACK);

        // Act
        ladder.AddLevel(1.5, new PriceSize(1.5, 100));

        // Assert
        ladder.GetDepth().Should().Be(1);
        ladder[1.5].Size.Should().Be(100);
        ladder[1.5].Price.Should().Be(1.5);
    }


    [Fact]
    public void RemoveLevelByPrice_RemovesLevel_WhenPriceExists()
    {
        // Arrange
        var ladder = new PriceLadder(SideEnum.BACK);
        ladder.AddLevel(1.5, new PriceSize(1.5, 100));

        // Act
        ladder.RemoveLevelByPrice(1.5);

        // Assert
        ladder.GetDepth().Should().Be(0);
    }


    [Fact]
    public void RemoveLevelByPrice_LeavesLadderUnchanged_WhenPriceNotExists()
    {
        // Arrange
        var ladder = new PriceLadder(SideEnum.BACK);
        ladder.AddLevel(1.77, new PriceSize(1.5, 100));

        // Act
        ladder.RemoveLevelByPrice(1.5);

        // Assert
        ladder.GetDepth().Should().Be(1);
        ladder[1.77].Size.Should().Be(100);
    }


    [Fact]
    public void AddOrUpdateLevelByPrice_UpdatesSize_WhenPriceExists()
    {
        // Arrange
        var ladder = new PriceLadder(SideEnum.BACK);
        ladder.AddLevel(1.5, new PriceSize(1.5, 100));

        // Act
        ladder.AddOrUpdateLevelByPrice(1.5, new PriceSize(1.5, 200));

        // Assert
        ladder[1.5].Size.Should().Be(200);
        ladder[1.5].Price.Should().Be(1.5);
    }


    [Fact]
    public void AddOrUpdateLevelByPrice_AddsLevel_WhenPriceNotExists()
    {
        // Arrange
        var ladder = new PriceLadder(SideEnum.BACK);

        // Act
        ladder.AddOrUpdateLevelByPrice(1.5, new PriceSize(1.5, 100));

        // Assert
        ladder.GetDepth().Should().Be(1);
        ladder[1.5].Size.Should().Be(100);
        ladder[1.5].Price.Should().Be(1.5);
    }


    [Fact]
    public void RemoveLevelByDepth_RemovesLevel_WhenPriceExists()
    {
        // Arrange
        var ladder = new PriceLadder(SideEnum.BACK);
        ladder.AddLevel(1.5, new PriceSize(1.5, 100));
        ladder.AddLevel(2.5, new PriceSize(2.5, 150));

        // Act
        ladder.RemoveLevelByDepth(0);

        // Assert
        ladder.GetDepth().Should().Be(1);
        ladder[1.5].Size.Should().Be(100);
    }


    [Fact]
    public void RemoveLevelByDepth_LeavesLadderUnchanged_WhenDepthOutOfRange()
    {
        // Arrange
        var ladder = new PriceLadder(SideEnum.BACK);
        ladder.AddLevel(1.5, new PriceSize(1.5, 100));
        ladder.AddLevel(2.5, new PriceSize(2.5, 150));

        // Act
        ladder.RemoveLevelByDepth(3);

        // Assert
        ladder.GetDepth().Should().Be(2);
        ladder[1.5].Size.Should().Be(100);
        ladder[2.5].Size.Should().Be(150);
    }


    [Fact]
    public void AddOrUpdateByDepth_AddsLevel_WhenPriceNotExists()
    {
        // Arrange
        var ladder = new PriceLadder(SideEnum.BACK);
        ladder.AddLevel(1.5, new PriceSize(1.5, 100));

        // Act
        ladder.AddOrUpdateLevelByDepth(1, new PriceSize(1.6, 100));

        // Assert
        ladder.GetDepth().Should().Be(2);
        ladder[1.5].Size.Should().Be(100);
        ladder[1.6].Size.Should().Be(100);
    }


    [Fact]
    public void AddOrUpdateByDepth_UpdatesSize_WhenPriceExists()
    {
        // Arrange
        var ladder = new PriceLadder(SideEnum.BACK);
        ladder.AddLevel(1.5, new PriceSize(1.5, 100));

        // Act
        ladder.AddOrUpdateLevelByDepth(0, new PriceSize(1.5, 200));

        // Assert
        ladder.GetDepth().Should().Be(1);
        ladder[1.5].Price.Should().Be(1.5);
        ladder[1.5].Size.Should().Be(200);
    }


    [Fact]
    public void PriceLadder_ShouldSortAscending_WhenSideIsBack()
    {
        // Arrange
        var ladder = new PriceLadder(SideEnum.BACK);
        ladder.AddLevel(2.5, new PriceSize(2.5, 150));
        ladder.AddLevel(3.5, new PriceSize(3.5, 200));
        ladder.AddLevel(1.5, new PriceSize(1.5, 100));

        // Assert
        ladder[0].Should().BeEquivalentTo(new PriceSize(3.5, 200));
        ladder[1].Should().BeEquivalentTo(new PriceSize(2.5, 150));
        ladder[2].Should().BeEquivalentTo(new PriceSize(1.5, 100));
    }


    [Fact]
    public void PriceLadder_ShouldSortDescending_WhenSideIsLay()
    {
        // Arrange
        var ladder = new PriceLadder(SideEnum.LAY);
        ladder.AddLevel(2.5, new PriceSize(2.5, 150));
        ladder.AddLevel(3.5, new PriceSize(3.5, 200));
        ladder.AddLevel(1.5, new PriceSize(1.5, 100));

        // Assert
        ladder[0].Should().BeEquivalentTo(new PriceSize(1.5, 100));
        ladder[1].Should().BeEquivalentTo(new PriceSize(2.5, 150));
        ladder[2].Should().BeEquivalentTo(new PriceSize(3.5, 200));
    }
}
