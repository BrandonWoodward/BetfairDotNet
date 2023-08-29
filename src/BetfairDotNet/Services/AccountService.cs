using BetfairDotNet.Endpoints;
using BetfairDotNet.Enums.Account;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Account;
using BetfairDotNet.Models.Betting;

namespace BetfairDotNet.Services;


/// <summary>
/// Provides functionalities for interacting with account-related services.
/// </summary>
public sealed class AccountService {


    private readonly IRequestResponseHandler _networkService;


    internal AccountService(IRequestResponseHandler networkService) {
        _networkService = networkService;
    }


    /// <summary>
    /// Asynchronously fetches the current status of the account funds.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The result contains 
    /// details about the account's funds.
    /// </returns>
    public async Task<AccountFundsResponse> GetAccountFunds() {
        return await _networkService.Request<AccountFundsResponse>(
            AccountEndpoints.BaseUrl,
            AccountEndpoints.GetAccountFunds
        );
    }


    /// <summary>
    /// Asynchronously retrieves detailed information about the account.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The result contains 
    /// detailed information about the account.
    /// </returns>
    public async Task<AccountDetailsResponse> GetAccountDetails() {
        return await _networkService.Request<AccountDetailsResponse>(
            AccountEndpoints.BaseUrl,
            AccountEndpoints.GetAccountDetails
        );
    }


    /// <summary>
    /// Get a statement relating to the account balance including , P/L, commission payments, deposits, withdrawals etc.
    /// </summary>
    /// <param name="locale">The language to be used where applicable. If not specified, the customer account default is returned.</param>
    /// <param name="fromRecord">Specifies the first record that will be returned. Records start at index zero. If not specified then it will default to 0.</param>
    /// <param name="recordCount">Specifies the maximum number of records to be returned. Note that there is a page size limit of 100.</param>
    /// <param name="itemDateRange">Return items with an itemDate within this date range. Both from and to date times are inclusive. 
    /// If from is not specified then the oldest available items will be in range. If to is not specified then the latest items will be in range. 
    /// This itemDataRange is currently only applied when includeItem is set to ALL or not specified, else items are NOT bound by itemDate.  
    /// You can only retrieve account statement items for the last 90 days.</param>
    /// <param name="includeItem">Which items to include, if not specified then defaults to ALL.</param>
    /// <param name="wallet">Which wallet to return statementItems for. If unspecified then the UK wallet will be selected</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The result contains 
    /// detailed information about the account balance.
    /// </returns>
    public async Task<AccountStatementReport> GetAccountStatement(
        string? locale = null,
        int? fromRecord = null,
        int? recordCount = null,
        TimeRange? itemDateRange = null,
        IncludeItemEnum? includeItem = null,
        WalletEnum? wallet = null) {

        var args = new Dictionary<string, object?>() {
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
    /// Returns a list of currency rates based on given currency. 
    /// The currency rates are updated once every hour a few seconds after the hour.
    /// </summary>
    /// <param name="fromCurrency">The currency from which the rates are computed. Please note: GBP is currently the only based currency support</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The result contains 
    /// detailed information about the account balance.
    /// </returns>
    public async Task<CurrencyRate> ListCurrencyRates(string? fromCurrency = null) {

        var args = new Dictionary<string, object?>() {
            ["fromCurrency"] = fromCurrency,
        };

        return await _networkService.Request<CurrencyRate>(
            AccountEndpoints.BaseUrl,
            AccountEndpoints.ListCurrencyRates,
            args
        );
    }
}
