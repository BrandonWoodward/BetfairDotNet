using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Enums.Streaming;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Exceptions;
using BetfairDotNet.Models.Streaming;

namespace BetfairDotNet.Services;


/// <summary>
/// The service for the Betfair Streaming API.
/// </summary>
public sealed class StreamingService 
{
    private readonly IStreamSubscriptionHandler _streamSubscriptionHandler;
    private readonly StreamConfiguration _streamConfiguration = new();
    private readonly MarketSubscription _marketSubscription = new();
    private readonly OrderSubscription _orderSubscription = new();

    private Action<MarketSnapshot>? _onMarketChange;
    private Action<OrderMarketSnapshot>? _onOrderChange;
    private Action<BetfairESAException>? _onException;
    

    internal StreamingService(IStreamSubscriptionHandler streamSubscriptionHandler, string apiKey) 
    {
        _streamSubscriptionHandler = streamSubscriptionHandler;
        _streamConfiguration.ApiKey = apiKey;
    }

    /// <summary>
    /// Create a stream for the given session token.
    /// </summary>
    /// <param name="sessionToken">Session token obtained from successful login.</param>
    public StreamingService CreateStream(string sessionToken)
    {
        _streamConfiguration.SessionToken = sessionToken;
        return this;
    }

    /// <summary>
    /// Configure the stream with auto reconnection options.
    /// </summary>
    /// <param name="recoveryThreshold">
    /// The amount of downtime on the socket needed to trigger a recovery attempt.
    /// </param>
    /// <param name="maxRecoveryWait">
    /// In the event of loss of connection, this is maximum
    /// amount of time the stream will wait for recovery before
    /// unsubscribing and closing the socket. Defaults to 2 minutes.
    /// </param>
    public StreamingService WithAutoReconnection(int recoveryThreshold, int maxRecoveryWait)
    {
        _streamConfiguration.RecoveryThresholdMs = recoveryThreshold;
        _streamConfiguration.MaxRecoveryWaitMs = maxRecoveryWait;
        return this;
    }

    /// <summary>
    /// Subscribe to markets with these country codes only.
    /// </summary>
    /// <param name="countryCodes"></param>
    public StreamingService ForCountryCodes(params string[] countryCodes)
    {
        _marketSubscription.MarketFilter.CountryCodes = countryCodes.ToList();
        return this;
    }

    /// <summary>
    /// Subscribe to markets for these betting types only.
    /// </summary>
    /// <param name="bettingTypes"></param>
    public StreamingService ForBettingTypes(params MarketBettingTypeEnum[] bettingTypes)
    {
        _marketSubscription.MarketFilter.BettingTypes = bettingTypes.ToList();
        return this;
    }

    /// <summary>
    /// Subscribe to markets that have the given in play enabled flag only.
    /// </summary>
    /// <param name="inPlayEnabled"></param>
    public StreamingService ForMarketsWithInPlayEnabled(bool inPlayEnabled)
    {
        _marketSubscription.MarketFilter.TurnInPlayEnabled = inPlayEnabled;
        return this;
    }

    /// <summary>
    /// Subscribe to the given market types only.
    /// </summary>
    /// <param name="marketTypes"></param>
    /// <returns></returns>
    public StreamingService ForMarketTypes(params string[] marketTypes)
    {
        _marketSubscription.MarketFilter.MarketTypes = marketTypes.ToList();
        return this;
    }

    /// <summary>
    /// Subscribe to markets at the given venues only.
    /// </summary>
    /// <param name="venues"></param>
    public StreamingService ForVenues(params string[] venues)
    {
        _marketSubscription.MarketFilter.Venues = venues.ToList();
        return this;
    }

    /// <summary>
    /// Subscribe to the following markets only.
    /// </summary>
    /// <param name="marketIds"></param>
    public StreamingService ForMarketIds(params string[] marketIds)
    {
        _marketSubscription.MarketFilter.MarketIds = marketIds.ToList();
        return this;
    }

    /// <summary>
    /// Subscribe to markets with the given event type ids only.
    /// </summary>
    /// <param name="eventTypeIds"></param>
    public StreamingService ForEventTypeIds(params string[] eventTypeIds)
    {
        _marketSubscription.MarketFilter.EventTypeIds = eventTypeIds.ToList();
        return this;
    }

    /// <summary>
    /// Subscribe to markets with the given eventIds only.
    /// </summary>
    /// <param name="eventIds"></param>
    public StreamingService ForEventIds(params string[] eventIds)
    {
        _marketSubscription.MarketFilter.EventIds = eventIds.ToList();
        return this;
    }

    /// <summary>
    /// Subscribe to markets that have the given bsp enabled flag only.
    /// </summary>
    /// <param name="bspEnabled"></param>
    public StreamingService ForMarketsWithBspEnabled(bool bspEnabled)
    {
        _marketSubscription.MarketFilter.BspMarket = bspEnabled;
        return this;
    }

    /// <summary>
    /// Filter the data returned by the stream. Only ask for what you need, as this will reduce
    /// initial image time and improve performance.
    /// </summary>
    /// <param name="ladderLevels">Number of prices either side of the book to return.</param>
    /// <param name="dataFilters">Types of data to receive back from the stream.</param>
    public StreamingService ReturningMarketDataFor(int ladderLevels, params MarketDataFilterEnum[] dataFilters)
    {
        _marketSubscription.MarketDataFilter.LadderLevels = ladderLevels;
        _marketSubscription.MarketDataFilter.Fields = dataFilters.ToList();
        return this;
    }

    /// <summary>
    /// Partition orders by strategy reference and filter by the given strategies.
    /// </summary>
    /// <param name="customerStrategyRefs">The strategies to return orders for.</param>
    public StreamingService ReturningOrdersWithStrategyRefs(params string[] customerStrategyRefs)
    {
        _orderSubscription.StreamingOrderFilter.PartitionMatchedByStrategyRef = true;
        _orderSubscription.StreamingOrderFilter.CustomerStrategyRefs = customerStrategyRefs.ToList();
        return this;
    }

    /// <summary>
    /// Limit updates from the stream to the given frequency.
    /// </summary>
    /// <param name="conflation">Conflation rate in ms.</param>
    public StreamingService ConflateUpdatesTo(int conflation)
    {
        _marketSubscription.ConflateMs = conflation;
        _orderSubscription.ConflateMs = conflation;
        return this;
    }

    /// <summary>
    /// Callback for market change messages.
    /// </summary>
    /// <param name="onMarketChange"></param>
    public StreamingService OnMarketChange(Action<MarketSnapshot> onMarketChange)
    {
        _onMarketChange = onMarketChange;
        return this;
    }
    
    /// <summary>
    /// Callback for order change messages.
    /// </summary>
    /// <param name="onOrderChange"></param>
    public StreamingService OnOrderChange(Action<OrderMarketSnapshot> onOrderChange)
    {
        _onOrderChange = onOrderChange;
        return this;
    }

    /// <summary>
    /// Callback for exceptions.
    /// </summary>
    /// <param name="onException"></param>
    public StreamingService OnException(Action<BetfairESAException> onException)
    {
        _onException = onException;
        return this;
    }

    /// <summary>
    /// Subscribe to the stream and begin receiving messages.
    /// </summary>
    public Task Subscribe()
    {
        return _streamSubscriptionHandler.Subscribe(
            _streamConfiguration,
            _marketSubscription,
            _orderSubscription,
            _onMarketChange,
            _onOrderChange,
            _onException
        );
    }
}
