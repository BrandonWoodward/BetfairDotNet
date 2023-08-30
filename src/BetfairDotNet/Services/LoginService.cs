using BetfairDotNet.Endpoints;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Login;

namespace BetfairDotNet.Services;


/// <summary>
/// A service that provides functionalities for user authentication.
/// </summary>
public sealed class LoginService {


    private readonly IRequestResponseHandler _requestHandler;
    private readonly string _username;
    private readonly string _password;
    private readonly string? _certPath;


    internal LoginService(
        IRequestResponseHandler requestHandler,
        string username,
        string password,
        string? certPath = null) {

        _requestHandler = requestHandler;
        _username = username;
        _password = password;
        _certPath = certPath;
    }


    /// <summary>
    /// Asynchronously attempts to authenticate a user using the provided credentials.
    /// This is the recommended way to login since it is the most secure.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The result contains 
    /// the <see cref="CertificateLoginResponse"/> if the login was successful.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if any of the arguments are null, empty, or if the certificate is not in the expected format.
    /// </exception>
    public async Task<CertificateLoginResponse> CertificateLogin() {
        if(string.IsNullOrWhiteSpace(_username)) {
            throw new ArgumentException("Username not provided.");
        }
        if(string.IsNullOrWhiteSpace(_password)) {
            throw new ArgumentException("Password not provided.");
        }
        if(string.IsNullOrWhiteSpace(_certPath)) {
            throw new ArgumentException("Certificate path not provided");
        }
        var ext = Path.GetExtension(_certPath).ToLowerInvariant();
        if(ext is not ".p12" and not ".pfx") {
            throw new ArgumentException("The certificate should be a .p12 or .pfx file.");
        }
        var args = new Dictionary<string, string>() {
            ["username"] = _username,
            ["password"] = _password,
        };
        // Sets the X-Authentication header to the ssoid returned by Betfair if successful.
        return await _requestHandler.Authenticate<CertificateLoginResponse>(LoginEndpoints.CertificateLogin, args, _certPath);
    }


    /// <summary>
    /// Asynchronously attempts to authenticate a user using the provided credentials.
    /// This is the recommended approach if you are creating a login form.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<InteractiveLoginResponse> InteractiveLogin() {
        if(string.IsNullOrWhiteSpace(_username)) {
            throw new ArgumentException("Username not provided.");
        }
        if(string.IsNullOrWhiteSpace(_password)) {
            throw new ArgumentException("Password not provided.");
        }
        var args = new Dictionary<string, string>() {
            ["username"] = _username,
            ["password"] = _password,
        };
        // Sets the X-Authentication header to the ssoid returned by Betfair if successful.
        return await _requestHandler.Authenticate<InteractiveLoginResponse>(LoginEndpoints.InteractiveLogin, args);
    }
}
