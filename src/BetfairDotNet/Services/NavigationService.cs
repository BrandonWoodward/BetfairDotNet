using BetfairDotNet.Endpoints;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Navigation;

namespace BetfairDotNet.Services;

public sealed class NavigationService
{
    
    private readonly IRequestResponseHandler _requestResponseHandler;
    
    internal NavigationService(IRequestResponseHandler requestResponseHandler)
    {
        _requestResponseHandler = requestResponseHandler;
    }

    public Task<NavigationRoot> NavigationMenu()
    {
        return _requestResponseHandler.Request<NavigationRoot>(
            NavigationEndpoints.Navigation
        );
    }
}