using BetfairDotNet.Converters;
using BetfairDotNet.Enums.Account;
using BetfairDotNet.Handlers;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models;
using BetfairDotNet.Models.Exceptions;
using BetfairDotNet.Models.Login;
using FluentAssertions;
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

    private readonly IHttpClient _mockNetwork;
    private readonly RequestResponseHandler _sut;


    public RequestResponseHandlerTests() {
        _mockNetwork = Substitute.For<IHttpClient>();
        _sut = new(_mockNetwork);
    }


    [Fact]
    public async Task Authenticate_SetsDefaultRequestHeader_WhenSuccessfulLogin() {
        // Arrange
        var response = new CertificateLoginResponse { Status = LoginStatusEnum.SUCCESS, SessionToken = "token" };
        _mockNetwork
            .PostUrlEncodedContent(Arg.Any<string>(), Arg.Any<FormUrlEncodedContent>())
            .Returns(JsonConvert.Serialize(response));

        // Act
        await _sut.Authenticate<CertificateLoginResponse>("user", new Dictionary<string, string>(), "cert");

        // Assert
        _mockNetwork.Received().AddDefaultRequestHeader("X-Authentication", "token");
    }


    [Fact]
    public async Task Authenticate_ThrowsBetfairServerException_WhenLoginFailure() {
        // Arrange
        var sampleUrl = "sampleUrl";
        var sampleCertPath = "sampleCertificatePath";
        var sampleCredentials = new Dictionary<string, string>() {
            ["username"] = "username",
            ["password"] = "password"
        };
        var failedResponse = new CertificateLoginResponse { Status = LoginStatusEnum.INVALID_USERNAME_OR_PASSWORD, SessionToken = "token" };
        _mockNetwork
            .PostUrlEncodedContent(Arg.Any<string>(), Arg.Any<FormUrlEncodedContent>())
            .Returns(JsonConvert.Serialize(failedResponse));

        // Act
        Func<Task> act = async () => await _sut.Authenticate<CertificateLoginResponse>(sampleUrl, sampleCredentials, sampleCertPath);

        // Assert
        await act.Should().ThrowAsync<BetfairNGException>();
    }


    [Fact]
    public async Task Authenticate_ThrowsBetfairNGException_WhenNoSessionTokenReturned() {
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
        Func<Task> act = async () => await _sut.Authenticate<CertificateLoginResponse>(sampleUrl, sampleCredentials, sampleCertPath);

        // Assert
        await act.Should().ThrowAsync<BetfairNGException>();
    }


    [Fact]
    public async Task Authenticate_ThrowsBetfairNGException_ForCryptographicErrors() {
        // Arrange
        var sampleUrl = "sampleUrl";
        var sampleCertPath = "sampleCertificatePath";
        var sampleCredentials = new Dictionary<string, string>() {
            ["username"] = "sampleUsername",
            ["password"] = "samplePassword"
        };

        _mockNetwork
            .When(x => x.AddClientCertifcate(Arg.Any<string>()))
            .Do(x => throw new CryptographicException());

        // Act
        var act = async () => await _sut.Authenticate<CertificateLoginResponse>(sampleUrl, sampleCredentials, sampleCertPath);

        // Assert
        await act.Should().ThrowAsync<BetfairNGException>();
    }


    [Fact]
    public async Task Authenticate_ThrowsBetfairNGException_ForNetworkErrors() {
        // Arrange
        var sampleUrl = "sampleUrl";
        var sampleCertPath = "sampleCertificatePath";
        var sampleCredentials = new Dictionary<string, string>() {
            ["username"] = "sampleUsername",
            ["password"] = "samplePassword"
        };

        _mockNetwork
            .PostUrlEncodedContent(Arg.Any<string>(), Arg.Any<FormUrlEncodedContent>())
            .Throws(new HttpRequestException("Network error", null, HttpStatusCode.InternalServerError));

        // Act
        var act = async () => await _sut.Authenticate<CertificateLoginResponse>(sampleUrl, sampleCredentials, sampleCertPath);

        // Assert
        await act.Should().ThrowAsync<BetfairNGException>();
    }


    [Fact]
    public async Task Authenticate_ThrowsBetfairNGException_WhenTimeoutOccurs() {
        // Arrange
        var sampleUrl = "sampleUrl";
        var sampleCertPath = "sampleCertificatePath";
        var sampleCredentials = new Dictionary<string, string>() {
            ["username"] = "sampleUsername",
            ["password"] = "samplePassword"
        };
        var timeoutException = new TaskCanceledException("Request timeout", new Exception(), new CancellationToken(false));
        _mockNetwork
            .PostUrlEncodedContent(Arg.Any<string>(), Arg.Any<FormUrlEncodedContent>())
            .Throws(timeoutException);

        // Act
        var act = async () => await _sut.Authenticate<CertificateLoginResponse>(sampleUrl, sampleCredentials, sampleCertPath);


        // Assert
        await act.Should().ThrowAsync<BetfairNGException>();
    }


    [Fact]
    public async Task Authenticate_ThrowsBetfairNGException_WhenJsonSerializationErrorOccurs() {
        // Arrange
        var sampleUrl = "sampleUrl";
        var sampleCertPath = "sampleCertificatePath";
        var sampleCredentials = new Dictionary<string, string>() {
            ["username"] = "sampleUsername",
            ["password"] = "samplePassword"
        };

        _mockNetwork
            .PostUrlEncodedContent(Arg.Any<string>(), Arg.Any<FormUrlEncodedContent>())
            .Returns("{invalidJson");

        // Act
        var act = async () => await _sut.Authenticate<CertificateLoginResponse>(sampleUrl, sampleCredentials, sampleCertPath);

        // Assert
        await act.Should().ThrowAsync<BetfairNGException>();
    }


    [Fact]
    public async Task Request_ReturnsDeserializedResponse_WhenRequestSuccessful() {
        // Arrange
        var sampleUrl = "sampleUrl";
        var sampleMethod = "sampleMethod";
        var sampleData = new Dictionary<string, object?>() {
            ["key"] = "value"
        };

        var expectedResponse = new BetfairServerResponse<string> {
            Response = "SuccessfulResponse"
        };

        _mockNetwork
            .PostStringContent(Arg.Any<string>(), Arg.Any<string>())
            .Returns(JsonSerializer.Serialize(expectedResponse));

        // Act
        var actualResponse = await _sut.Request<string>(sampleUrl, sampleMethod, sampleData);

        // Assert
        actualResponse.Should().BeEquivalentTo(expectedResponse.Response);
    }


    [Fact]
    public async Task Request_ThrowsBetfairNGException_WhenNetworkErrorOccurs() {
        // Arrange
        var sampleUrl = "sampleUrl";
        var sampleMethod = "sampleMethod";
        var sampleData = new Dictionary<string, object?>() {
            ["key"] = "value"
        };

        _mockNetwork
            .PostStringContent(Arg.Any<string>(), Arg.Any<string>())
            .Throws(new HttpRequestException("Network error", null, HttpStatusCode.InternalServerError));

        // Act
        var act = async () => await _sut.Request<string>(sampleUrl, sampleMethod, sampleData);

        // Assert
        await act.Should().ThrowAsync<BetfairNGException>();
    }


    [Fact]
    public async Task Request_ThrowsBetfairNGException_WhenTimeoutOccurs() {
        // Arrange
        var sampleUrl = "sampleUrl";
        var sampleMethod = "sampleMethod";
        var sampleData = new Dictionary<string, object?>() {
            ["key"] = "value"
        };

        var timeoutException = new TaskCanceledException("Request timeout", new Exception(), new CancellationToken(false));
        _mockNetwork.
            PostStringContent(Arg.Any<string>(), Arg.Any<string>())
            .Throws(timeoutException);

        // Act
        var act = async () => await _sut.Request<string>(sampleUrl, sampleMethod, sampleData);

        // Assert
        await act.Should().ThrowAsync<BetfairNGException>();
    }


    [Fact]
    public async Task Request_ThrowsBetfairNGException_WhenJsonSerializationErrorOccurs() {
        // Arrange
        var sampleUrl = "sampleUrl";
        var sampleMethod = "sampleMethod";
        var sampleData = new Dictionary<string, object?>() {
            ["key"] = "value"
        };

        _mockNetwork
            .PostStringContent(Arg.Any<string>(), Arg.Any<string>())
            .Throws(new JsonException("JSON serialization error"));

        // Act
        var act = async () => await _sut.Request<string>(sampleUrl, sampleMethod, sampleData);

        // Assert
        await act.Should().ThrowAsync<BetfairNGException>();
    }
}