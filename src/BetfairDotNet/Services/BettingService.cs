using BetfairDotNet.Endpoints;
using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models;
using BetfairDotNet.Models.Betting;

namespace BetfairDotNet.Services;


public class BettingService {


    private readonly IRequestResponseHandler _networkService;


    internal BettingService(IRequestResponseHandler networkService) {
        _networkService = networkService;
    }


    /// <summary>
    /// Retrieves a list of cleared orders based on the provided filtering criteria.
    /// </summary>
    /// <param name="betStatus">Required status of the bet for filtering.</param>
    /// <param name="eventTypeIds">Optional list of event type IDs to filter by.</param>
    /// <param name="eventIds">Optional list of event IDs to filter by.</param>
    /// <param name="marketIds">Optional list of market IDs to filter by.</param>
    /// <param name="runnerIds">Optional list of runner IDs to filter by.</param>
    /// <param name="betIds">Optional list of bet IDs to filter by.</param>
    /// <param name="customerOrderRefs">Optional list of customer order references to filter by.</param>
    /// <param name="customerStrategyRefs">Optional list of customer strategy references to filter by.</param>
    /// <param name="side">Optional side (BACK or LAY) to filter by.</param>
    /// <param name="settledDateRange">Optional date range to filter by settlement date.</param>
    /// <param name="groupBy">Optional group by criteria.</param>
    /// <param name="includeItemDescription">Optional flag to indicate if item descriptions should be included.</param>
    /// <param name="fromRecord">Optional starting record for pagination.</param>
    /// <param name="recordCount">Optional count of records to return for pagination.</param>
    /// <param name="locale">Optional locale for translations.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="ClearedOrderSummaryReport"/></returns>
    public Task<ClearedOrderSummaryReport> ListClearedOrders(
            BetStatusEnum betStatus,
            IList<string>? eventTypeIds = null,
            IList<string>? eventIds = null,
            IList<string>? marketIds = null,
            IList<RunnerId>? runnerIds = null,
            IList<string>? betIds = null,
            IList<string>? customerOrderRefs = null,
            IList<string>? customerStrategyRefs = null,
            SideEnum? side = null,
            TimeRange? settledDateRange = null,
            GroupByEnum? groupBy = null,
            bool? includeItemDescription = null,
            int? fromRecord = null,
            int? recordCount = null,
            string? locale = null) {

        var args = new Dictionary<string, object?> {
            ["betStatus"] = betStatus,
            ["eventTypeIds"] = eventTypeIds,
            ["eventIds"] = eventIds,
            ["marketIds"] = marketIds,
            ["runnerIds"] = runnerIds,
            ["betIds"] = betIds,
            ["customerOrderRefs"] = customerOrderRefs,
            ["customerStrategyRefs"] = customerStrategyRefs,
            ["side"] = side,
            ["settledDateRange"] = settledDateRange,
            ["groupBy"] = groupBy,
            ["includeItemDescription"] = includeItemDescription,
            ["fromRecord"] = fromRecord,
            ["recordCount"] = recordCount,
            ["locale"] = locale,
        };

        return _networkService.Request<ClearedOrderSummaryReport>(
            BettingEndpoints.BaseUrl,
            BettingEndpoints.ListClearedOrders,
            args
        );
    }


    /// <summary>
    /// Returns a list of Competitions (i.e., World Cup 2013) associated with the markets selected by the MarketFilter. 
    /// Currently only Football markets have an associated competition.
    /// </summary>
    /// <param name="marketFilter">The filter to select desired markets. All markets that match the criteria in the filter are selected.</param>
    /// <param name="locale">The language used for the response. If not specified, the default is returned.</param>
    /// <returns> A task that represents the asynchronous operation. 
    /// The task result contains a list of <see cref="CompetitionResult"/>.</returns>
    public Task<IReadOnlyList<CompetitionResult>> ListCompetitions(MarketFilter marketFilter, string? locale = null) {
        var args = new Dictionary<string, object?> {
            ["filter"] = marketFilter,
            ["locale"] = locale,
        };
        return _networkService.Request<IReadOnlyList<CompetitionResult>>(
            BettingEndpoints.BaseUrl,
            BettingEndpoints.ListCompetitions,
            args
        );
    }


