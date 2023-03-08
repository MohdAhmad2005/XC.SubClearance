using XC.XSC.ValidateMail.Models;

namespace XC.CCMP.Queue.ASB
{
    public interface IAzureServiceBus : IQueue
    {
        /// <summary>
        /// Used to send Out Scope Message to queue
        /// </summary>
        /// <param name="queueMessage"></param>
        /// <returns></returns>
        Task SendMessageAsync(OutScopeMessage outScopeMessage);
    }
}
