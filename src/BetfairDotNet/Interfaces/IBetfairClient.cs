using BetfairDotNet.Services;

namespace BetfairDotNet.Interfaces;

public interface IBetfairClient {
    AccountService Account { get; }
    BettingService Betting { get; }
    HeartbeatService Heartbeat { get; }
}