    /// <summary>
    /// Returns a list of Countries associated with the markets selected by the <see cref=" MarketFilter"/>.
    /// </summary>
    /// <param name="marketFilter"></param>
    /// <param name="locale"></param>
    /// <returns> A task that represents the asynchronous operation. 
    /// The task result contains a list of <see cref="CountryCodeResult"/>.</returns>
    public Task<IReadOnlyList<CountryCodeResult>> ListCountries(MarketFilter marketFilter, string? locale = null) {
        var args = new Dictionary<string, object?> {
            ["filter"] = marketFilter,
            ["locale"] = locale,
        };
        return _networkService.Request<IReadOnlyList<CountryCodeResult>>(
            BettingEndpoints.BaseUrl,
            BettingEndpoints.ListCountries,
            args
        );
    }


    /// <summary>
    /// Returns a list of your current orders. Optionally you can filter and sort your current orders using the various parameters, 
    /// setting none of the parameters will return all of your current orders up to a maximum of 1000 bets, 
    /// ordered BY_BET and sorted EARLIEST_TO_LATEST.
    /// <para>To retrieve more than 1000 orders, you need to make use of the 
    /// fromRecord and recordCount parameters.</para> 
    /// </summary>
    /// <param name="betIds">Optional list of bet IDs to filter by.</param>
    /// <param name="marketIds">Optional list of market IDs to filter by.</param>
    /// <param name="orderProjection">Optional projection type for the orders.</param>
    /// <param name="placedDateRange">Optional date range to filter by placement date.</param>
    /// <param name="dateRange">Optional general date range to filter by.</param>
    /// <param name="orderBy">Optional order by criteria.</param>
    /// <param name="sortDir">Optional sort direction.</param>
    /// <param name="fromRecord">Optional starting record for pagination.</param>
    /// <param name="recordCount">Optional count of records to return for pagination.</param>
    /// <returns> A task that represents the asynchronous operation. 
    /// The task result contains a <see cref="CurrentOrderSummaryReport"/>.</returns>
    public Task<CurrentOrderSummaryReport> ListCurrentOrders(
            IList<string>? betIds = null,
            IList<string>? marketIds = null,
            OrderProjectionEnum? orderProjection = null,
            TimeRange? placedDateRange = null,
            TimeRange? dateRange = null,
            OrderByEnum? orderBy = null,
            SortDirEnum? sortDir = null,
            int? fromRecord = null,
            int? recordCount = null) {

        var args = new Dictionary<string, object?> {
            ["betIds"] = betIds,
            ["marketIds"] = marketIds,
            ["orderProjection"] = orderProjection,
            ["placedDateRange"] = placedDateRange,
            ["dateRange"] = dateRange,
            ["orderBy"] = orderBy,
            ["sortDir"] = sortDir,
            ["fromRecord"] = fromRecord,
            ["recordCount"] = recordCount
        };

        return _networkService.Request<CurrentOrderSummaryReport>(
            BettingEndpoints.BaseUrl,
            BettingEndpoints.ListCurrentOrders,
            args
        );
    }


    /// <summary>
    /// Returns a list of Events (i.e, Reading vs. Man United) associated with the markets selected by the MarketFilter.
    /// </summary>
    /// <param name="marketFilter">The filter to select desired markets. All markets that match the criteria in the filter are selected.</param>
    /// <param name="locale">The language used for the response. If not specified, the default is returned.</param>
    /// <returns> A task that represents the asynchronous operation. 
    /// The task result contains a <see cref="BetfairServerResponse{T}"/> containing the list of <see cref="EventResult"/>.</returns>
    public Task<IReadOnlyList<EventResult>> ListEvents(MarketFilter marketFilter, string? locale = null) {
        var args = new Dictionary<string, object?> {
            ["filter"] = marketFilter,
            ["locale"] = locale,
        };
        return _networkService.Request<IReadOnlyList<EventResult>>(
            BettingEndpoints.BaseUrl,
            BettingEndpoints.ListEvents,
            args
        );
    }


