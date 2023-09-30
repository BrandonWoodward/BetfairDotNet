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

public class RequestResponseHandlerTests 
{
    private readonly IHttpClient _mockNetwork;
    private readonly RequestResponseHandler _sut;

    public RequestResponseHandlerTests() {
        _mockNetwork = Substitute.For<IHttpClient>();
        _sut = new(_mockNetwork);
    }

    [Fact]
    public async Task Request_SetsDefaultRequestHeader_WhenSuccessfulLogin() 
    {
        // Arrange
        var response = new InteractiveLoginResponse { Status = LoginStatusEnum.SUCCESS, SessionToken = "token" };
        _mockNetwork
            .Post(Arg.Any<string>(), Arg.Any<FormUrlEncodedContent>())
            .Returns(JsonConvert.Serialize(response));

        // Act
        await _sut.Request<InteractiveLoginResponse>("user", new());

        // Assert
        _mockNetwork.Received().AddDefaultRequestHeader("X-Authentication", "token");
    }

    [Fact]
    public async Task Request_ThrowsBetfairServerException_WhenLoginFailure() 
    {
        // Arrange
        var sampleUrl = "sampleUrl";
        var sampleCertPath = "sampleCertificatePath";
        var sampleCredentials = new Dictionary<string, string> {
            ["username"] = "username",
            ["password"] = "password"
        };
        var failedResponse = new CertificateLoginResponse { Status = LoginStatusEnum.INVALID_USERNAME_OR_PASSWORD, SessionToken = "token" };
        _mockNetwork
            .Post(Arg.Any<string>(), Arg.Any<FormUrlEncodedContent>())
            .Returns(JsonConvert.Serialize(failedResponse));

        // Act
        Func<Task> act = async () => await _sut.Request<CertificateLoginResponse>(sampleUrl, sampleCredentials, sampleCertPath);

        // Assert
        await act.Should().ThrowAsync<BetfairNGException>();
    }

    [Fact]
    public async Task Request_ThrowsBetfairNGException_WhenCryptographicExceptionIsThrown() {
        // Arrange
        var sampleUrl = "sampleUrl";
        var sampleCertPath = "sampleCertificatePath";
        var sampleCredentials = new Dictionary<string, string> {
            ["username"] = "sampleUsername",
            ["password"] = "samplePassword"
        };

        _mockNetwork
            .When(x => x.AddClientCertificate(Arg.Any<string>()))
            .Do(_ => throw new CryptographicException());

        // Act
        var act = async () => await _sut.Request<CertificateLoginResponse>(sampleUrl, sampleCredentials, sampleCertPath);

        // Assert
        await act.Should().ThrowAsync<BetfairNGException>();
    }

    [Fact]
    public async Task Request_ThrowsBetfairNGException_WhenHttpRequestExceptionIsThrown() 
    {
        // Arrange
        var sampleUrl = "sampleUrl";
        var sampleCertPath = "sampleCertificatePath";
        var sampleCredentials = new Dictionary<string, string> {
            ["username"] = "sampleUsername",
            ["password"] = "samplePassword"
        };

        _mockNetwork
            .Post(Arg.Any<string>(), Arg.Any<FormUrlEncodedContent>())
            .Throws(new HttpRequestException("Network error", null, HttpStatusCode.InternalServerError));

        // Act
        var act = async () => await _sut.Request<CertificateLoginResponse>(sampleUrl, sampleCredentials, sampleCertPath);

        // Assert
        await act.Should().ThrowAsync<BetfairNGException>();
    }

    [Fact]
    public async Task Request_ReturnsDeserializedResponse_WhenRequestSuccessful() 
    {
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
            .Post(Arg.Any<string>(), Arg.Any<string>())
            .Returns(JsonSerializer.Serialize(expectedResponse));

        // Act
        var actualResponse = await _sut.Request<string>(sampleUrl, sampleMethod, sampleData);

        // Assert
        actualResponse.Should().BeEquivalentTo(expectedResponse.Response);
    }


    [Fact]
    public async Task Request_ThrowsBetfairNGException_WhenTaskCanceledExceptionIsThrown() {
        // Arrange
        var sampleUrl = "sampleUrl";
        var timeoutException = new TaskCanceledException("Request timeout", new(), new(false));
        _mockNetwork.Get(Arg.Any<string>()).Throws(timeoutException);

        // Act
        var act = async () => await _sut.Request<string>(sampleUrl);

        // Assert
        await act.Should().ThrowAsync<BetfairNGException>();
    }
}