using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XC.XSC.ViewModels.CommentsClearance;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.Enum;
using XC.XSC.ViewModels.Log.Response;
using XC.XSC.ViewModels.Submission;
using XC.XSC.ViewModels.Submission.SubmissionGeneralInfo;
using XC.XSC.ViewModels.TenantActionDetail;

namespace XC.XSC.Service.Submission
{
    public interface ISubmissionService
    {
        /// <summary>
        /// This method is defined to get the Performance related information  Like Date/ProcessorName, AssignedCount, CompletionCount, Accuracy and TatBreachedCount for My-Performance and Team-Perfrmance a provided date range.
        /// </summary>
        /// <param name="startDate">Start date of selected date range.</param>
        /// <param name="endDate">End date of selected date range.</param>
        /// <param name="performanceType">Performance Type Like My-Performance or Team-Performance.</param>
        /// <param name="region">This is region parameter.</param>
        /// <param name="lob">This is lob parameter.</param>
        /// <returns>Date/ProcessorName, AssignedCount, Completedcount, Accuracy and TatBreachedCount.</returns>
        Task<IResponse> GetPerformanceAsync(DateTime startDate, DateTime endDate, PerformanceType performanceType, int region, int lob);

        /// <summary>
        /// This method is defined to get TotalCount,InScopeCount and OutScopeCount of submissions based on selected date range.
        /// </summary>
        /// <param name="startDate">This is Start date of selected date range.</param>
        /// <param name="endDate">This is End date of selected date range.</param>
        /// <param name="region">This is region parameter.</param>
        /// <param name="lob">This is lob parameter.</param>
        /// <returns>TotalCount, InScopeCount and OutScopeCount.</returns>
        Task<IResponse> GetSubmissionScopeCountAsync(DateTime startDate, DateTime endDate, int region, int lob);

        /// <summary>
        /// This service is used to get the details of Submission like All list of submissions containing isInScope field.
        /// </summary>
        /// <returns></returns>
        Task<IResponse> GetSubmissionListAsync();

        /// <summary>
        /// Save Submission
        /// </summary>
        /// <param name="request"></param>
        /// <returns>while add submission record it return SUCCESS</returns>
        Task<Models.Entity.Submission.Submission> SaveSubmission(SubmissionRequest request);

        /// <summary>
        /// Get Task TatMetrics
        /// </summary>
        /// <param name="submissionId"></param>
        /// <returns>Tat metrics report detail</returns>
        Task<TaskTatMetricsResponse> GetTaskTatMetricsAsync(long submissionId);

        /// <summary>
        /// Get all inscope submission from the submission table on the basis of requested filters.
        /// </summary>
        /// <param name="caseNumber">case number of the submission to filter.</param>
        /// <param name="brokerName">broker name of the submission to filter.</param>
        /// <param name="insuredName">insured name of the submission to filter.</param>
        /// <param name="statusId">status id of the submission to filter.</param>
        /// <param name="assignedTo">assigned user id of any submission to filter.</param>
        /// <param name="receivedFromDate">received from date of any submission to filter.</param>
        /// <param name="receivedToDate">received to date of any submission to filter.</param>
        /// <param name="dueFromDate">due from date of any submission to filter.</param>
        /// <param name="dueToDate">due date of any submission to filter.</param>
        /// <param name="page">current page number.</param>
        /// <param name="limit">current page limit.</param>
        /// <param name="sortField">sort filed to be sorted on the grid.</param>
        /// <param name="sortOrder">sort order ascending or descending.</param>
        /// <param name="isInScope">isInScope filter parameter used for identifyning submission scope.</param>
        /// <param name="region">This is region filter parameter.</param>
        /// <param name="lob">This is lob filter parameter.</param>
        /// <param name="isMySubmission">isMySubmission filter to filter data of self assigned submission.</param>
        /// <param name="isNewSubmission">isNewSubmission filter to filter data of new submission.</param>
        /// <param name="cancellationToken">default cancellation token.</param>
        /// <returns>return the submission list after implementing requested filters as common IResponse.</returns>
        Task<IResponse> GetSubmissions(
                                                                string? caseNumber, string? brokerName,
                                                                string? insuredName, int? statusId, string? assignedTo,
                                                                DateTime? receivedFromDate, DateTime? receivedToDate,
                                                                DateTime? dueFromDate, DateTime? dueToDate,
                                                               int page, int limit, string? sortField, int sortOrder,bool isInScope, int region, int lob, bool isMySubmission, bool isNewSubmission, CancellationToken cancellationToken);
       
