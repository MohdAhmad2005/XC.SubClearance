using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.CCMP.Queue;
using XC.CCMP.Queue.ASB;
using XC.CCMP.Queue.Broker;
using XC.CCMP.Queue.Enum;
using XC.XSC.ValidateMail.Models;
using XC.XSC.ViewModels;

namespace XC.XSC.Service.Queue
{
    public class QueueService : IQueueService
    {
        private readonly IQueueProcessor _queueProcessor;
        private readonly IQueueConfiguration _queueConfiguration;

        private readonly QueueType _queueType;
        private readonly IQueue _queue;
        private readonly IAzureServiceBus _azureServiceBus;

        public QueueService(IQueueConfiguration queueConfiguration, IQueueProcessor queueProcessor, IAzureServiceBus azureServiceBus)
        {
            _queueConfiguration = queueConfiguration;
            _queueProcessor = queueProcessor;
            _queueType = queueConfiguration.QueueType;
            _queue = GetQueueInstance();
            _azureServiceBus = azureServiceBus;
        }

        public Task ReceiveMessageAsync(QueueMessage queueMessage)
        {
            switch (_queueType)
            {
                case QueueType.AzureServiceBus:
                    return ReceiveAzureServiceMessage(queueMessage);
                    break;

                default:
                    throw new ArgumentException("Queue configuration has not yet implemented.");
            }
        }

        public async Task SendMessageAsync(OutScopeMessage outScopeMessage)
        {
            switch (_queueType)
            {
                case QueueType.AzureServiceBus:
                    await SendAzureServiceMessage(outScopeMessage);
                    break;

                default:
                    throw new ArgumentException("Queue configuration has not yet implemented.");
            }
        }

        #region "Private methods"

        private async Task SendAzureServiceMessage(OutScopeMessage outScopeMessage)
        {
            await _azureServiceBus.SendMessageAsync(outScopeMessage);
        }

        private Task ReceiveAzureServiceMessage(QueueMessage queueMessage)
        {
            return _queue.ReceiveMessageAsync(queueMessage);
        }

        private IQueue GetQueueInstance()
        {
            IQueue queue;

            switch (_queueType)
            {
                case QueueType.AzureServiceBus:
                    queue = new AzureServiceBus(_queueConfiguration, _queueProcessor);
                    break;

                default:
                    throw new ArgumentException("Queue type has not yet implemented.");
            }

            return queue;
        }

        #endregion
    }
}
