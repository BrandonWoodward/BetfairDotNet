using BetfairDotNet.Enums.Account;
using BetfairDotNet.Handlers;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models;
using BetfairDotNet.Models.Exceptions;
using BetfairDotNet.Models.Login;
using BetfairDotNet.Utils;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.Net;
using System.Security.Cryptography;
using System.Text.Json;
using Xunit;

namespace BetfairDotNet.Tests.HandlerTests;

/// <summary>
/// Unit tests for the <see cref="RequestResponseHandler"/> class.
/// </summary>
public class RequestResponseHandlerTests {

    private readonly IBetfairHttpClient _mockNetwork;
    private readonly IRequestResponseHandler _processor;

    public RequestResponseHandlerTests() {
        _mockNetwork = Substitute.For<IBetfairHttpClient>();
        _processor = new RequestResponseHandler(_mockNetwork);
    }


    [Fact]
    public async Task Authenticate_SetsHeader_OnSuccessfulLogin() {

        // Arrange
        var sampleUrl = "sampleUrl";
        var sampleCertPath = "sampleCertificatePath";
        var sampleCredentials = new Dictionary<string, string>();
        var sampleResponse = new CertificateLoginResponse { Status = LoginStatusEnum.SUCCESS, SessionToken = "sampleToken" };

        // Act
        _mockNetwork.PostUrlEncodedContent(Arg.Any<string>(), Arg.Any<FormUrlEncodedContent>())
            .Returns(JsonConvert.Serialize(sampleResponse));
        await _processor.Authenticate<CertificateLoginResponse>(sampleUrl, sampleCredentials, sampleCertPath);

        // Assert
        _mockNetwork.Received().AddDefaultRequestHeader("X-Authentication", "sampleToken");
    }


    [Fact]
    public async Task Authenticate_ThrowsBetfairServerException_OnLoginFailure() {
        // Arrange
        var sampleUrl = "sampleUrl";
        var sampleCertPath = "sampleCertificatePath";
        var sampleCredentials = new Dictionary<string, string>() {
            ["username"] = "sampleUsername",
            ["password"] = "samplePassword"
        };
        var failedResponse = new CertificateLoginResponse { Status = LoginStatusEnum.INVALID_USERNAME_OR_PASSWORD, SessionToken = "sampleToken" };

        _mockNetwork.PostUrlEncodedContent(Arg.Any<string>(), Arg.Any<FormUrlEncodedContent>())
            .Returns(JsonConvert.Serialize(failedResponse));

        // Act
        var exception = await Assert.ThrowsAsync<BetfairNGException>(()
            => _processor.Authenticate<CertificateLoginResponse>(sampleUrl, sampleCredentials, sampleCertPath));

        // Assert
        Assert.Equal("INVALID_USERNAME_OR_PASSWORD", exception.Message);
        Assert.Equal(sampleUrl, exception.Endpoint);
        Assert.Equal(sampleCredentials["username"], exception.RequestParameters?["username"]);
        Assert.Equal(sampleCredentials["password"], exception.RequestParameters?["password"]);
    }


    [Fact]
    public async Task Authenticate_ThrowsBetfairClientException_WhenNoSessionTokenReturned() {

        // Arrange
        var sampleUrl = "sampleUrl";
        var sampleCertPath = "sampleCertificatePath";
        var sampleCredentials = new Dictionary<string, string>() {
            ["username"] = "sampleUsername",
            ["password"] = "samplePassword"
        };

        var noTokenResponse = new CertificateLoginResponse { Status = LoginStatusEnum.SUCCESS, SessionToken = string.Empty };

        _mockNetwork.PostUrlEncodedContent(Arg.Any<string>(), Arg.Any<FormUrlEncodedContent>())
            .Returns(JsonConvert.Serialize(noTokenResponse));

        // Act
        var exception = await Assert.ThrowsAsync<BetfairNGException>(()
            => _processor.Authenticate<CertificateLoginResponse>(sampleUrl, sampleCredentials, sampleCertPath));

        // Assert
        Assert.Equal("NO_SESSION_TOKEN_RETURNED", exception.Message);
        Assert.Equal(sampleUrl, exception.Endpoint);
        Assert.Equal(sampleCredentials["username"], exception.RequestParameters?["username"]);
        Assert.Equal(sampleCredentials["password"], exception.RequestParameters?["password"]);
    }


