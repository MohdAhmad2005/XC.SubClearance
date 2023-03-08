using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.Service.SubmissionClearance
{
    public interface ISubmissionClearanceService
    {
        /// <summary>
        /// Execute submission clearance check rule on the basis of predefined rules
        /// </summary>
        /// <param name="submissionId">Accept submissionId</param>
        /// <returns>SUCCESS</returns>
        Task<IResponse> SubmissionClearanceCheckAsync(long SubmissionId);

        /// <summary>
        /// Get all submission clearances by submissionId
        /// </summary>
        /// <param name="submissionId">Accept submissionId</param>
        /// <returns>SUCCESS</returns>
        Task<IResponse> GetSubmissionClearancesAsync(long SubmissionId);
    }
}
