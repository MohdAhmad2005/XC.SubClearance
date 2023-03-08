using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.ViewModels.Log.Response;
using XC.XSC.ViewModels.QueueProcessor;
using XC.XSC.ViewModels.Submission;

namespace XC.XSC.Service.Submission
{
    public interface IEmailProcessorService
    {
        /// <summary>
        /// Save Email Info and email attachment 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> SaveEmailInfo(EmailProcessorRequest request);
    }
}
