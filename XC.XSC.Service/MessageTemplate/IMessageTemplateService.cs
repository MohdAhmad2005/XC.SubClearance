using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.Service.MessageTemplate
{
    public interface IMessageTemplateService
    {
        /// <summary>
        /// Get Message Template based on TenantId and key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<Models.Mongo.Entity.MessageTemplate.MessageTemplate?> GetMessageTemplate(string key);
    }
}
