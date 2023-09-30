using BetfairDotNet.Converters;
using System.Text.Json.Serialization;

namespace BetfairDotNet.Enums.Account;

[JsonConverter(typeof(EmptyStringToEnumConverter<LoginStatusEnum>))]
public enum LoginStatusEnum {

    /// <summary>
    /// The login was successful.
    /// </summary>
    SUCCESS,

    /// <summary>
    /// The username or password are invalid.
    /// </summary>
    INVALID_USERNAME_OR_PASSWORD,

    /// <summary>
    /// The account was just locked.
    /// </summary>
    ACCOUNT_NOW_LOCKED,

    /// <summary>
    /// The account is already locked.
    /// </summary>
    ACCOUNT_ALREADY_LOCKED,

    /// <summary>
    /// Pending authentication.
    /// </summary>
    PENDING_AUTH,

    /// <summary>
    /// Telbet terms and conditions rejected.
    /// </summary>
    TELBET_TERMS_CONDITIONS_NA,

    /// <summary>
    /// Duplicate cards.
    /// </summary>
    DUPLICATE_CARDS,

    /// <summary>
    /// The user has entered wrong the security answer 3 times.
    /// </summary>
    SECURITY_QUESTION_WRONG_3X,

    /// <summary>
    /// KYC suspended.
    /// </summary>
    KYC_SUSPEND,

    /// <summary>
    /// The account is suspended
    /// </summary>
    SUSPENDED,

    /// <summary>
    /// The account is closed.
    /// </summary>
    CLOSED,

    /// <summary>
    /// The account is self-excluded.
    /// </summary>
    SELF_EXCLUDED,

    /// <summary>
    /// the DK regulator cannot be accessed due to some internal problems 
    /// in the system behind or in at regulator; timeout cases included.
    /// </summary>
    INVALID_CONNECTIVITY_TO_REGULATOR_DK,

    /// <summary>
    /// The user identified by the given credentials is not authorized in the DK's 
    /// jurisdictions due to the regulators' policies. Ex: the user for which 
    /// this session should be created is not allowed to act(play, bet) in the DK's jurisdiction.
    /// </summary>
    NOT_AUTHORISED_BY_REGULATOR_DK,

    /// <summary>
    /// The IT regulator cannot be accessed due to some internal problems in the system 
    /// behind or in at regulator; timeout cases included.
    /// </summary>
    INVALID_CONNECTIVITY_TO_REGULATOR_IT,

    /// <summary>
    /// The user identified by the given credentials is not authorized in the IT's 
    /// jurisdictions due to the regulators' policies. Ex: the user for which this 
    /// session should be created is not allowed to act(play, bet) in the IT's jurisdiction.
    /// </summary>
    NOT_AUTHORISED_BY_REGULATOR_IT,

    /// <summary>
    /// The account is restricted due to security concerns.
    /// </summary>
    SECURITY_RESTRICTED_LOCATION,

    /// <summary>
    /// The account is accessed from a location where betting is restricted.
    /// </summary>
    BETTING_RESTRICTED_LOCATION,

    /// <summary>
    /// Trading Master Account.
    /// </summary>
    TRADING_MASTER,

    /// <summary>
    /// Suspended Trading Master Account.
    /// </summary>
    TRADING_MASTER_SUSPENDED,

    /// <summary>
    /// Agent Client Master.
    /// </summary>
    AGENT_CLIENT_MASTER,

    /// <summary>
    /// Suspended Agent Client Master.
    /// </summary>
    AGENT_CLIENT_MASTER_SUSPENDED,

    /// <summary>
    /// Danish authorization required.
    /// </summary>
    DANISH_AUTHORIZATION_REQUIRED,

    /// <summary>
    /// Spain migration required.
    /// </summary>
    SPAIN_MIGRATION_REQUIRED,

    /// <summary>
    /// Denmark migration required.
    /// </summary>
    DENMARK_MIGRATION_REQUIRED,

    /// <summary>
    /// The latest Spanish terms and conditions version must be accepted. 
    /// You must login to the website to accept the new conditions.
    /// </summary>
    SPANISH_TERMS_ACCEPTANCE_REQUIRED,

    /// <summary>
    /// The latest Italian contract version must be accepted. 
    /// You must login to the website to accept the new conditions.
    /// </summary>
    ITALIAN_CONTRACT_ACCEPTANCE_REQUIRED,

    /// <summary>
    /// Certificate required or certificate present but could not authenticate with it.  
    /// Please check that the correct file path is specified and ensure you are entering the correct password. 
    /// You should ensure that your username and password values are encoded before being sent to the API; 
    /// if your password contains special characters and isn't encoded, the login request will fail
    /// </summary>
    CERT_AUTH_REQUIRED,

    /// <summary>
    /// Change password required.
    /// </summary>
    CHANGE_PASSWORD_REQUIRED,

    /// <summary>
    /// Personal message required for the user.
    /// </summary>
    PERSONAL_MESSAGE_REQUIRED,

    /// <summary>
    /// The latest international terms and conditions must be accepted prior to logging in.
    /// </summary>
    INTERNATIONAL_TERMS_ACCEPTANCE_REQUIRED,

    /// <summary>
    /// This account has not opted in to log in with the email.
    /// </summary>
    EMAIL_LOGIN_NOT_ALLOWED,

    /// <summary>
    /// There is more than one account with the same credential.
    /// </summary>
    MULTIPLE_USERS_WITH_SAME_CREDENTIALS,

    /// <summary>
    /// The account must undergo password recovery to reactivate via https://identitysso.betfair.com/view/recoverpassword
    /// </summary>
    ACCOUNT_PENDING_PASSWORD_CHANGE,

    /// <summary>
    /// The limit for successful login requests per minute has been exceeded. 
    /// New login attempts will be banned for 20 minutes
    /// </summary>
    TEMPORARY_BAN_TOO_MANY_REQUESTS,

    /// <summary>
    /// You must login to the website to accept the new conditions.
    /// </summary>
    ITALIAN_PROFILING_ACCEPTANCE_REQUIRED,

    /// <summary>
    /// You are attempting to login to the Betfair Romania domain with a non .ro account.
    /// </summary>
    AUTHORIZED_ONLY_FOR_DOMAIN_RO,

    /// <summary>
    /// You are attempting to login to the Betfair Swedish domain with a non .se account.
    /// </summary>
    AUTHORIZED_ONLY_FOR_DOMAIN_SE,

    /// <summary>
    /// You must provided your Swedish National identifier via Betfair.se before proceeding.
    /// </summary>
    SWEDEN_NATIONAL_IDENTIFIER_REQUIRED,

    /// <summary>
    /// You must provided your Swedish bank id via Betfair.se before proceeding.
    /// </summary>
    SWEDEN_BANK_ID_VERIFICATION_REQUIRED,

    /// <summary>
    /// You must login to https://www.betfair.com to provide the missing information.
    /// </summary>
    ACTIONS_REQUIRED,

    /// <summary>
    /// There is a problem with the data validity contained within the request. 
    /// Please check that the request (including headers) is in the correct format,
    /// </summary>
    INPUT_VALIDATION_ERROR
}
