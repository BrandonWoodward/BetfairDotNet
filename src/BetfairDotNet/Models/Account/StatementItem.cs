using BetfairDotNet.Enums.Account;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Models.Account;


/// <summary>
/// A item returned in the AccountStatementReport.
/// </summary>
public sealed record class StatementItem {

    /// <summary>
    /// An external reference, eg. equivalent to betId in the case of an exchange bet statement item.
    /// </summary>
    [JsonPropertyName("refId")]
    public string RefId { get; init; } = string.Empty;

    /// <summary>
    /// The date and time of the statement item, eg. equivalent to settledData for an exchange bet statement item.
    /// </summary>
    [JsonPropertyName("itemDate"), JsonRequired]
    public required DateTime ItemDate { get; init; }

    /// <summary>
    /// The amount of money the balance is adjusted by.
    /// </summary>
    [JsonPropertyName("amount")]
    public double Amount { get; init; }

    /// <summary>
    /// Account balance.
    /// </summary>
    [JsonPropertyName("balance")]
    public double Balance { get; init; }

    /// <summary>
    /// Class of statement item. This value will determine which set of keys will be included in itemClassData.
    /// </summary>
    [JsonPropertyName("itemClass")]
    public ItemClassEnum ItemClass { get; init; }

    /// <summary>
    /// Key value pairs describing the current statement item. The set of keys will be determined by the itemClass
    /// </summary>
    [JsonPropertyName("itemClassData")]
    public Dictionary<string, string> ItemClassData { get; init; } = new();

}
