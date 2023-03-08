using XC.XSC.Models.Entity.Log;

namespace XC.XSC.Repositories.Log
{
    public interface IActivityLogRepository : IRepository<ActivityLog>
    {
        Task<List<ActivityLog>> GetActivityLogByTenantAsync(string tenantId);
        Task<bool> AddActivityLogAsync(ActivityLog model);
        
    }
}
