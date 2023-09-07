using BetfairDotNet.Converters;
using BetfairDotNet.Enums.Account;
using BetfairDotNet.Models.Heartbeat;
using FluentAssertions;
using Xunit;

namespace BetfairDotNet.Tests.ModelsTests.Heartbeat;

public class KeepAliveResponseTests {

    [Fact]
    public void KeepAliveResponse_ShouldSerializeProperly() {
        // Arrange
        var keepAliveResponse = new KeepAliveResponse() { Status = LoginStatusEnum.SUCCESS };

        // Act
        var json = JsonConvert.Serialize(keepAliveResponse);
        var deserialized = JsonConvert.Deserialize<KeepAliveResponse>(json);

        // Assert
        deserialized.Should().BeEquivalentTo(keepAliveResponse);
    }
}
