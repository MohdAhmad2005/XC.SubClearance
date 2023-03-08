using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace XC.XSC.ViewModels.Email
{
    public class EmailRequest
    {
        [Required(ErrorMessage ="Required")]
        public string To { get; set; } = String.Empty;
        [Required(ErrorMessage = "Required")]
        public string Subject { get; set; } = String.Empty;
        [Required(ErrorMessage = "Required")]
        public string Body { get; set; } = String.Empty;
        public string Cc { get; set; } = String.Empty;
        public string Bcc { get; set; } = String.Empty;

        public List<IFormFile> Attachments { get; set; }
    }

    public class SendResetPasswordLinkRequest
    {
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
    }
}
