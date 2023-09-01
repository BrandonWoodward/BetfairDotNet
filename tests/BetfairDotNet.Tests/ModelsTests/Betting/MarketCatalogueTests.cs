using BetfairDotNet.Models.Betting;
using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Betting;

public class MarketCatalogueTests {


    [Fact]
    public void TestMarketCatalogueSerialization() {
        // Arrange
        var marketCatalogue = new MarketCatalogue {
            MarketId = "1.123456",
            MarketName = "Some Market",
            MarketStartTime = DateTime.Now,
            Description = new(),
            TotalMatched = 1000.0,
            Runners = new List<RunnerCatalog> {
                new() {
                    SelectionId = 12345,
                    RunnerName = "Runner 1",
                    Handicap = 0.0,
                    SortPriority = 1,
                    Metadata = new(),
                }
            },
            EventType = new(),
            Competition = new(),
            Event = new(),
        };

        // Act
        var json = JsonSerializer.Serialize(marketCatalogue);
        var deserializedMarketCatalogue = JsonSerializer.Deserialize<MarketCatalogue>(json);

        // Assert
        marketCatalogue.Should().BeEquivalentTo(deserializedMarketCatalogue);
    }
}
