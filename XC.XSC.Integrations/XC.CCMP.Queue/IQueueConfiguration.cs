using XC.CCMP.Queue.ASB;
using XC.CCMP.Queue.Enum;

namespace XC.CCMP.Queue
{
    public interface IQueueConfiguration
    {
        public QueueType QueueType { get; set; }
        public AzureServiceConfiguration ASBConfiguration { get; set; }
    }

}
