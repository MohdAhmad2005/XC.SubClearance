using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static XC.XSC.Repositories.SubmissionStatus.SubmissionStatusRepository;
using XC.XSC.Data;
using System.Linq.Expressions;
using XC.XSC.Models.Entity.SubmissionStage;
using Microsoft.EntityFrameworkCore;
using XC.XSC.Models.Interface.SubmissionStage;
using XC.XSC.Models.Entity.Submission;

namespace XC.XSC.Repositories.SubmissionStatus
{
    /// <summary>
    /// This is the implementation class of ISubmissionStatusRepository interface.
    /// </summary>
    public class SubmissionStatusRepository : ISubmissionStatusRepository
    {
        private readonly MSSqlContext _msSqlContext;
        public SubmissionStatusRepository(MSSqlContext msSqlContext)
        {
          _msSqlContext = msSqlContext;
        }
        /// <summary>
        /// This method is implemented to save new record in database.
        /// </summary>
        /// <param name="submissionStatus"></param>
        /// <returns>There is no return type it just save the changes.</returns>
        public async Task AddAsync(Models.Entity.SubmissionStatus.SubmissionStatus submissionStatus)
        {
            await _msSqlContext.SubmissionStatus.AddAsync(submissionStatus);
            await _msSqlContext.SaveChangesAsync();
        }
        /// <summary>
        /// This method is implemented to retrieve all record of submission status from database.
        /// </summary>
        /// <returns>List of Submission status</returns>
        public IQueryable<Models.Entity.SubmissionStatus.SubmissionStatus> GetAll()
        {
            return _msSqlContext.SubmissionStatus.AsQueryable<Models.Entity.SubmissionStatus.SubmissionStatus>();
        }

        /// <summary>
        /// This method is implemented to Update an existing record.It takes id to update the particular record.
        /// </summary>
        /// <param name="submissionStatus"></param>
        /// <returns>Updated record of submission status.</returns>
        public async Task<Models.Entity.SubmissionStatus.SubmissionStatus> UpdateAsync(Models.Entity.SubmissionStatus.SubmissionStatus submissionStatus)
        {
            _msSqlContext.Entry(submissionStatus).State = EntityState.Modified;
             await _msSqlContext.SaveChangesAsync();
            return submissionStatus;
        }
        /// <summary>
        /// This method is implemented to get a retrieve a single submission status record from the datatbase based on provided Id.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<Models.Entity.SubmissionStatus.SubmissionStatus> GetSingleAsync(Expression<Func<Models.Entity.SubmissionStatus.SubmissionStatus, bool>> predicate)
        {
            var submissionStatusObj = await _msSqlContext.SubmissionStatus.SingleOrDefaultAsync(predicate);
            return submissionStatusObj;
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        Task IRepository<Models.Entity.SubmissionStatus.SubmissionStatus>.DeleteAsync(Expression<Func<Models.Entity.SubmissionStatus.SubmissionStatus, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
