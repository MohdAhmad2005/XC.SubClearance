using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.Service.MessageSent
{
    public interface IMessageSentService
    {
        /// <summary>
        /// Add Message sent based on UserId
        /// </summary>
        /// <returns></returns>
        Task<IResponse> AddMessageSent(Models.Mongo.Entity.MessageSent.MessageSent messageSent);
    }
}
