using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Models.Betting;
using BetfairDotNet.Models.Streaming;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Betting;
public class PriceLadderTests 
{
    
    [Fact]
    public void AddOrUpdateLevelByPrice_UpdatesSize_WhenPriceExists() {
        // Arrange
        var ladder = new PriceLadder(SideEnum.BACK);
        ladder.AddOrUpdateLevel(new(1.5, 100));

        // Act
        ladder.AddOrUpdateLevel(new(1.5, 200));

        // Assert
        ladder[1.5].Size.Should().Be(200);
        ladder[1.5].Price.Should().Be(1.5);
    }


    [Fact]
    public void AddOrUpdateLevelByPrice_AddsLevel_WhenPriceNotExists() {
        // Arrange
        var ladder = new PriceLadder(SideEnum.BACK);

        // Act
        ladder.AddOrUpdateLevel(new(1.5, 100));

        // Assert
        ladder.GetDepth().Should().Be(1);
        ladder[1.5].Size.Should().Be(100);
        ladder[1.5].Price.Should().Be(1.5);
    }


    [Fact]
    public void RemoveLevelByDepth_RemovesLevel_WhenPriceExists() {
        // Arrange
        var ladder = new PriceLadder(SideEnum.BACK);
        ladder.AddOrUpdateLevel(new(1.5, 100));
        ladder.AddOrUpdateLevel(new(2.5, 150));

        // Act
        ladder.RemoveLevel(2.5);

        // Assert
        ladder.GetDepth().Should().Be(1);
        ladder[1.5].Size.Should().Be(100);
    }


    [Fact]
    public void RemoveLevel_LeavesLadderUnchanged_WhenPriceNotExists() {
        // Arrange
        var ladder = new PriceLadder(SideEnum.BACK);
        ladder.AddOrUpdateLevel(new(1.5, 100));
        ladder.AddOrUpdateLevel(new(2.5, 150));

        // Act
        ladder.RemoveLevel(3);

        // Assert
        ladder.GetDepth().Should().Be(2);
        ladder[1.5].Size.Should().Be(100);
        ladder[2.5].Size.Should().Be(150);
    }
    

    [Fact]
    public void PriceLadder_ShouldSortAscending_WhenSideIsBack() {
        // Arrange
        var ladder = new PriceLadder(SideEnum.BACK);
        ladder.AddOrUpdateLevel(new(2.5, 150));
        ladder.AddOrUpdateLevel(new(3.5, 200));
        ladder.AddOrUpdateLevel(new(1.5, 100));

        // Assert
        ladder[0].Should().BeEquivalentTo(new PriceSize(3.5, 200));
        ladder[1].Should().BeEquivalentTo(new PriceSize(2.5, 150));
        ladder[2].Should().BeEquivalentTo(new PriceSize(1.5, 100));
    }


    [Fact]
    public void PriceLadder_ShouldSortDescending_WhenSideIsLay() {
        // Arrange
        var ladder = new PriceLadder(SideEnum.LAY);
        ladder.AddOrUpdateLevel(new(2.5, 150));
        ladder.AddOrUpdateLevel(new(3.5, 200));
        ladder.AddOrUpdateLevel(new(1.5, 100));

        // Assert
        ladder[0].Should().BeEquivalentTo(new PriceSize(1.5, 100));
        ladder[1].Should().BeEquivalentTo(new PriceSize(2.5, 150));
        ladder[2].Should().BeEquivalentTo(new PriceSize(3.5, 200));
    }
}
