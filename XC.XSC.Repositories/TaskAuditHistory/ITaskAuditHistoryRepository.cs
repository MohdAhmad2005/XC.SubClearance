using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Models.Entity.Log;
using XC.XSC.Models.Entity.Submission;
using XC.XSC.Models.Interface.Submission;
using XC.XSC.ViewModels.Submission;

namespace XC.XSC.Repositories.TaskAuditHistory
{
    /// <summary>
    /// This is an interface and inherits the property of IRepository.
    /// </summary>
    public interface ITaskAuditHistoryRepository : IRepository<Models.Entity.TaskAuditHistory.TaskAuditHistory>
    {
    }
}
