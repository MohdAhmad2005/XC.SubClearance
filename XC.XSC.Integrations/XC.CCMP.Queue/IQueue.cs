using XC.XSC.ViewModels;

namespace XC.CCMP.Queue
{
    public interface IQueue
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueMessage"></param>
        /// <returns></returns>
        Task SendMessageAsync(QueueMessage queueMessage);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueMessage"></param>
        /// <returns></returns>
        Task ReceiveMessageAsync(QueueMessage queueMessage);
    }
}