#  BetfairDotNet 

<div align="left">

[![Tests](https://github.com/BrandonWoodward/BetfairDotNet/actions/workflows/dotnet.yml/badge.svg)](https://github.com/BrandonWoodward/BetfairDotNet/actions/workflows/dotnet.yml)
[![NuGet Version](https://img.shields.io/nuget/v/BetfairDotNet.svg?style=flat)](https://www.nuget.org/packages/BetfairDotNet/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/yourusername/yourrepository/blob/main/LICENSE)

</div>

---

A fast, easy to use Betfair API client for .NET 7. Includes functionality for login, accounts, betting, heartbeat and streaming.

---

## Table of Contents

- [📦 Installation](#installation)
- [📖 Examples](#examples)
  - [Login](#login)
  - [List Markets](#list-markets)
  - [Place Orders](#place-orders)
  - [Streaming](#streaming)

---

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

## 📖 Basic Usage

###  Login

The recommended and most secure login flow uses a self-signed SSL certificate. 
You can find more information about creating a certificate [here](https://docs.developer.betfair.com/display/1smk3cen4v3lu3yomq5qye0ni/Non-Interactive+%28bot%29+login)

```csharp
// Enter your credentials here
var client = new BetfairClient(apiKey, username, password, certPath)

// Returns the SessionToken and the error code if unsuccessful
var session = await client.Login.CertificateLogin();
```


You can also provide just your username and password. This is recommended if you're creating your own login form.

```csharp
// Enter your credentials here
var client = new BetfairClient(apiKey, username, password)

// Returns the SessionToken and the error code if unsuccessful
var session = await client.Login.InteractiveLogin();
```

### List Markets

Here's how you can list today's GB/IRE horse racing markets.
MarketFilterHelpers contains some pre-configured filters for common use cases.

```csharp
var todaysHorseRacing = await client.Betting.ListMarketCatalogue(
    MarketFilterHelpers.TodaysGBAndIREHorseRacingWinOnly(),
    Enum.GetValues(typeof(MarketProjectionEnum)).Cast<MarketProjectionEnum>().ToList(),
    MarketSortEnum.FIRST_TO_START,
    maxResults: 100
);
```

### Place Orders

For detailed options on placing orders, see the official Betfair documentation [here](https://docs.developer.betfair.com/display/1smk3cen4v3lu3yomq5qye0ni/placeOrders).

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
    /* market id */, 
    placeInstructions,
    /* optional customer ref */
);
```


### Streaming

The streaming functionality was implemented using [Rx.NET](https://github.com/dotnet/reactive). Define your subscription criteria, 
create the stream using the SessionToken (obtained from login) and subscribe to the events you are interested in. 
`BetfairDotNet` produces immutable, atomic snapshots for each market in your subscription so you don't have to worry about implementing a cache yourself.

```csharp
// Define your subscription criteria
var marketSubscription = new MarketSubscription(
  new MarketFilter() { /* your filter */ },
  new MarketDataFilter { /* your data filter */ },
  conflateMs: 200
);

var orderSubscription = new OrderSubscription(
  new OrderFilter { /* your filter */ }
);

// Create the stream using the SessionToken
var stream = await client.Streaming.CreateStream(
  session.SessionToken, // Obtained from login
  marketSubscription,
  orderSubscription
);

// Provide callbacks for the events you are interested in
stream.Subscribe(
  ms => { /* handle market snapshots */ },
  os => { /* handle order snapshots */ },
  ex => { /* handle BetfairESAException */ }
);
```

A fluent interface is also exposed which optionally allows the chaining of predicates to filter market and order snapshot events.

```csharp
stream
  .FilterMarkets(ms => /* your condition */)
  .FilterOrders(os => /* your condition */)
  .Subscribe(
    ms => { /* handle market snapshots */ },
    os => { /* handle order snapshots */ },
    ex => { /* handle BetfairESAException */ }
  );
```
