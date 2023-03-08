using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using XC.XSC.Data;
using XC.XSC.Models.Entity.SubmissionStage;
using XC.XSC.Models.Entity.SubmissionStatus;
using XC.XSC.ViewModels.Submission;

namespace XC.XSC.Repositories.Submission
{
    public class SubmissionRepository : ISubmissionRepository
    {
        private readonly MSSqlContext _msSqlContext;
        /// <summary>
        /// SubmissionRepository Constructor 
        /// </summary>
        /// <param name="msSqlContext">DBContext contain Entity model </param>
        public SubmissionRepository(MSSqlContext msSqlContext)
        {
            _msSqlContext = msSqlContext;
        }
        /// <summary>
        /// Add Submission
        /// </summary>
        /// <param name="submission"></param>
       
        public async Task AddAsync(Models.Entity.Submission.Submission submission)
        {
            _msSqlContext.Submissions.Add(submission);
            await _msSqlContext.SaveChangesAsync();
        }
        /// <summary>
        /// get All Submission
        /// </summary>
       
        public IQueryable<Models.Entity.Submission.Submission> GetAll()
        {
            return _msSqlContext.Submissions
                                      .Include(EmailInfo => EmailInfo.EmailInfo)
                                      .Include(SubmissionStatus => SubmissionStatus.SubmissionStatus)
                                      .Include(SubmissionStage => SubmissionStage.SubmissionStage)
                                      .Include(lob => lob.Lob)
                                      .AsQueryable();
        }
        public Task DeleteAsync(Expression<Func<Models.Entity.Submission.Submission, bool>> predicate)
        {
            throw new NotImplementedException();
        }/// <summary>
         /// get first default based on filter predicate
         /// </summary>
         /// <param name="predicate">filter predicate</param>

        public async Task<Models.Entity.Submission.Submission> GetSingleAsync(Expression<Func<Models.Entity.Submission.Submission, bool>> predicate)
        {
            return await _msSqlContext.Submissions.Include(EmailInfo => EmailInfo.EmailInfo)
                                      .Include(SubmissionStatus => SubmissionStatus.SubmissionStatus)
                                      .Include(SubmissionStage => SubmissionStage.SubmissionStage)
                                      .Include(lob => lob.Lob)
                                      .SingleOrDefaultAsync(predicate);
        }
        /// <summary>
        /// Update Submission 
        /// </summary>       
        /// <returns></returns>
        public async Task<Models.Entity.Submission.Submission> UpdateAsync(Models.Entity.Submission.Submission submission)
        {
            _msSqlContext.Entry(submission).State = EntityState.Modified;
            await _msSqlContext.SaveChangesAsync();

            return submission;
        }

    }
}