    /// <summary>
    /// Returns a list of Event Types (i.e. Sports) associated with the markets selected by the MarketFilter.
    /// </summary>
    /// <param name="marketFilter">The filter to select desired markets. All markets that match the criteria in the filter are selected.</param>
    /// <param name="locale">The language used for the response. If not specified, the default is returned.</param>
    /// <returns> A task that represents the asynchronous operation. 
    /// The task result contains a <see cref="BetfairServerResponse{T}"/> containing the list of <see cref="EventTypeResult"/>.</returns>
    public Task<IReadOnlyList<EventTypeResult>> ListEventTypes(MarketFilter marketFilter, string? locale = null) {
        var args = new Dictionary<string, object?> {
            ["filter"] = marketFilter,
            ["locale"] = locale,
        };
        return _networkService.Request<IReadOnlyList<EventTypeResult>>(
            BettingEndpoints.BaseUrl,
            BettingEndpoints.ListEventTypes,
            args
        );
    }


    /// <summary>
    /// Returns a list of dynamic data about markets. Dynamic data includes prices, the status of the market, the status of selections, 
    /// the traded volume, and the status of any orders you have placed in the market.
    /// </summary>
    /// <param name="marketIds">One or more market ids. The number of markets returned depends on the amount of data you request via the price projection.</param>
    /// <param name="priceProjection">The projection of price data you want to receive in the response.</param>
    /// <param name="orderProjection">The orders you want to receive in the response.</param>
    /// <param name="matchProjection">If you ask for orders, specifies the representation of matches.</param>
    /// <param name="includeOverallPosition">If you ask for orders, returns matches for each selection. Defaults to true if unspecified.</param>
    /// <param name="partitionMatchedByStrategyRef">If you ask for orders, returns the breakdown of matches by strategy for each selection.</param>
    /// <param name="customerStrategyRefs">If you ask for orders, restricts the results to orders matching any of the specified set of customer defined strategies. 
    /// Also filters which matches by strategy for selections are returned, if partitionMatchedByStrategyRef is true. </param>
    /// <param name="currencyCode">A Betfair standard currency code. If not specified, the default currency code is used.</param>
    /// <param name="locale">The language used for the response. If not specified, the default is returned.</param>
    /// <param name="matchedSince">If you ask for orders, restricts the results to orders that have at least one fragment matched since 
    /// the specified date(all matched fragments of such an order will be returned even if some were matched before the specified date). 
    /// All EXECUTABLE orders will be returned regardless of matched date.</param>
    /// <param name="betIds">If you ask for orders, restricts the results to orders with the specified bet IDs. Omitting this parameter means that all bets
    /// will be included in the response. A maximum of 250 betId's can be provided at a time.</param>
    /// <returns> A task that represents the asynchronous operation. 
    /// The task result contains a <see cref="BetfairServerResponse{T}"/> containing the list of <see cref="MarketBook"/>.</returns>
    public Task<IReadOnlyList<MarketBook>> ListMarketBook(
        IEnumerable<string> marketIds,
        PriceProjection? priceProjection = null,
        OrderProjectionEnum? orderProjection = null,
        MatchProjectionEnum? matchProjection = null,
        bool includeOverallPosition = false,
        bool partitionMatchedByStrategyRef = false,
        IList<string>? customerStrategyRefs = null,
        string? currencyCode = null,
        string? locale = null,
        DateTime? matchedSince = null,
        IList<string>? betIds = null) {

        var args = new Dictionary<string, object?> {
            ["marketIds"] = marketIds,
            ["priceProjection"] = priceProjection,
            ["orderProjection"] = orderProjection,
            ["matchProjection"] = matchProjection,
            ["includeOverallPosition"] = includeOverallPosition,
            ["partitionMatchedByStrategyRef"] = partitionMatchedByStrategyRef,
            ["customerStrategyRefs"] = customerStrategyRefs,
            ["currencyCode"] = currencyCode,
            ["locale"] = locale,
            ["matchedSince"] = matchedSince,
            ["betIds"] = betIds
        };
        return _networkService.Request<IReadOnlyList<MarketBook>>(
            BettingEndpoints.BaseUrl,
            BettingEndpoints.ListMarketBook,
            args
        );
    }


