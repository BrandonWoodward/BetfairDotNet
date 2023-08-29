namespace BetfairDotNet.Enums.Account;
public enum ItemClassEnum {

    /// <summary>
    /// Statement item not mapped to a specific class. All values will be concatenated into a single key/value pair. 
    /// The key will be 'unknownStatementItem' and the value will be a comma separated string. 
    /// This is used to represent commission payment items.
    /// </summary>
    UNKNOWN
}
