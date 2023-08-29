using System.Text.Json.Serialization;


namespace BetfairDotNet.Models.Account;

/// <summary>
/// Represents a report of account statements.
/// </summary>
public sealed record AccountStatementReport {

    [JsonPropertyName("accountStatement"), JsonRequired]
    public required List<StatementItem> AccountStatement { get; init; }

    [JsonPropertyName("moreAvailable"), JsonRequired]
    public required bool MoreAvailable { get; init; }
}
