using System.Text.Json.Serialization;
using BetfairDotNet.Converters;

namespace BetfairDotNet.Models.Navigation;

[JsonConverter(typeof(NavigationItemConverter))]
public abstract record NavigationItem
{
    [JsonPropertyName("id"), JsonRequired]
    public string Id { get; init; }

    [JsonPropertyName("name"), JsonRequired]
    public string Name { get; init; }

    [JsonPropertyName("type")]
    public abstract string Type { get; }

    [JsonPropertyName("children")]
    public List<NavigationItem> Children { get; init; } = new();
}