using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.Service.SubmissionExtraction
{
    public interface ISubmissionExtractionService
    {
        /// <summary>
        /// Add submission detail on mongo db.
        /// </summary>
        /// <returns></returns>
        Task<IResponse> AddSubmissionDetail();

        /// <summary>
        /// Get Submission form Data.
        /// </summary>
        /// <returns> Submission edit screen form schema.  </returns>
        Task<IResponse> GetSubmissionForm();


        /// <summary>
        /// Get Submission Edit screen details.
        /// </summary>
        /// <param name="submissionId">Submission id of case.</param>
        /// <returns>Returns the submission details on submission edit screen.</returns>
        Task<IResponse> GetSubmissionTransformedData(long submissionId);


        /// <summary>
        /// Get Submission Edit screen details.
        /// </summary>
        /// <param name="submissionId">Submission id of case.</param>
        /// <returns>Returns the submission details on submission edit screen.</returns>
        Task<IResponse> UpdateSubmissionTransformedData(Models.Mongo.Entity.SubmissionExtraction submissionExtraction);

        /// <summary>
        /// Check mail is duplicate or not based on submissionId
        /// </summary>
        /// <param name="submissionId">Submission id of case.</param>
        /// <returns>Returns true if mail is duplicate otherwise false.</returns>
        Task<IResponse> ValidateMailDuplicityAsync(long submissionId);
    }
}
