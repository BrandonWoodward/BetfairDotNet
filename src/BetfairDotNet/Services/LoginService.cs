using BetfairDotNet.Endpoints;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Account;

namespace BetfairDotNet.Services;


/// <summary>
/// A service that provides functionalities for user authentication.
/// </summary>
public sealed class LoginService {


    private readonly IRequestResponseHandler _requestHandler;
    private readonly string _username;
    private readonly string _password;
    private readonly string _certPath;


    internal LoginService(
        IRequestResponseHandler requestHandler,
        string username,
        string password,
        string certPath) {

        _requestHandler = requestHandler;
        _username = username;
        _password = password;
        _certPath = certPath;
    }


    /// <summary>
    /// Asynchronously attempts to authenticate a user using the provided credentials.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The result contains 
    /// the <see cref="LoginResponse"/> if the login was successful.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if any of the arguments are null, empty, or if the certificate is not in the expected format.
    /// </exception>
    public async Task<LoginResponse> CertificateLogin() {

        // Arguments cannot be null or empty. Might as well fail early and throw here
        if(string.IsNullOrWhiteSpace(_username)) {
            throw new ArgumentException("Username not provided.");
        }
        if(string.IsNullOrWhiteSpace(_password)) {
            throw new ArgumentException("Password not provided.");
        }
        if(string.IsNullOrWhiteSpace(_certPath)) {
            throw new ArgumentException("Certificate path not provided");
        }

        // File must be .p12 or .pfx
        var ext = Path.GetExtension(_certPath).ToLowerInvariant();
        if(ext is not ".p12" and not ".pfx") {
            throw new ArgumentException("The certificate should be a .p12 or .pfx file.");
        }

        // Payload to send in the request. This will be url encoded
        var args = new Dictionary<string, string>() {
            ["username"] = _username,
            ["password"] = _password,
        };

        // Sets the X-Authentication header to the ssoid returned by Betfair if successful.
        return await _requestHandler.Authenticate(LoginEndpoints.CertificateLogin, _certPath, args);
    }
}
