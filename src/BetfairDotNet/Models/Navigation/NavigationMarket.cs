using System.Text.Json.Serialization;
using BetfairDotNet.Converters;

namespace BetfairDotNet.Models.Navigation;

public sealed record NavigationMarket : NavigationItem
{
    [JsonPropertyName("exchangeId"), JsonRequired]
    public string ExchangeId { get; init; }

    [JsonPropertyName("marketStartTime")]
    public DateTime? MarketStartTime { get; init; }

    [JsonPropertyName("marketType")]
    public string MarketType { get; init; } = string.Empty;

    [JsonPropertyName("numberOfWinners")]
    [JsonConverter(typeof(NumberOrEmptyStringConverter))]
    public int NumberOfWinners { get; init; }

    public override string Type { get; } = "MARKET";
}