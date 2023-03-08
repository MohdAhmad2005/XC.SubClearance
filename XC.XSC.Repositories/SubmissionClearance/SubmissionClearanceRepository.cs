using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using XC.XSC.Data;

namespace XC.XSC.Repositories.SubmissionClearance
{
    /// <summary>
    /// This is the implementation class of ISubmissionClearanceRepository interface.
    /// </summary>
    public class SubmissionClearanceRepository : ISubmissionClearanceRepository
    {
        private readonly MSSqlContext _msSqlContext;
        public SubmissionClearanceRepository(MSSqlContext msSqlContext)
        {
            _msSqlContext = msSqlContext;
        }

        /// <summary>
        /// Add submissionClearance data
        /// </summary>
        /// <param name="submissionClearance"></param>
        /// <returns>void</returns>
        public async Task AddAsync(Models.Entity.SubmissionClearance.SubmissionClearance submissionClearance)
        {
            _msSqlContext.SubmissionClearances.Add(submissionClearance);
            await _msSqlContext.SaveChangesAsync();
        }

        public Task DeleteAsync(Expression<Func<Models.Entity.SubmissionClearance.SubmissionClearance, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is implemented to retrieve all record of SubmissionClearances table.
        /// </summary>
        /// <returns>List of SubmissionClearance data</returns>
        public IQueryable<Models.Entity.SubmissionClearance.SubmissionClearance> GetAll()
        {
            return _msSqlContext.SubmissionClearances.AsQueryable();
        }

        /// <summary>
        /// This method is implemented to retrieve single record of SubmissionClearances table.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns> Single SubmissionClearance data</returns>
        public async Task<Models.Entity.SubmissionClearance.SubmissionClearance> GetSingleAsync(Expression<Func<Models.Entity.SubmissionClearance.SubmissionClearance, bool>> predicate)
        {
            return await GetAll().SingleAsync<Models.Entity.SubmissionClearance.SubmissionClearance>(predicate);
        }

        /// <summary>
        /// This method is implemented to retrieve single record of SubmissionClearances table.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns> Single SubmissionClearance data or default if no such element is found</returns>
        public async Task<Models.Entity.SubmissionClearance.SubmissionClearance?> SingleOrDefaultAsync(Expression<Func<Models.Entity.SubmissionClearance.SubmissionClearance, bool>> predicate)
        {
            return await GetAll().SingleOrDefaultAsync<Models.Entity.SubmissionClearance.SubmissionClearance>(predicate);
        }

        /// <summary>
        /// This method is implemented to update record of SubmissionClearances table.
        /// </summary>
        /// <returns>SubmissionClearances table updated record</returns>
        public async Task<Models.Entity.SubmissionClearance.SubmissionClearance> UpdateAsync(Models.Entity.SubmissionClearance.SubmissionClearance submissionClearance)
        {
            _msSqlContext.Entry(submissionClearance).State = EntityState.Modified;
            await _msSqlContext.SaveChangesAsync();
            return submissionClearance;
        }

        /// <summary>
        /// Add multipple record
        /// </summary>
        /// <param name="submissionClearancesAdd"></param>
        /// <returns></returns>
        public async Task AddRangeAsync(List<Models.Entity.SubmissionClearance.SubmissionClearance> submissionClearancesAdd)
        {
            await _msSqlContext.SubmissionClearances.AddRangeAsync(submissionClearancesAdd);
            await _msSqlContext.SaveChangesAsync();
        }

        /// <summary>
        /// Remove multipple record
        /// </summary>
        /// <param name="submissionClearances"></param>
        /// <returns></returns>
        public async Task RemoveRange(List<Models.Entity.SubmissionClearance.SubmissionClearance> submissionClearances)
        {
            _msSqlContext.SubmissionClearances.RemoveRange(submissionClearances);
            await _msSqlContext.SaveChangesAsync();
        }
    }
}
