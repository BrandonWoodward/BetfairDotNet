using System.Collections;
using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Models.Betting;
using BetfairDotNet.Utils;

namespace BetfairDotNet.Models.Streaming;


public sealed class PriceLadder
{
    // Maintaining two collections,
    // but it offers O(1) lookups for price-based access and O(log n) for rank-based access
    private readonly SortedList<double, PriceSize> _byRank = new(new ReverseComparer());
    private readonly Dictionary<double, PriceSize> _byPrice = new();


    public PriceSize? this[int index]
        => index >= 0 && index < _byRank.Values.Count
            ? _byRank.Values.ElementAt(index)
            : default;

    public PriceSize? this[double price]
        => _byPrice.TryGetValue(price, out var value) ? value : default;


    internal PriceLadder(SideEnum side) {
        _byRank = side == SideEnum.BACK
            ? new(new ReverseComparer())
            : new();
        _byPrice = new();
    }


    internal PriceLadder(SideEnum side, List<PriceSize> initialLevels) {
        _byRank = side == SideEnum.BACK
            ? new(new ReverseComparer())
            : new();
        _byPrice = new();

        foreach(var level in initialLevels) {
            AddLevel(level.Price, level);
        }
    }


    internal void AddLevel(double price, PriceSize priceSize) {
        _byPrice[price] = priceSize;
        _byRank[price] = priceSize;
    }


    internal void RemoveLevelByPrice(double price) {
        _byPrice.Remove(price);
        _byRank.Remove(price);
    }


    internal void RemoveLevelByDepth(int depth) {
        if(depth >= 0 && depth < _byRank.Count) {
            var price = _byRank.Keys[depth];
            _byRank.RemoveAt(depth);
            _byPrice.Remove(price);
        }
    }


    internal void AddOrUpdateLevelByPrice(double price, PriceSize level) {
        if(_byPrice.ContainsKey(price)) {
            _byPrice[price] = level;
            _byRank[price] = level;
        }
        else {
            AddLevel(price, level);
        }
    }


    internal void AddOrUpdateLevelByDepth(int depth, PriceSize level) {
        if(depth >= 0 && depth < _byRank.Count) {
            var price = _byRank.Keys[depth];
            _byPrice[price] = level;
            _byRank[price] = level;
        }
        else {
            _byRank.Add(level.Price, level);
            _byPrice.Add(level.Price, level);
        }
    }


    public int GetDepth() {
        return _byRank.Count;
    }
}
