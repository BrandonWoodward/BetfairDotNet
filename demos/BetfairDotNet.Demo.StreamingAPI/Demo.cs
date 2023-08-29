using BetfairDotNet;
using BetfairDotNet.Demos;
using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Enums.Streaming;
using BetfairDotNet.Models.Betting;
using BetfairDotNet.Models.Streaming;
using BetfairDotNet.Utils;
using Microsoft.Extensions.Configuration;


// Read the environment variables from credentials.json
var config = new ConfigurationBuilder()
    .AddJsonFile("credentials.json", optional: true, reloadOnChange: true)
    .Build();


// Get the credentials from config
var apiKey = config["API_KEY"] ?? throw new Exception("API_KEY not found.");
var username = config["USERNAME"] ?? throw new Exception("USERNAME not found.");
var password = config["PASSWORD"] ?? throw new Exception("PASSWORD not found.");
var certPath = config["CERT_PATH"] ?? throw new Exception("CERT_PATH not found.");


// Initialise the client and login
var client = new BetfairClient(apiKey, username, password, certPath);
var session = await client.Login.CertificateLogin();


// Get todays horse racing markets
// ListMarketCatalogue returns 1 result if maxResults not specified
var todaysHorseRacing = await client.Betting.ListMarketCatalogue(
    MarketFilterHelpers.TodaysGBAndIREHorseRacingWinOnly(),
    Enum.GetValues(typeof(MarketProjectionEnum)).Cast<MarketProjectionEnum>().ToList(),
    MarketSortEnum.FIRST_TO_START,
    maxResults: 10
);


// Create a market subscription
var marketSubscription = new MarketSubscription(
    new MarketFilter() { MarketIds = new List<string> { todaysHorseRacing[0].MarketId } },
    new MarketDataFilter { Fields = Enum.GetValues(typeof(MarketPriceFilterEnum)).Cast<MarketPriceFilterEnum>().ToList() },
    conflateMs: 200
);


// Create an order subscription
var orderSubscription = new OrderSubscription(
    new OrderFilter { IncludeOverallPosition = true }
);


// Connect to the streaming service
using var stream = await client.Streaming.CreateStream(
    session.SessionToken,
    marketSubscription,
    orderSubscription
);


// Subscribe to the stream
// Can easily filter updates for either markets or orders (or both) using a predicate
stream
    .FilterMarkets(ms => ms.MarketId == todaysHorseRacing[0].MarketId)
    .Subscribe(
        ms => Display.RenderMarketSnapshot(todaysHorseRacing[0], ms),
        ex => Console.WriteLine($"Exception: {ex.Message}")
    );


// Wait for user input to exit
Console.ReadLine();