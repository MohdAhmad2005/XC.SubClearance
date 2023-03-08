using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using XC.CCMP.KeyVault;
using XC.XSC.EmailSender.Models;

namespace XC.XSC.EmailSender
{
    public class EmailService : IEmailService
    {
        private readonly IKeyVaultConfig _keyVaultConfig;

        public EmailService(IKeyVaultConfig keyVaultConfig)
        {            
            _keyVaultConfig = keyVaultConfig;
        }

        public async Task SendEmailAsync(Email request)
        {
            var email = new MimeMessage();
            MailboxAddress emailFrom = new MailboxAddress(_keyVaultConfig.SmtpSender, _keyVaultConfig.SmtpUser);
            email.From.Add(emailFrom);
            email.Subject = request.Subject;

            string[] ToEmail = request.To.Split(';');
            foreach (string To in ToEmail)
            {
                if (!string.IsNullOrEmpty(To))
                    email.To.Add(MailboxAddress.Parse(To));
            }
            
            string[] CcEmail = request.Cc.Split(';');
            foreach (string Cc in CcEmail)
            {
                if (!string.IsNullOrEmpty(Cc))
                    email.Cc.Add(MailboxAddress.Parse(Cc));
            }
            
            string[] BCcEmail = request.Bcc.Split(';');
            foreach (string Bcc in BCcEmail)
            {
                if(!string.IsNullOrEmpty(Bcc))
                    email.Bcc.Add(MailboxAddress.Parse(Bcc));
            }

            BodyBuilder builder = new BodyBuilder();
            if (request.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var attachment in request.Attachments)
                {
                    if (attachment.Length > 0)
                    {
                        using (MemoryStream stream = new MemoryStream())
                        {
                            attachment.CopyTo(stream);
                            fileBytes = stream.ToArray();
                        }
                        builder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                    }
                }
            }

            builder.HtmlBody = request.Body;
            email.Body = builder.ToMessageBody();

            using (SmtpClient smtp = new SmtpClient())
            {
                try
                {
                    Boolean enableSsl= Convert.ToBoolean(_keyVaultConfig.SmtpEnableSsl);
                    Boolean enableTls = Convert.ToBoolean(_keyVaultConfig.SmtpEnableTls);

                    if (enableSsl && enableTls)
                    {
                       smtp.Connect(_keyVaultConfig.SmtpServer, Convert.ToInt32(_keyVaultConfig.SmtpPort),options: SecureSocketOptions.Auto);
                    }
                    else if (!enableTls && !enableSsl)
                    {
                        smtp.Connect(_keyVaultConfig.SmtpServer, Convert.ToInt32(_keyVaultConfig.SmtpPort),options: SecureSocketOptions.Auto);
                    }
                    else if(!enableSsl || enableTls)
                    {
                        smtp.Connect(_keyVaultConfig.SmtpServer, Convert.ToInt32(_keyVaultConfig.SmtpPort),options: SecureSocketOptions.StartTls);
                    } 
                    else if (!enableTls || enableSsl)
                    {
                        smtp.Connect(_keyVaultConfig.SmtpServer, Convert.ToInt32(_keyVaultConfig.SmtpPort),options: SecureSocketOptions.Auto);
                    }

                    smtp.Authenticate(_keyVaultConfig.SmtpUser, _keyVaultConfig.SmtpPassword);
                    await smtp.SendAsync(email);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    smtp.Disconnect(true);
                    smtp.Dispose();
                }
            }
        }
    }
}
