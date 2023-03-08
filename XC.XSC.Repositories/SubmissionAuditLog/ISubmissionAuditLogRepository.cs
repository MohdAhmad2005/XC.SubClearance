using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Models.Entity.Log;
using XC.XSC.Models.Entity.SubmissionAuditLog;
using XC.XSC.Models.Interface.SubmissionAuditLog;
using XC.XSC.ViewModels.Submission;

namespace XC.XSC.Repositories.SubmissionAuditLog
{
    public interface ISubmissionAuditLogRepository: IRepository<Models.Entity.SubmissionAuditLog.SubmissionAuditLog>
    {
    }
}