        ///<summary>
        ///Get submissions glance based on corresponding user tenant id.
        /// </summary>
        /// <returns>return the submission status response as common IResponse.</returns>
        Task<IResponse> GetSubmissionsGlanceAsync(int region, int lob);

        /// <summary>
        /// using SubmissionUnderQuery this feature move the particular  submission into under query and also add comments for putting submssion to under query and added comments will also be display under submission general information. this feature will only be available when submission  status is any of the (ReviewFail,ReviewPass,InProgress(Paused),InProgress (Play)
        /// </summary>
        /// <param name="commentRequest">CommentType  Remark = 1, Query = 2, Review = 3, OutOfScope = 4,Comment Text,Submission Id,JsonData</param>
        /// <returns> IResponse  as SUCCESS while update comment.  </returns>
        Task<IResponse> SaveUnderQuery(XC.XSC.ViewModels.Comment.CommentRequest commentRequest);

        ///<summary>
        ///Assign submission to the corresponding user by submission id.
        /// </summary>
        ///<param name="submissionId">submission id of which we are assigning to the login user.</param>
        ///<param name="userId">user id of the login user.</param>
        /// <returns>return the corresponding submission response as common IResponse.</returns>
        Task<IResponse> AssignSubmissionAsync(long submissionId, string userId);

        /// <summary>
        /// "Send back to Queue" feature:  the user will be able to  send submission back to queue. this feature will only be availabe  when  submission status is any of the  ("Not Started ", and In Progress (Paused)).
        /// </summary>
        /// <param name="submissionId">Use as Submission ID</param>
        /// <returns>IResponse as success while udpate Status and </returns>

        Task<IResponse> SendAssignedSubmissionBackToQueue(long submissionId);

        /// <summary>
        /// Save Comment based on CommentType Clearance and update ClearanceConscent in Submission
        /// </summary>
        /// <param name="commentsClearanceRequest">CommentType Clearance = 5 , Comment Text,SubmissionId,ClearanceConscent</param>
        /// <returns> IResponse  as SUCCESS while add comment.  </returns>
        Task<IResponse> AddClearanceCommentAsync(CommentsClearanceRequest commentsClearanceRequest);

        /// <summary>
        /// Get submission detail by its submission id.
        /// </summary>
        /// <param name="submissionId">Submission id of case.</param>
        /// <returns>Returns the submission details.</returns>
        Task<IResponse> GetInScopeSubmissionById(long submissionId);

        /// <summary>
        /// Send submission to review.
        /// </summary>
        /// <param name="submitProcessorRequest">It's a request model which consists CommentText, CommentType, SubmissionId, JsonData.</param>
        /// <returns>Return IsSuccess, Message and Result as IResponse.</returns>
        Task<IResponse> SendSubmissionToReviewAsync(SubmitProcessorRequest submitProcessorRequest);

        /// <summary>
        /// Submission Review Reply.
        /// </summary>
        /// <param name="submitReviewerRequest">It's a request model which consists CommentText, CommentType, SubmissionId, JsonData, ReviewStatus.</param>
        /// <returns>Return IsSuccess, Message and Result as IResponse.</returns>
        Task<IResponse> SendSubmissionToProcessorAsync(SubmitReviewerRequest submitReviewerRequest);

        /// <summary>
        /// Update submission status.
        /// </summary>
        /// <param name="submissionId">submission id.</param>
        /// <param name="submissionStatusId ">submission status id.</param>
        /// <returns>IResponse.</returns>
        Task<IResponse> UpdateSubmissionStatusAsync(long submissionId, int submissionStatusId);
    
        /// <summary>
        /// Get submission general information data by its submission id.
        /// </summary>
        /// <param name="submissionId">Submission id of case.</param>
        /// <returns>Returns the submission general information details.</returns>
        Task<IResponse> GetSubmissionGeneralInformation(long submissionId);


    }
}
