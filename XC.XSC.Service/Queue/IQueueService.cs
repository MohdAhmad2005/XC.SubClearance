using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.ValidateMail.Models;
using XC.XSC.ViewModels;

namespace XC.XSC.Service.Queue
{
    public interface IQueueService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueMessage"></param>
        /// <returns></returns>
        Task SendMessageAsync(OutScopeMessage outScopeMessage);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueMessage"></param>
        /// <returns></returns>
        Task ReceiveMessageAsync(QueueMessage queueMessage);
    }
}
