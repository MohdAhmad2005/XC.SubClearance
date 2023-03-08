using XC.CCMP.IAM.Keycloak.Models;

namespace XC.CCMP.IAM.Keycloak.Connect
{
    public interface IIAMClient
    {
        /// <summary>
        /// Try to login with provided username and password. If username or password are invalide, this method will return new empty KeycloakToken object.
        /// </summary>
        /// <param name="username">Username of user that needs to be authenticated.</param>
        /// <param name="password">Password of user that needs to be authenticated.</param>
        /// <returns></returns>
        Task<TokenResponse> Login(string userName, string password);

        /// <summary>
        /// Method to get admin login token.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<TokenResponse> GetAdminLoginToken(string username, string password);

        /// <summary>
        /// Register new user with filled Registration object.
        /// </summary>
        /// <param name="userRegistration">Instance of Registration object with filled data.</param>
        /// <returns></returns>
        Task<KeycloakToken> Registration(Registration userRegistration);

        /// <summary>
        /// Logout user which information is filled in Logout object.
        /// </summary>
        /// <param name="logout">Instance of Logout object with filled data.</param>
        /// <returns></returns>
        Task<bool> Logout(string accessToken, string refreshToken);

        /// <summary>
        /// Delete user which information is filled in DeleteUser object.
        /// </summary>
        /// <param name="logout">Instance of DeleteUser object with filled data.</param>
        /// <returns></returns>
        Task<bool> DeleteUser(string AccessToken, string UserGuid);

        /// <summary>
        /// Create new Keycloak client with filled KeycloakClient information and with access token of account that have rights to create new client.
        /// </summary>
        /// <param name="keycloakClient">Instance of KeycloakClient object with filled data.</param>
        /// <param name="accessToken">Access token of account that have rights to create new client.</param>
        /// <returns></returns>
        Task<bool> CreateClient(KeycloakClient keycloakClient, string accessToken);

        /// <summary>
        /// Delete existing client with provided guid of client and access token of account that have rights to delete client.
        /// </summary>
        /// <param name="clientGuid">Validi client guid.</param>
        /// <param name="accessToken">Access token of account that have rights to delete client.</param>
        /// <returns></returns>
        Task<bool> DeleteClient(string clientGuid, string accessToken);

        /// <summary>
        /// Update existing client with filled instance of KeycloakClient object and valid client guid. 
        /// </summary>
        /// <param name="keycloakClient">Filled instance of KeycloakClient object.</param>
        /// <param name="clientGuid">Valid client guid.</param>
        /// <returns></returns>
        Task<bool> UpdateClient(KeycloakClient keycloakClient, string accessToken, string clientGuid);

        /// <summary>
        /// Reset existing user's password by userId and admin access token.
        /// </summary>
        /// <param name="accessToken">Access token</param>
        /// <param name="userId">User Id to whom we are going to reset password</param>
        /// <param name="newPassword">New password</param>
        /// <returns></returns>
        Task<bool> ResetPassword(string userId, string newPassword);
                
        /// <summary>
        /// Try to login with provided client_id and client_secret. If client_id and client_secret are in-valide, 
        /// this method will return new empty KeycloakToken object.
        /// </summary>
        /// <returns></returns>
        Task<TokenResponse> GetResourceServerToken();

        /// <summary>
        /// Method to return the list of the matching resources by resource uri.
        /// </summary>
        /// <param name="resourceUri"></param>
        /// <returns></returns>
        Task<IIAMResponse> GetResourcesByUri(string resourceUri);

        /// <summary>
        /// Validate users accessed resource.
        /// </summary>
        /// <param name="userToken">userToken: Logged-in user access token.</param>
        /// <param name="resourceUri">resourceUri: Logged-in user trying to access api/resource.</param>
        /// <returns></returns>
        Task<bool> ValidateResourcePermission(string userToken, string resourceUri);

    }
}
