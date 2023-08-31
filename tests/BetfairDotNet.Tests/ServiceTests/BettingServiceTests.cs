using BetfairDotNet.Enums.Betting;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Betting;
using BetfairDotNet.Services;
using NSubstitute;
using Xunit;

namespace BetfairDotNet.Tests.ServiceTests;

/// <summary>
/// Unit tests for the <see cref="BettingService"/>.
/// </summary>
public class BettingServiceTests {

    private readonly IRequestResponseHandler _mockNetwork = Substitute.For<IRequestResponseHandler>();
    private readonly BettingService _bettingService;

    public BettingServiceTests() {
        _bettingService = new BettingService(_mockNetwork);
    }


    [Fact]
    public async Task ListClearedOrders_SendCorrectRequest() {

        // Arrange
        var betStatus = BetStatusEnum.SETTLED;
        var eventTypeIds = new List<string>() { "testEventTypeId" };
        var eventIds = new List<string>() { "testEventId" };
        var marketIds = new List<string>() { "testMarketId" };
        var runnerIds = new List<RunnerId>() { new RunnerId() };
        var betIds = new List<string>() { "testBetId" };
        var customerOrderRefs = new List<string>() { "testCustomerOrderRef" };
        var customerStrategyRefs = new List<string>() { "testCustomerStrategyRef" };
        var side = SideEnum.BACK;
        var settledDateRange = new TimeRange() { From = DateTime.Now, To = DateTime.Now.AddDays(1) };
        var groupBy = GroupByEnum.BET;
        var includeItemDescription = true;
        var fromRecord = 1;
        var recordCount = 10;
        var locale = "testLocale";

        // Act
        await _bettingService.ListClearedOrders(
            betStatus,
            eventTypeIds,
            eventIds,
            marketIds,
            runnerIds,
            betIds,
            customerOrderRefs,
            customerStrategyRefs,
            side,
            settledDateRange,
            groupBy,
            includeItemDescription,
            fromRecord,
            recordCount,
            locale
        );

        // Assert
        await _mockNetwork.Received().Request<ClearedOrderSummaryReport>(
            "https://api.betfair.com/exchange/betting/json-rpc/v1",
            "SportsAPING/v1.0/listClearedOrders",
            Arg.Is<Dictionary<string, object?>>(args =>
                (BetStatusEnum?)args["betStatus"] == betStatus &&
                (List<string>?)args["eventTypeIds"] == eventTypeIds &&
                (List<string>?)args["eventIds"] == eventIds &&
                (List<string>?)args["marketIds"] == marketIds &&
                (List<RunnerId>?)args["runnerIds"] == runnerIds &&
                (List<string>?)args["betIds"] == betIds &&
                (List<string>?)args["customerOrderRefs"] == customerOrderRefs &&
                (List<string>?)args["customerStrategyRefs"] == customerStrategyRefs &&
                (SideEnum?)args["side"] == side &&
                (TimeRange?)args["settledDateRange"] == settledDateRange &&
                (GroupByEnum?)args["groupBy"] == groupBy &&
                (bool?)args["includeItemDescription"] == includeItemDescription &&
                (int?)args["fromRecord"] == fromRecord &&
                (int?)args["recordCount"] == recordCount &&
                (string?)args["locale"] == locale)
            );
    }


    [Fact]
    public async Task ListCompetitions_SendsCorrectRequest() {

        // Arrange
        var marketFilter = new MarketFilter();
        var locale = "testLocale";

        // Act
        await _bettingService.ListCompetitions(marketFilter, locale);

        // Assert
        await _mockNetwork.Received().Request<List<CompetitionResult>>(
            "https://api.betfair.com/exchange/betting/json-rpc/v1",
            "SportsAPING/v1.0/listCompetitions",
            Arg.Is<Dictionary<string, object?>>(args =>
                (MarketFilter?)args["filter"] == marketFilter &&
                (string?)args["locale"] == locale)
        );
    }


