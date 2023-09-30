namespace BetfairDotNet.Models.Navigation;

public sealed record NavigationEventType : NavigationItem
{
    public override string Type { get; } = "EVENT_TYPE";
}