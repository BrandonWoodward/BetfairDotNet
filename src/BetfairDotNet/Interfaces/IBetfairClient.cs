using BetfairDotNet.Services;

namespace BetfairDotNet.Interfaces;

/// <summary>
/// The top level client containing the services for the different Betfair APIs.
/// </summary>
public interface IBetfairClient 
{
    /// <summary>
    /// Login functionality for interactive and non-interactive workflows.
    /// </summary>
    LoginService Login { get; }
    
    /// <summary>
    /// Functionality for the Betfair Account API
    /// such as getting account details, funds, statements, etc.
    /// </summary>
    AccountService Account { get; }
    
    /// <summary>
    /// Functionality for retrieving navigation data for applications.
    /// </summary>
    NavigationService Navigation { get; }
    
    /// <summary>
    /// Functionality for the Betfair Betting API.
    /// Operations include listing markets, placing bets, cancelling bets, etc.
    /// </summary>
    BettingService Betting { get; }
    
    /// <summary>
    /// Functionality for the Betfair Heartbeat API
    /// which can perform an action when connection to the Betfair API is lost.
    /// </summary>
    HeartbeatService Heartbeat { get; }
    
    /// <summary>
    /// Provides a fluent interface for subscribing to the Betfair Streaming API
    /// using a reactive approach. Includes built in error handling.
    /// </summary>
    StreamingService Streaming { get; }
}