    [Fact]
    public async Task ListCountries_SendsCorrectRequest() {
        // Arrange
        var marketFilter = new MarketFilter();
        var locale = "testLocale";

        // Act
        await _bettingService.ListCountries(marketFilter, locale);

        // Assert
        await _mockNetwork.Received().Request<List<CountryCodeResult>>(
            "https://api.betfair.com/exchange/betting/json-rpc/v1",
            "SportsAPING/v1.0/listCountries",
            Arg.Is<Dictionary<string, object?>>(args =>
                (MarketFilter?)args["filter"] == marketFilter &&
                (string?)args["locale"] == locale)
        );
    }


    [Fact]
    public async Task ListCurrentOrders_SendsCorrectRequest() {

        // Arrange
        var betIds = new List<string>() { "testbetid" };
        var marketIds = new List<string>() { "testmarketid" };
        var orderProjection = OrderProjectionEnum.EXECUTABLE;
        var placedDateRange = new TimeRange() { From = DateTime.Now, To = DateTime.Now.AddDays(1) };
        var dateRange = new TimeRange() { From = DateTime.Now, To = DateTime.Now.AddDays(1) };
        var orderBy = OrderByEnum.BY_PLACE_TIME;
        var sortDir = SortDirEnum.EARLIEST_TO_LATEST;
        var fromRecord = 1;
        var recordCount = 10;

        // Act 
        await _bettingService.ListCurrentOrders(
            betIds,
            marketIds,
            orderProjection,
            placedDateRange,
            dateRange,
            orderBy,
            sortDir,
            fromRecord,
            recordCount
        );

        // Assert
        await _mockNetwork.Received().Request<CurrentOrderSummaryReport>(
            "https://api.betfair.com/exchange/betting/json-rpc/v1",
            "SportsAPING/v1.0/listCurrentOrders",
            Arg.Is<Dictionary<string, object?>>(args =>
                (List<string>?)args["betIds"] == betIds &&
                (List<string>?)args["marketIds"] == marketIds &&
                (OrderProjectionEnum?)args["orderProjection"] == orderProjection &&
                (TimeRange?)args["placedDateRange"] == placedDateRange &&
                (TimeRange?)args["dateRange"] == dateRange &&
                (OrderByEnum?)args["orderBy"] == orderBy &&
                (SortDirEnum?)args["sortDir"] == sortDir &&
                (int?)args["fromRecord"] == fromRecord &&
                (int?)args["recordCount"] == recordCount
            )
        );
    }


    [Fact]
    public async Task ListEvents_SendsCorrectRequest() {

        // Arrange
        var marketFilter = new MarketFilter() {
            MarketCountries = new List<string>() { "testCountry" },
            EventTypeIds = new List<string>() { "testEventTypeId" },
            MarketTypeCodes = new List<string>() { "testMarketTypeCode" },
            MarketStartTime = new TimeRange() { From = DateTime.Now, To = DateTime.Now.AddDays(1) },
            InPlayOnly = true,
        };

        // Act
        await _bettingService.ListEvents(marketFilter);

        // Assert
        await _mockNetwork.Received().Request<List<EventResult>>(
            "https://api.betfair.com/exchange/betting/json-rpc/v1",
            "SportsAPING/v1.0/listEvents",
            Arg.Is<Dictionary<string, object?>>(args =>
                (MarketFilter?)args["filter"] == marketFilter
            )
        );
    }


    [Fact]
    public async Task ListEventTypes_SendsCorrectRequest() {
        // Arrange
        var marketFilter = new MarketFilter();
        var locale = "testLocale";

        // Act
        await _bettingService.ListEventTypes(marketFilter, locale);

        // Assert
        await _mockNetwork.Received().Request<List<EventTypeResult>>(
            "https://api.betfair.com/exchange/betting/json-rpc/v1",
            "SportsAPING/v1.0/listEventTypes",
            Arg.Is<Dictionary<string, object?>>(args =>
                (MarketFilter?)args["filter"] == marketFilter &&
                (string?)args["locale"] == locale)
        );
    }


