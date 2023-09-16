using BetfairDotNet.Endpoints;
using BetfairDotNet.Enums.Account;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Account;
using BetfairDotNet.Models.Betting;

namespace BetfairDotNet.Services;


/// <summary>
/// Provides functionalities for interacting with account-related services.
/// </summary>
public sealed class AccountService
{

    private readonly IRequestResponseHandler _networkService;

    internal AccountService(IRequestResponseHandler networkService)
    {
        _networkService = networkService;
    }

    /// <summary>
    /// Asynchronously fetches accout funds.
    /// </summary>
    /// <example>
    /// Usage:
    /// <code>
    /// var accountFunds = await client.Account.GetAccountFunds();
    /// </code>
    /// </example>
    /// <returns>A task with the account funds upon completion.</returns>
    public async Task<AccountFundsResponse> GetAccountFunds() => await _networkService.Request<AccountFundsResponse>(
            AccountEndpoints.BaseUrl,
            AccountEndpoints.GetAccountFunds
        );

    /// <summary>
    /// Asynchronously fetches account details.
    /// </summary>
    /// <example>
    /// Usage:
    /// <code>
    /// var accountDetails = await client.Account.GetAccountDetails();
    /// </code>
    /// </example>
    /// <returns>A task with the account details upon completion.</returns>
    public async Task<AccountDetailsResponse> GetAccountDetails() => await _networkService.Request<AccountDetailsResponse>(
            AccountEndpoints.BaseUrl,
            AccountEndpoints.GetAccountDetails
        );

    /// <summary>
    /// Fetches an account statement with details such as P/L, deposits, withdrawals, and more.
    /// </summary>
    /// <example>
    /// Usage:
    /// <code>
    /// var statement = await client.Account.GetAccountStatement(
    ///     locale: "en", 
    ///     recordCount: 10
    ///     itemDateRange: new TimeRange(From = new DateTime(...), To = new DateTime(...))
    /// );
    /// </code>
    /// </example>
    /// <param name="locale">Language used; defaults to account's setting.</param>
    /// <param name="fromRecord">Start index for records; default is 0.</param>
    /// <param name="recordCount">Max records to return; max limit is 100.</param>
    /// <param name="itemDateRange">Date range for items; max range is last 90 days.</param>
    /// <param name="includeItem">Items to include; default is ALL.</param>
    /// <param name="wallet">Wallet for statement; default is UK.</param>
    /// <returns>A task with the account statement details.</returns>
    public async Task<AccountStatementReport> GetAccountStatement(
        string? locale = null,
        int? fromRecord = null,
        int? recordCount = null,
        TimeRange? itemDateRange = null,
        IncludeItemEnum? includeItem = null,
        WalletEnum? wallet = null)
    {
        var args = new Dictionary<string, object?>()
        {
            ["locale"] = locale,
            ["fromRecord"] = fromRecord,
            ["recordCount"] = recordCount,
            ["itemDateRange"] = itemDateRange,
            ["includeItem"] = includeItem,
            ["wallet"] = wallet,
        };
        return await _networkService.Request<AccountStatementReport>(
            AccountEndpoints.BaseUrl,
            AccountEndpoints.GetAccountStatement,
            args
        );
    }

    /// <summary>
    /// Fetches currency rates, updated hourly, based on a specified currency.
    /// </summary>
    /// <example>
    /// Usage:
    /// <code>
    /// var rates = await client.Account.ListCurrencyRates("GBP");
    /// </code>
    /// </example>
    /// <param name="fromCurrency">Base currency for rates; only "GBP" is currently supported.</param>
    /// <returns>A task with the currency rates.</returns>
    public async Task<CurrencyRate> ListCurrencyRates(string? fromCurrency = null)
    {
        var args = new Dictionary<string, object?>()
        {
            ["fromCurrency"] = fromCurrency,
        };
        return await _networkService.Request<CurrencyRate>(
            AccountEndpoints.BaseUrl,
            AccountEndpoints.ListCurrencyRates,
            args
        );
    }
}
