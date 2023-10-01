using BetfairDotNet.Endpoints;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Navigation;

namespace BetfairDotNet.Services;

/// <summary>
/// A service that provides functionality for replicating the Betfair navigation menu.
/// </summary>
public sealed class NavigationService
{
    
    private readonly IRequestResponseHandler _requestResponseHandler;
    
    internal NavigationService(IRequestResponseHandler requestResponseHandler)
    {
        _requestResponseHandler = requestResponseHandler;
    }
    
    /// <summary>
    /// This Navigation Data for Applications service allows the retrieval of the full Betfair market navigation menu
    /// from a compressed file. 
    /// </summary>
    /// <remarks>
    /// The file data is cached and new request for the file once an hour should be suitable
    /// for those looking to accurately recreate the Betfair navigation menu.
    /// </remarks>
    public Task<NavigationRoot> NavigationMenu()
    {
        return _requestResponseHandler.Request<NavigationRoot>(
            NavigationEndpoints.Navigation
        );
    }
}