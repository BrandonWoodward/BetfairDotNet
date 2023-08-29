using BetfairDotNet.Interfaces;
using BetfairDotNet.Services;
using NSubstitute;
using Xunit;

namespace BetfairDotNet.Tests.ServiceTests;
public class LoginServiceTests {

    private readonly IRequestResponseHandler _mockHandler;


    public LoginServiceTests() {
        _mockHandler = Substitute.For<IRequestResponseHandler>();
    }


    [Fact]
    public async Task Login_SendsCorrectRequest() {
        // Arrange
        var username = "testuser";
        var password = "testpass";
        var certPath = "test/path/to/cert.pfx";
        var sut = new LoginService(_mockHandler, username, password, certPath);

        // Act 
        await sut.CertificateLogin();

        // Assert
        await _mockHandler.Received().Authenticate(
            "https://identitysso-cert.betfair.com/api/certlogin",
            certPath,
            Arg.Is<Dictionary<string, string>>(args =>
                args["username"] == username &&
                args["password"] == password
            )
        );
    }


    [Fact]
    public async Task Login_ThrowsArgumentException_ForEmptyUsername() {
        // Arrange
        var username = string.Empty;
        var password = "testpass";
        var certPath = "test/path/to/cert.pfx";
        var sut = new LoginService(_mockHandler, username, password, certPath);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(sut.CertificateLogin);
    }


    [Fact]
    public async Task Login_ThrowsArgumentException_ForEmptyPassword() {
        // Arrange
        var username = "testuser";
        var password = string.Empty;
        var certPath = "test/path/to/cert.pfx";
        var sut = new LoginService(_mockHandler, username, password, certPath);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(sut.CertificateLogin);
    }


    [Fact]
    public async Task Login_ThrowsArgumentException_ForInvalidCertFileExtension() {
        // Arrange
        var username = "testuser";
        var password = "testpass";
        var certPath = "test/path/to/cert.pem";
        var sut = new LoginService(_mockHandler, username, password, certPath);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(sut.CertificateLogin);
    }
}