    [Fact]
    public async Task ListMarketBook_SendsCorrectRequest() {

        // Arrange
        var marketIds = new List<string> { "marketId1", "marketId2" };
        var priceProjection = new PriceProjection();
        var orderProjection = OrderProjectionEnum.EXECUTABLE;
        var matchProjection = MatchProjectionEnum.ROLLED_UP_BY_PRICE;
        var includeOverallPosition = true;
        var partitionMatchedByStrategyRef = true;
        var customerStrategyRefs = new List<string> { "customerStrategyRef1", "customerStrategyRef2" };
        var currencyCode = "testCurrencyCode";
        var locale = "testLocale";
        var matchedSince = DateTime.Now;
        var betIds = new List<string> { "betId1", "betId2" };

        // Act
        await _bettingService.ListMarketBook(
            marketIds,
            priceProjection,
            orderProjection,
            matchProjection,
            includeOverallPosition,
            partitionMatchedByStrategyRef,
            customerStrategyRefs,
            currencyCode,
            locale,
            matchedSince,
            betIds
        );

        // Assert
        await _mockNetwork.Received().Request<List<MarketBook>>(
            "https://api.betfair.com/exchange/betting/json-rpc/v1",
            "SportsAPING/v1.0/listMarketBook",
            Arg.Is<Dictionary<string, object?>>(args =>
                (List<string>?)args["marketIds"] == marketIds &&
                (PriceProjection?)args["priceProjection"] == priceProjection &&
                (OrderProjectionEnum?)args["orderProjection"] == orderProjection &&
                (MatchProjectionEnum?)args["matchProjection"] == matchProjection &&
                (bool?)args["includeOverallPosition"] == includeOverallPosition &&
                (bool?)args["partitionMatchedByStrategyRef"] == partitionMatchedByStrategyRef &&
                (List<string>?)args["customerStrategyRefs"] == customerStrategyRefs &&
                (string?)args["currencyCode"] == currencyCode &&
                (string?)args["locale"] == locale &&
                (DateTime?)args["matchedSince"] == matchedSince &&
                (List<string>?)args["betIds"] == betIds
            )
        );
    }


    [Fact]
    public async Task ListMarketCatalogue_SendsCorrectRequest() {

        // Arrange
        var marketFilter = new MarketFilter();
        var marketProjection = new List<MarketProjectionEnum>() { MarketProjectionEnum.COMPETITION, MarketProjectionEnum.EVENT };
        var marketSort = MarketSortEnum.FIRST_TO_START;
        var maxResult = 1;
        var locale = "testLocale";

        // Act
        await _bettingService.ListMarketCatalogue(
            marketFilter,
            marketProjection,
            marketSort,
            maxResult,
            locale
        );

        // Assert
        await _mockNetwork.Received().Request<List<MarketCatalogue>>(
            "https://api.betfair.com/exchange/betting/json-rpc/v1",
            "SportsAPING/v1.0/listMarketCatalogue",
            Arg.Is<Dictionary<string, object?>>(args =>
                (MarketFilter?)args["filter"] == marketFilter &&
                (List<MarketProjectionEnum>?)args["marketProjection"] == marketProjection &&
                (MarketSortEnum?)args["sort"] == marketSort &&
                (int?)args["maxResults"] == maxResult &&
                (string?)args["locale"] == locale)
        );
    }

    [Fact]
    public async Task ListMarketProfitAndLoss_SendsCorrectRequest() {

        // Arrange
        var marketIds = new List<string> { "marketId1", "marketId2" };
        var includeSettledBets = true;
        var includeBspBets = true;
        var netOfCommission = true;

        // Act
        await _bettingService.ListMarketProfitAndLoss(
            marketIds,
            includeSettledBets,
            includeBspBets,
            netOfCommission
        );

        // Assert
        await _mockNetwork.Received().Request<List<MarketProfitAndLoss>>(
            "https://api.betfair.com/exchange/betting/json-rpc/v1",
            "SportsAPING/v1.0/listMarketProfitAndLoss",
            Arg.Is<Dictionary<string, object?>>(args =>
                ((List<string>?)args["marketIds"])!.SequenceEqual(marketIds) &&
                (bool?)args["includeSettledBets"] == includeSettledBets &&
                (bool?)args["includeBspBets"] == includeBspBets &&
                (bool?)args["netOfCommission"] == netOfCommission
            )
        );
    }


