using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XC.XSC.Models.Entity.Submission;
using XC.XSC.Service.Submission;
using XC.XSC.Service.SubmissionAuditLog;
using XC.XSC.Service.SubmissionClearance;
using XC.XSC.Service.User;
using XC.XSC.Service.ValidateMail;
using XC.XSC.ValidateMail.Models.Request;
using XC.XSC.ValidateMail.Models.Response;
using XC.XSC.ViewModels.Comment;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.Enum;
using XC.XSC.ViewModels.Submission;
using XC.XSC.ViewModels.TenantActionDetail;
using XC.XSC.ViewModels.CommentsClearance;
using XC.XSC.Service.JsonFileService;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using XC.XSC.ViewModels.Submission.SubmissionGeneralInfo;
using XC.XSC.Service.Comments;

namespace XC.XSC.API.Controllers
{

    /// <summary>
    /// SubmissionController for handling all operations related to submissions.
    /// </summary>
    [Authorize]
    [Route("api/xsc/[controller]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly IUserContext _userContext; 
        private readonly ISubmissionService _submission;
        private readonly IResponse _operationResponse;
        private readonly IValidateMailService _validateMailService;
        private readonly ISubmissionClearanceService _submissionClearanceService;
        private readonly ISubmissionAuditLogService _submissionAuditLogService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IJsonFileService _jsonFileService;
        private readonly ICommentService _commentService;

        /// <summary>
        /// Submission construtor.
        /// </summary>
        /// <param name="userContext"> Login user detail. </param>
        /// <param name="submission"> submission Service.</param>
        /// <param name="validateMailService">validate mail Service.</param>
        /// <param name="submissionClearanceService">Submission Clearance Service.</param>
        /// <param name="commentService">Comment service to add comments.</param>
        public SubmissionController(IUserContext userContext, IResponse operationResponse, ISubmissionService submission, IValidateMailService validateMailService
            , ISubmissionClearanceService submissionClearanceService, IHostingEnvironment hostEnvironment, IJsonFileService jsonFileService,
            ICommentService commentService)
        {
            _userContext = userContext;
            _operationResponse = operationResponse;
            _submission = submission;
            _validateMailService = validateMailService;
            _submissionClearanceService = submissionClearanceService;
            _hostingEnvironment = hostEnvironment;
            _jsonFileService = jsonFileService;
            _commentService = commentService;
        }

        /// <summary>
        /// Endpoint to get performance related information for My-Performance and Team-Performance based on selected date range.
        /// </summary>
        /// <param name="startDate">Start date of selected date range.</param>
        /// <param name="endDate">End date of selected date range.</param>
        /// <param name="performanceType">Performance Type Like My-Performance or Team-Performance.</param>
        /// <param name="region">This is region parameter.</param>
        /// <param name="lob">This is lob parameter.</param>
        /// <returns> Date/ProcessorName, AssignedCount, CompletedCount, Accuracy and TatBreachedCount.</returns>
        [HttpGet]
        [Route("getPerformance")]
        public async Task<IResponse> GetPerformance(DateTime startDate, DateTime endDate, PerformanceType performanceType, int region, int lob)
        {
            if (ModelState.IsValid)
            {
                var totalDays = (endDate.Date - startDate.Date).TotalDays + 1;
                if(totalDays > 30 || startDate.Date > DateTime.Now.Date || endDate.Date > DateTime.Now.Date) 
                {
                    _operationResponse.IsSuccess = false;
                    _operationResponse.Message = "Date Range can be selected for past 30days only. Please select Valid Date Range.";
                    _operationResponse.Result = null; 
                    return _operationResponse;
                }
                return await _submission.GetPerformanceAsync(startDate, endDate, performanceType, region, lob);
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "Please add required parameters.";
                return _operationResponse;
            }
        }

        /// <summary>
        /// Endpoint to get TotalCount,InScopeCount and OutScopeCount of submissions based on selected date range.
        /// </summary>
        /// <param name="startDate">This is Start date of selected date range.</param>
        /// <param name="endDate">This is End date of selected date range.</param>
        /// <param name="region">This is region parameter.</param>
        /// <param name="lob">This is lob parameter.</param>
        /// <returns> TotalCount, InScopeCount and OutScopeCount</returns>
        [HttpGet]
        [Route("getSubmissionScopeCount")]
        public async Task<IResponse> GetSubmissionScopeCount(DateTime startDate, DateTime endDate, int region, int lob)
        {
            if (ModelState.IsValid)
            {
                var totalDays = (endDate.Date - startDate.Date).TotalDays + 1;
                if (totalDays > 30 || startDate.Date > DateTime.Now.Date || endDate.Date > DateTime.Now.Date)
                {
                    _operationResponse.IsSuccess = false;
                    _operationResponse.Message = "Date Range can be selected for past 30days only. Please select Valid Date Range.";
                    _operationResponse.Result = null;
                    return _operationResponse;
                }
                return await _submission.GetSubmissionScopeCountAsync(startDate, endDate, region, lob);
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "Please add required parameters.";
                return _operationResponse;
            }
        }

        /// <summary>
        /// This service is used to get the details of Submission like All list of submissions and Save submission.
        /// </summary>
        /// <returns>This service is used to get the details of Submission like All list of submissions and Save submission.</returns>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IResponse> Get()
        {
            return (IResponse)await _submission.GetSubmissionListAsync();
        }

        /// <summary>
        /// Save Submission
        /// </summary>
        /// <param name="submissionObj"></param>
        /// <returns>while add submission record it return SUCCESS</returns>
        [HttpPost]
        [Route("SaveSubmission")]
        public async Task<Submission> SaveSubmission(SubmissionRequest submissionObj)
        {
            return await _submission.SaveSubmission(submissionObj);
        }

        /// <summary>
        ///  Get Task TatMetrics
        /// </summary>
        /// <param name="submissionId"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("getTaskTatMetrics/{submissionId}")]
        public async Task<TaskTatMetricsResponse> GetTaskTatMetrics(long submissionId)
        {
            return await _submission.GetTaskTatMetricsAsync(submissionId);
        }

        /// <summary>
        ///  get InScope Submissions data based on the filters.
        /// </summary>
        /// <param name="caseNumber">this is Submission case Number.</param>
        /// <param name="brokerName">broker name filter field.</param>
        /// <param name="insuredName">insured name filter field.</param>
        /// <param name="statusId">submission status id filter field.</param>
        /// <param name="assignedTo">user id filter field.</param>
        /// <param name="receivedFromDate">submission received date filter field.</param>
        /// <param name="receivedToDate">submission received to date filter filed.</param>
        /// <param name="dueFromDate">submission due from date filetr field.</param>
        /// <param name="dueToDate">submission due to date filter field.</param>
        /// <param name="page">current page number.</param>
        /// <param name="limit">current page limit.</param>
        /// <param name="sortField">sort filed to be sorted on the grid.</param>
        /// <param name="sortOrder">sort order ascending or descending.</param>
        /// <param name="isInScope">isInScope filter parameter used for identifyning submission scope.</param>
        /// <param name="region">This is region filter parameter.</param>
        /// <param name="lob">This is lob filter parameter.</param>
        /// <param name="isMySubmission">isMySubmission filter to filter data of self assigned submission.</param>
        /// <param name="isNewSubmission">isNewSubmission filter to filter data of new submission.</param>
        /// <param name="cancellationToken">cancellation token.</param>
        /// <returns>return the submission list after implementing requested filters as common IResponse.</returns>
        [HttpGet]
        [Route("getSubmissions")]

        public async Task<IResponse> GetSubmissions(string? caseNumber, string? brokerName, string? insuredName, int? statusId,
            string? assignedTo, DateTime? receivedFromDate, DateTime? receivedToDate, DateTime? dueFromDate, DateTime? dueToDate,
            int page, int limit, string? sortField, int sortOrder, bool isInScope, int region, int lob, bool isMySubmission, bool isNewSubmission, CancellationToken cancellationToken)

        {
            if (page > 0 && limit > 0)
            {
                return await _submission.GetSubmissions(caseNumber, brokerName, insuredName, statusId,
                                                                               assignedTo, receivedFromDate, receivedToDate, dueFromDate,
                                                                               dueToDate, page, limit, sortField, sortOrder, isInScope, region, lob, isMySubmission, isNewSubmission, cancellationToken);
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "In-Valid page number and page limit.";

                return _operationResponse;
            }

        }

        /// <summary>
        /// Get submission glance based on tenant id of the login user.
        /// </summary>
        /// <returns>return the submission status response as common IResponse.</returns>
        [HttpGet]
        [Authorize]
        [Route("getSubmissionsGlance")]

        public async Task<IResponse> GetSubmissionsGlance(int region, int lob)
        {
            return await _submission.GetSubmissionsGlanceAsync(region, lob);
        }

        /// <summary>
        /// Assign submission to a user by user id.
        /// </summary>
        /// <param name="reallocateSubmissionRequest">This is the reallocateSubmissionRequest parameter.</param>
        /// <returns>return the corresponding submission response as common IResponse.</returns>
        [HttpPost]
        [Route("assignSubmissionToUser")]
        [Authorize]
        public async Task<IResponse> AssignSubmissionToUser(ReallocateSubmissionRequest reallocateSubmissionRequest)
        {
            if (ModelState.IsValid)
            {
                return await _submission.AssignSubmissionAsync(reallocateSubmissionRequest.submissionId, reallocateSubmissionRequest.userId);
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "In-Valid submission/Case Number and user.";

                return _operationResponse;
            }
        }

        /// <summary>
        /// Assign submission to self.
        /// </summary>
        ///<param name="submissionId">submission id of which we are assigning to the login user.</param>
        /// <returns>return the corresponding submission response as common IResponse.</returns>
        [HttpPost]
        [Route("assignSubmissionToSelf")]
        [Authorize]
        public async Task<IResponse> AssignSubmissionToSelf([FromBody] long submissionId)
        {
            if (submissionId > 0)
            {
                return await _submission.AssignSubmissionAsync(submissionId, _userContext.UserInfo.UserId);
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "In-Valid Submission/Case Number.";

                return _operationResponse;
            }
        }

        /// <summary>
        /// Validate e-mail is in scope or out of scope on the basis of predefined rules.
        /// </summary>
        /// <param name="validateMailScopeRequest">Accept Camunda TaskId, SubmissionId, Stage, TenantId </param>
        /// <returns>return TaskId, SubmissionId, Stage, TenantId, Scope</returns>
        [HttpPost]
        [Route("validateMailScope")]
        public async Task<IResponse> ValidateMailScope([FromBody] ValidateMailScopeRequest validateMailScopeRequest)
        {
            return await _validateMailService.ValidateMailScopeAsync(validateMailScopeRequest);
        }

        /// <summary>
        /// using SubmissionUnderQuery this feature move the particular  submission into under query and also add comments for putting submssion to under query and added comments will also be display under submission general information. this feature will only be available when submission  status is any of the (ReviewFail,ReviewPass,InProgress(Paused),InProgress (Play)
        /// </summary>
        /// <param name="commentRequest">CommentType  Remark = 1, Query = 2, Review = 3, OutOfScope = 4,Comment Text,Submission Id,JsonData</param>
        /// <returns> IResponse  as SUCCESS while update comment.  </returns>
        [HttpPost]
        [Route("submissionUnderQuery")]
        public async Task<IResponse> SubmissionUnderQuery(CommentRequest commentRequest)
        {
            if (ModelState.IsValid)
            {
                if (Enum.GetNames(typeof(CommentType)).Contains(commentRequest.CommentType) && !string.IsNullOrWhiteSpace(commentRequest.CommentText) && commentRequest.SubmissionId > 0)
                {
                    return await _submission.SaveUnderQuery(commentRequest);

                }
                else
                {

                    _operationResponse.Result = null;
                    _operationResponse.IsSuccess = false;
                    _operationResponse.Message = "In-Valid comment type.";

                    return _operationResponse;
                }
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "Please add required parameters";
                return _operationResponse;
            }



        }
        /// <summary>
        /// "Send back to Queue" feature:  the user will be able to  send submission back to queue. this feature will only be availabe  when  submission status is any of the  ("Not Started ", and In Progress (Paused)).
        /// </summary>
        /// <param name="submissionId">Use as Submission ID</param>
        /// <returns>IResponse as success while udpate Status and </returns>
        [HttpGet]
        [Route("sendAssignedSubmissionBackToQueue/{submissionId}")]   

        public async Task<IResponse> SendAssignedSubmissionBackToQueue(long submissionId)
        {
            if (submissionId > 0)
            {
                return await _submission.SendAssignedSubmissionBackToQueue(submissionId);
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "In-Valid submission/Case Number.";

                return _operationResponse;
            }

        }

        /// <summary>
        /// Get all submission clearances by submissionId
        /// </summary>
        /// <param name="submissionId">Accept submissionId</param>
        /// <returns>SUCCESS</returns>
        [HttpGet]
        [Route("getSubmissionClearances/{submissionId}")]

        public async Task<IResponse> GetSubmissionClearances(long submissionId)
        {
            if (submissionId > 0)
            {
                return await _submissionClearanceService.GetSubmissionClearancesAsync(submissionId);
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "In-Valid submission.";

                return _operationResponse;
            }

        }

        /// <summary>
        /// Save Comment based on CommentType Clearance and update ClearanceConscent in Submission
        /// </summary>
        /// <param name="commentsClearanceRequest">CommentType Clearance = 5 , Comment Text,SubmissionId,ClearanceConscent</param>
        /// <returns> IResponse  as SUCCESS while add comment.  </returns>
        [HttpPost]
        [Route("addClearanceComment")]
        public async Task<IResponse> AddClearanceComment(CommentsClearanceRequest commentsClearanceRequest)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrWhiteSpace(commentsClearanceRequest.CommentText) && commentsClearanceRequest.SubmissionId > 0)
                {
                    return await _submission.AddClearanceCommentAsync(commentsClearanceRequest);
                }
                else
                {
                    _operationResponse.Result = null;
                    _operationResponse.IsSuccess = false;
                    _operationResponse.Message = "In-Valid comment type.";

                    return _operationResponse;
                }
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "Please add required parameters";
                return _operationResponse;
            }
        }

        /// <summary>
        /// Get Submission Action Detail
        /// </summary>
        /// <param name="statusId">this property used for Submission StatusId</param>        
        /// <param name="roleName">this is parameter used for role Name </param>
        /// <returns>Submission Action of List</returns>
        [HttpGet]
        [Route("getSubmissionActions")]
        public async Task<IResponse> GetSubmissionActions([FromQuery] GetActionRequest actionRequest)
        {
            return await _jsonFileService.GetSubmissionActions(actionRequest.StatusId, Path.Combine(_hostingEnvironment.ContentRootPath, "JsonData/SubmissonStatusMappedWithAction.json"));
        }

        /// <summary>
        /// Get submission detail by its submission id.
        /// </summary>
        /// <param name="submissionId">Submission id of case.</param>
        /// <returns>Returns the submission details.</returns>
        [HttpGet]
        [Route("getInScopeSubmissionById/{submissionId}")]
        [Authorize]
        public async Task<IResponse> GetInScopeSubmissionById(long submissionId)
        {
            if (submissionId > 0)
            {
                return await _submission.GetInScopeSubmissionById(submissionId);
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "In-Valid submission/Case Number.";
                return _operationResponse;
            }
        }

        /// <summary>
        /// Save Processor/Reviewer comment.
        /// </summary>
        /// <param name="commentRequest">This is the request parameter of type CommentRequest.</param>
        /// <returns>Return IsSuccess, Message and Result as IResponse.</returns>
        [HttpPost]
        [Route("saveSubmissionComment")]
        [Authorize]
        public async Task<IResponse> SaveSubmissionComment(CommentRequest commentRequest)
        {
            if(ModelState.IsValid)
            {
                return await _commentService.AddComment(commentRequest);
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "In-Valid paramenter passed.";
                return _operationResponse;
            }
        }

        /// <summary>
        /// Send submission to review.
        /// </summary>
        /// <param name="submitProcessorRequest">It's a request model which consists CommentText, CommentType, SubmissionId, JsonData.</param>
        /// <returns>Return IsSuccess, Message and Result as IResponse.</returns>
        [HttpPost]
        [Route("sendSubmissionToReview")]
        [Authorize]
        public async Task<IResponse> SendSubmissionToReview(SubmitProcessorRequest submitProcessorRequest)
        {
            if (ModelState.IsValid)
            {
                return await _submission.SendSubmissionToReviewAsync(submitProcessorRequest);
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "Parameters not matched.";
                return _operationResponse;
            }
        }

        /// <summary>
        /// Send reply to processor pass/fail given by reviewer.
        /// </summary>
        /// <param name="submitReviewerRequest">It's a request model which consists CommentText, CommentType, SubmissionId, JsonData, ReviewStatus.</param>
        /// <returns>Return IsSuccess, Message and Result as IResponse.</returns>
        [HttpPost]
        [Route("sendSubmissionToProcessor")]
        [Authorize]
        public async Task<IResponse> SendSubmissionToProcessor(SubmitReviewerRequest submitReviewerRequest)
        {
            if (ModelState.IsValid)
            {
                return await _submission.SendSubmissionToProcessorAsync(submitReviewerRequest);
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "Parameters not matched.";
                return _operationResponse;
            }
        }

        /// <summary>
        /// Update submission status.
        /// </summary>
        /// <param name="submissionId">submission id.</param>
        /// <param name="submissionStatusId ">Not Assigned Yet =1, Not Started=2,In Progress Play=3,In Progress Paused=4,Under Query=5,Review Pending=6,
        /// Under Review Play=7,Under Review Paused=8,Review Fail=9,Review Pass=10,Submitted to PAS=11 .</param>
        /// <returns>IResponse.</returns>
        [HttpPost]
        [Route("updateSubmissionStatus")]
        [Authorize]
        public async Task<IResponse> UpdateSubmissionStatus(long submissionId, int submissionStatusId)
        {
            if (submissionId > 0 && Enum.IsDefined(typeof(SubmissionStatusType), submissionStatusId))
            {
                return await _submission.UpdateSubmissionStatusAsync(submissionId, submissionStatusId);
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "In-Valid submission/Case Number or submission status type.";
                return _operationResponse;
            }
        }

        /// <summary>
        /// Get submission general information data by its submission id.
        /// </summary>
        /// <param name="submissionId">Submission id of case.</param>
        /// <returns>Returns the submission general information details.</returns>
        [HttpGet]
        [Route("getSubmissionGeneralInformation/{submissionId}")]
        [Authorize]
        public async Task<IResponse> GetSubmissionGeneralInformation(long submissionId)
        {
            if (submissionId > 0)
            {
                return await _submission.GetSubmissionGeneralInformation(submissionId);
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "In-Valid submission id.";
                return _operationResponse;
            }
        }
    }
}
