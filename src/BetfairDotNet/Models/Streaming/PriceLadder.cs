using System.Collections;
using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Models.Betting;
using BetfairDotNet.Utils;

namespace BetfairDotNet.Models.Streaming;


public sealed class PriceLadder
{
    // Maintaining two collections,
    // but it offers O(1) lookups for price-based access and O(log n) for rank-based access
    private readonly SortedList<double, PriceSize> _byRank;
    private readonly Dictionary<double, PriceSize> _byPrice;

    public static PriceLadder Empty
        => new(SideEnum.BACK);

    public PriceSize? this[int index]
        => index >= 0 && index < _byRank.Values.Count
            ? _byRank.Values.ElementAt(index)
            : default;

    public PriceSize? this[double price]
        => _byPrice.GetValueOrDefault(price);


    internal PriceLadder(SideEnum side) 
    {
        _byRank = side == SideEnum.BACK ? new(new ReverseComparer()) : new();
        _byPrice = new();
    }

    internal PriceLadder(SideEnum side, List<PriceSize> initialLevels) 
    {
        _byRank = side == SideEnum.BACK ? new(new ReverseComparer()) : new();
        _byPrice = new();

        foreach(var level in initialLevels) 
        {
            AddOrUpdateLevel(level);
        }
    }
    internal void AddOrUpdateLevel(PriceSize level) 
    {
        _byRank[level.Price] = level;
        _byPrice[level.Price] = level;
    }

    internal void RemoveLevel(double price) 
    {
        _byPrice.Remove(price);
        _byRank.Remove(price);
    }
    
    public int GetDepth() 
    {
        return _byRank.Count;
    }
}
