#  BetfairDotNet 

<div align="left">

[![Tests](https://github.com/BrandonWoodward/BetfairDotNet/actions/workflows/dotnet.yml/badge.svg)](https://github.com/BrandonWoodward/BetfairDotNet/actions/workflows/dotnet.yml)
  [![NuGet Version](https://img.shields.io/nuget/v/BetfairDotNet.svg?style=flat)](https://www.nuget.org/packages/BetfairDotNet/)
  [![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/yourusername/yourrepository/blob/main/LICENSE)
</div>

<br>

**:rocket: A fast, easy to use Betfair API client for .NET 7. Includes functionality for login, accounts, betting, heartbeat and streaming.**
<br>
<br>

## 📦 Installation

<br>

Install via NuGet Package Manager or the .NET CLI:

```bash
dotnet add package BetfairDotNet --version x.y.z
```
<br>

To work directly with the source code, clone this repository and build the project locally:

```bash
git clone https://github.com/yourusername/BetfairDotNet.git
cd BetfairDotNet
dotnet build
```

<br>

To run the demo, you also need to provide a `credentials.json` file in the root of the project:

```json
{
	"API_KEY": "your_api_key_here",
	"USERNAME": "your_username_here",
	"PASSWORD": "your_password_here",
	"CERT_PATH": "full_path_to_.pfx_or_.p12"
}
```

<br>
<br>


## 📖 Basic Usage

<br>

###  Login

The recommended and most secure login flow uses a self-signed SSL certificate. More info [here](https://docs.developer.betfair.com/display/1smk3cen4v3lu3yomq5qye0ni/Non-Interactive+%28bot%29+login)

```csharp
using BetfairDotNet;


// Enter your credentials here
var client = new BetfairClient(apiKey, username, password, certPath)

// Returns the SessionToken and the error code if unsuccessful
var session = await client.Login.CertificateLogin();

```

<br>

You can also provide just your username and password:

```csharp
using BetfairDotNet;


// Enter your credentials here
var client = new BetfairClient(apiKey, username, password)

// Returns the SessionToken and the error code if unsuccessful
var session = await client.Login.InteractiveLogin();
```

<br>

### List Markets

Here is a simple snippet to fetch today's GB/IRE horse racing markets. MarketFilterHelpers contains some pre-configured filters for common use cases.

```csharp
var todaysHorseRacing = await client.Betting.ListMarketCatalogue(
    MarketFilterHelpers.TodaysGBAndIREHorseRacingWinOnly(),
    Enum.GetValues(typeof(MarketProjectionEnum)).Cast<MarketProjectionEnum>().ToList(),
    MarketSortEnum.FIRST_TO_START,
    maxResults: 100
);
```

<br>

### Place Orders

There are lots of different options for placing orders on the exchange, see [here](https://docs.developer.betfair.com/display/1smk3cen4v3lu3yomq5qye0ni/placeOrders) for more details.

```csharp
var placeInstructions = new List<PlaceInstruction> {
      new PlaceInstruction {
            OrderType = OrderTypeEnum.LIMIT,
            SelectionId = 123456789,
            Side = SideEnum.BACK,
            LimitOrder = new LimitOrder {
                Size = 100,
                Price = 2.00,
                PersistenceType = PersistenceTypeEnum.LAPSE,
            }
        }
};

var report await client.Betting.PlaceOrders(
    "someMarketId", 
    placeInstructions,
    "myCustomerRef"
);
```

<br>

### Streaming

The streaming functionality was implemented using [Rx.NET](https://github.com/dotnet/reactive). First define your subscription criteria. You can create a market subscription, an order subscription or both:

```csharp
var marketSubscription = new MarketSubscription(
    new MarketFilter() { ... },
    new MarketDataFilter { ... },
    conflateMs: 200 // default to no conflation
);

var orderSubscription = new OrderSubscription(
    new OrderFilter { ... }
);
```

Next, connect to the stream using the SessionToken obtained from a successful login and the subscription criteria you defined previously:


```csharp
var stream = await client.Streaming.CreateStream(
    session.SessionToken,
    marketSubscription,
    orderSubscription
);
```

`BetfairDotNet` produces immutable, atomic snapshots of each market in your subscription so you don't have to worry about implementing a cache yourself. Simply define a callback for each of your subscription criteria. You should also provide a callback for any handled exceptions to allow you to reconnect to the stream.


```csharp
stream.Subscribe(
    ms => ..., // A callback for market snapshots
    os => ..., // A callback for order snapshots
    ex => ... // A callback for BetfairESAException
);
```

A fluent interface is also exposed which optionally allows the chaining of predicates to filter market and order snapshot events.

```csharp
stream
    .FilterMarkets(ms => ...)
    .FilterOrders(os => ...)
    .Subscribe(
        ms => ..., // A callback for market snapshots
        os => ..., // A callback for order snapshots
        ex => ... // A callback for BetfairESAException
    );
```
