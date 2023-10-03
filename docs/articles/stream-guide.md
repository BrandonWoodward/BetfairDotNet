# Stream Guide

---

<br/>

`BetfairDotNet` abstracts away all the complexity of using the Betfair Stream API. 
The streaming client produces Immutable, atomic snapshots for each market in
your subscription so you don't have to worry about implementing a cache yourself.

<br/>

Create a stream configuration:
- `SessionToken` - The session token you received from the login flow.
- `RecoveryThresholdMs` - How long to wait before attempting to recover from a socket error.
- `MaxRecoveryWaitMs` - How long to wait before giving up on recovery and disposing the socket.

```csharp
// Create a streaming configuration
var streamConfiguration = new StreamConfiguration()
{
    SessionToken = /* your sessionToken */,
    RecoveryThresholdMs = 3_000,
    MaxRecoveryWaitMs = 120_000
};
```

<br/>

Create a market subscription (or order subscription or both):
- `StreamingMarketFilter` - The markets to subscribe to, see [MarketFilter](../api/BetfairDotNet.Models.Streaming.StreamingMarketFilter.yml) for details.
- `MarketDataFilter` - The data to receive in the update, see [MarketDataFilter](../api/BetfairDotNet.Models.Streaming.StreamingMarketDataFilter.yml) for details.
- `ConflateMs` - The update frequency. Defaults to the tick frequency of the Stream API (50ms).
- `OrderFilter` - Configure how order updates are sent, see [OrderFilter](../api/BetfairDotNet.Models.Streaming.OrderFilter.yml) 
  for details.

```csharp
// Define your subscription criteria
var marketSubscription = new MarketSubscription(
    new StreamingMarketFilter() { /* your filter */ },
    new MarketDataFilter { /* your data filter */ },
    conflateMs: 200
);

var orderSubscription = new OrderSubscription(
    new OrderFilter { /* your filter */ }
);
```

<br/>

>[!NOTE]
> The order stream returns only **EXECUTABLE** orders on initial image. Therefore
> if you have **EXECUTION_COMPLETE** orders in the market before you subscribe, i.e
> fully matched orders, these will not be received in the stream. You can use
> the `Betting.ListCurrentOrders` or `Betting.ListMarketBook` method to retrieve these orders.
> This is a limitation of the Betfair API and not specific to `BetfairDotNet`.


<br/>

Create a streaming client:

```csharp
var stream = client.Streaming
    .CreateStream(streamConfiguration)
    .WithMarketSubscription(marketSubscription)
    .WithOrderSubscription(orderSubscription)
```

<br/>

Subscribe to the callbacks you are interested in:

```csharp
await stream.Subscribe(
    ms => { /* handle market snapshots */ },
    os => { /* handle order snapshots */ },
    ex => { /* handle BetfairESAException */ }
);
```

