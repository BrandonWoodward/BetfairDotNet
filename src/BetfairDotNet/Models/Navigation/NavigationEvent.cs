using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Navigation;

public sealed record NavigationEvent : NavigationItem
{
    [JsonPropertyName("countryCode")]
    public string CountryCode { get; init; }

    public override string Type { get; } = "EVENT";
}