using BetfairDotNet.Converters;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Enums.Account;


[JsonConverter(typeof(CustomStringToEnumConverter<WalletEnum>))]
public enum WalletEnum {

    /// <summary>
    /// The Global Exchange wallet.
    /// </summary>
    UK
}
