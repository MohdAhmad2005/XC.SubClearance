using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using XC.CCMP.KeyVault;
using XC.XSC.Models.Entity.EmailInfo;
using XC.XSC.EMS.Connector;
using XC.XSC.EMS.Model;
using XC.XSC.Models.Entity.Prefrence;
using XC.XSC.Repositories.Comment;
using XC.XSC.Repositories.ReviewConfiguration;
using XC.XSC.Repositories.Submission;
using XC.XSC.Repositories.SubmissionAuditLog;
using XC.XSC.Repositories.SubmissionStatus;
using XC.XSC.Service.Comments;
using XC.XSC.Service.Lobs;
using XC.XSC.Service.Notification;
using XC.XSC.Service.PagerExtensions;
using XC.XSC.Service.Preferences;
using XC.XSC.Service.ReviewConfiguration;
using XC.XSC.Service.Sla;
using XC.XSC.Service.SubmissionAuditLog;
using XC.XSC.Service.SubmissionStatus;
using XC.XSC.Service.User;
using XC.XSC.UAM.Models;
using XC.XSC.UAM.UAM;
using XC.XSC.ViewModels.Comment;
using XC.XSC.ViewModels.CommentsClearance;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.Enum;
using XC.XSC.ViewModels.PagedModel;
using XC.XSC.ViewModels.ReviewConfiguration;
using XC.XSC.ViewModels.Sla;
using XC.XSC.ViewModels.Submission;
using XC.XSC.ViewModels.SubmissionAuditLog;
using XC.XSC.ViewModels.SubmissionStatus;
using XC.XSC.ViewModels.TanentAction;
using XC.XSC.ViewModels.TenantActionDetail;
using XC.XSC.ViewModels.User;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Drawing;
using XC.XSC.Models.Entity.Lob;
using Microsoft.Azure.Amqp.Framing;
using XC.XSC.Repositories.EmailInfo;
using XC.XSC.Data;
using XC.XSC.ViewModels.Submission.SubmissionGeneralInfo;

namespace XC.XSC.Service.Submission
{
    public class SubmissionService : ISubmissionService
    {
        private readonly IUserContext _userContext;
        private readonly ISubmissionRepository _submissionRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IPreferenceService _preferenceService;
        private readonly IResponse _operationResponse;
        private readonly ISubmissionStatusService _submissionStatusService;
        private readonly ISubmissionStatusRepository _submissionStatusRepository;
        private readonly ICommentService _commentService;
        private readonly ISubmissionAuditLogService _submissionAuditLogService;
        private readonly ISlaConfigurationService _slaConfigService;
        private readonly ISubmissionAuditLogRepository _submissionAuditLogRepository;
        private readonly IUamService _uamService;
        private readonly IReviewConfigurationRepository _reviewConfigurationRepository;
        private readonly IEMSClient _emsClient;
        private readonly ILobService _lobService;
        private readonly IReviewConfigurationService _reviewConfigurationService;
        private readonly INotificationService _notificationService;
        private readonly IEmailInfoRepository _emailInfoRepository;
        private readonly MSSqlContext _dbContext;

        public SubmissionService(ISubmissionRepository submissionRepository,
            IKeyVaultConfig keyVaultConfig,
            IPreferenceService preferenceService,
            IResponse operationResponse,
            ISubmissionStatusService submissionStatusService,
            ICommentRepository commentRepository,
            IUserContext userContext,
            ISubmissionStatusRepository submissionStatusRepository,
            ICommentService commentService,
            ISubmissionAuditLogService submissionAuditLogService,
            ISlaConfigurationService slaConfigService,
            ISubmissionAuditLogRepository submissionAuditLogRepository,
            IUamService uamService,
            IReviewConfigurationRepository reviewConfigurationRepository,
            IEMSClient emsClient,
            ILobService lobService,
            IReviewConfigurationService reviewConfigurationService,
            INotificationService notificationService,
            IEmailInfoRepository emailInfoRepository,
            MSSqlContext dbContext)
        {
            _submissionRepository = submissionRepository;
            _preferenceService = preferenceService;
            _operationResponse = operationResponse;
            _submissionStatusService = submissionStatusService;
            _commentRepository = commentRepository;
            _userContext = userContext;
            _submissionStatusRepository = submissionStatusRepository;
            _commentService = commentService;
            _submissionAuditLogService = submissionAuditLogService;
            _slaConfigService = slaConfigService;
            _submissionAuditLogRepository = submissionAuditLogRepository;
            _emsClient = emsClient;
            _lobService = lobService;
            _uamService = uamService;
            _reviewConfigurationRepository = reviewConfigurationRepository;
            _reviewConfigurationService = reviewConfigurationService;
            _notificationService = notificationService;
            _emailInfoRepository = emailInfoRepository;
            _dbContext = dbContext;
        }

        /// <summary>
        /// This method is implemented to get SubmissionsList contaning isInScoped status field.
        /// </summary>
        /// <returns></returns>
        public async Task<IResponse> GetSubmissionListAsync()
        {
            _operationResponse.Result = _submissionRepository.GetAll()
                                 .Where(t => t.TenantId == _userContext.UserInfo.TenantId)
                                .Select(p => new SubmissionResponse()
                                {
                                    SubmissionID = p.Id,
                                    AssignedTo = p.AssignedId,
                                    BrokerName = p.BrokerName,
                                    CaseNumber = p.CaseId,
                                    InsuredName = p.InsuredName,
                                    IsInScope = p.IsInScope,
                                    FromEmail = p.EmailInfo.FromEmail,
                                    StatusId = p.SubmissionStatusId,
                                    DueDate = p.DueDate,
                                    RecieveDate = p.EmailInfo.ReceivedDate,
                                }).ToList();
            if (_operationResponse.Result == null)
            {
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "No result found";
                return await Task.FromResult(_operationResponse);
            }
            _operationResponse.IsSuccess = true;
            _operationResponse.Message = "SUCCESS";
            return await Task.FromResult(_operationResponse);
        }

        /// <summary>
        /// Private method to get My-Performance related information Like Date, AssignedCount, CompletionCount, Accuracy and TatBreachedCount between selected date range.
        /// </summary>
        /// <param name="startDate">Start date of selected date range.</param>
        /// <param name="endDate">End date of selected date range.</param>
        /// <param name="region">This is region parameter.</param>
        /// <param name="lob">This is lob parameter.</param>
        /// <returns>Date, AssignedCount, CompletionCount, Accuracy and TatBreachedCount.</returns>
        private async Task<IResponse> GetMyPerformance(DateTime startDate, DateTime endDate, int region, int lob)
        {
            var submissionList = _submissionRepository.GetAll().Where(x => x.TenantId == _userContext.UserInfo.TenantId &&
                                                                           x.AssignedId == _userContext.UserInfo.UserId &&
                                                                           x.LobId == lob && x.RegionId == region &&
                                                                           x.IsActive == true &&
                                                                           x.CompletionDate.Value.Date >= startDate.Date &&
                                                                           x.CompletionDate.Value.Date <= endDate.Date).ToList()
                                                                           .GroupBy(x => x.CompletionDate.Value.Date)
                                                                           .Select(x => new PerformanceResponse
                                                                           {
                                                                               Date = x.Key,
                                                                               CompletedCount = x.Count(),
                                                                               TatBreachedCount = x.Where(y => y.CompletionDate.GetValueOrDefault().Date > y.DueDate.Date).Count(),
                                                                           }).ToList();
            var submissionAuditLogList = _submissionAuditLogRepository.GetAll().Where(x => x.TenantId == _userContext.UserInfo.TenantId &&
                                                                        x.NewAssignedToId == _userContext.UserInfo.UserId &&
                                                                        x.NewStatus == (int)SubmissionStatusType.NotStarted &&
                                                                        x.CreatedDate.Date >= startDate.Date &&
                                                                        x.CreatedDate.Date <= endDate.Date).
                                                                        Join(_submissionRepository.GetAll().
                                                                        Where(a => a.TenantId == _userContext.UserInfo.TenantId &&
                                                                                   a.RegionId == region && a.LobId == lob), 
                                                                                   sa => sa.SubmissionId, s => s.Id,
                                                                                   (sa,s) => new {s.Id,sa.SubmissionId,sa.CreatedDate}).
                                                                         GroupBy(x =>x.CreatedDate.Date).
                                                                         Select(y => new PerformanceResponse
                                                                         {
                                                                            Date = y.Key,
                                                                            AssignedCount = y.Count(),
                                                                         }).ToList();
            if ((submissionList != null && submissionList.Any()) || (submissionAuditLogList != null && submissionAuditLogList.Any()))
            {
                _operationResponse.Result = submissionAuditLogList.Union(submissionList).GroupBy(x => x.Date).
                                                                       Select(x => new PerformanceResponse
                                                                       {
                                                                           Date = x.Key.Date,
                                                                           AssignedCount = x.Sum(y => y.AssignedCount),
                                                                           CompletedCount = x.Sum(y => y.CompletedCount),
                                                                           Accuracy = 100 + "%",
                                                                           TatBreachedCount = x.Sum(y => y.TatBreachedCount)
                                                                       }).OrderBy(x => x.Date).ToList();
                _operationResponse.IsSuccess = true;
                _operationResponse.Message = "SUCCESS";
                return _operationResponse; 
            }
            _operationResponse.IsSuccess = false;
            _operationResponse.Message = "No record found for My Performnace.";
            _operationResponse.Result = null;
            return _operationResponse;
        }

