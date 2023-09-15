using BetfairDotNet.Endpoints;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Login;

namespace BetfairDotNet.Services;


/// <summary>
/// A service that provides functionalities for user authentication.
/// </summary>
public sealed class LoginService
{

    private readonly IRequestResponseHandler _requestHandler;

    internal LoginService(IRequestResponseHandler requestHandler)
    {
        _requestHandler = requestHandler;
    }


    /// <summary>
    /// Asynchronously attempts to authenticate a user using the provided credentials.
    /// This is the recommended way to login for automated bots.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The result contains 
    /// the <see cref="CertificateLoginResponse"/> if the login was successful.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Thrown if any of the arguments are null, empty, or if the certificate is not in the expected format.
    /// </exception>
    public async Task<CertificateLoginResponse> CertificateLogin(string username, string password, string certPath)
    {
        if(string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Username not provided.");
        }
        if(string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Password not provided.");
        }
        if(string.IsNullOrWhiteSpace(certPath))
        {
            throw new ArgumentException("Certificate path not provided");
        }
        if(Path.GetExtension(certPath).ToLowerInvariant() is not ".p12" and not ".pfx")
        {
            throw new ArgumentException("The certificate should be a .p12 or .pfx file.");
        }
        var args = new Dictionary<string, string>()
        {
            ["username"] = username,
            ["password"] = password,
        };
        // Sets the X-Authentication header to the ssoid returned by Betfair if successful.
        return await _requestHandler.Authenticate<CertificateLoginResponse>(LoginEndpoints.CertificateLogin, args, certPath);
    }


    /// <summary>
    /// Asynchronously attempts to authenticate a user using the provided credentials.
    /// This is the recommended approach if you are creating a login form.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<InteractiveLoginResponse> InteractiveLogin(string username, string password)
    {
        if(string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentException("Username not provided.");
        }
        if(string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Password not provided.");
        }
        var args = new Dictionary<string, string>()
        {
            ["username"] = username,
            ["password"] = password,
        };
        // Sets the X-Authentication header to the ssoid returned by Betfair if successful.
        return await _requestHandler.Authenticate<InteractiveLoginResponse>(LoginEndpoints.InteractiveLogin, args);
    }
}
