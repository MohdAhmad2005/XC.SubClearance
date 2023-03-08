using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using XC.XSC.Data;
namespace XC.XSC.Repositories.SubmissionAuditLog
{
    public class SubmissionAuditLogRepository : ISubmissionAuditLogRepository
    {
        private readonly MSSqlContext _msSqlContext;
        /// <summary>
        /// SubmissionRepository Constructor 
        /// </summary>
        /// <param name="msSqlContext">DBContext contain Entity model </param>
        public SubmissionAuditLogRepository(MSSqlContext msSqlContext)
        {
            _msSqlContext = msSqlContext;
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task AddAsync(Models.Entity.SubmissionAuditLog.SubmissionAuditLog obj)
        {
            _msSqlContext.SubmissionAuditLog.Add(obj);
            await _msSqlContext.SaveChangesAsync();
        }

        public Task DeleteAsync(Expression<Func<Models.Entity.SubmissionAuditLog.SubmissionAuditLog, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Models.Entity.SubmissionAuditLog.SubmissionAuditLog> GetAll()
        {
            return _msSqlContext.SubmissionAuditLog.AsQueryable();
        }

        public async Task<Models.Entity.SubmissionAuditLog.SubmissionAuditLog> GetSingleAsync(Expression<Func<Models.Entity.SubmissionAuditLog.SubmissionAuditLog, bool>> predicate)
        {
            return await _msSqlContext.SubmissionAuditLog.SingleOrDefaultAsync(predicate);
        }

        public Task<Models.Entity.SubmissionAuditLog.SubmissionAuditLog> UpdateAsync(Models.Entity.SubmissionAuditLog.SubmissionAuditLog obj)
        {
            throw new NotImplementedException();
        }

        
    }
}