        /// <summary>
        /// Private method to get Team-Performance related information Like ProcessorName, AssignedCount, CompletionCount, Accuracy and TatBreachedCount between selected date range.
        /// </summary>
        /// <param name="startDate">Start date of selected date range.</param>
        /// <param name="endDate">End date of selected date range.</param>
        /// <param name="region">This is region parameter.</param>
        /// <param name="lob">This is lob parameter.</param>
        /// <returns>ProcessorName, AssignedCount, CompletionCount, Accuracy and TatBreachedCount.</returns>
        private async Task<IResponse> GetTeamPerformance(DateTime startDate, DateTime endDate, int region, int lob)
        {
            var submissionList = _submissionRepository.GetAll().Where(x => x.TenantId == _userContext.UserInfo.TenantId &&
                                                                            x.IsActive == true && x.LobId == lob &&
                                                                            x.RegionId == region &&
                                                                            x.CompletionDate.Value.Date >= startDate.Date &&
                                                                            x.CompletionDate.Value.Date <= endDate.Date &&
                                                                            x.AssignedId != string.Empty && x.AssignedId != null
                                                                            ).ToList().GroupBy(x => x.AssignedId).Select(x => new PerformanceResponse
                                                                            {
                                                                                  ProcessorName = x.Key,
                                                                                  CompletedCount = x.Count(),
                                                                                  TatBreachedCount = x.Where(y => y.CompletionDate.Value.Date > y.DueDate.Date).Count(),
                                                                            }).ToList();
            var submissionAuditLogList = _submissionAuditLogRepository.GetAll().Where(x => x.TenantId == _userContext.UserInfo.TenantId &&
                                                                              x.CreatedDate.Date >= startDate.Date && x.CreatedDate.Date <= endDate.Date &&
                                                                              x.NewAssignedToId != string.Empty && x.NewAssignedToId != null &&
                                                                              x.NewStatus == (int)SubmissionStatusType.NotStarted
                                                                              ).ToList().Join(_submissionRepository.GetAll().
                                                                              Where(a => a.TenantId == _userContext.UserInfo.TenantId &&
                                                                                         a.RegionId == region && a.LobId == lob), sa => sa.SubmissionId, s => s.Id,
                                                                                         (sa, s) => new { sa.CreatedDate, sa.NewAssignedToId}).ToList().
                                                                                         GroupBy(x => x.NewAssignedToId).
                                                                                         Select(x => new PerformanceResponse
                                                                                         {
                                                                                            ProcessorName = x.Key,
                                                                                            AssignedCount = x.Count()
                                                                                         }).ToList();
            if((submissionList != null && submissionList.Any()) || (submissionAuditLogList != null && submissionAuditLogList.Any()))
            {
                var userList = (List<UserResponseResult>)(await _uamService.GetUsersByFilters(new UserFilterRequest())).Result;
                if(userList != null && userList.Any())
                {
                   _operationResponse.Result = submissionList.Union(submissionAuditLogList).ToList().GroupBy(x => x.ProcessorName).
                                                 Select(y => new PerformanceResponse
                                                 {
                                                     ProcessorName = userList.Where(x1 => x1.Id == y.Key).Select(y => y.Name).FirstOrDefault(),
                                                     AssignedCount = y.Sum(z => z.AssignedCount),
                                                     CompletedCount = y.Sum(z => z.CompletedCount),
                                                     Accuracy = 100 + "%",
                                                     TatBreachedCount = y.Sum(z => z.TatBreachedCount)
                                                 }).ToList().OrderBy(o => o.ProcessorName);
                    _operationResponse.IsSuccess = true;
                    _operationResponse.Message = "SUCCESS";
                    return _operationResponse;
                }
            }
            _operationResponse.IsSuccess = false;
            _operationResponse.Message = "No record found for Team Performance.";
            _operationResponse.Result = null;
            return _operationResponse;
        }
        /// <summary>
        /// This method is implemented to get the Performance related information for My-Performance and Team-Performance.
        /// </summary>
        /// <param name="startDate">Start date of selected date range.</param>
        /// <param name="endDate">End date of selected date range.</param>
        /// <param name="performanceType">Performance Type Like My-Performance or Team-Performance.</param>
        /// <param name="region">This is region parameter.</param>
        /// <param name="lob">This is lob parameter.</param>
        /// <returns>Date/ProcessorName, AssignedCount, CompletionCount, Accuracy and TatBreachedCount.</returns>
        public async Task<IResponse> GetPerformanceAsync(DateTime startDate, DateTime endDate, PerformanceType performanceType, int region, int lob)
        {
            if (performanceType == PerformanceType.MyPerformance)
            {
                return await GetMyPerformance(startDate, endDate,region,lob);
            }
            else if (performanceType == PerformanceType.TeamPerformance)
            { 
                return await GetTeamPerformance(startDate,endDate,region,lob);
            }
            _operationResponse.IsSuccess = false;
            _operationResponse.Message = "Invalid performance chart type.";
            _operationResponse.Result = null;
            return _operationResponse;
        }

        /// <summary>
        /// This method is implemented to get the TotalCount,InScopeCount and OutScopeCount of submissions based on selected date range.
        /// </summary>
        /// <param name="startDate">This is Start date of selected date range.</param>
        /// <param name="endDate">This is End date of selected date range.</param>
        /// <param name="region">This is region parameter.</param>
        /// <param name="lob">This is lob parameter.</param>
        /// <returns>TotalCount, InScopeCount and OutScopeCount</returns>
        public async Task<IResponse> GetSubmissionScopeCountAsync(DateTime startDate, DateTime endDate, int region, int lob)
        {
            var SubmissionList = _submissionRepository.GetAll().Where(x => x.TenantId == _userContext.UserInfo.TenantId
                                                                      && x.RegionId==region && x.LobId==lob && x.IsActive==true
                                                                      && x.EmailInfo.IsActive == true 
                                                                      && x.EmailInfo.ReceivedDate.Date >= startDate.Date 
                                                                      && x.EmailInfo.ReceivedDate.Date <= endDate.Date);
            if (SubmissionList != null && SubmissionList.Any())
            {
                var SubmissionCount = new SubmissionScopeCountResponse()
                {
                    TotalCount = SubmissionList.Count(),
                    InScopeCount = SubmissionList.Where(x => x.IsInScope == true).Count(),
                    OutScopeCount = SubmissionList.Where(x => x.IsInScope == false).Count()
                };
                _operationResponse.IsSuccess = true;
                _operationResponse.Message = "SUCCESS";
                _operationResponse.Result = SubmissionCount;
                return await Task.FromResult(_operationResponse);
            }
            _operationResponse.IsSuccess = false;
            _operationResponse.Message = "No data found for e-mails picked for automation.";
            _operationResponse.Result = null;
            return await Task.FromResult(_operationResponse);
        }

