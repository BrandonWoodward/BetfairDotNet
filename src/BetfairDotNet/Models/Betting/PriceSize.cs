using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Betting;

/// <summary>
/// Price and volume.
/// </summary>
public sealed record PriceSize {

    /// <summary>
    /// The price available.
    /// </summary>
    [JsonPropertyName("price"), JsonRequired]
    public double Price { get; init; }

    /// <summary>
    /// The stake available.
    /// </summary>
    [JsonPropertyName("size"), JsonRequired]
    public double Size { get; init; }


    public PriceSize(double price, double size) {
        Price = price;
        Size = size;
    }
}
