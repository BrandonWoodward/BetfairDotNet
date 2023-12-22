using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Models.Betting;
using BetfairDotNet.Models.Streaming;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Betting;
public class PriceLadderTests 
{
    
    [Fact]
    public void PriceLadder_UpdatesSize_WhenPriceExists() 
    {
        // Arrange
        var ladder = new PriceLadder(SideEnum.BACK)
        {
            [1.5] = new(1.5, 100)
        };

        // Act
        ladder[1.5] = new(1.5, 200);

        // Assert
        ladder[1.5].Should().NotBeNull();
        ladder[1.5]?.Size.Should().Be(200);
        ladder[1.5]?.Price.Should().Be(1.5);
    }


    [Fact]
    public void PriceLadder_AddsLevel_WhenPriceNotExists() 
    {
        // Arrange
        var ladder = new PriceLadder(SideEnum.BACK);

        // Act
        ladder[1.5] = new(1.5, 100);

        // Assert
        ladder.Depth.Should().Be(1);
        ladder[1.5]?.Size.Should().Be(100);
        ladder[1.5]?.Price.Should().Be(1.5);
    }


    [Fact]
    public void PriceLadder_RemovesLevel_WhenPriceExists() 
    {
        // Arrange
        var ladder = new PriceLadder(SideEnum.BACK)
        {
            [1.5] = new(1.5, 100),
            [2.5] = new(2.5, 150)
        };

        // Act
        ladder[2.5] = new(2.5, 0.00);

        // Assert
        ladder.Depth.Should().Be(1);
        ladder[1.5]?.Size.Should().Be(100);
    }


    [Fact]
    public void PriceLadder_LeavesLadderUnchanged_WhenPriceNotExists() {
        // Arrange
        var ladder = new PriceLadder(SideEnum.BACK)
        {
            [1.5] = new(1.5, 100),
            [2.5] = new(2.5, 150)
        };

        // Act
        ladder[3.00] = new(3.00, 0.00);

        // Assert
        ladder.Depth.Should().Be(2);
        ladder[1.5]?.Size.Should().Be(100);
        ladder[2.5]?.Size.Should().Be(150);
    }
    
    [Fact]
    public void DepthBasedPriceLadder_UpdatesSize_WhenPriceExists() 
    {
        // Arrange
        var ladder = new PriceLadder(SideEnum.BACK)
        {
            [0] = new(1.5, 100)
        };

        // Act
        ladder[0] = new(1.5, 200);

        // Assert
        ladder[0].Should().NotBeNull();
        ladder[0]?.Size.Should().Be(200);
        ladder[0]?.Price.Should().Be(1.5);
    }


    [Fact]
    public void DepthBasedPriceLadder_AddsLevel_WhenPriceNotExists() 
    {
        // Arrange
        var ladder = new PriceLadder(SideEnum.BACK)
        {
            [0] = new(1.5, 100)
        };

        // Act
        ladder[0] = new(2.5, 50);

        // Assert
        ladder.Depth.Should().Be(2);
        ladder[0]?.Size.Should().Be(50);
        ladder[0]?.Price.Should().Be(2.5);
        ladder[1]?.Size.Should().Be(100);
        ladder[1]?.Price.Should().Be(1.5);
    }


    [Fact]
    public void DepthBasedPriceLadder_RemovesLevel_WhenPriceExists() 
    {
        // Arrange
        var ladder = new PriceLadder(SideEnum.BACK)
        {
            [0] = new(2.5, 150),
            [1] = new(1.5, 100),
        };

        // Act
        ladder[0] = new(2.5, 0.00);

        // Assert
        ladder.Depth.Should().Be(1);
        ladder[0]?.Price.Should().Be(1.5);
        ladder[0]?.Size.Should().Be(100);
    }


    [Fact]
    public void DepthBasedPriceLadder_LeavesLadderUnchanged_WhenIndexOutOfRange() {
        // Arrange
        var ladder = new PriceLadder(SideEnum.BACK)
        {
            [0] = new(2.5, 150),
            [1] = new(1.5, 100),
        };

        // Act
        ladder[2] = new(3.00, 0.00);

        // Assert
        ladder.Depth.Should().Be(2);
        ladder[0]?.Price.Should().Be(2.5);
        ladder[0]?.Size.Should().Be(150);
        ladder[1]?.Price.Should().Be(1.5);
        ladder[1]?.Size.Should().Be(100);
    }
    
    [Fact]
    public void PriceLadder_ShouldSortAscending_WhenSideIsBack() {
        // Arrange
        var ladder = new PriceLadder(SideEnum.BACK)
        {
            [2.5] = new(2.5, 150),
            [3.5] = new(3.5, 200),
            [1.5] = new(1.5, 100)
        };

        // Assert
        ladder[0].Should().BeEquivalentTo(new PriceSize(3.5, 200));
        ladder[1].Should().BeEquivalentTo(new PriceSize(2.5, 150));
        ladder[2].Should().BeEquivalentTo(new PriceSize(1.5, 100));
    }

    [Fact]
    public void PriceLadder_ShouldSortDescending_WhenSideIsLay() {
        // Arrange
        var ladder = new PriceLadder(SideEnum.LAY)
        {
            [2.5] = new(2.5, 150),
            [3.5] = new(3.5, 200),
            [1.5] = new(1.5, 100)
        };

        // Assert
        ladder[0].Should().BeEquivalentTo(new PriceSize(1.5, 100));
        ladder[1].Should().BeEquivalentTo(new PriceSize(2.5, 150));
        ladder[2].Should().BeEquivalentTo(new PriceSize(3.5, 200));
    }
}
