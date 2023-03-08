using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Models.Mongo.Entity;

namespace XC.XSC.Repositories.SubmissionExtraction
{
    public interface ISubmissionExtractionRepository : IRepository<Models.Mongo.Entity.SubmissionExtraction>
    {
        /// <summary>
        /// Get Submission Edit screen details.
        /// </summary>
        /// <param name="submissionId">Submission id of case.</param>
        /// <returns>Returns the submission details on submission edit screen.</returns>
        public Task<Models.Mongo.Entity.SubmissionExtraction> Add(Models.Mongo.Entity.SubmissionExtraction configuration);

        /// <summary>
        /// This method will get single extraction document from mongo.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>It returns a document.</returns>
        Task<Models.Mongo.Entity.SubmissionExtraction> GetSingleAsync(FilterDefinition<Models.Mongo.Entity.SubmissionExtraction> filter);

        /// <summary>
        /// This method will get submission form from mongo.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>It returns a document.</returns>
        Task<SubmissionForm> GetSubmissionForm(FilterDefinition<SubmissionForm> filter);
    }
}
