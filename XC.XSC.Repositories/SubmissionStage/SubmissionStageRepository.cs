using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Data;
using XC.XSC.ViewModels.SubmissionStage;

namespace XC.XSC.Repositories.SubmissionStage
{
    /// <summary>
    /// This class implements the methods of ISubmissionStageRepository and intracts with dabase.
    /// </summary>
    public class SubmissionStageRepository : ISubmissionStageRepository
    {
        private readonly MSSqlContext _msSqlContext;

        public SubmissionStageRepository(MSSqlContext mSSqlContext)
        {
            _msSqlContext = mSSqlContext;
        }

        /// <summary>
        /// This method is the implementation of AddAsync() method to add a new record.
        /// </summary>
        /// <param name="submissionStage"></param>
        /// <returns>This is a null return type method.</returns>
        public async Task AddAsync(Models.Entity.SubmissionStage.SubmissionStage submissionStage)
        {
            await _msSqlContext.SubmissionStage.AddAsync(submissionStage);
            await _msSqlContext.SaveChangesAsync();           
        }

        /// <summary>
        /// This method is the impplementation of GetAll() method to find the list of SubmissionStage.
        /// </summary>
        /// <returns>It returns a list of SubmissionStage</returns>
        public IQueryable<Models.Entity.SubmissionStage.SubmissionStage> GetAll()
        {
            return _msSqlContext.SubmissionStage.AsQueryable<Models.Entity.SubmissionStage.SubmissionStage>();
        }

        /// <summary>
        /// This method is is the implementation of GetSingleAsync() to find a single SubmissionStage.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>It returns a single SubmissionStageObject</returns>
        public async Task<Models.Entity.SubmissionStage.SubmissionStage> GetSingleAsync(Expression<Func<Models.Entity.SubmissionStage.SubmissionStage, bool>> predicate)
        {
            var getSubmissionStageObj = await _msSqlContext.SubmissionStage.SingleOrDefaultAsync(predicate);
            return getSubmissionStageObj;
        }

        /// <summary>
        /// This method is is the implementation of UpdateAsync() method to update a stored record of SubmissionStage.
        /// </summary>
        /// <param name="submissionStage"></param>
        /// <returns>It returns the updated record object of SubmissionStage</returns>
        public async Task<Models.Entity.SubmissionStage.SubmissionStage> UpdateAsync(Models.Entity.SubmissionStage.SubmissionStage submissionStage)
        {
            _msSqlContext.Entry(submissionStage).State = EntityState.Modified;
            await _msSqlContext.SaveChangesAsync();
            return submissionStage;
        }

        public Task DeleteAsync(Expression<Func<Models.Entity.SubmissionStage.SubmissionStage, bool>> predicate)
        {
            throw new NotImplementedException();
        }    
    }
}
