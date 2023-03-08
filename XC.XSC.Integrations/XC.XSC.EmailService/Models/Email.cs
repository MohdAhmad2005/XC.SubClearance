using Microsoft.AspNetCore.Http;


namespace XC.XSC.EmailSender.Models
{
    public class Email
    {
        public string To { get; set; } = String.Empty;
        public string Subject { get; set; } = String.Empty;

        public string Body { get; set; } = String.Empty;
        public string Cc { get; set; } = String.Empty;
        public string Bcc { get; set; } = String.Empty;

        public List<IFormFile> Attachments { get; set; }
    }
}
