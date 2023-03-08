using XC.XSC.Repositories.Log;
using XC.XSC.ViewModels.Log.Request;
using XC.XSC.ViewModels.Log.Response;

namespace XC.XSC.Service.Log
{
    public class ActivityLogService : IActivityLogService
    {
        private readonly IActivityLogRepository _activityLogRepository;
        public ActivityLogService(IActivityLogRepository activityLogRepository)
        {
            _activityLogRepository = activityLogRepository;
        }

        public Task<bool> AddActivityLogAsync(ActivityLogRequest model)
        {
            return _activityLogRepository.AddActivityLogAsync(new Models.Entity.Log.ActivityLog()
            {
                TenantId = model.TenantId,
                ActivityBy = model.ActivityBy,
                ActivityOn = model.ActivityOn,
                Data = model.Data,
                LogType = model.LogType,
            });
        }

        public Task<List<ActivityLogResponse>> GetActivityLogByTenantAsync(string tenantId)
        {
            var activityLog = _activityLogRepository.GetActivityLogByTenantAsync(tenantId);

            return null;
        }
    }
}
