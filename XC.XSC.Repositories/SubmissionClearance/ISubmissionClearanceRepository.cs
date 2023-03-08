using System.Linq.Expressions;

namespace XC.XSC.Repositories.SubmissionClearance
{
    public interface ISubmissionClearanceRepository : IRepository<Models.Entity.SubmissionClearance.SubmissionClearance>
    {
        /// <summary>
        /// This method is implemented to retrieve single record of SubmissionClearances table.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns> Single SubmissionClearance data or default if no such element is found</returns>
        Task<Models.Entity.SubmissionClearance.SubmissionClearance?> SingleOrDefaultAsync(Expression<Func<Models.Entity.SubmissionClearance.SubmissionClearance, bool>> predicate);

        /// <summary>
        /// Add multipple record
        /// </summary>
        /// <param name="submissionClearancesAdd"></param>
        /// <returns></returns>
        Task AddRangeAsync(List<Models.Entity.SubmissionClearance.SubmissionClearance> submissionClearancesAdd);

        /// <summary>
        /// Remove multipple record
        /// </summary>
        /// <param name="submissionClearances"></param>
        /// <returns></returns>
        Task RemoveRange(List<Models.Entity.SubmissionClearance.SubmissionClearance> submissionClearances);

    }
}
