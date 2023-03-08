using System.Text;
using XC.XSC.Repositories.MessageTemplate;
using XC.XSC.Service.User;
using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.Service.MessageTemplate
{
    public class MessageTemplateService : IMessageTemplateService
    {
        private readonly IUserContext _userContext;
        private readonly IMessageTemplateRepository _messageTemplateRepository;
        private readonly IResponse _operationResponse;

        public MessageTemplateService(IMessageTemplateRepository messageTemplateRepository, IResponse operationResponse, IUserContext userContext)
        {
            _messageTemplateRepository = messageTemplateRepository;
            _operationResponse = operationResponse;
            _userContext = userContext;
        }

        /// <summary>
        /// Get Message Template based on TenantId and templateKey
        /// </summary>
        /// <param name="templateKey">template Key</param>
        /// <returns></returns>
        public async Task<Models.Mongo.Entity.MessageTemplate.MessageTemplate?> GetMessageTemplate(string templateKey)
        {
            Models.Mongo.Entity.MessageTemplate.MessageTemplate messageTemplate = await _messageTemplateRepository.GetSingleAsync(
                x => x.TenantId == _userContext.UserInfo.TenantId && x.TemplateKey == templateKey.ToUpper());
            if (messageTemplate == null)
            {
                messageTemplate = await _messageTemplateRepository.GetSingleAsync(x => x.TenantId == "0");
            }
            if (messageTemplate != null)
            {
                if (!string.IsNullOrEmpty(messageTemplate.Mails.Subject))
                {
                    messageTemplate.Mails.Subject = Base64Decode(messageTemplate.Mails.Subject);
                }
                if (!string.IsNullOrEmpty(messageTemplate.Mails.TemplateBody))
                {
                    messageTemplate.Mails.TemplateBody = Base64Decode(messageTemplate.Mails.TemplateBody);
                }
                if (!string.IsNullOrEmpty(messageTemplate.Notifications.Subject))
                {
                    messageTemplate.Notifications.Subject = Base64Decode(messageTemplate.Notifications.Subject);
                }
                if (!string.IsNullOrEmpty(messageTemplate.Notifications.TemplateBody))
                {
                    messageTemplate.Notifications.TemplateBody = Base64Decode(messageTemplate.Notifications.TemplateBody);
                }
            }
            return messageTemplate;
        }

        #region Encode/Decode

        public string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
        #endregion
    }
}
