using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.Service.SubmissionAuditLog
{
    public interface ISubmissionAuditLogService
    {
        Task<IResponse> SaveSubmissionAuditLog(Models.Entity.SubmissionAuditLog.SubmissionAuditLog submissionAuditRequest);
    }
}
