using XC.XSC.ViewModels;

namespace XC.CCMP.Queue.Broker
{
    public interface IQueueProcessor
    {
        Task<bool> ProcessAsync(QueueMessage queueMessage);
    }
}