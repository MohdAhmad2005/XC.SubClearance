using XC.XSC.Repositories.SubmissionAuditLog;
using XC.XSC.Service.User;
using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.Service.SubmissionAuditLog
{
    public class SubmissionAuditLogService:ISubmissionAuditLogService
    {
        private readonly IUserContext _userContext;
        private readonly ISubmissionAuditLogRepository _submissionAuditLogRepository;
        private readonly IResponse _operationResponse;
        public SubmissionAuditLogService(ISubmissionAuditLogRepository submissionAuditLogRepository, IResponse response, IUserContext userContext) 
        {
            _submissionAuditLogRepository = submissionAuditLogRepository;
            this._operationResponse = response;
            this._userContext = userContext;    
        }
        public async Task<IResponse> SaveSubmissionAuditLog(Models.Entity.SubmissionAuditLog.SubmissionAuditLog submissionAuditRequest)
        {
            try
            {
                Models.Entity.SubmissionAuditLog.SubmissionAuditLog submissionAuditLog = new Models.Entity.SubmissionAuditLog.SubmissionAuditLog()
                {
                    SubmissionId = submissionAuditRequest.SubmissionId,
                    PrevStatus = submissionAuditRequest.PrevStatus,
                    NewStatus = submissionAuditRequest.NewStatus,
                    PrevAssignedToId = submissionAuditRequest.PrevAssignedToId,
                    NewAssignedToId = submissionAuditRequest.NewAssignedToId,
                    TenantId = _userContext.UserInfo.TenantId,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    CreatedBy = _userContext.UserInfo.UserId,
                    ModifiedBy = _userContext.UserInfo.UserId,
                    IsActive = true,
                };
                await _submissionAuditLogRepository.AddAsync(submissionAuditLog);
                _operationResponse.Result = submissionAuditLog;
            }
            catch (Exception ex)
            { 
            }
            _operationResponse.IsSuccess= true;
            _operationResponse.Message = "Success";
          
            return _operationResponse;

        }
    }
}
