using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using XC.CCMP.Logger;
using XC.XSC.Service.IAM;
using XC.XSC.ViewModels.Authentication;
using XC.CCMP.IAM;
using XC.CCMP.IAM.Keycloak;

namespace XC.XSC.API.Controllers
{
    [ApiController]
    [Route("api/xsc/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IIAMService _iamService;

        public AuthenticationController(ILoggerManager logger, IIAMService iamService)
        {
            _logger = logger;
            _iamService = iamService;

            _logger.LogInfo("Initialized - AuthenticationController");
        }
        
        [HttpPost("getToken")]
        public async Task<ActionResult<AuthResponse>> GetToken([Required] TokenRequest request)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInfo("API called - GetToken");

                var tokenResponse = await _iamService.Login(request.UserName, request.Password);
                
                if (tokenResponse == null)
                {
                    return Unauthorized(new AuthResponse { StatusCode = HttpStatusCode.Unauthorized, Message = "Invalid Authentication" });
                }

                return new AuthResponse
                {
                    IsAuthSuccessful = (tokenResponse.StatusCode == HttpStatusCode.OK),
                    Message = tokenResponse.Token,
                    StatusCode = tokenResponse.StatusCode,
                    Error = tokenResponse.Error,
                    ErrorDescription = tokenResponse.ErrorDescription
                };
            }
            else
            {
                return Unauthorized(new AuthResponse { StatusCode = HttpStatusCode.NoContent, Message = "Invalid request parameters." });
            }
            
        }

        [HttpPost("logout")]
        public async Task<ActionResult<bool>> Logout([Required] LogoutRequest request)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInfo("API called - Logout");

                return await _iamService.Logout(request.AccessToken, request.RefreshToken);
            }
            else
            {
                return Unauthorized(new AuthResponse { StatusCode = HttpStatusCode.NoContent, Message = "Invalid request parameters." });
            }
        }

        [HttpPost("forgotPassword")]
        public async Task<ActionResult<bool>> ForgotPassword([Required] ForgotPasswordRequest request)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInfo("API called - Forgot Password");

                return await _iamService.ForgotPassword(request.UserId, request.NewPassword);
            }
            else
            {
                return Unauthorized(new AuthResponse { StatusCode = HttpStatusCode.NoContent, Message = "Invalid request parameters." });
            }

        }

        [HttpPost("resetPassword")]
        public async Task<ActionResult<bool>> ResetPassword([Required] ResetPasswordRequest request)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInfo("API called - Reset Password");

                return await _iamService.ResetPassword(request.UserName, request.OldPassword, request.NewPassword, request.ConfirmPassword);
            }
            else
            {
                return Unauthorized(new AuthResponse { StatusCode = HttpStatusCode.NoContent, Message = "Invalid request parameters." });
            }

        }

        [Authorize]
        [HttpGet("getUser")]
        public async Task<IActionResult> AuthenticationAsync()
        {
            //Find claims for the current user
            ClaimsPrincipal currentUser = this.User;
            //Get username, for keycloak you need to regex this to get the clean username
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            //logs an error so it's easier to find - thanks debug.
            _logger.LogError(currentUserName);

            //Debug this line of code if you want to validate the content jwt.io
            string accessToken = await HttpContext.GetTokenAsync("access_token");
            string idToken = await HttpContext.GetTokenAsync("id_token");
            string refreshToken = await HttpContext.GetTokenAsync("refresh_token");

            //Get all claims for roles that you have been granted access to 
            IEnumerable<Claim> roleClaims = User.FindAll(ClaimTypes.Role);
            IEnumerable<string> roles = roleClaims.Select(r => r.Value);
            foreach (var role in roles)
            {
                _logger.LogError(role);
            }

            //Another way to display all role claims
            var currentClaims = currentUser.FindAll(ClaimTypes.Role).ToList();

            return Ok();

        }
    }
}
