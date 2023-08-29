using BetfairDotNet.Enums.Account;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Account;
using BetfairDotNet.Models.Betting;
using BetfairDotNet.Services;
using NSubstitute;
using Xunit;

namespace BetfairDotNet.Tests.ServiceTests;

/// <summary>
/// Unit tests for the <see cref="AccountService"/>.
/// </summary>
public class AccountServiceTests {

    private readonly IRequestResponseHandler _mockNetwork = Substitute.For<IRequestResponseHandler>();
    private readonly AccountService _accountService;

    public AccountServiceTests() {
        _accountService = new AccountService(_mockNetwork);
    }


    [Fact]
    public async Task GetAccountFunds_SendsCorrectRequest() {

        // Act 
        await _accountService.GetAccountFunds();

        // Assert
        await _mockNetwork.Received().Request<AccountFundsResponse>(
            "https://api.betfair.com/exchange/account/json-rpc/v1",
            "/AccountAPING/v1.0/getAccountFunds"
        );
    }


    [Fact]
    public async Task GetAccountDetails_SendsCorrectRequest() {

        // Act 
        await _accountService.GetAccountDetails();

        // Assert
        await _mockNetwork.Received().Request<AccountDetailsResponse>(
            "https://api.betfair.com/exchange/account/json-rpc/v1",
            "/AccountAPING/v1.0/getAccountDetails"
        );
    }


    [Fact]
    public async Task GetAccountStatement_SendsCorrectRequest() {

        // Arrange
        var locale = "testLocale";
        var fromRecord = 1;
        var recordCount = 10;
        var itemDateRange = new TimeRange() { From = DateTime.Now, To = DateTime.Now.AddDays(1) };
        var includeItem = IncludeItemEnum.ALL;
        var wallet = WalletEnum.UK;

        // Act 
        await _accountService.GetAccountStatement(
            locale,
            fromRecord,
            recordCount,
            itemDateRange,
            includeItem,
            wallet
        );

        // Assert
        await _mockNetwork.Received().Request<AccountStatementReport>(
            "https://api.betfair.com/exchange/account/json-rpc/v1",
            "/AccountAPING/v1.0/getAccountStatement",
            Arg.Is<Dictionary<string, object?>>(args =>
                (string?)args["locale"] == locale &&
                (int?)args["fromRecord"] == fromRecord &&
                (int?)args["recordCount"] == recordCount &&
                (TimeRange?)args["itemDateRange"] == itemDateRange &&
                (IncludeItemEnum?)args["includeItem"] == includeItem &&
                (WalletEnum?)args["wallet"] == wallet
            )
        );
    }


    [Fact]
    public async Task ListCurrencyRates_SendsCorrectRequest() {

        // Arrange
        var fromCurrency = "GBP";

        // Act 
        await _accountService.ListCurrencyRates(fromCurrency);

        // Assert
        await _mockNetwork.Received().Request<CurrencyRate>(
            "https://api.betfair.com/exchange/account/json-rpc/v1",
            "/AccountAPING/v1.0/listCurrencyRates",
            Arg.Is<Dictionary<string, object?>>(args =>
                (string?)args["fromCurrency"] == fromCurrency
            )
        );
    }
}
