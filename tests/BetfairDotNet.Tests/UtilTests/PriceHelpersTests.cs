using BetfairDotNet.Utils;
using Xunit;

namespace BetfairDotNet.Tests.UtilTests;

/// <summary>
/// Unit tests for <see cref="PriceHelpers"/>
/// </summary>
public class PriceHelpersTests {

    [Fact]
    public void AddTick_AddsOneTick_ToValidPrices() {
        Assert.Equal(1.02, PriceHelpers.AddTick(1.01));
        Assert.Equal(2.02, PriceHelpers.AddTick(2.00));
    }


    [Fact]
    public void AddTick_ReturnsMaxPrice_WhenPriceIsMax() {
        var maxPrice = PriceHelpers.PriceLadder[^1];
        Assert.Equal(maxPrice, PriceHelpers.AddTick(maxPrice));
    }


    [Fact]
    public void AddTicks_AddsCorrectNumberOfTicks() {
        Assert.Equal(1.03, PriceHelpers.AddTicks(1.01, 2));
        Assert.Equal(2.06, PriceHelpers.AddTicks(2.00, 3));
    }


    [Fact]
    public void AddTicks_ReturnsMaxPrice_WhenExceedsLadder() {
        var maxPrice = PriceHelpers.PriceLadder[^1];
        Assert.Equal(maxPrice, PriceHelpers.AddTicks(maxPrice, 3));
    }


    [Fact]
    public void SubtractTick_SubtractsOneTick_FromValidPrices() {
        Assert.Equal(1.01, PriceHelpers.SubtractTick(1.02));
        Assert.Equal(1.99, PriceHelpers.SubtractTick(2.00));
    }


    [Fact]
    public void SubtractTick_ReturnsMinPrice_WhenPriceIsMin() {
        var minPrice = PriceHelpers.PriceLadder[0];
        Assert.Equal(minPrice, PriceHelpers.SubtractTick(minPrice));
    }


    [Fact]
    public void SubtractTicks_SubtractsCorrectNumberOfTicks() {
        Assert.Equal(1.01, PriceHelpers.SubtractTicks(1.03, 2));
        Assert.Equal(1.97, PriceHelpers.SubtractTicks(2.00, 3));
    }


    [Fact]
    public void SubtractTicks_ReturnsMinPrice_WhenBelowLadder() {
        var minPrice = PriceHelpers.PriceLadder[0];
        Assert.Equal(minPrice, PriceHelpers.SubtractTicks(minPrice, 3));
    }


    [Fact]
    public void RoundToNearestBetfairPrice_ReturnsCorrectRoundedPrice() {
        Assert.Equal(1.01, PriceHelpers.RoundToNearestBetfairPrice(1.005));
        Assert.Equal(1.02, PriceHelpers.RoundToNearestBetfairPrice(1.015));
        Assert.Equal(2.00, PriceHelpers.RoundToNearestBetfairPrice(1.995));
    }


    [Fact]
    public void RoundToNearestBetfairPrice_ReturnsMinOrMax_ForExtremeValues() {
        var minPrice = PriceHelpers.PriceLadder[0];
        var maxPrice = PriceHelpers.PriceLadder[^1];
        Assert.Equal(minPrice, PriceHelpers.RoundToNearestBetfairPrice(0));
        Assert.Equal(minPrice, PriceHelpers.RoundToNearestBetfairPrice(-1));
        Assert.Equal(maxPrice, PriceHelpers.RoundToNearestBetfairPrice(1001));
    }
}
