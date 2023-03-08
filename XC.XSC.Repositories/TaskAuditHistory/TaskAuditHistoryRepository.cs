using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using XC.XSC.Data;
using XC.XSC.Models.Entity.SubmissionStage;
using XC.XSC.Models.Entity.SubmissionStatus;
using XC.XSC.Models.Entity.TaskAuditHistory;

namespace XC.XSC.Repositories.TaskAuditHistory
{
    /// <summary>
    /// This is the implementation class of ITaskAuditHistoryRepository.
    /// </summary>
    public class TaskAuditHistoryRepository : ITaskAuditHistoryRepository
    {
        private readonly MSSqlContext _msSqlContext;
        public TaskAuditHistoryRepository(MSSqlContext msSqlContext)
        {
            _msSqlContext = msSqlContext;
        }

        /// <summary>
        /// This method is implemented to get list of existing records of TaskAuditHistory from Database.
        /// </summary>
        /// <returns>List of TaskAuditHistory.</returns>
        public IQueryable<Models.Entity.TaskAuditHistory.TaskAuditHistory> GetAll()
        {
            return _msSqlContext.TaskAuditHistory
                                      .Include(submission => submission.Submission)
                                      .Include(SubmissionStatus => SubmissionStatus.SubmissionStatus)
                                      .Include(SubmissionStage => SubmissionStage.SubmissionStage)
                                      .AsQueryable();
        }

        /// <summary>
        /// This method is implemented to get a single existing record of TaskAuditHistory from Database based on provided predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Single TaskAuditHistory Object</returns>
        public Task<Models.Entity.TaskAuditHistory.TaskAuditHistory> GetSingleAsync(Expression<Func<Models.Entity.TaskAuditHistory.TaskAuditHistory, bool>> predicate)
        {
            return _msSqlContext.TaskAuditHistory
                                      .Include(submission => submission.Submission)
                                      .Include(SubmissionStatus => SubmissionStatus.SubmissionStatus)
                                      .Include(SubmissionStage => SubmissionStage.SubmissionStage)
                                      .SingleAsync(predicate);
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<Models.Entity.TaskAuditHistory.TaskAuditHistory> UpdateAsync(Models.Entity.TaskAuditHistory.TaskAuditHistory obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task AddAsync(Models.Entity.TaskAuditHistory.TaskAuditHistory obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task DeleteAsync(Expression<Func<Models.Entity.TaskAuditHistory.TaskAuditHistory, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}


