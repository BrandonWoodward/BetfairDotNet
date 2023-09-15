using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Login;
using BetfairDotNet.Services;
using NSubstitute;
using Xunit;

namespace BetfairDotNet.Tests.ServiceTests;
public class LoginServiceTests
{

    private readonly IRequestResponseHandler _mockHandler;
    private readonly LoginService _sut;


    public LoginServiceTests()
    {
        _mockHandler = Substitute.For<IRequestResponseHandler>();
        _sut = new LoginService(_mockHandler);
    }


    [Fact]
    public async Task CertificateLogin_SendsCorrectRequest()
    {
        // Arrange
        var username = "testuser";
        var password = "testpass";
        var certPath = "test/path/to/cert.pfx";

        // Act 
        await _sut.CertificateLogin(username, password, certPath);

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
    public async Task CertificateLogin_ThrowsArgumentException_ForEmptyUsername()
    {
        // Arrange
        var username = string.Empty;
        var password = "testpass";
        var certPath = "test/path/to/cert.pfx";

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _sut.CertificateLogin(username, password, certPath));
    }


    [Fact]
    public async Task CertificateLogin_ThrowsArgumentException_ForEmptyPassword()
    {
        // Arrange
        var username = "testuser";
        var password = string.Empty;
        var certPath = "test/path/to/cert.pfx";

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _sut.CertificateLogin(username, password, certPath));
    }


    [Fact]
    public async Task CertificateLogin_ThrowsArgumentException_ForEmptyCertifcatePath()
    {
        // Arrange
        var username = "testuser";
        var password = "testpassword";
        var certPath = "";

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _sut.CertificateLogin(username, password, certPath));
    }


    [Fact]
    public async Task CertificateLogin_ThrowsArgumentException_ForInvalidCertFileExtension()
    {
        // Arrange
        var username = "testuser";
        var password = "testpass";
        var certPath = "test/path/to/cert.pem";

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _sut.CertificateLogin(username, password, certPath));
    }


    [Fact]
    public async Task InteractiveLogin_SendsCorrectRequest()
    {
        // Arrange
        var username = "testuser";
        var password = "testpass";

        // Act 
        await _sut.InteractiveLogin(username, password);

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
    public async Task InteractiveLogin_ThrowsArgumentException_ForEmptyUsername()
    {
        // Arrange
        var username = string.Empty;
        var password = "testpass";

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _sut.InteractiveLogin(username, password));
    }


    [Fact]
    public async Task InteractiveLogin_ThrowsArgumentException_ForEmptyPassword()
    {
        // Arrange
        var username = "testuser";
        var password = string.Empty;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _sut.InteractiveLogin(username, password));
    }
}
