using BetfairDotNet.Endpoints;
using BetfairDotNet.Interfaces;
using BetfairDotNet.Models.Login;
using CommunityToolkit.Diagnostics;

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
        Guard.IsNotNullOrWhiteSpace(username);
        Guard.IsNotNullOrWhiteSpace(password);
        Guard.IsNotNullOrWhiteSpace(certPath);

        var extension = Path.GetExtension(certPath).ToLowerInvariant();
        Guard.IsTrue(extension is ".p12" or ".pfx", "The certificate should be a .p12 or .pfx file.");
        
        var args = new Dictionary<string, string>
        {
            ["username"] = username,
            ["password"] = password,
        };

        return await _requestHandler.Request<CertificateLoginResponse>(
            LoginEndpoints.CertificateLogin, 
            args, 
            certPath
        );
    }


    /// <summary>
    /// Asynchronously attempts to authenticate a user using the provided credentials.
    /// This is the recommended approach if you are creating a login form.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<InteractiveLoginResponse> InteractiveLogin(string username, string password)
    {
        Guard.IsNotNullOrWhiteSpace(username);
        Guard.IsNotNullOrWhiteSpace(password);
        
        var args = new Dictionary<string, string>
        {
            ["username"] = username,
            ["password"] = password,
        };
        
        return await _requestHandler.Request<InteractiveLoginResponse>(
            LoginEndpoints.InteractiveLogin, 
            args
        );
    }
}
