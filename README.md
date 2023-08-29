# BetfairDotNet

A simple and easy to use client for the Betfair API-NG and Streaming API.

## Table of Contents

- [Features](#features)
- [Requirements](#requirements)
- [Installation](#installation)
- [Examples](#examples)
- [Contributing](#contributing)
- [License](#license)


## Features

Methods and abstractions are currently provided for the following functionalites:

### Login API

- Interactive Login
- Certificate Login

### Account API

- /getAccountDetails
- /getAccountFunds
- /getAccountStatement
- /listCurrencyRates

### Betting API

- /listCompetitions
- /listCountries
- /listCurrentOrders
- /listClearedOrders
- /listEventTypes
- /listEvents
- /listMarketCatalogue
- /listMarketBook
- /listMarketProfitAndLoss
- /listMarketTypes
- /listRunnerBook
- /listTimeRanges
- /listVenues
- /placeOrders
- /cancelOrders
- /replaceOrders
- /updateOrders

### Heartbeat API

- /heartbeat

### Streaming API

- A fluent interface for order and market subscriptions based on System.Reactive.
- Easy error handling / reconnection.
