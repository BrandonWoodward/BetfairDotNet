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
            _byRank[level.Price] = level;
            _byPrice[level.Price] = level;
        }
    }

    /// <summary>
    /// Access the <see cref="PriceSize"/> at the given index, where index 0
    /// represents the best price.
    /// </summary>
    public PriceSize? this[int index]
    {
        get
        {
            return index >= 0 && index < _byRank.Values.Count
                ? _byRank.Values.ElementAt(index)
                : default;
        }
        internal set
        {
            if (value != null && (value.Price == 0 || value.Size == 0) && (index < _byRank.Values.Count))
            {
                var key = _byRank.Keys.ElementAt(index);
                _byRank.RemoveAt(index);
                _byPrice.Remove(key);
            }
            else if(value != null && _byRank.ContainsKey(value.Price))
            {
                var key = _byRank.Keys.ElementAt(index);
                _byRank[key] = value;
                _byPrice[key] = value;
            }
            else if (value is { Price: > 0, Size: > 0 })
            {
                _byRank[value.Price] = value;
                _byPrice[value.Price] = value;
            }
        }
    }

    /// <summary>
    /// Access the <see cref="PriceSize"/> at the given price, where the index
    /// given represents a valid Betfair price.
    /// </summary>
    public PriceSize? this[double price]
    {
        get
        {
            return _byPrice.GetValueOrDefault(price);
        }
        internal set
        {
            if (value != null && (value.Price == 0 || value.Size == 0))
            {
                _byRank.Remove(price);
                _byPrice.Remove(price);
            }
            else if(value != null)
            {
                _byRank[price] = value;
                _byPrice[price] = value;
            }
        }
    }
    
    /// <summary>
    /// Get the count of prices in the ladder.
    /// </summary>
    public int Depth
    {
        get { return _byRank.Count; }
    }
    
    internal static PriceLadder Empty
    {
        get { return new(SideEnum.BACK); }
    }
}
