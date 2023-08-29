#  BetfairDotNet 

<div align="center">
  <img src="https://your-logo-link.png" alt="BetfairDotNet Logo" width="200"/>
  <br>

  [![Build Status](https://travis-ci.com/yourusername/yourrepository.svg?branch=main)](https://travis-ci.com/yourusername/yourrepository)
  [![NuGet Version](https://img.shields.io/nuget/v/BetfairDotNet.svg?style=flat)](https://www.nuget.org/packages/BetfairDotNet/)
  [![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/yourusername/yourrepository/blob/main/LICENSE)
</div>

**:rocket: A blazingly fast and user-friendly Betfair API client for .NET 7.**

---

## 📚 Table of Contents

- [🌟 Features](#features)
- [🛠️ Requirements](#requirements)
- [📦 Installation](#installation)
- [📖 Examples](#examples)
- [🤝 Contributing](#contributing)
- [📜 License](#license)

---

## 🌟 Features

BetfairDotNet offers you the power to seamlessly integrate Betfair functionalities into your application. We've got you covered with these capabilities:

### 🛡️ Login

-  Interactive Login
-  Certificate Login

### 💰 Accounts API

-  `/getAccountDetails` - Get all the nitty-gritty details of your account.
-  `/getAccountFunds` - Peek into your Betfair wallet.
-  `/getAccountStatement` - Your transactions, laid bare.
-  `/listCurrencyRates` - Latest exchange rates at your fingertips.

### 🎲 Betting API

- `/listCompetitions` - All the games you can bet on.
-  `/listCountries` - Where the action is happening.
-  `/listCurrentOrders` - Keep tabs on your pending bets.
-  `/listClearedOrders` - Bets you've already won or lost.
-  `/listEventTypes` - Types of events available for betting.
-  `/listEvents` - All ongoing and upcoming events.
-  `/listMarketCatalogue` - Detailed market data.
-  `/listMarketBook` - Active market book.
-  `/listMarketProfitAndLoss` - See how you're doing financially.
-  `/listMarketTypes` - Types of markets available.
-  `/listRunnerBook` - Detailed runner-level market data.
-  `/listTimeRanges` - Time-based data.
-  `/listVenues` - All the venues you can bet on.
-  `/placeOrders` - Place new orders.
-  `/cancelOrders` - Cancel existing orders.
-  `/replaceOrders` - Replace one or more orders.
-  `/updateOrders` - Update the status of your orders.

### ❤️ Heartbeat API

-  `/heartbeat` - Keep your session alive.

### 📡 Streaming API

- A fluent interface for order and market subscriptions based on System.Reactive.
-  Easy error handling / reconnection.

---

## 🛠️ Requirements

### 🎯 Runtime

- .NET 7

### 📦 Dependencies

This library depends on:

- `Newtonsoft.Json` 🗂️
- `Microsoft.Extensions.Http` 🌐
- `System.Reactive` ⚡

> **Note**: These dependencies should be automatically resolved if you install via NuGet.

---

## 📦 Installation

```bash
dotnet add package BetfairDotNet --version x.y.z