        public async Task<Models.Entity.Submission.Submission> SaveSubmission(SubmissionRequest submissionRequest)
        {
            Models.Entity.Submission.Submission submission = new Models.Entity.Submission.Submission()
            {
                CaseId = submissionRequest.CaseId,
                BrokerName = submissionRequest.BrokerName,
                InsuredName = submissionRequest.InsuredName,
                EmailInfoId = submissionRequest.EmailInfoId,
                DueDate = submissionRequest.DueDate,
                AssignedId = submissionRequest.AssignedId,
                SubmissionStatusId = submissionRequest.SubmissionStatusId,
                SubmissionStageId = submissionRequest.SubmissionStageId,
                IsInScope = submissionRequest.IsInScope,
                LobId = submissionRequest.LobId,
                ExtendedDate = submissionRequest.ExtendedDate,
                EmailBody = submissionRequest.EmailBody,
                TaskId = submissionRequest.TaskId,
                TenantId = _userContext.UserInfo.TenantId,
                CreatedDate = submissionRequest.CreatedDate,
                CreatedBy = _userContext.UserInfo.UserId,
                IsActive = submissionRequest.IsActive,
            };

            await _submissionRepository.AddAsync(submission);
            return submission;

        }

        public async Task<TaskTatMetricsResponse> GetTaskTatMetricsAsync(long submissionId)
        {
            TaskTatMetricsResponse taskTatMetricsResponse = new TaskTatMetricsResponse();

            var submission = await _submissionRepository.GetSingleAsync(a => a.Id == submissionId);
            if (submission != null)
            {
                taskTatMetricsResponse.ReceivedDate = submission.CreatedDate;

                var lob = _lobService.GetLobById(submission.LobId).Result;
                if (lob == null)
                {
                    throw new Exception($"GetTaskTatMetricsAsync(): Lob does not exists.");
                }

                IResponse mailBoxResponse = await _emsClient.GetMailBoxList(submission.RegionId, lob.LOBID, submission.TeamId, submission.TenantId);
                if (mailBoxResponse.Result == null || !mailBoxResponse.IsSuccess)
                {
                    throw new Exception($"GetTaskTatMetricsAsync(): MailBox does not exists.");
                }
                else
                {
                    List<EmailBoxResponse> emailBoxes = (List<EmailBoxResponse>)mailBoxResponse.Result;
                    var emailBox = emailBoxes.Select(e => e).Where(e => e.MailboxEmailID.Equals(submission.EmailInfo.MailboxName));
                    if (emailBox.Any())
                    {
                        EmailBoxResponse? emailBoxResponse = emailBox.FirstOrDefault();
                        if (emailBoxResponse != null)
                        {
                            IResponse slaConfig = await _slaConfigService.GetSlaConfiguration(submission.RegionId, submission.TeamId, submission.LobId, SlaType.TAT, emailBoxResponse.MailBoxId, submission.TenantId);
                            if (slaConfig.Result != null || mailBoxResponse.IsSuccess)
                            {
                                taskTatMetricsResponse.DefinedTat = ((SlaConfigurationResponse)slaConfig.Result).Day;
                            }
                        }
                        else
                        {
                            throw new Exception($"GetTaskTatMetricsAsync(): MailBox does not found.");
                        }

                    }
                }

                taskTatMetricsResponse.DueDate = submission.DueDate;

                List<Preference> preferencelist = _preferenceService.GetPreferenceByTenantAsync(_userContext.UserInfo.TenantId).Result;
                taskTatMetricsResponse.BusinessDaysText = preferencelist.Where(x => x.Key == PreferenceConstants.BusinessDays).FirstOrDefault().Value;
                string completionStatusIdValue = preferencelist.Where(x => x.Key == PreferenceConstants.CompletionStatus).FirstOrDefault().Value;
                if (!string.IsNullOrEmpty(completionStatusIdValue))
                {
                    int completionStatusId = 0;
                    int.TryParse(completionStatusIdValue, out completionStatusId);

                    //completed
                    if (submission.SubmissionStatusId == completionStatusId)
                    {
                        DateTime? CompletionDate = submission.CompletionDate;
                        if (CompletionDate != null)
                        {
                            if (taskTatMetricsResponse.DueDate < CompletionDate)
                            {
                                taskTatMetricsResponse.TatMissed = true;
                            }
                        }
                        taskTatMetricsResponse.DaysOverdue = 0;
                    }
                    else
                    {
                        if (taskTatMetricsResponse.DueDate < DateTime.Now)
                        {
                            taskTatMetricsResponse.TatMissed = true;
                            taskTatMetricsResponse.DaysOverdue = (DateTime.Now.AddDays(1) - taskTatMetricsResponse.DueDate).Days;

                        }
                    }
                }
                else
                {
                    if (taskTatMetricsResponse.DueDate < DateTime.Now)
                    {
                        taskTatMetricsResponse.TatMissed = true;
                        taskTatMetricsResponse.DaysOverdue = (DateTime.Now.AddDays(1) - taskTatMetricsResponse.DueDate).Days;

                    }
                }
            }
            return taskTatMetricsResponse;
        }

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
        /// <param name="cancellationToken">cancellation token.</param>
        /// <returns>return the submission list after implementing requested filters as common IResponse.</returns>
        public async Task<IResponse> GetSubmissions(string? caseNumber, string? brokerName, string? insuredName, int? statusId, string? assignedTo,
            DateTime? receivedFromDate, DateTime? receivedToDate, DateTime? dueFromDate, DateTime? dueToDate,
            int page, int limit, string? sortField, int sortOrder, bool isInScope, int region, int lob, bool isMySubmission, bool isNewSubmission, CancellationToken cancellationToken)
        {
            List<UserResponseResult> userInfo = new List<UserResponseResult>();
            UserFilterRequest userFilterRequest = new UserFilterRequest();
            var usersList = await _uamService.GetUsersByFilters(userFilterRequest);
            if (usersList.Result != null)
            {
                userInfo = (List<UserResponseResult>)usersList.Result;
            }
            var CommentResponse = _commentRepository.GetAll().Where(x=>x.TenantId==_userContext.UserInfo.TenantId && x.IsActive==true).ToList();

            var EntityResult = _submissionRepository.GetAll().Where(k => k.TenantId == _userContext.UserInfo.TenantId
                                                                      && k.IsActive == true && k.IsInScope == isInScope
                                                                      && k.EmailInfo.IsActive == true
                                                                      && k.RegionId == region && k.LobId == lob);

            if (isMySubmission)
            {
                EntityResult = EntityResult.Where(t => t.AssignedId == _userContext.UserInfo.UserId);
            }
            if (isNewSubmission)
            {
                EntityResult = EntityResult.Where(t => string.IsNullOrEmpty(t.AssignedId) && string.IsNullOrEmpty(t.ReviewerId));
            }
            if (!string.IsNullOrWhiteSpace(caseNumber))
            {
                EntityResult = EntityResult.Where(t => t.CaseId.ToLower().Trim().Contains(caseNumber.Trim().ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(brokerName))
            {
                EntityResult = EntityResult.Where(t => t.BrokerName.ToLower().Trim().Contains(brokerName.Trim().ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(insuredName))
            {
                EntityResult = EntityResult.Where(t => t.InsuredName.ToLower().Trim().Contains(insuredName.Trim().ToLower()));
            }
            if (statusId > 0)
            {
                EntityResult = EntityResult.Where(t => t.SubmissionStatusId == statusId);
            }
            if (!string.IsNullOrWhiteSpace(assignedTo))
            {
                EntityResult = EntityResult.Where(t => t.AssignedId.ToLower() == assignedTo.Trim().ToLower());
            }
            if (receivedFromDate != null)
            {
                EntityResult = EntityResult.Where(d => d.EmailInfo.ReceivedDate.Date >= receivedFromDate.Value.Date);
            }
            if (receivedToDate != null)
            {
                EntityResult = EntityResult.Where(m => m.EmailInfo.ReceivedDate.Date <= receivedToDate.Value.Date);
            }
            if (dueFromDate != null)
            {
                EntityResult = EntityResult.Where(s => s.DueDate.Date >= dueFromDate.Value.Date);
            }
            if (dueToDate != null)
            {
                EntityResult = EntityResult.Where(k => k.DueDate.Date <= dueToDate.Value.Date);
            }
            var submission = await EntityResult.PaginateAsync(page, limit, sortOrder, sortField, cancellationToken);

            if (submission.Items.Count > 0)
            {
                _operationResponse.Result = new SubmissionList
                {
                    CurrentPage = submission.CurrentPage,
                    TotalPages = submission.TotalPages,
                    TotalItems = submission.TotalItems,
                    Submissions = submission.Items.Select(p => new SubmissionResponse
                    {
                        SubmissionID = p.Id,
                        AssignedTo = (userInfo.Count > 0) ? userInfo.Where(r => r.Id == p.AssignedId).
                                     Select(s => string.Format("{0} {1}", s.FirstName, s.LastName)).FirstOrDefault() : string.Empty,
                        BrokerName = p.BrokerName,
                        CaseNumber = p.CaseId,
                        InsuredName = p.InsuredName,
                        FromEmail = p.EmailInfo.FromEmail,
                        StatusId = p.SubmissionStatusId,
                        DueDate = p.DueDate,
                        RecieveDate = p.EmailInfo.ReceivedDate,
                        StatusColor = p.SubmissionStatus.Color,
                        StatusLabel = p.SubmissionStatus.Label,
                        StatusName = p.SubmissionStatus.Name,
                        StageId = p.SubmissionStageId,
                        StageLabel = p.SubmissionStage.Label,
                        StageColor = p.SubmissionStage.Color,
                        StageName = p.SubmissionStage.Name,
                        LobId = p.LobId,
                        TeamId = p.TeamId,
                        RegionId = p.RegionId,
                        Comments= p.Comment?.Where(x => x.CommentTypeId.Equals(Convert.ToInt32(CommentType.OutOfScope)))?.FirstOrDefault()?.CommentText

                    }).ToList()
                };
                _operationResponse.IsSuccess = true;
                _operationResponse.Message = "SUCCESS";
                return _operationResponse;
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "No result found";
                return _operationResponse;
            }

        }

        ///<summary>
        ///Get submissions glance based on corresponding user tenant id.
        /// </summary>
        /// <returns>return the submission status response as common IResponse.</returns>
        public async Task<IResponse> GetSubmissionsGlanceAsync(int region, int lob)
        {
            var allSubmissionStatus = await _submissionStatusService.GetAllSubmissionStatusAsync(_userContext.UserInfo.TenantId);
            var allSubmissions = _submissionRepository.GetAll().Where(item => item.TenantId == _userContext.UserInfo.TenantId && item.IsActive && item.IsInScope == true && item.RegionId == region && item.LobId == lob);
            if (allSubmissionStatus.Any() && allSubmissions.Any())
            {
                var result = (from ss in allSubmissionStatus
                              join s in allSubmissions on ss.Id equals s.SubmissionStatusId into values
                              from collection in values.DefaultIfEmpty()
                              group collection by new { ss.Id, ss.Color, ss.Name, ss.Label } into rowValues
                              select new SubmissionStatusResponse
                              {
                                  Id = rowValues.Key.Id,
                                  Name = rowValues.Key.Name,
                                  Label = rowValues.Key.Label,
                                  Color = rowValues.Key.Color,
                                  StatusCount = rowValues.Count(item => item != null)
                              }).ToList();
                if (result.Any())
                {
                    _operationResponse.Result = result;
                    _operationResponse.IsSuccess = true;
                    _operationResponse.Message = "SUCCESS";
                    return _operationResponse;
                }
                else
                {
                    _operationResponse.Result = null;
                    _operationResponse.IsSuccess = false;
                    _operationResponse.Message = "Sorry no result found";
                    return _operationResponse;
                }
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "Sorry something went wrong please try again";
                return _operationResponse;
            }

        }

        ///<summary>
        ///Assign submission to the corresponding user by submission id.
        /// </summary>
        ///<param name="submissionId">submission id.</param>
        ///<param name="userId">user id of the login user.</param>
        /// <returns>return the corresponding submission response as common IResponse.</returns>
        public async Task<IResponse> AssignSubmissionAsync(long submissionId, string userId)
        {
            var submission = await _submissionRepository
                                    .GetSingleAsync(a => a.Id == submissionId && a.IsActive == true
                                                    && a.TenantId == _userContext.UserInfo.TenantId);
            var prevCloneId = new SubmissionCloneData();
            prevCloneId.PrevStatusId = submission.SubmissionStatusId;
            if (submission.AssignedId == null)
            {
                prevCloneId.PrevAssignedId = _userContext.UserInfo.UserId;
            }
            else
            {
                prevCloneId.PrevAssignedId = submission.AssignedId;
            }

            if (submission != null)
            {
                submission.AssignedId = userId;
                submission.ModifiedBy = userId;
                submission.ModifiedDate = DateTime.Now;
                submission.SubmissionStatusId = (int)SubmissionStatusType.NotStarted;
                _operationResponse.IsSuccess = true;
                _operationResponse.Message = "SUCCESS";
                await _submissionRepository.UpdateAsync(submission);

                //To insert the data in Submission Audit log table 
                Models.Entity.SubmissionAuditLog.SubmissionAuditLog submissionAuditLogRequest = new Models.Entity.SubmissionAuditLog.SubmissionAuditLog()
                {
                    SubmissionId = submission.Id,
                    NewStatus = submission.SubmissionStatusId,
                    NewAssignedToId = submission.AssignedId,
                    PrevStatus = prevCloneId.PrevStatusId,
                    PrevAssignedToId = prevCloneId.PrevAssignedId,
                    TenantId = _userContext.UserInfo.TenantId,
                    CreatedBy = _userContext.UserInfo.UserId,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                };
                await _submissionAuditLogService.SaveSubmissionAuditLog(submissionAuditLogRequest);
                if (_operationResponse.Result != null)
                {
                    Models.Entity.SubmissionAuditLog.SubmissionAuditLog submissionAuditLog = (Models.Entity.SubmissionAuditLog.SubmissionAuditLog)_operationResponse.Result;
                    if (submissionAuditLog.Id > 0)
                    {
                        await _notificationService.SendNotification(submission: submission, userId: userId, templateKey: "ASSIGNTOMYSELF");
                    }
                }

                return _operationResponse;
            }
            else
            {
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "Sorry invalid submission Id";
                _operationResponse.Result = null;
                return _operationResponse;
            }
        }
        /// <summary>
        /// using SubmissionUnderQuery this feature move the particular  submission into under query and also add comments for putting submssion to under query and added comments will also be display under submission general information. this feature will only be available when submission  status is any of the (ReviewFail,ReviewPass,InProgress(Paused),InProgress (Play)
        /// /// Once UnderQuery  activity happen then coressponding data insert in SubmissionAuditLog 
        /// </summary>
        /// <param name="commentRequest">CommentType  Remark = 1, Query = 2, Review = 3, OutOfScope = 4,Comment Text,Submission Id,JsonData</param>
        /// <returns></returns>
        public async Task<IResponse> SaveUnderQuery(CommentRequest commentRequest)
        {
            var submission = await _submissionRepository.GetSingleAsync(t => t.Id == commentRequest.SubmissionId && t.TenantId == _userContext.UserInfo.TenantId);

            if (submission != null)
            {
                var prevCloneId = new SubmissionCloneData();
                prevCloneId.PrevStatusId = submission.SubmissionStatusId;
                if (submission.AssignedId == null)
                {
                    prevCloneId.PrevAssignedId = _userContext.UserInfo.UserId;
                }
                else
                {
                    prevCloneId.PrevAssignedId = submission.AssignedId;
                }

                if (submission.SubmissionStatusId == (int)SubmissionStatusType.ReviewFail || submission.SubmissionStatusId == (int)SubmissionStatusType.ReviewPass || submission.SubmissionStatusId == (int)SubmissionStatusType.InProgressPaused || submission.SubmissionStatusId == (int)SubmissionStatusType.InProgressPlay)
                {
                    Models.Entity.Comment.Comment comments = new Models.Entity.Comment.Comment()
                    {
                        CommentText = commentRequest.CommentText,
                        CommentTypeId = (int)(CommentType)System.Enum.Parse(typeof(CommentType), commentRequest.CommentType),
                        TenantId = _userContext.UserInfo.TenantId,
                        CreatedBy = _userContext.UserInfo.UserId,
                        CreatedDate = DateTime.Now,
                        SubmissionId = commentRequest.SubmissionId,
                        JsonData = commentRequest.JsonData,
                        IsActive = true,
                    };
                    await _commentRepository.AddAsync(comments);

                    var submissionStatus = await _submissionStatusRepository.GetSingleAsync(k => k.TenantId == _userContext.UserInfo.TenantId && k.Id == (int)SubmissionStatusType.UnderQuery);

                    submission.SubmissionStatusId = (submissionStatus != null ? submissionStatus.Id : (int)SubmissionStatusType.UnderQuery);
                    submission.ModifiedBy = _userContext.UserInfo.UserId;
                    submission.ModifiedDate = DateTime.Now;
                    await _submissionRepository.UpdateAsync(submission);

                    _operationResponse.IsSuccess = true;
                    _operationResponse.Message = "SUCCESS";

                    //To insert the data in Submission Audit log table 
                    Models.Entity.SubmissionAuditLog.SubmissionAuditLog submissionAuditLogRequest = new Models.Entity.SubmissionAuditLog.SubmissionAuditLog()
                    {
                        SubmissionId = submission.Id,
                        NewStatus = submission.SubmissionStatusId,
                        NewAssignedToId = _userContext.UserInfo.UserId,
                        PrevStatus = prevCloneId.PrevStatusId,
                        PrevAssignedToId = prevCloneId.PrevAssignedId,
                        TenantId = _userContext.UserInfo.TenantId,
                        CreatedBy = _userContext.UserInfo.UserId,
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                    };
                    await _submissionAuditLogService.SaveSubmissionAuditLog(submissionAuditLogRequest);

                }
                else
                {
                    _operationResponse.IsSuccess = false;
                }

            }
            else
            {
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "submission against this case number not found.";

            }

            return _operationResponse;

        }

        /// <summary>
        /// "Send back to Queue" feature:  the user will be able to  send submission back to queue. this feature will only be availabe  when  submission status is any of the  ("Not Started ", and In Progress (Paused)).
        /// </summary>
        /// <param name="submissionId">Use as Submission ID</param>
        /// <returns>IResponse as success while udpate Status and </returns>
        /// 
        public async Task<IResponse> SendAssignedSubmissionBackToQueue(long submissionId)
        {

            var submission = await _submissionRepository.GetSingleAsync(t => t.Id == submissionId && t.TenantId == _userContext.UserInfo.TenantId);
            var prevCloneId = new SubmissionCloneData();
            string? assignedId = null;
            prevCloneId.PrevStatusId = submission.SubmissionStatusId;
            if (submission.AssignedId == null)
            {
                prevCloneId.PrevAssignedId = _userContext.UserInfo.UserId;
            }
            else
            {
                prevCloneId.PrevAssignedId = submission.AssignedId;
            }
            if (submission != null)
            {

                if (submission.SubmissionStatusId == (int)SubmissionStatusType.NotStarted || submission.SubmissionStatusId == (int)SubmissionStatusType.InProgressPaused)
                {
                    assignedId = submission.AssignedId;                    
                    var submissionStatus = await _submissionStatusRepository.GetSingleAsync(k => k.TenantId == _userContext.UserInfo.TenantId && k.Id == (int)SubmissionStatusType.NotAssignedYet);

                    submission.AssignedId = null;
                    submission.SubmissionStatusId = submissionStatus.Id;
                    submission.ModifiedBy = _userContext.UserInfo.UserId;
                    submission.ModifiedDate = DateTime.Now;
                    await _submissionRepository.UpdateAsync(submission);

                    _operationResponse.IsSuccess = true;
                    _operationResponse.Message = "SUCCESS";

                    //To insert the data in Submission Audit log table 
                    Models.Entity.SubmissionAuditLog.SubmissionAuditLog submissionAuditLogRequest = new Models.Entity.SubmissionAuditLog.SubmissionAuditLog()
                    {
                        SubmissionId = submission.Id,
                        NewStatus = submission.SubmissionStatusId,
                        NewAssignedToId = _userContext.UserInfo.UserId,
                        PrevStatus = prevCloneId.PrevStatusId,
                        PrevAssignedToId = prevCloneId.PrevAssignedId,
                        TenantId = _userContext.UserInfo.TenantId,
                        CreatedBy = _userContext.UserInfo.UserId,
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                    };
                    await _submissionAuditLogService.SaveSubmissionAuditLog(submissionAuditLogRequest);
                }
                if (_operationResponse.Result != null)
                {
                    Models.Entity.SubmissionAuditLog.SubmissionAuditLog submissionAuditLog = (Models.Entity.SubmissionAuditLog.SubmissionAuditLog)_operationResponse.Result;
                    if (submissionAuditLog.Id > 0)
                    {
                        if (!string.IsNullOrEmpty(assignedId))
                        {
                            if (assignedId == _userContext.UserInfo.UserId)
                            {
                                //Processor notification by you
                                await _notificationService.SendNotification(submission: submission, userId: _userContext.UserInfo.UserId, templateKey: "SNTBCKQUEUEPROC");
                            }
                            else
                            {
                                //Processor notification by name
                                await _notificationService.SendNotification(submission: submission, userId: assignedId, templateKey: "SNTBCKQUEUE");
                            }

                            string AllocatorUserId = await GetAllocatorUserId(assignedId);
                            if (AllocatorUserId != _userContext.UserInfo.UserId)
                            {
                                //Allocator notification by name
                                await _notificationService.SendNotification(submission: submission, userId: AllocatorUserId, templateKey: "SNTBCKQUEUE");
                            }
                        }
                    }
                }
            }
            else
            {
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "submission against this case number not found.";

            }
            return _operationResponse;

        }


        /// <summary>
        /// Save Comment based on CommentType Clearance and update ClearanceConscent in Submission
        /// </summary>
        /// <param name="commentsClearanceRequest">CommentType Clearance = 5 , Comment Text,SubmissionId,ClearanceConscent</param>
        /// <returns> IResponse  as SUCCESS while add comment.  </returns>
        public async Task<IResponse> AddClearanceCommentAsync(CommentsClearanceRequest commentsClearanceRequest)
        {
            var submission = await _submissionRepository.GetSingleAsync(t => t.Id == commentsClearanceRequest.SubmissionId);

            if (submission != null)
            {
                await _commentService.AddComment(new CommentRequest
                {
                    CommentText = commentsClearanceRequest.CommentText,
                    CommentType = CommentType.Clearance.ToString(),
                    SubmissionId = commentsClearanceRequest.SubmissionId
                });

                submission.ClearanceConscent = commentsClearanceRequest.ClearanceConscent;
                await _submissionRepository.UpdateAsync(submission);

                _operationResponse.IsSuccess = true;
                _operationResponse.Message = "SUCCESS";
            }
            else
            {
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "submission against this case number not found.";

            }

            return _operationResponse;
        }

        /// <summary>
        /// Get submission detail by its submission id.
        /// </summary>
        /// <param name="submissionId">Submission id of case.</param>
        /// <returns>Returns the submission details.</returns>
        public async Task<IResponse> GetInScopeSubmissionById(long submissionId)
        {
            var submissionQuery = _submissionRepository.GetAll()
                .Where(k => k.Id == submissionId)
                .Where(k => k.TenantId == _userContext.UserInfo.TenantId && k.IsActive == true && k.IsInScope == true)
                .Select(p => new SubmissionResponse
                {
                    SubmissionID = p.Id,
                    AssignedTo = p.AssignedId,
                    BrokerName = p.BrokerName,
                    CaseNumber = p.CaseId,
                    InsuredName = p.InsuredName,
                    FromEmail = p.EmailInfo.FromEmail,
                    StatusId = p.SubmissionStatusId,
                    DueDate = p.DueDate,
                    RecieveDate = p.EmailInfo.ReceivedDate,
                    StatusColor = p.SubmissionStatus.Color,
                    StatusLabel = p.SubmissionStatus.Label,
                    StatusName = p.SubmissionStatus.Name,
                    StageId = p.SubmissionStageId,
                    StageLabel = p.SubmissionStage.Label,
                    StageColor = p.SubmissionStage.Color,
                    StageName = p.SubmissionStage.Name,
                    EmailInfoId = p.EmailInfoId,
                    /*LobId = p.Lob.LOBID,*/
                LobName = p.Lob.Name,
                    ClearanceConsent = p.ClearanceConscent,
                    isDataCompleted = p.isDataCompleted
                });

            SubmissionResponse? submission = await submissionQuery.FirstOrDefaultAsync();

            if (submission != null)
            {
                _operationResponse.Result = submission;
                _operationResponse.IsSuccess = true;
                _operationResponse.Message = "SUCCESS";
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "No result found";
            }

            return _operationResponse;
        }

        /// <summary>
        /// Send submission to review.
        /// </summary>
        /// <param name="submitProcessorRequest">It's a request model which consists CommentText, CommentType, SubmissionId, JsonData.</param>
        /// <returns>Return IsSuccess, Message and Result as IResponse.</returns>       
        public async Task<IResponse> SendSubmissionToReviewAsync(SubmitProcessorRequest submitProcessorRequest)
        {
            var submission = await _submissionRepository.GetSingleAsync(t => t.Id == submitProcessorRequest.SubmissionId && t.TenantId == _userContext.UserInfo.TenantId
                             && t.IsActive);
            if (submission != null)
            {
                var reviewerDetails = await _reviewConfigurationService.GetReviewConfig(null, true);
                if (reviewerDetails.IsSuccess && reviewerDetails.Result != null)
                {
                    SlaConfigurationResponse slaDayDetails = new SlaConfigurationResponse();
                    var result = reviewerDetails.Result as ReviewConfigurationResponse;
                    if (result.ReviewTypeId == (int)ReviewType.PercentageReview)
                    {
                        IResponse mailBoxResponse = await _emsClient.GetMailBoxList(submission.RegionId, submission.Lob.LOBID, submission.TeamId, submission.TenantId);
                        if (mailBoxResponse.Result != null && mailBoxResponse.IsSuccess)
                        {
                            var emailBox = mailBoxResponse.Result as List<EmailBoxResponse>;
                            var mailBoxDetails = emailBox.Where(item => item.MailboxEmailID == submission.EmailInfo.MailboxName).FirstOrDefault();
                            if (mailBoxDetails != null)
                            {
                                SlaConfigurationResponse percentageDetails = new SlaConfigurationResponse();
                                var slaPercentage = await _slaConfigService.GetSlaConfiguration(submission.RegionId, submission.TeamId, submission.LobId, SlaType.Accuracy, mailBoxDetails.Id, submission.TenantId);
                                if (slaPercentage.Result != null && slaPercentage.IsSuccess)
                                {
                                    percentageDetails = slaPercentage.Result as SlaConfigurationResponse;
                                }
                                var slaDays = await _slaConfigService.GetSlaConfiguration(submission.RegionId, submission.TeamId, submission.LobId, SlaType.TAT, mailBoxDetails.Id, submission.TenantId);
                                if (slaDays.Result != null && slaDays.IsSuccess)
                                {
                                    slaDayDetails = slaDays.Result as SlaConfigurationResponse;
                                }
                                if (percentageDetails.SamplePercentage > 0 && slaDayDetails.Day > 0)
                                {
                                    var reviewList = await _reviewConfigurationService.GetAllReviewConfigurationAsync();
                                    if (reviewList.IsSuccess && reviewList.Result != null)
                                    {
                                        var reviewfilteredList = reviewList.Result as List<ReviewConfigurationResponse>;
                                        var reviewPercentageList = reviewfilteredList.Where(item => item.ReviewTypeId == (int)ReviewType.PercentageReview && item.TeamId == _userContext.UserInfo.TeamId).ToList();
                                        if (reviewPercentageList != null && reviewPercentageList.Count > 0)
                                        {
                                            var processorIDs = reviewPercentageList.Select(x => x.ProcessorId).ToList();
                                            var submissionAuditLogCount = (from records in _dbContext.SubmissionAuditLog
                                                                            where processorIDs.Contains(records.PrevAssignedToId) && records.NewStatus
                                                                          == (int)SubmissionStatusType.ReviewPending &&
                                            records.ModifiedDate >= DateTime.Now.AddDays(-slaDayDetails.Day) && records.IsActive && records.TenantId == _userContext.UserInfo.TenantId
                                                                           select records).Count();
                                            var submissionCount = (from sub in _dbContext.Submissions
                                                                   join email in _dbContext.EmailInfos on sub.EmailInfoId equals email.Id
                                                                   where email.MailboxName == submission.EmailInfo.MailboxName && sub.IsInScope &&
                                                                   sub.CreatedDate >= DateTime.Now.AddDays(-slaDayDetails.Day) && sub.IsActive && sub.TenantId == _userContext.UserInfo.TenantId
                                                                   && email.IsActive && email.TenantId == _userContext.UserInfo.TenantId
                                                                   select sub).Count();
                                            if (submissionAuditLogCount >= (submissionCount * percentageDetails.SamplePercentage / 100) && submission.ClearanceConscent && submission.isDataCompleted)
                                            {
                                                submission.ModifiedBy = _userContext.UserInfo.UserId;
                                                submission.ModifiedDate = DateTime.Now;
                                                await _submissionRepository.UpdateAsync(submission);
                                                var response = await UpdateSubmissionStatusAsync(submitProcessorRequest.SubmissionId, (int)SubmissionStatusType.SubmittedtoPAS);
                                                if (response.IsSuccess)
                                                {
                                                    ViewModels.Comment.CommentRequest comments = new ViewModels.Comment.CommentRequest()
                                                    {
                                                        SubmissionId = submitProcessorRequest.SubmissionId,
                                                        CommentText = submitProcessorRequest.CommentText,
                                                        CommentType = submitProcessorRequest.CommentType,
                                                        JsonData = submitProcessorRequest.JsonData,
                                                    };

                                                    if (comments != null)
                                                    {
                                                        await _commentService.AddComment(comments);
                                                    }

                                                    _operationResponse.Result = null;
                                                    _operationResponse.IsSuccess = true;
                                                    _operationResponse.Message = "Successfully submitted to PAS";
                                                }
                                                else
                                                {
                                                    _operationResponse.Result = null;
                                                    _operationResponse.IsSuccess = false;
                                                    _operationResponse.Message = "Sorry something went wrong";
                                                }
                                            }
                                            else if (submissionAuditLogCount < (submissionCount * percentageDetails.SamplePercentage / 100) && submission.ClearanceConscent && submission.isDataCompleted)
                                            {
                                                submission.ReviewerId = result.ReviewerId;
                                                submission.ModifiedBy = _userContext.UserInfo.UserId;
                                                submission.ModifiedDate = DateTime.Now;
                                                await _submissionRepository.UpdateAsync(submission);
                                                var response = await UpdateSubmissionStatusAsync(submitProcessorRequest.SubmissionId, (int)SubmissionStatusType.ReviewPending);
                                                if (response.IsSuccess)
                                                {
                                                    ViewModels.Comment.CommentRequest comments = new ViewModels.Comment.CommentRequest()
                                                    {
                                                        SubmissionId = submitProcessorRequest.SubmissionId,
                                                        CommentText = submitProcessorRequest.CommentText,
                                                        CommentType = submitProcessorRequest.CommentType,
                                                        JsonData = submitProcessorRequest.JsonData,
                                                    };

                                                    if (comments != null)
                                                    {
                                                        await _commentService.AddComment(comments);
                                                    }

                                                    _operationResponse.Result = null;
                                                    _operationResponse.IsSuccess = true;
                                                    _operationResponse.Message = "Successfully submitted to review";
                                                }
                                                else
                                                {
                                                    _operationResponse.Result = null;
                                                    _operationResponse.IsSuccess = false;
                                                    _operationResponse.Message = "Sorry something went wrong";
                                                }
                                            }
                                            else
                                            {
                                                _operationResponse.Result = null;
                                                _operationResponse.IsSuccess = false;
                                                _operationResponse.Message = "Submit for review criteria didn't match";
                                            }
                                        }
                                        else
                                        {
                                            _operationResponse.Result = null;
                                            _operationResponse.IsSuccess = false;
                                            _operationResponse.Message = "Review percentage user list aren't found";
                                        }
                                    }
                                    else
                                    {
                                        _operationResponse.Result = null;
                                        _operationResponse.IsSuccess = false;
                                        _operationResponse.Message = "Review details list aren't found";
                                    }
                                }
                                else
                                {
                                    _operationResponse.Result = null;
                                    _operationResponse.IsSuccess = false;
                                    _operationResponse.Message = "Review percentage details are not found";
                                }
                            }
                            else
                            {
                                _operationResponse.Result = null;
                                _operationResponse.IsSuccess = false;
                                _operationResponse.Message = "Submission mail details are not found";
                            }
                        }
                        else
                        {
                            _operationResponse.Result = null;
                            _operationResponse.IsSuccess = false;
                            _operationResponse.Message = "Mailbox details are not found";
                        }
                    }
                    else
                    {
                        if (submission.ClearanceConscent && submission.isDataCompleted)
                        {
                            submission.ReviewerId = result.ReviewerId;
                            submission.ModifiedBy = _userContext.UserInfo.UserId;
                            submission.ModifiedDate = DateTime.Now;
                            await _submissionRepository.UpdateAsync(submission);

                            var response = await UpdateSubmissionStatusAsync(submitProcessorRequest.SubmissionId, (int)SubmissionStatusType.ReviewPending);
                            if (response.IsSuccess)
                            {
                                ViewModels.Comment.CommentRequest comments = new ViewModels.Comment.CommentRequest()
                                {
                                    SubmissionId = submitProcessorRequest.SubmissionId,
                                    CommentText = submitProcessorRequest.CommentText,
                                    CommentType = submitProcessorRequest.CommentType,
                                    JsonData = submitProcessorRequest.JsonData,
                                };

                                if (comments != null)
                                {
                                    await _commentService.AddComment(comments);
                                }

                                _operationResponse.Result = null;
                                _operationResponse.IsSuccess = true;
                                _operationResponse.Message = "Successfully submitted to review";
                            }
                            else
                            {
                                _operationResponse.Result = null;
                                _operationResponse.IsSuccess = false;
                                _operationResponse.Message = "Sorry something went wrong";
                            }
                        }
                        else
                        {
                            _operationResponse.Result = null;
                            _operationResponse.IsSuccess = false;
                            _operationResponse.Message = "Submit for review criteria didn't match";
                        }
                    }
                }
                else
                {
                    _operationResponse.Result = null;
                    _operationResponse.IsSuccess = false;
                    _operationResponse.Message = "Current user reviewer details are not added yet";
                }
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "submission against this case number not found.";
            }

            if (submission != null && _operationResponse.IsSuccess && !string.IsNullOrEmpty(submission.ReviewerId))
            {                
                await _notificationService.SendNotification(submission: submission, userId: submission.ReviewerId, templateKey: "TSKGNDNRVW");
            }

            return _operationResponse;
        }

        /// <summary>
        /// Submission review reply by Reviewer.
        /// </summary>
        /// <param name="submitReviewerRequest">It's a request model which consists CommentText, CommentType, SubmissionId, JsonData, ReviewStatus.</param>
        /// <returns>Return IsSuccess, Message and Result as IResponse.</returns>        
        public async Task<IResponse> SendSubmissionToProcessorAsync(SubmitReviewerRequest submitReviewerRequest)
        {
            var submissionStatusId = 0;
            var submission = await _submissionRepository.GetSingleAsync(t => t.Id == submitReviewerRequest.SubmissionId && t.TenantId == _userContext.UserInfo.TenantId
            && t.IsActive);
            if (submission != null)
            {
                    if (submission.ClearanceConscent && submission.isDataCompleted)
                    {

                    CommentRequest comments = new CommentRequest()
                    {
                        SubmissionId = submitReviewerRequest.SubmissionId,
                        CommentText = submitReviewerRequest.CommentText,
                        CommentType = submitReviewerRequest.CommentType,
                        JsonData = submitReviewerRequest.JsonData,
                    };

                    if (comments != null)
                    {
                        await _commentService.AddComment(comments);
                    }

                    if (submitReviewerRequest.ReviewStatus == (int)SubmissionStatusType.ReviewPass)
                        {
                            submissionStatusId = (int)SubmissionStatusType.ReviewPass;
                        }
                        else if(submitReviewerRequest.ReviewStatus == (int)SubmissionStatusType.ReviewFail)
                        {
                            submissionStatusId = (int)SubmissionStatusType.ReviewFail;
                        }
                        else
                        {
                            submissionStatusId = (int)SubmissionStatusType.ReviewPending;
                        }

                        var response = await UpdateSubmissionStatusAsync(submitReviewerRequest.SubmissionId, submissionStatusId);
                        if (response.IsSuccess)
                        {
                            _operationResponse.Result = null;
                            _operationResponse.IsSuccess = true;
                            _operationResponse.Message = "Success";
                        }
                        else
                        {
                            _operationResponse.Result = null;
                            _operationResponse.IsSuccess = false;
                            _operationResponse.Message = "Sorry something went wrong";
                        }

                    }
                    else
                    {
                        _operationResponse.Result = null;
                        _operationResponse.IsSuccess = false;
                        _operationResponse.Message = "Submit for review criteria didn't match";
                    }
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "submission against this case number not found.";
            }
            if (submission != null && _operationResponse.IsSuccess && !string.IsNullOrEmpty(submission.AssignedId) && submissionStatusId > 0)
            {
                if (submitReviewerRequest.ReviewStatus == (int)SubmissionStatusType.ReviewPass)
                {                    
                    await _notificationService.SendNotification(submission: submission, userId: submission.AssignedId, templateKey: "SNTBCKREVIEWPASS");
                }
                if (submitReviewerRequest.ReviewStatus == (int)SubmissionStatusType.ReviewFail)
                {                    
                    await _notificationService.SendNotification(submission: submission, userId: submission.AssignedId, templateKey: "SNTBCKREVIEWFAIL");
                }
            }

            return _operationResponse;
        }

        /// <summary>
        /// Update submission status.
        /// </summary>
        /// <param name="submissionId">submission id.</param>
        /// <param name="submissionStatusId ">submission status id.</param>
        /// <returns>IResponse.</returns>
        public async Task<IResponse> UpdateSubmissionStatusAsync(long submissionId, int submissionStatusId)
        {
            var submission = await _submissionRepository.GetSingleAsync(t => t.Id == submissionId && t.TenantId == _userContext.UserInfo.TenantId
            && t.IsActive);
            if (submission != null)
            {
                var prevCloneId = new SubmissionCloneData();
                prevCloneId.PrevStatusId = submission.SubmissionStatusId;
                 if (string.IsNullOrEmpty(submission.AssignedId) || string.IsNullOrWhiteSpace(submission.AssignedId))
                {
                    prevCloneId.PrevAssignedId = _userContext.UserInfo.UserId;
                }
                else
                {
                    prevCloneId.PrevAssignedId = submission.AssignedId;
                }
                submission.SubmissionStatusId = (int)(SubmissionStatusType)Enum.ToObject(typeof(SubmissionStatusType), submissionStatusId);
                submission.ModifiedBy = _userContext.UserInfo.UserId;
                submission.ModifiedDate = DateTime.Now;
                await _submissionRepository.UpdateAsync(submission);
                Models.Entity.SubmissionAuditLog.SubmissionAuditLog submissionAuditLogRequest = new Models.Entity.SubmissionAuditLog.SubmissionAuditLog()
                {
                    SubmissionId = submission.Id,
                    NewStatus = submission.SubmissionStatusId,
                    PrevStatus = prevCloneId.PrevStatusId,
                    PrevAssignedToId = prevCloneId.PrevAssignedId,
                    TenantId = _userContext.UserInfo.TenantId,
                    CreatedBy = _userContext.UserInfo.UserId,
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    NewAssignedToId = _userContext.UserInfo.UserId
                };
                await _submissionAuditLogService.SaveSubmissionAuditLog(submissionAuditLogRequest);
                _operationResponse.IsSuccess = true;
                _operationResponse.Message = "Success";
            }
            else
            {
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "submission against this case number not found.";
            }
            return _operationResponse;
        }

        /// <summary>
        /// Get submission general information data by its submission id.
        /// </summary>
        /// <param name="submissionId">Submission id of case.</param>
        /// <returns>Returns the submission general information details.</returns>
        public async Task<IResponse> GetSubmissionGeneralInformation(long submissionId)
        {
            var submissionInfo = await _submissionRepository.GetSingleAsync(a => a.Id == submissionId && a.IsActive == true
                         && a.TenantId == _userContext.UserInfo.TenantId);

            var submissionAuditLogInfo = _submissionAuditLogRepository.GetAll().Where(b => b.SubmissionId == submissionId && b.IsActive == true
             && b.TenantId == _userContext.UserInfo.TenantId).OrderByDescending(c => c.CreatedDate).ToList();

            if (submissionAuditLogInfo.Count <= 0 || submissionAuditLogInfo == null)
            {
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "No Record Found";
                return _operationResponse;
            }

            var submissionAuditLogReviewPending = submissionAuditLogInfo.Where(x => x.NewStatus == (int)SubmissionStatusType.ReviewPending).OrderByDescending(x => x.Id).FirstOrDefault();
            var submissionAuditLogReviewPass = submissionAuditLogInfo.Where(x => x.NewStatus == (int)SubmissionStatusType.ReviewPass).OrderByDescending(x => x.Id).FirstOrDefault();

            List<UserResponseResult> userInfo = new List<UserResponseResult>();
            var usersList = await _uamService.GetUsersByFilters(new UserFilterRequest());
            if (usersList.Result != null)
            {
                userInfo = (List<UserResponseResult>)usersList.Result;
            }

            var CommentResponse = _commentRepository.GetAll().Where(x => x.TenantId == _userContext.UserInfo.TenantId
            && x.SubmissionId == submissionId && x.IsActive == true).OrderByDescending(p => p.CreatedDate).ToList().Select(p => new Comment
            {
                Id = p.Id,
                CommentType = p.CommentTypeId,
                CommentDate = p.CreatedDate,
                Description = p.CommentText,
                CommentBy = userInfo.Where(x => x.Id == p.CreatedBy).Select(x => x.Name).FirstOrDefault()
            }).ToList();

            SubmissionGeneralInformation submissionGeneralInformation = new SubmissionGeneralInformation();
            if (submissionInfo != null)
            {
                var result = new SubmissionGeneralInformation
                {
                    ReviewInformation = new ReviewInformation()
                    {
                        ProcessorName = userInfo.Where(x => x.Id == submissionInfo.AssignedId).Select(x => x.Name).FirstOrDefault(),
                        ReceivedDate = submissionInfo.EmailInfo.ReceivedDate,
                        DueDate = submissionInfo.DueDate,
                        ReviewerName = userInfo.Where(x => x.Id == submissionInfo.ReviewerId).Select(x => x.Name).FirstOrDefault(),
                        ReviewSubmitDate = (submissionAuditLogReviewPending != null) ? submissionAuditLogReviewPending.CreatedDate : null,
                        ReviewApprovalDate = (submissionAuditLogReviewPass != null) ? submissionAuditLogReviewPass.CreatedDate : null,
                        ReviewStatus = submissionAuditLogInfo.OrderByDescending(x => x.Id).Where(y=>y.NewStatus==(int)SubmissionStatusType.ReviewFail 
                        || y.NewStatus == (int)SubmissionStatusType.ReviewPass || y.NewStatus ==(int)SubmissionStatusType.ReviewPending).Select(x => x.NewStatus).FirstOrDefault(),
                        Comments = CommentResponse
                    },
                    PASInformation = new PASInformation()
                    {
                        SubmissionId = submissionInfo.Id,
                    }
                };
                _operationResponse.Result = result;
            }
            return _operationResponse;
        }

        /// <summary>
        /// Get Allocator UserId based on submission assignedId and not started
        /// </summary>
        /// <param name="assignedId"></param>
        /// <param name="notStartedSubmissionStatusId">Not Started Status</param>
        /// <returns>AllocatorUserId</returns>
        public async Task<string> GetAllocatorUserId(string? assignedId)
        {
            if (assignedId == null)
            {
                return "";
            }
            Preference? preference = await _preferenceService.GetPreferenceAsync(key: PreferenceConstants.NotStarted, _userContext.UserInfo.TenantId, string.Empty);
            int notStartedSubmissionStatusId = 0;
            if (preference != null)
            {
                int.TryParse(preference.Value, out notStartedSubmissionStatusId);
            }
            List<Models.Entity.SubmissionAuditLog.SubmissionAuditLog> submissionAuditLogs = await _submissionAuditLogRepository.GetAll()
                                                                                   .Where(t => t.NewAssignedToId == assignedId
                                                                                     && t.IsActive == true
                                                                                     && t.TenantId == _userContext.UserInfo.TenantId
                                                                                     && t.NewStatus == notStartedSubmissionStatusId
                                                                                     ).ToListAsync<Models.Entity.SubmissionAuditLog.SubmissionAuditLog>();
            Models.Entity.SubmissionAuditLog.SubmissionAuditLog? submissionAuditLog = submissionAuditLogs.OrderByDescending(s => s.CreatedDate).FirstOrDefault();
            if (submissionAuditLog != null)
            {
                return submissionAuditLog.CreatedBy;
            }
            return "";
        }
    }
}


