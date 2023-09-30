namespace BetfairDotNet.Models.Navigation;

public sealed record NavigationGroup : NavigationItem
{
    public override string Type { get; } = "GROUP";
}