    [Fact]
    public async Task Authenticate_ThrowsBetfairClientException_ForCryptographicErrors() {

        // Arrange
        var sampleUrl = "sampleUrl";
        var sampleCertPath = "sampleCertificatePath";
        var sampleCredentials = new Dictionary<string, string>() {
            ["username"] = "sampleUsername",
            ["password"] = "samplePassword"
        };

        _mockNetwork.When(x => x.AddClientCertifcate(Arg.Any<string>()))
            .Do(x => throw new CryptographicException());

        // Act
        var exception = await Assert.ThrowsAsync<BetfairNGException>(()
            => _processor.Authenticate<CertificateLoginResponse>(sampleUrl, sampleCredentials, sampleCertPath));

        // Assert
        Assert.Equal("CERTIFICATE_NOT_FOUND", exception.Message);
        Assert.Equal(sampleUrl, exception.Endpoint);
        Assert.NotNull(exception.InnerException);
        Assert.Equal(sampleCredentials["username"], exception.RequestParameters?["username"]);
        Assert.Equal(sampleCredentials["password"], exception.RequestParameters?["password"]);
    }


    [Fact]
    public async Task Authenticate_ThrowsBetfairClientException_ForNetworkErrors() {

        // Arrange
        var sampleUrl = "sampleUrl";
        var sampleCertPath = "sampleCertificatePath";
        var sampleCredentials = new Dictionary<string, string>() {
            ["username"] = "sampleUsername",
            ["password"] = "samplePassword"
        };

        _mockNetwork.PostUrlEncodedContent(Arg.Any<string>(), Arg.Any<FormUrlEncodedContent>())
            .Throws(new HttpRequestException("Network error", null, HttpStatusCode.InternalServerError));

        // Act
        var exception = await Assert.ThrowsAsync<BetfairNGException>(()
            => _processor.Authenticate<CertificateLoginResponse>(sampleUrl, sampleCredentials, sampleCertPath));

        // Assert
        Assert.Equal("NETWORK_ERROR (InternalServerError)", exception.Message);
        Assert.Equal(sampleUrl, exception.Endpoint);
        Assert.NotNull(exception.InnerException);
        Assert.Equal(sampleCredentials["username"], exception.RequestParameters?["username"]);
        Assert.Equal(sampleCredentials["password"], exception.RequestParameters?["password"]);
    }


    [Fact]
    public async Task Authenticate_ThrowsBetfairClientException_ForTimeout() {
        // Arrange
        var sampleUrl = "sampleUrl";
        var sampleCertPath = "sampleCertificatePath";
        var sampleCredentials = new Dictionary<string, string>() {
            ["username"] = "sampleUsername",
            ["password"] = "samplePassword"
        };

        // Create a TaskCanceledException where CancellationToken is not set to canceled, simulating a timeout
        var timeoutException = new TaskCanceledException("Request timeout", new Exception(), new CancellationToken(false));
        _mockNetwork.PostUrlEncodedContent(Arg.Any<string>(), Arg.Any<FormUrlEncodedContent>())
            .Throws(timeoutException);

        // Act
        var exception = await Assert.ThrowsAsync<BetfairNGException>(()
            => _processor.Authenticate<CertificateLoginResponse>(sampleUrl, sampleCredentials, sampleCertPath));

        // Assert
        Assert.Equal("NETWORK_ERROR (Timeout)", exception.Message);
        Assert.Equal(sampleUrl, exception.Endpoint);
        Assert.NotNull(exception.InnerException);
        Assert.Equal(sampleCredentials["username"], exception.RequestParameters?["username"]);
        Assert.Equal(sampleCredentials["password"], exception.RequestParameters?["password"]);
    }


    [Fact]
    public async Task Authenticate_ThrowsBetfairClientException_ForJsonErrors() {

        // Arrange
        var sampleUrl = "sampleUrl";
        var sampleCertPath = "sampleCertificatePath";
        var sampleCredentials = new Dictionary<string, string>() {
            ["username"] = "sampleUsername",
            ["password"] = "samplePassword"
        };

        _mockNetwork.PostUrlEncodedContent(Arg.Any<string>(), Arg.Any<FormUrlEncodedContent>())
            .Returns("{invalidJson");

        // Act
        var exception = await Assert.ThrowsAsync<BetfairNGException>(()
            => _processor.Authenticate<CertificateLoginResponse>(sampleUrl, sampleCredentials, sampleCertPath));

        // Assert
        Assert.Equal("JSON_SERIALIZATION_ERROR", exception.Message);
        Assert.Equal(sampleUrl, exception.Endpoint);
        Assert.NotNull(exception.InnerException);
        Assert.Equal(sampleCredentials["username"], exception.RequestParameters?["username"]);
        Assert.Equal(sampleCredentials["password"], exception.RequestParameters?["password"]);
    }


