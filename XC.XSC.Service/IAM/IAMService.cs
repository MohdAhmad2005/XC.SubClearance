using System.IdentityModel.Tokens.Jwt;
using System.Net;
using XC.CCMP.KeyVault;
using XC.XSC.Utilities;
using XC.CCMP.IAM;
using XC.CCMP.IAM.Keycloak.Connect;
using XC.CCMP.IAM.Keycloak.Models;

namespace XC.XSC.Service.IAM
{
    public class IAMService : IIAMService
    {
        private readonly IAMType _iamtype;
        private readonly IIAMClient _keyCloakClient;
        private readonly IKeyVaultConfig _keyVaultConfig;

        public IAMService(IIAMClient keyCloakClient, IKeyVaultConfig keyVaultConfig)
        {
            _keyCloakClient = keyCloakClient;
            _keyVaultConfig = keyVaultConfig;
            _iamtype = (_keyVaultConfig.IAMType == "Keycloak" ? IAMType.Keycloak : IAMType.None);
        }

        /// <summary>
        /// IAM Login by UserName and Password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<TokenResponse> Login(string UserName, string Password)
        {
            switch (_iamtype)
            {
                case IAMType.Keycloak:
                    {
                        var tokenResponse = await _keyCloakClient.Login(UserName, Password);

                        return tokenResponse;                        
                    }
                default:
                    return new TokenResponse() { ErrorDescription = "IAM not found.", StatusCode = HttpStatusCode.NotFound };
            }
        }

        /// <summary>
        /// User's - Logout
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public async Task<bool> Logout(string accessToken, string refreshToken)
        {
            switch (_iamtype)
            {
                case IAMType.Keycloak:
                    {
                        return await _keyCloakClient.Logout(accessToken, refreshToken);                        
                    }
                default:
                    return false;
            }
        }

        /// <summary>
        /// User's - Forgot Password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public async Task<bool> ForgotPassword(string userId, string newPassword)
        {
            switch (_iamtype)
            {
                case IAMType.Keycloak:
                    {
                        return await _keyCloakClient.ResetPassword(userId, newPassword);
                    }
                default:
                    return false;
            }
        }

        /// <summary>
        /// User's - Reset Password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="confirmPassword"></param>
        /// <returns></returns>
        public async Task<bool> ResetPassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            switch (_iamtype)
            {
                case IAMType.Keycloak:
                    {
                        return await keycloakResetPassword(userName, oldPassword, newPassword, confirmPassword);
                    }
                default:
                    return false;
            }
        }


        #region "Private Methods"

        private async Task<bool> keycloakResetPassword(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            TokenResponse tokenResponse = await _keyCloakClient.Login(userName, oldPassword);

            if (tokenResponse == null)
            {
                throw new ArgumentNullException("Invalid user."); 
            }
            else
            {
                if(tokenResponse.StatusCode==HttpStatusCode.Unauthorized)
                {
                    return false;
                }
                else
                {
                    using (JwtToken jwtToken = new JwtToken())
                    {
                        JwtSecurityToken jwtSecurityToken = jwtToken.GetDecodedToken(tokenResponse.Token.AccessToken);

                        if (jwtSecurityToken != null)
                        {
                            return await _keyCloakClient.ResetPassword(jwtSecurityToken.Subject, confirmPassword);
                        }
                        else
                        {
                            throw new ArgumentNullException("Invalid user's token.");
                        }
                    }
                }
            }    
                
        }

        #endregion        
    }
}