    [Fact]
    public async Task ListMarketTypes_SendsCorrectRequest() {

        // Arrange
        var marketFilter = new MarketFilter();
        var locale = "testLocale";

        // Act
        await _bettingService.ListMarketTypes(marketFilter, locale);

        // Assert
        await _mockNetwork.Received().Request<List<MarketTypeResult>>(
            "https://api.betfair.com/exchange/betting/json-rpc/v1",
            "SportsAPING/v1.0/listMarketTypes",
            Arg.Is<Dictionary<string, object?>>(args =>
                (MarketFilter?)args["filter"] == marketFilter &&
                (string?)args["locale"] == locale
            )
        );
    }


    [Fact]
    public async Task ListRunnerBook_SendsCorrectRequest() {

        // Arrange
        var marketId = "testMarketId";
        var selectionId = "1";
        var handicap = 2.0;
        var priceProjection = new PriceProjection();
        var orderProjection = OrderProjectionEnum.EXECUTABLE;
        var matchProjection = MatchProjectionEnum.ROLLED_UP_BY_PRICE;
        var includeOverallPosition = true;
        var partitionMatchedByStrategyRef = true;
        var customerStrategyRefs = new List<string> { "customerStrategyRef1", "customerStrategyRef2" };
        var currencyCode = "testCurrencyCode";
        var locale = "testLocale";
        var matchedSince = DateTime.Now;
        var betIds = new List<string> { "betId1", "betId2" };

        // Act
        await _bettingService.ListRunnerBook(
            marketId,
            selectionId,
            handicap,
            priceProjection,
            orderProjection,
            matchProjection,
            includeOverallPosition,
            partitionMatchedByStrategyRef,
            customerStrategyRefs,
            currencyCode,
            locale,
            matchedSince,
            betIds
        );

        // Assert
        await _mockNetwork.Received().Request<MarketBook>(
            "https://api.betfair.com/exchange/betting/json-rpc/v1",
            "SportsAPING/v1.0/listRunnerBook",
            Arg.Is<Dictionary<string, object?>>(args =>
                (string?)args["marketId"] == marketId &&
                (string?)args["selectionId"] == selectionId &&
                (double?)args["handicap"] == handicap &&
                (PriceProjection?)args["priceProjection"] == priceProjection &&
                (OrderProjectionEnum?)args["orderProjection"] == orderProjection &&
                (MatchProjectionEnum?)args["matchProjection"] == matchProjection &&
                (bool?)args["includeOverallPosition"] == includeOverallPosition &&
                (bool?)args["partitionMatchedByStrategyRef"] == partitionMatchedByStrategyRef &&
                (List<string>?)args["customerStrategyRefs"] == customerStrategyRefs &&
                (string?)args["currencyCode"] == currencyCode &&
                (string?)args["locale"] == locale &&
                (DateTime?)args["matchedSince"] == matchedSince &&
                (List<string>?)args["betIds"] == betIds
            )
        );
    }


    [Fact]
    public async Task ListTimeRanges_SendsCorrectRequest() {

        // Arrange
        var marketFilter = new MarketFilter();
        var granularity = TimeGranularityEnum.DAYS;

        // Act
        await _bettingService.ListTimeRanges(marketFilter, granularity);

        // Assert
        await _mockNetwork.Received().Request<List<TimeRangeResult>>(
            "https://api.betfair.com/exchange/betting/json-rpc/v1",
            "SportsAPING/v1.0/listTimeRanges",
            Arg.Is<Dictionary<string, object?>>(args =>
                (MarketFilter?)args["filter"] == marketFilter &&
                (TimeGranularityEnum?)args["granularity"] == granularity
            )
        );
    }


