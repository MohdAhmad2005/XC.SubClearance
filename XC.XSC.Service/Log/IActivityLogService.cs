using XC.XSC.ViewModels.Log.Request;
using XC.XSC.ViewModels.Log.Response;

namespace XC.XSC.Service.Log
{
    public interface IActivityLogService
    {
        Task<List<ActivityLogResponse>> GetActivityLogByTenantAsync(string tenantId);
        Task<bool> AddActivityLogAsync(ActivityLogRequest model);
    }
}
