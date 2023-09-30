<h1 align="center">
  BetfairDotNet
  <br>
</h1>

<div align="center">

[![ci](https://github.com/BrandonWoodward/BetfairDotNet/actions/workflows/CI.yml/badge.svg)](https://github.com/BrandonWoodward/BetfairDotNet/actions/workflows/CI.yml)
[![cd](https://github.com/BrandonWoodward/BetfairDotNet/actions/workflows/CD.yml/badge.svg)](https://github.com/BrandonWoodward/BetfairDotNet/actions/workflows/CD.yml)
[![codecov](https://codecov.io/gh/BrandonWoodward/BetfairDotNet/branch/master/graph/badge.svg)](https://codecov.io/gh/BrandonWoodward/BetfairDotNet)
[![NuGet](https://img.shields.io/nuget/v/BetfairDotNet.svg?style=flat)](https://www.nuget.org/packages/BetfairDotNet/)

</div>

<h4 align="center"> 🚀 A fast, easy to use C# client for the <a href="https://docs.developer.betfair.com/display/1smk3cen4v3lu3yomq5qye0ni" target="_blank">Betfair API</a>.</h4>

<br>

## Table of Contents

- [Requirements](#requirements)
- [Installation](#installation)
- [Login](#login)
- [API-NG](#api-ng)
- [Streaming](#streaming)

<br>

## Requirements

You will need a Betfair account, an api key and a self-signed SSL certificate.

<br>
<br>

You can obtain an api key by following the instructions [here](https://docs.developer.betfair.com/display/1smk3cen4v3lu3yomq5qye0ni/Application+Keys"). If you want to use a non-interactive login flow, you 
should follow the instructions [here](https://docs.developer.betfair.com/display/1smk3cen4v3lu3yomq5qye0ni/Non-Interactive+%28bot%29+login) to create an SSL certificate.

<br>

## Installation

Install via NuGet Package Manager or the .NET CLI:

```bash
dotnet add package BetfairDotNet
```

Or clone this repository and build the project locally:

```bash
git clone https://github.com/yourusername/BetfairDotNet.git
cd BetfairDotNet
dotnet build
```

<br>

##  Login

If you're developing an automated trading algorithm, login with an SSL certificate:

```csharp
// Enter your credentials here
var client = new BetfairClient(apiKey)

// Returns the SessionToken and the error code if unsuccessful
var session = await client.Login.CertificateLogin(username, password, certPath);
```

Use the `InteractiveLogin` if you're creating a login form:

```csharp
// Enter your credentials here
var client = new BetfairClient(apiKey)

// Returns the SessionToken and the error code if unsuccessful
var session = await client.Login.InteractiveLogin(username, password);
```

<br>

## API-NG

Functionality is provided for the following endpoints:

- Account
	- `GetAccountFunds`
	- `GetAccountDetails`
	- `GetAccountStatement`
	- `ListCurrencyRates`
- Betting
	- `ListClearedOrders`
	- `ListCompetitions`
	- `ListCountries`
	- `ListCurrentOrders`
	- `ListEvents`
	- `ListEventTypes`
	- `ListMarketBook`
	- `ListMarketCatalogue`
	- `ListMarketProfitAndLoss`
	- `ListMarketTypes`
	- `ListRunnerBook`
	- `ListTimeRanges`
	- `ListVenues`
	- `PlaceOrders`
	- `UpdateOrders`
	- `ReplaceOrders`
- Heartbeat
	- `Heartbeat`
	- `KeepAlive`
- Navigation
	- `NavigationMenu`

<br>

## Streaming

`BetfairDotNet` produces immutable, atomic snapshots for each market in your subscription so you don't have to worry about implementing a cache yourself. The stream will automatically attempt to recover in the event of a connection loss.


```csharp
// Create a streaming configuration
var streamConfiguration = new StreamConfiguration()
{
    SessionToken = /* your sessionToken */,
    RecoveryThresholdMs = 3_000, // How long before attempting to recover
    MaxRecoveryWaitMs = 120_000 // If can't recover in this time, the socket closes and is disposed
};

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
var stream = client.Streaming
    .CreateStream(streamConfiguration)
    .WithMarketSubscription(marketSubscription)
    .WithOrderSubscription(orderSubscription)

// Provide callbacks for the events you are interested in
await stream.Subscribe(
    ms => { /* handle market snapshots */ },
    os => { /* handle order snapshots */ },
    ex => { /* handle BetfairESAException */ }
);
```
