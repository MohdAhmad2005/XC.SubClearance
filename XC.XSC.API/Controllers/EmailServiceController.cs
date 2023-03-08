using Microsoft.AspNetCore.Mvc;
using System.Net;
using XC.CCMP.KeyVault;
using XC.CCMP.Logger;
using XC.XSC.EmailSender;
using XC.XSC.EmailSender.Models;
using XC.XSC.Service.IAM;
using XC.XSC.ViewModels.Authentication;
using XC.XSC.ViewModels.Email;

namespace XC.XSC.API.Controllers
{
    [Route("api/xsc/[controller]")]
    [ApiController]

    public class EmailServiceController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IIAMService _iamService;
        private readonly ILoggerManager _logger;
        private readonly IKeyVaultConfig _keyVaultConfig;

        public EmailServiceController(IEmailService emailService, IConfiguration configuration, IIAMService iamService, ILoggerManager logger, IKeyVaultConfig keyVaultConfig)
        {
            _emailService = emailService;
            _configuration = configuration;
            _iamService = iamService;
            _logger = logger;
            _keyVaultConfig = keyVaultConfig;

            _logger.LogInfo("Initialized - Email Service Controller");
        }

        [HttpPost("emailSend")]
        public async Task<IActionResult> SendEmail([FromForm] EmailRequest request)
        {
            Email email = new Email() { Attachments = request.Attachments, Bcc = request.Bcc, Body = request.Body, Cc = request.Cc, Subject = request.Subject, To = request.To };
            await _emailService.SendEmailAsync(email);
            return Ok();
        }

        /*
        [HttpPost("SendResetPasswordLink")]
        public async Task<IActionResult> SendResetPasswordLink(SendResetPasswordLinkRequest request)
        {

            if (ModelState.IsValid)
            {
                _logger.LogInfo("API called - SendResetPasswordLink");

                UserDetail userDetails = await _iamService.GetUsers();
                if (userDetails != null && userDetails.KeycloakUser != null)
                {
                    var filteredEmail = userDetails.KeycloakUser?.Where(x => x.Email == request.Email)?.FirstOrDefault();
                    if (filteredEmail != null)
                    {
                        var lnkHref = "<a href='" + _keyVaultConfig.XscWebUrl + "SetupNewPassword?xsc_uid=" + filteredEmail.Id + "'>Reset Password</a>";
                                                
                        Email email = new()
                        {
                            Body = $"Hi {filteredEmail.FirstName + ' ' + filteredEmail.LastName} <br><br>You recently requested a password reset for your account. To complete the process, click the button below.<br><br> {lnkHref}",
                            Subject = "XSC - Reset Password Link",
                            To = request.Email

                        };
                        await _emailService.SendEmailAsync(email);
                        return Ok();
                    }
                    else
                    {
                        return Unauthorized(new AuthResponse { StatusCode = HttpStatusCode.Unauthorized, Message = "Invalid Email" });
                    }
                }
                else
                {
                    return Unauthorized(new AuthResponse { StatusCode = HttpStatusCode.Unauthorized });
                }
            }
            else
            {
                return Unauthorized(new AuthResponse { StatusCode = HttpStatusCode.NoContent, Message = "Invalid request parameters." });
            }
           

        }
        */
    }
}
