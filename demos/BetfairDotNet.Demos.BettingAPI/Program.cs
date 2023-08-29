/*
 * BetfairDotNet.Demo.HorseRacingBetting
 * 
 * Overview:
 * This demo showcases a typical flow of operations on horse racing markets using the Betfair API.
 * 
 * 1. **Login**:
 *    - Authenticate with the Betfair API using provided credentials.
 *
 * 2. **List Market Catalogue**:
 *    - Fetch the next horse racing market, ordered by event time.
 *
 * 3. **List Market Book**:
 *    - Select the nearest upcoming horse race from the retrieved markets.
 *    - Display the market book for the selected race.
 *
 * 4. **Place a Bet**:
 *    - Identify the favorite in the selected market book.
 *    - Place a bet on the favorite at the current price for less than the minimum size.
 *    - Observe that the bet is rejected.
 */
using BetfairDotNet;
using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Models.Betting;
using Microsoft.Extensions.Configuration;

// Setup: Fetch credentials from the credentials.json file
var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("credentials.json")
    .Build();

// Extract necessary credentials and paths from configuration
var apiKey = configuration["DelayedApiKey"] ?? throw new NullReferenceException("ApiKey not found.");
var username = configuration["Username"] ?? throw new NullReferenceException("Username not found.");
var password = configuration["Password"] ?? throw new NullReferenceException("Password not found.");
var certPath = configuration["CertificatePath"] ?? throw new NullReferenceException("Certificate path not found.");

// Initialize BetfairClient: For production use, prefer dependency injection (register as singleton).
// Here, for demonstration, we're instantiating directly.
var client = new BetfairClient(apiKey, 5000);

// Authenticate with Betfair and obtain session details
await client.Account.Login(username, password, certPath);

// Define a market filter to retrieve the next horse racing market, ordered by event time
// GB or IE WIN markets only
var marketFilter = new MarketFilter {
    EventTypeIds = new List<string>() { "7" },
    MarketCountries = new List<string>() { "GB" },
    MarketTypeCodes = new List<string>() { "WIN" },
    MarketStartTime = new TimeRange {
        From = DateTime.UtcNow,
        To = DateTime.UtcNow.AddDays(5)
    }
};

// Create a market projection to retrieve essential market details
var marketProjection = new List<MarketProjectionEnum>() {
    MarketProjectionEnum.EVENT,
    MarketProjectionEnum.EVENT_TYPE,
    MarketProjectionEnum.MARKET_DESCRIPTION,
    MarketProjectionEnum.MARKET_START_TIME,
    MarketProjectionEnum.RUNNER_DESCRIPTION,
};

// Operations should be wrapped in a try-catch block to handle exceptions
// It is also possible for this method to return an empty list if no markets were found
var markets = await client.Betting.ListMarketCatalogue(
    marketFilter,
    marketProjection,
    MarketSortEnum.FIRST_TO_START
);

// Declare a projection for the price response
var priceProjection = new PriceProjection {
    PriceData = new List<PriceDataEnum>() {
        PriceDataEnum.EX_BEST_OFFERS,
    }
};

// Operations should be wrapped in a try-catch block to handle exceptions
// It is also possible for this method to return an empty list if not markets were found
var book = await client.Betting.ListMarketBook(new List<string>() { markets[0].MarketId }, priceProjection);


// Display the retrieved market with essential details
Console.WriteLine($"{markets[0].MarketStartTime.Value.ToShortTimeString()}: {markets[0].Event.Venue} {markets[0].MarketName}");
Console.WriteLine("-------------------------------------------------------------------------------------");
Console.WriteLine("| Runner Name                    |     Back   |     Lay    |");
Console.WriteLine("-------------------------------------------------------------------------------------");

foreach(var runner in book[0].Runners) {
    var bestBackPrice = runner.ExchangePrices?.AvailableToBack?.FirstOrDefault()?.Price ?? 0.00;
    var bestLayPrice = runner.ExchangePrices?.AvailableToLay?.FirstOrDefault()?.Price ?? 0.00;
    var staticRunnerData = markets[0].Runners.First(r => r.SelectionId == runner.SelectionId);

    Console.WriteLine($"| {staticRunnerData.RunnerName,-30} | {bestBackPrice,10:N2} | {bestLayPrice,10:N2} |");
}

Console.WriteLine("-------------------------------------------------------------------------------------");

// Create a place instruction for the bet
var placeInstruction = new PlaceInstruction {
    OrderType = OrderTypeEnum.LIMIT,
    SelectionId = book[0].Runners.First().SelectionId,
    Side = SideEnum.BACK,
    LimitOrder = new LimitOrder {
        Size = 2.00,
        Price = book[0].Runners.First().ExchangePrices.AvailableToBack.First().Price,
        PersistenceType = PersistenceTypeEnum.LAPSE
    }
};

// Place the bet
var placeExecutionReport = await client.Betting.PlaceOrders(
    markets[0].MarketId,
    new List<PlaceInstruction>() { placeInstruction }
);

// Display the bet result
Console.WriteLine($"Bet result: {placeExecutionReport.Status}");