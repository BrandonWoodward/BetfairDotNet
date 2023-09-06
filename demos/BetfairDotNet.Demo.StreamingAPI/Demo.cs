using BetfairDotNet;
using BetfairDotNet.Demos;
using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Enums.Streaming;
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
    MarketSortEnum.FIRST_TO_START
);


// Create a streaming configuration.
var streamConfiguration = new StreamConfiguration() {
    SessionToken = session.SessionToken,
    RecoveryThresholdMs = 3_000,
    MaxRecoveryWaitMs = 120_000
};


// Create a market subscription
var marketSubscription = new MarketSubscription(
    new StreamingMarketFilter() { MarketIds = new List<string> { todaysHorseRacing[0].MarketId } },
    new StreamingMarketDataFilter { Fields = Enum.GetValues(typeof(MarketPriceFilterEnum)).Cast<MarketPriceFilterEnum>().ToList() },
    200
);


// Connect to the streaming service
var stream = client.Streaming
    .CreateStream(streamConfiguration)
    .WithMarketSubscription(marketSubscription);


// Subscribe to the stream
await stream.Subscribe(
    onMarketChange: ms => Display.RenderMarketSnapshot(todaysHorseRacing[0], ms),
    onException: ex => Console.WriteLine(ex.Message)
);


// Wait for user input to exit
Console.ReadLine();