    [Fact]
    public async Task Request_ReturnsDeserializedResponse_OnSuccessfulRequest() {

        // Arrange
        var sampleUrl = "sampleUrl";
        var sampleMethod = "sampleMethod";
        var sampleData = new Dictionary<string, object?>() {
            ["key"] = "value"
        };

        var expectedResponse = new BetfairServerResponse<string> {
            Response = "SuccessfulResponse"
        };

        _mockNetwork.PostStringContent(Arg.Any<string>(), Arg.Any<string>())
            .Returns(JsonSerializer.Serialize(expectedResponse));

        // Act
        var actualResponse = await _processor.Request<string>(sampleUrl, sampleMethod, sampleData);

        // Assert
        Assert.Equal(expectedResponse.Response, actualResponse);
    }


    [Fact]
    public async Task Request_ThrowsBetfairClientException_ForNetworkErrors() {

        // Arrange
        var sampleUrl = "sampleUrl";
        var sampleMethod = "sampleMethod";
        var sampleData = new Dictionary<string, object?>() {
            ["key"] = "value"
        };

        _mockNetwork.PostStringContent(Arg.Any<string>(), Arg.Any<string>())
            .Throws(new HttpRequestException("Network error", null, HttpStatusCode.InternalServerError));

        // Act
        var exception = await Assert.ThrowsAsync<BetfairNGException>(()
            => _processor.Request<string>(sampleUrl, sampleMethod, sampleData));

        // Assert
        Assert.Equal("NETWORK_ERROR (InternalServerError)", exception.Message);
        Assert.Equal(sampleUrl + sampleMethod, exception.Endpoint);
        Assert.NotNull(exception.InnerException);
        Assert.Equal(sampleData["key"], exception.RequestParameters?["key"]);
    }


    [Fact]
    public async Task Request_ThrowsBetfairClientException_ForTimeout() {

        // Arrange
        var sampleUrl = "sampleUrl";
        var sampleMethod = "sampleMethod";
        var sampleData = new Dictionary<string, object?>() {
            ["key"] = "value"
        };

        var timeoutException = new TaskCanceledException("Request timeout", new Exception(), new CancellationToken(false));
        _mockNetwork.PostStringContent(Arg.Any<string>(), Arg.Any<string>())
            .Throws(timeoutException);

        // Act
        var exception = await Assert.ThrowsAsync<BetfairNGException>(()
            => _processor.Request<string>(sampleUrl, sampleMethod, sampleData));

        // Assert
        Assert.Equal("NETWORK_ERROR (Timeout)", exception.Message);
        Assert.Equal(sampleUrl + sampleMethod, exception.Endpoint);
        Assert.NotNull(exception.InnerException);
        Assert.Equal(sampleData["key"], exception.RequestParameters?["key"]);
    }


    [Fact]
    public async Task Request_ThrowsBetfairClientException_ForJsonSerializationErrors() {

        // Arrange
        var sampleUrl = "sampleUrl";
        var sampleMethod = "sampleMethod";
        var sampleData = new Dictionary<string, object?>() {
            ["key"] = "value"
        };

        _mockNetwork.PostStringContent(Arg.Any<string>(), Arg.Any<string>())
            .Throws(new JsonException("JSON serialization error"));

        // Act
        var exception = await Assert.ThrowsAsync<BetfairNGException>(()
            => _processor.Request<string>(sampleUrl, sampleMethod, sampleData));

        // Assert
        Assert.Equal("JSON_SERIALIZATION_ERROR", exception.Message);
        Assert.Equal(sampleUrl + sampleMethod, exception.Endpoint);
        Assert.NotNull(exception.InnerException);
        Assert.Equal(sampleData["key"], exception.RequestParameters?["key"]);
    }
}