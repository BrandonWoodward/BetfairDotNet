using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Navigation;
using BetfairDotNet.Services;
using NSubstitute;
using Xunit;

namespace BetfairDotNet.Tests.ServiceTests;

public class NavigationServiceTests
{
    private readonly IRequestResponseHandler _mockNetwork = Substitute.For<IRequestResponseHandler>();
    private readonly NavigationService _sut;

    public NavigationServiceTests() 
    {
        _sut = new(_mockNetwork);
    }
    
    [Fact]
    public async Task NavigationMenu_SendsCorrectRequest()
    {
        // Arrange
        // Act
        await _sut.NavigationMenu();

        // Assert
        await _mockNetwork.Received().Request<NavigationRoot>(
            "https://api.betfair.com/exchange/betting/rest/v1/en/navigation/menu.json"
        );
    }
}