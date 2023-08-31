using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Login;
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
    public async Task CertificateLogin_SendsCorrectRequest() {
        // Arrange
        var username = "testuser";
        var password = "testpass";
        var certPath = "test/path/to/cert.pfx";
        var sut = new LoginService(_mockHandler, username, password, certPath);

        // Act 
        await sut.CertificateLogin();

        // Assert
        await _mockHandler.Received().Authenticate<CertificateLoginResponse>(
            "https://identitysso-cert.betfair.com/api/certlogin",
            Arg.Is<Dictionary<string, string>>(args =>
                args["username"] == username &&
                args["password"] == password
            ),
            certPath
        );
    }


    [Fact]
    public async Task CertificateLogin_ThrowsArgumentException_ForEmptyUsername() {
        // Arrange
        var username = string.Empty;
        var password = "testpass";
        var certPath = "test/path/to/cert.pfx";
        var sut = new LoginService(_mockHandler, username, password, certPath);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(sut.CertificateLogin);
    }


    [Fact]
    public async Task CertificateLogin_ThrowsArgumentException_ForEmptyPassword() {
        // Arrange
        var username = "testuser";
        var password = string.Empty;
        var certPath = "test/path/to/cert.pfx";
        var sut = new LoginService(_mockHandler, username, password, certPath);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(sut.CertificateLogin);
    }


    [Fact]
    public async Task CertificateLogin_ThrowsArgumentException_ForEmptyCertifcatePath() {
        // Arrange
        var username = "testuser";
        var password = "testpassword";
        var certPath = "";
        var sut = new LoginService(_mockHandler, username, password, certPath);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(sut.CertificateLogin);
    }


    [Fact]
    public async Task CertificateLogin_ThrowsArgumentException_ForInvalidCertFileExtension() {
        // Arrange
        var username = "testuser";
        var password = "testpass";
        var certPath = "test/path/to/cert.pem";
        var sut = new LoginService(_mockHandler, username, password, certPath);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(sut.CertificateLogin);
    }


    [Fact]
    public async Task InteractiveLogin_SendsCorrectRequest() {
        // Arrange
        var username = "testuser";
        var password = "testpass";
        var sut = new LoginService(_mockHandler, username, password);

        // Act 
        await sut.InteractiveLogin();

        // Assert
        await _mockHandler.Received().Authenticate<InteractiveLoginResponse>(
            "https://identitysso.betfair.com/api/login",
            Arg.Is<Dictionary<string, string>>(args =>
                args["username"] == username &&
                args["password"] == password
            )
        );
    }


    [Fact]
    public async Task InteractiveLogin_ThrowsArgumentException_ForEmptyUsername() {
        // Arrange
        var username = string.Empty;
        var password = "testpass";
        var sut = new LoginService(_mockHandler, username, password);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(sut.InteractiveLogin);
    }


    [Fact]
    public async Task InteractiveLogin_ThrowsArgumentException_ForEmptyPassword() {
        // Arrange
        var username = "testuser";
        var password = string.Empty;
        var sut = new LoginService(_mockHandler, username, password);

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(sut.InteractiveLogin);
    }
}
