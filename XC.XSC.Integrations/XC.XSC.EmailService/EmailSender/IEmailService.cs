
using XC.XSC.EmailSender.Models;

namespace XC.XSC.EmailSender
{
    public interface IEmailService
    {
        Task SendEmailAsync(Email request);
    }
}

