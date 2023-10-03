# Login Guide

---

<br/>

BetfairDotNet includes support for both interactive and non-interactive login flows.
If you're building a login form, the interactive flow is the one you want. If
you're building an automated algorithm, use the non-interactive login flow
with a self-signed SSL certificate. 

<br/>

## 1. Interactive Login

<br/>

Interactive login is the simplest way to login to Betfair. It requires a username and password.

```csharp
// Enter your api key here
var client = new BetfairClient(apiKey)

try
{
    var session = await client.Login.InteractiveLogin(username, password);
}
catch (BetfairNGException ex)
{
    // Handle login error
}
```

<br/>

## 2. Non-Interactive Login

<br/>

Provide the **full path** to the certificate along with your username and password.

```csharp
// Enter your api key here
var client = new BetfairClient(apiKey)

try
{
    var session = await client.Login.CertificateLogin(username, password, certPath);
}
catch (BetfairNGException ex)
{
    // Handle login error
}
```

<br/>

>[!NOTE]
> If you have not set up your Betfair account with an SSL certificate, follow the instructions [here](https://docs.developer.betfair.com/display/1smk3cen4v3lu3yomq5qye0ni/Non-Interactive+%28bot%29+login)

<br/>

## 3. Error Handling

<br/>

If the login fails, a `BetfairNGException` will be thrown. For an exhaustive 
list of the possible error code, see [LoginStatusCodeEnum](../api/BetfairDotNet.Enums.Account.LoginStatusEnum.yml).