    /// <summary>
    /// Returns a list of information about published (ACTIVE/SUSPENDED) markets that does not change (or changes very rarely). 
    /// Use listMarketCatalogue to retrieve the name of the market, the names of selections and other information about markets.
    /// <para>Market Data Request Limits apply to requests made to listMarketCatalogue.</para> 
    /// <para>listMarketCatalogue does not return markets that are CLOSED.</para>
    /// </summary>
    /// <param name="marketFilter">The filter to select desired markets. All markets that match the criteria in the filter are selected.</param>
    /// <param name="marketProjections">The type and amount of data returned about the market.</param>
    /// <param name="sort">The order of the results. Will default to RANK if not passed. RANK is an assigned priority that is determined by Betfair's 
    /// Market Operations team. A result's overall rank is derived from the ranking given to the following attributes for the result:
    /// EventType, Competition, StartTime, MarketType, MarketId</param>
    /// <param name="maxResults">limit on the total number of results returned, must be greater than 0 and less than or equal to 1000</param>
    /// <param name="locale">The language used for the response. If not specified, the default is returned.</param>
    /// <returns> A task that represents the asynchronous operation. 
    /// The task result contains a <see cref="BetfairServerResponse{T}"/> containing the list of <see cref="MarketCatalogue"/>.</returns>
    public Task<IReadOnlyList<MarketCatalogue>> ListMarketCatalogue(
            MarketFilter marketFilter,
            IList<MarketProjectionEnum>? marketProjections = null,
            MarketSortEnum? sort = null,
            int maxResults = 1,
            string? locale = null) {

        var args = new Dictionary<string, object?> {
            ["filter"] = marketFilter,
            ["marketProjection"] = marketProjections,
            ["sort"] = sort,
            ["maxResults"] = maxResults,
            ["locale"] = locale
        };
        return _networkService.Request<IReadOnlyList<MarketCatalogue>>(
            BettingEndpoints.BaseUrl,
            BettingEndpoints.ListMarketCatalogue,
            args
        );
    }


    /// <summary>
    /// Retrieve profit and loss for a given list of OPEN markets. The values are calculated using matched bets and optionally settled bets. 
    /// <para>Only odds (MarketBettingType = ODDS) markets  are implemented, markets of other types are silently ignored.</para> 
    /// </summary>
    /// <param name="marketIds">List of markets to calculate profit and loss</param>
    /// <param name="includeSettledBets">Option to include settled bets (partially settled markets only). Defaults to false if not specified.</param>
    /// <param name="includeBsbBets">Option to include BSP bets. Defaults to false if not specified.</param>
    /// <param name="netOfCommission">Option to return profit and loss net of users current commission rate for this market including any special tariffs.</param>
    /// <returns> A task that represents the asynchronous operation. 
    /// The task result contains a <see cref="BetfairServerResponse{T}"/> containing the list of <see cref="MarketProfitAndLoss"/>.</returns>
    public Task<IReadOnlyList<MarketProfitAndLoss>> ListMarketProfitAndLoss(
            IList<string> marketIds,
            bool includeSettledBets = false,
            bool includeBsbBets = false,
            bool netOfCommission = false) {

        var args = new Dictionary<string, object?> {
            ["marketIds"] = marketIds,
            ["includeSettledBets"] = includeSettledBets,
            ["includeBspBets"] = includeBsbBets,
            ["netOfCommission"] = netOfCommission
        };
        return _networkService.Request<IReadOnlyList<MarketProfitAndLoss>>(
            BettingEndpoints.BaseUrl,
            BettingEndpoints.ListMarketProfitAndLoss,
            args
        );
    }


