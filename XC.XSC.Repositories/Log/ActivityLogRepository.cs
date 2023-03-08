using MongoDB.Driver;
using System.Linq.Expressions;
using XC.XSC.Models.Entity.Log;

namespace XC.XSC.Repositories.Log
{
    public class ActivityLogRepository : IActivityLogRepository
    {
        private readonly IMongoCollection<ActivityLog> _activityLog;

        public ActivityLogRepository(IMongoDatabase database)
        {
            _activityLog = database.GetCollection<ActivityLog>("activityLog");
        }

        public async Task<List<ActivityLog>> GetActivityLogByTenantAsync(string tenantId)
        {          
            var filter = Builders<ActivityLog>.Filter.Where(l => l.TenantId == tenantId);

            return (await _activityLog.FindAsync(filter)).ToList();
        }

        public async Task<bool> AddConfigurationAsync(ActivityLog model)
        {
            await AddAsync(model);

            return true;
        }

        public async Task<ActivityLog> GetSingleAsync(Expression<Func<ActivityLog, bool>> predicate)
        {
            var filter = Builders<ActivityLog>.Filter.Where(predicate);

            return (await _activityLog.FindAsync(filter)).FirstOrDefault();
        }

        public async Task AddAsync(ActivityLog obj)
        {
            await _activityLog.InsertOneAsync(obj);
        }        

        public IQueryable<ActivityLog> GetAll()
        {
            return _activityLog.AsQueryable();
        }

        public Task<bool> AddActivityLogAsync(ActivityLog model)
        {
            throw new NotImplementedException();
        }

        public Task<ActivityLog> UpdateAsync(ActivityLog obj)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Expression<Func<ActivityLog, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
