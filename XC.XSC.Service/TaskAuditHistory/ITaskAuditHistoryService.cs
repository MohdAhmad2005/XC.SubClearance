using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.TaskAuditHistory;

namespace XC.XSC.Service.TaskAuditHistory
{
    /// <summary>
    /// This is an interface containing one method "GetTaskAuditHistoryDetailAsync".
    /// </summary>
    public interface ITaskAuditHistoryService
    {
        /// <summary>
        /// This method is defined to retrieve TaskAuditHistoryDetails.
        /// </summary>
        /// <param name="submissionId">this parameter use for submissio id </param>
        Task<IResponse> GetTaskAuditHistoryDetailAsync(long submissionId);

        /// <summary>
        /// This method is defined to retrieve TaskAuditHistory Duration.
        /// </summary>
        /// <param name="submissionId">this parameter use for submissio id</param>
        Task<IResponse> GetTaskAuditHistoryDurationAsync(long submissionId);
    }
}
