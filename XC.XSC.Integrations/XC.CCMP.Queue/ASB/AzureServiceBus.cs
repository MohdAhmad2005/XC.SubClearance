using Azure.Messaging.ServiceBus;
using System.Text.Json;
using XC.CCMP.Queue.Broker;
using XC.XSC.ValidateMail.Models;
using XC.XSC.ViewModels;

namespace XC.CCMP.Queue.ASB
{
    public class AzureServiceBus : IAzureServiceBus
    {
        private readonly IQueueConfiguration _queueConfiguration;
        private readonly IQueueProcessor _queueProcessor;

        public AzureServiceBus(IQueueConfiguration queueConfiguration, IQueueProcessor queueProcessor)
        {
            _queueConfiguration = queueConfiguration;
            _queueProcessor = queueProcessor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task ReceiveMessageAsync(QueueMessage queueMessage)
        {
            await using (ServiceBusClient client = new ServiceBusClient(_queueConfiguration.ASBConfiguration.ConnectionString))
            {
                var options = new ServiceBusProcessorOptions
                {
                    // By default or when AutoCompleteMessages is set to true, the processor will complete the message after executing the message handler
                    // Set AutoCompleteMessages to false to [settle messages](/azure/service-bus-messaging/message-transfers-locks-settlement#peeklock) on your own.
                    // In both cases, if the message handler throws an exception without settling the message, the processor will abandon the message.
                    AutoCompleteMessages = false,

                    // I can also allow for multi-threading
                    MaxConcurrentCalls = 2
                };

                await using (ServiceBusProcessor processor = client.CreateProcessor(_queueConfiguration.ASBConfiguration.QueueName, options))
                {
                    // configure the message and error handler to use
                    processor.ProcessMessageAsync += MessageHandler;
                    processor.ProcessErrorAsync += ErrorHandler;

                    async Task MessageHandler(ProcessMessageEventArgs args)
                    {
                        string _queueMessage = args.Message.Body.ToString();
                        Console.WriteLine(_queueMessage);

                        //--------------------------------------------
                        QueueMessage queueMessage = JsonSerializer.Deserialize<QueueMessage>(_queueMessage);

                        if (queueMessage == null)
                            throw new ArgumentNullException("QueueMessage has not found.");

                        await _queueProcessor.ProcessAsync(queueMessage);
                        //--------------------------------------------

                        // we can evaluate application logic and use that to determine how to settle the message.
                        await args.CompleteMessageAsync(args.Message);
                    }

                    Task ErrorHandler(ProcessErrorEventArgs args)
                    {
                        // the error source tells me at what point in the processing an error occurred
                        Console.WriteLine(args.ErrorSource);
                        // the fully qualified namespace is available
                        Console.WriteLine(args.FullyQualifiedNamespace);
                        // as well as the entity path
                        Console.WriteLine(args.EntityPath);
                        Console.WriteLine(args.Exception.ToString());
                        return Task.CompletedTask;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueMessage"></param>
        /// <returns></returns>
        public async Task SendMessageAsync(QueueMessage queueMessage)
        {
            await using (var client = new ServiceBusClient(_queueConfiguration.ASBConfiguration.ConnectionString))
            {
                ServiceBusSender sender = client.CreateSender(_queueConfiguration.ASBConfiguration.QueueName);

                var jsonMessage = JsonSerializer.Serialize(queueMessage);

                ServiceBusMessage message = new ServiceBusMessage(jsonMessage);

                await sender.SendMessageAsync(message);
            }                
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outScopeMessage"></param>
        /// <returns></returns>
        public async Task SendMessageAsync(OutScopeMessage outScopeMessage)
        {
            await using (var client = new ServiceBusClient(_queueConfiguration.ASBConfiguration.ConnectionString))
            {
                ServiceBusSender sender = client.CreateSender(_queueConfiguration.ASBConfiguration.QueueName);

                var jsonMessage = JsonSerializer.Serialize(outScopeMessage);

                ServiceBusMessage message = new ServiceBusMessage(jsonMessage);

                await sender.SendMessageAsync(message);
            }
        }
    }
}
