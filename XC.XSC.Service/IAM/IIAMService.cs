using XC.CCMP.IAM;
using XC.CCMP.IAM.Keycloak.Models;

namespace XC.XSC.Service.IAM
{
    public interface IIAMService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        Task<TokenResponse> Login(string UserName, string Password);

        /// <summary>
        /// User's - Login
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<bool> Logout(string accessToken, string refreshToken);

        /// <summary>
        /// User's - Forgot Password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task<bool> ForgotPassword(string userId, string newPassword);

        /// <summary>
        /// User's - Reset Password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <param name="confirmPassword"></param>
        /// <returns></returns>
        Task<bool> ResetPassword(string userName, string oldPassword, string newPassword, string confirmPassword);

    }
}