    /// <summary>
    /// Returns a list of market types (i.e. MATCH_ODDS, NEXT_GOAL) associated with the markets selected by the MarketFilter. 
    /// The market types are always the same, regardless of locale.
    /// </summary>
    /// <param name="marketFilter">The filter to select desired markets. All markets that match the criteria in the filter are selected.</param>
    /// <param name="locale">The language used for the response. If not specified, the default is returned.</param>
    /// <returns> A task that represents the asynchronous operation. 
    /// The task result contains a <see cref="BetfairServerResponse{T}"/> containing the list of <see cref="MarketTypeResult"/>.</returns>
    public Task<IReadOnlyList<MarketTypeResult>> ListMarketTypes(MarketFilter marketFilter, string? locale = null) {
        var args = new Dictionary<string, object?> {
            ["filter"] = marketFilter,
            ["locale"] = locale
        };
        return _networkService.Request<IReadOnlyList<MarketTypeResult>>(
            BettingEndpoints.BaseUrl,
            BettingEndpoints.ListMarketTypes,
            args
        );
    }


    /// <summary>
    /// Returns a list of dynamic data about a market and a specified runner. Dynamic data includes prices, the status of the market, 
    /// the status of selections, the traded volume, and the status of any orders you have placed in the market.
    /// </summary>
    /// <param name="marketId">The unique id for the market.</param>
    /// <param name="selectionId">The unique id for the selection in the market.</param>
    /// <param name="handicap">The handicap associated with the runner in case of Asian handicap market.</param>
    /// <param name="priceProjection">The projection of price data you want to receive in the response.</param>
    /// <param name="orderProjection">The orders you want to receive in the response.</param>
    /// <param name="matchProjection">If you ask for orders, specifies the representation of matches.</param>
    /// <param name="includeOverallPostion">If you ask for orders, returns matches for each selection. Defaults to true if unspecified.</param>
    /// <param name="partitionMatchByStrategyRef">If you ask for orders, returns the breakdown of matches by strategy for each selection.</param>
    /// <param name="customerStrategyRef">If you ask for orders, restricts the results to orders matching any of the specified set of customer defined strategies. 
    /// Also filters which matches by strategy for selections are returned, if partitionMatchedByStrategyRef is true. </param>
    /// <param name="currencyCode">A Betfair standard currency code. If not specified, the default currency code is used.</param>
    /// <param name="locale">The language used for the response. If not specified, the default is returned.</param>
    /// <param name="matchedSince">If you ask for orders, restricts the results to orders that have at least one fragment matched since 
    /// the specified date(all matched fragments of such an order will be returned even if some were matched before the specified date). 
    /// All EXECUTABLE orders will be returned regardless of matched date.</param>
    /// <param name="betIds">If you ask for orders, restricts the results to orders with the specified bet IDs. Omitting this parameter means that 
    /// all bets will be included in the response. Please note: A maximum of 250 betId's can be provided at a time.</param>
    /// <returns> A task that represents the asynchronous operation. 
    /// The task result contains a <see cref="BetfairServerResponse{T}"/> containing the <see cref="MarketBook"/> and the <see cref="RunnerBook"/>
    /// for the specified runner.</returns>
    public Task<MarketBook> ListRunnerBook(
        string marketId,
        string selectionId,
        double? handicap = null,
        PriceProjection? priceProjection = null,
        OrderProjectionEnum? orderProjection = null,
        MatchProjectionEnum? matchProjection = null,
        bool includeOverallPostion = false,
        bool partitionMatchByStrategyRef = false,
        IList<string>? customerStrategyRef = null,
        string? currencyCode = null,
        string? locale = null,
        DateTime? matchedSince = null,
        IList<string>? betIds = null) {

        var args = new Dictionary<string, object?> {
            ["marketId"] = marketId,
            ["selectionId"] = selectionId,
            ["handicap"] = handicap,
            ["priceProjection"] = priceProjection,
            ["orderProjection"] = orderProjection,
            ["matchProjection"] = matchProjection,
            ["includeOverallPosition"] = includeOverallPostion,
            ["partitionMatchedByStrategyRef"] = partitionMatchByStrategyRef,
            ["customerStrategyRefs"] = customerStrategyRef,
            ["currencyCode"] = currencyCode,
            ["locale"] = locale,
            ["matchedSince"] = matchedSince,
            ["betIds"] = betIds
        };

        // TODO does this return a list with one item or just the item?
        return _networkService.Request<MarketBook>(
            BettingEndpoints.BaseUrl,
            BettingEndpoints.ListRunnerBook,
            args
        );
    }


