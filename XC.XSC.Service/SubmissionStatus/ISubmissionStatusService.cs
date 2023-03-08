using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.Submission;
using XC.XSC.ViewModels.SubmissionStatus;

namespace XC.XSC.Service.SubmissionStatus
{
    /// <summary>
    /// It is Service interface
    /// </summary>
    public interface ISubmissionStatusService
    {
        /// <summary>
        /// This method is defined to retrieve list of SubmissionStatus from database.
        /// </summary>
        Task<IResponse> GetSubmissionStatusListAsync();

        /// <summary>
        /// This method is defined to retrieve single SubmissionStatus from database based on provide Id.
        /// </summary>
        /// <param name="submissionStatusId"></param>
        Task<IResponse> GetSubmissionStatusByIdAsync(int submissionStatusId);

        /// <summary>
        /// This method is defined to add a new record in SubmissionStatus table. 
        /// </summary>
        /// <param name="addSubmissionStatusObj"></param>
        Task<IResponse> AddSubmissionStatusAsync(AddSubmissionStatusRequest addSubmissionStatus);

        /// <summary>
        /// This method is defined to modify an existing record of SubmissionStatus.
        /// </summary>
        /// <param name="updateSubmissionStatusObj"></param>
        Task<IResponse> UpdateSubmissionStatusAsync(UpdateSubmissionStatusRequest updateSubmissionStatus);

        /// <summary>
        /// This method is used to return all the submission status in a sbumission status response format based on tenant id.
        /// </summary>
        /// <param name="tenantId">tenant Id of the corresponding user.</param>
        /// <returns>return the submission status response.</returns>
        Task<List<SubmissionStatusResponse>> GetAllSubmissionStatusAsync(string tenantId);
    }
}