    [Fact]
    public async Task ListVenues_SendsCorrectRequest() {

        // Arrange
        var marketFilter = new MarketFilter();
        var locale = "testLocale";

        // Act
        await _bettingService.ListVenues(marketFilter, locale);

        // Assert
        await _mockNetwork.Received().Request<List<VenueResult>>(
            "https://api.betfair.com/exchange/betting/json-rpc/v1",
            "SportsAPING/v1.0/listVenues",
            Arg.Is<Dictionary<string, object?>>(args =>
                (MarketFilter?)args["filter"] == marketFilter &&
                (string?)args["locale"] == locale
            )
        );
    }


    [Fact]
    public async Task PlaceOrders_SendsCorrectRequest() {

        // Arrange
        var marketId = "testMarketId";
        var placeInstructions = new List<PlaceInstruction> {
            new PlaceInstruction() {
                OrderType = OrderTypeEnum.LIMIT,
                SelectionId = 12345,
                Side = SideEnum.BACK
            }
        };
        var customerRef = "testCustomerRef";
        var marketVersion = new MarketVersion() { Version = 12345 };
        var customerStrategyRef = "testStrategyRef";
        var asyncFlag = true;

        // Act
        await _bettingService.PlaceOrders(marketId, placeInstructions, customerRef, marketVersion, customerStrategyRef, asyncFlag);

        // Assert
        await _mockNetwork.Received().Request<PlaceExecutionReport>(
            "https://api.betfair.com/exchange/betting/json-rpc/v1",
            "SportsAPING/v1.0/placeOrders",
            Arg.Is<Dictionary<string, object?>>(args =>
                (string?)args["marketId"] == marketId &&
                (List<PlaceInstruction>?)args["instructions"] == placeInstructions &&
                (string?)args["customerRef"] == customerRef &&
                (MarketVersion?)args["marketVersion"] == marketVersion &&
                (string?)args["customerStrategyRef"] == customerStrategyRef &&
                (bool?)args["async"] == asyncFlag
            )
        );
    }


    [Fact]
    public async Task UpdateOrders_SendsCorrectRequest() {

        // Arrange
        var marketId = "testMarketId";
        var instructions = new List<UpdateInstruction> {
            new UpdateInstruction() {
                BetId = "testbetid",
                NewPersistenceType = PersistenceTypeEnum.PERSIST
            }
        };
        var customerRef = "testCustomerRef";

        // Act
        await _bettingService.UpdateOrders(marketId, instructions, customerRef);

        // Assert
        await _mockNetwork.Received().Request<UpdateExecutionReport>(
            "https://api.betfair.com/exchange/betting/json-rpc/v1",
            "SportsAPING/v1.0/updateOrders",
            Arg.Is<Dictionary<string, object?>>(args =>
                (string?)args["marketId"] == marketId &&
                (List<UpdateInstruction>?)args["instructions"] == instructions &&
                (string?)args["customerRef"] == customerRef
            )
        );
    }


    [Fact]
    public async Task ReplaceOrders_SendsCorrectRequest() {

        // Arrange
        var marketId = "testMarketId";
        var instructions = new List<ReplaceInstruction> {
            new ReplaceInstruction() { BetId = "testbetid", NewPrice = 1000 }
        };
        var customerRef = "testCustomerRef";
        var marketVersion = new MarketVersion() { Version = 12345 };
        var asyncFlag = true;

        // Act
        await _bettingService.ReplaceOrders(marketId, instructions, customerRef, marketVersion, asyncFlag);

        // Assert
        await _mockNetwork.Received().Request<ReplaceExecutionReport>(
            "https://api.betfair.com/exchange/betting/json-rpc/v1",
            "SportsAPING/v1.0/replaceOrders",
            Arg.Is<Dictionary<string, object?>>(args =>
                (string?)args["marketId"] == marketId &&
                (List<ReplaceInstruction>?)args["instructions"] == instructions &&
                (string?)args["customerRef"] == customerRef &&
                (MarketVersion?)args["marketVersion"] == marketVersion &&
                (bool?)args["async"] == asyncFlag
            )
        );
    }
}