    /// <summary>
    /// Returns a list of time ranges in the granularity specified in the request (i.e. 3PM to 4PM, Aug 14th to Aug 15th) 
    /// associated with the markets selected by the MarketFilter.
    /// </summary>
    /// <param name="marketFilter">The filter to select desired markets. All markets that match the criteria in the filter are selected.</param>
    /// <param name="timeGranularity">The granularity of time periods that correspond to markets selected by the market filter.</param>
    /// <returns> A task that represents the asynchronous operation. 
    /// The task result contains a <see cref="BetfairServerResponse{T}"/> containing the list of <see cref="TimeRangeResult"/>.</returns>
    public Task<IReadOnlyList<TimeRangeResult>> ListTimeRanges(MarketFilter marketFilter, TimeGranularityEnum timeGranularity) {
        var args = new Dictionary<string, object?> {
            ["filter"] = marketFilter,
            ["granularity"] = timeGranularity
        };
        return _networkService.Request<IReadOnlyList<TimeRangeResult>>(
            BettingEndpoints.BaseUrl,
            BettingEndpoints.ListTimeRanges,
            args
        );
    }


    /// <summary>
    /// Returns a list of Venues (i.e. Cheltenham, Ascot) associated with the markets selected by the MarketFilter. 
    /// Currently, only Horse Racing markets are associated with a Venue.
    /// </summary>
    /// <param name="marketFilter">The filter to select desired markets. All markets that match the criteria in the filter are selected.</param>
    /// <param name="locale">The language used for the response. If not specified, the default is returned.</param>
    /// <returns> A task that represents the asynchronous operation. 
    /// The task result contains a <see cref="BetfairServerResponse{T}"/> containing the list of <see cref="VenueResult"/>.</returns>
    public Task<IReadOnlyList<VenueResult>> ListVenues(MarketFilter marketFilter, string? locale = null) {
        var args = new Dictionary<string, object?> {
            ["filter"] = marketFilter,
            ["locale"] = locale
        };
        return _networkService.Request<IReadOnlyList<VenueResult>>(
            BettingEndpoints.BaseUrl,
            BettingEndpoints.ListVenues,
            args
        );
    }


    /// <summary>
    /// Place new orders into market. Additional bet sizing rules apply to bets placed into the Italian Exchange.
    /// <para>In normal circumstances the placeOrders is an atomic operation.PLEASE NOTE: if the 'Best Execution' features is switched off, 
    /// placeOrders can return ‘PROCESSED_WITH_ERRORS’ meaning that some bets can be rejected and other placed when 
    /// submitted in the same <see cref="PlaceInstruction"/></para>
    /// </summary>
    /// <param name="marketId">The market id these orders are to be placed on</param>
    /// <param name="placeInstructions">The number of place instructions. The limit of place instructions per request is 200 for the 
    /// Global Exchange and 50 for the Italian Exchange.</param>
    /// <param name="customerRef">Optional parameter allowing the client to pass a unique string (up to 32 chars) that is used to de-dupe mistaken re-submissions.   
    /// customerRef can contain: upper/lower chars, digits, chars : - . _ + * : ; ~ only.  This field does not persist into the placeOrders response/Order Stream API 
    /// and should not be confused with customerOrderRef, which is separate field that can be sent in the PlaceInstruction.</param>
    /// <param name="marketVersion">Optional parameter allowing the client to specify which version of the market the 
    /// orders should be placed on.If the current market version is higher than that sent on an order, 
    /// the bet will be lapsed.</param>
    /// <param name="customerStrategyRef">An optional reference customers can use to specify which strategy has sent the order. 
    /// The reference will be returned on order change messages through the stream API.The string is 
    /// limited to 15 characters. </param>
    /// <param name="async">An optional flag (not setting equates to false) which specifies if the orders should be placed asynchronously. 
    /// Orders can be tracked via the Exchange Stream API or the API-NG by providing a customerOrderRef for each place order.
    /// An order's status will be PENDING and no bet ID will be returned. </param>
    /// <returns> A task that represents the asynchronous operation. 
    /// The task result contains a <see cref="BetfairServerResponse{T}"/> containing the <see cref="PlaceExecutionReport"/>.</returns>
    public Task<PlaceExecutionReport> PlaceOrders(
        string marketId,
        IList<PlaceInstruction> placeInstructions,
        string? customerRef = null,
        MarketVersion? marketVersion = null,
        string? customerStrategyRef = null,
        bool async = false) {

        var args = new Dictionary<string, object?> {
            ["marketId"] = marketId,
            ["instructions"] = placeInstructions,
            ["customerRef"] = customerRef,
            ["marketVersion"] = marketVersion,
            ["customerStrategyRef"] = customerStrategyRef,
            ["async"] = async
        };
        return _networkService.Request<PlaceExecutionReport>(
            BettingEndpoints.BaseUrl,
            BettingEndpoints.PlaceOrders,
            args
        );
    }


