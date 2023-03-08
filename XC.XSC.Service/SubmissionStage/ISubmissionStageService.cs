using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.Submission;
using XC.XSC.ViewModels.SubmissionStage;

namespace XC.XSC.Service.SubmissionStage
{
    /// <summary>
    /// This interface contains four methods for SubmissionStage Api.
    /// </summary>
    public interface ISubmissionStageService
    {
        /// <summary>
        /// This method is defined to retrieve list of SibmissionSrage from database.
        /// </summary>
        Task<IResponse> GetAllSubmissionStageAsync();

        /// <summary>
        /// This method is defined to retrieve a single SubmisiionStage based on provided Id from database.
        /// </summary>
        /// <param name="submissionStageId"></param>
        Task<IResponse> GetSubmissionStageByIdAsync(int submissionStageId);

        /// <summary>
        /// This method is defined to add a new record in SubmissionStage table.
        /// </summary>
        /// <param name="addSubmissionStageRequest"></param>
        Task<IResponse> AddSubmissionStageAsync(AddSubmissionStageRequest addSubmissionStageRequest);

        /// <summary>
        /// This method is defined to modify an existing record of SubmissionStage.
        /// </summary>
        /// <param name="updateSubmissionStageRequest"></param>
        Task<IResponse> UpdateSubmissionStageAsync(UpdateSubmissionStageRequest updateSubmissionStageRequest);
    }
}