    /// <summary>
    /// Update non-exposure changing fields.
    /// </summary>
    /// <param name="marketId">The market id these orders are to be placed on.</param>
    /// <param name="instructions">The number of update instructions. The limit of update instructions per request is 60.</param>
    /// <param name="customerRef">Optional parameter allowing the client to pass a unique string (up to 32 chars) that is used to 
    /// de-dupe mistaken re-submissions.</param>
    /// <returns> A task that represents the asynchronous operation. 
    /// The task result contains a <see cref="BetfairServerResponse{T}"/> containing the <see cref="UpdateExecutionReport"/>.</returns>
    public Task<UpdateExecutionReport> UpdateOrders(
        string marketId,
        IList<UpdateInstruction> instructions,
        string? customerRef = null) {

        var args = new Dictionary<string, object?> {
            ["marketId"] = marketId,
            ["instructions"] = instructions,
            ["customerRef"] = customerRef
        };
        return _networkService.Request<UpdateExecutionReport>(
            BettingEndpoints.BaseUrl,
            BettingEndpoints.UpdateOrders,
            args
        );
    }


    /// <summary>
    /// This operation is logically a bulk cancel followed by a bulk place. The cancel is completed first then the new orders are placed. 
    /// The new orders will be placed atomically in that they will all be placed or none will be placed. 
    /// <para>In the case where the new orders cannot be placed the cancellations will not be rolled back.</para>  
    /// </summary>
    /// <param name="marketId">The market id these orders are to be placed on.</param>
    /// <param name="instructions">The number of replace instructions.  The limit of replace instructions per request is 60.</param>
    /// <param name="customerRef">Optional parameter allowing the client to pass a unique string (up to 32 chars) that is used to de-dupe mistaken re-submissions.</param>
    /// <param name="marketVersion">Optional parameter allowing the client to specify which version of the market the 
    /// orders should be placed on.If the current market version is higher than that sent on an order, 
    /// the bet will be lapsed.</param>
    /// <param name="async">An optional flag (not setting equates to false) which specifies if the orders should be replaced asynchronously. 
    /// Orders can be tracked via the Exchange Stream API or the API-NG by providing a customerOrderRef for each replace order.
    /// Not available for MOC or LOC bets.</param>
    /// <returns></returns>
    public Task<ReplaceExecutionReport> ReplaceOrders(
        string marketId,
        IList<ReplaceInstruction> instructions,
        string? customerRef = null,
        MarketVersion? marketVersion = null,
        bool async = false) {

        var args = new Dictionary<string, object?> {
            ["marketId"] = marketId,
            ["instructions"] = instructions,
            ["customerRef"] = customerRef,
            ["marketVersion"] = marketVersion,
            ["async"] = async
        };
        return _networkService.Request<ReplaceExecutionReport>(
            BettingEndpoints.BaseUrl,
            BettingEndpoints.ReplaceOrders,
            args
        );
    }
}
