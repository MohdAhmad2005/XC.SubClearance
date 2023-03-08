using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Reflection;
using XC.CCMP.KeyVault;
using XC.CCMP.Logger;
using XC.XSC.EmailSender;
using XC.XSC.EmailSender.Models;
using XC.XSC.EMS.Connector;
using XC.XSC.EMS.Model;
using XC.XSC.Models.Entity.Prefrence;
using XC.XSC.Repositories.Notification;
using XC.XSC.Repositories.Sla;
using XC.XSC.Repositories.Submission;
using XC.XSC.Repositories.SubmissionAuditLog;
using XC.XSC.Service.Lobs;
using XC.XSC.Service.MessageSent;
using XC.XSC.Service.MessageTemplate;
using XC.XSC.Service.Preferences;
using XC.XSC.Service.Sla;
using XC.XSC.Service.User;
using XC.XSC.UAM.Models;
using XC.XSC.UAM.UAM;
using XC.XSC.ViewModels.ClientPlaceHolder;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.Enum;
using Attribute = XC.XSC.UAM.Models.Attribute;

namespace XC.XSC.Service.Notification
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IResponse _operationResponse;
        private readonly IPreferenceService _preferenceService;
        private readonly IUserContext _userContext;
        IMessageTemplateService _messageTemplateService;
        private readonly IEmailService _emailService;
        private readonly IMessageSentService _messageSentService;
        private readonly IKeyVaultConfig _keyVaultConfig;
        private readonly ISubmissionRepository _submissionRepository;
        private readonly IUamService _uamService;
        private readonly ISubmissionAuditLogRepository _submissionAuditLogRepository;
        private readonly ILobService _lobService;
        private readonly ISlaConfigurationService _slaConfigurationService;
        private readonly IEMSClient _emsClient;
        private readonly ILoggerManager _logger;
        private readonly ISlaConfigurationRepository _slaConfigRepository;

        /// <summary>
        /// Notification Service constructor
        /// </summary>
        /// <param name="NotificationRepository">Notification repository.</param>
        /// <param name="operationResponse">Returns generic IResponse.</param>
        public NotificationService(INotificationRepository notificationRepository, IResponse operationResponse
            , IPreferenceService preferenceService, IUserContext userContext, IMessageTemplateService messageTemplateService
            , IEmailService emailService, IMessageSentService messageSentService
            , IKeyVaultConfig keyVaultConfig
            , ISubmissionRepository submissionRepository
            , IUamService uamService
            , ISubmissionAuditLogRepository submissionAuditLogRepository
            , ILobService lobService
            , ISlaConfigurationService slaConfigurationService
            , IEMSClient emsClient
            , ILoggerManager logger
            , ISlaConfigurationRepository slaRepository
            )
        {
            _operationResponse = operationResponse;
            _notificationRepository = notificationRepository;
            _preferenceService = preferenceService;
            _userContext = userContext;
            _messageTemplateService = messageTemplateService;
            _emailService = emailService;
            _messageSentService = messageSentService;
            _keyVaultConfig = keyVaultConfig;
            _submissionRepository = submissionRepository;
            _uamService = uamService;
            _submissionAuditLogRepository = submissionAuditLogRepository;
            _lobService = lobService;
            _slaConfigurationService = slaConfigurationService;
            _emsClient = emsClient;
            _logger = logger;
            _slaConfigRepository = slaRepository;

            _logger.LogInfo("Initialized - Notification Service");
        }

        /// <summary>
        /// Add Notifications
        /// </summary>
        /// <param name="notification"> notification request.</param>
        /// <returns>Returns bool: Success then true and false if failed.</returns>
        public async Task<IResponse> AddNotification(Models.Entity.Notification.Notification notification)
        {
            _logger.LogInfo("XC.XSC.Service.Notification.NotificationService called - AddNotification");
            await _notificationRepository.AddAsync(notification);
            _operationResponse.IsSuccess = true;
            _operationResponse.Message = "SUCCESS";
            return _operationResponse;
        }

        /// <summary>
        /// Add Notification
        /// </summary>
        /// <param name="notification"> notification request.</param>
        /// <returns>Returns bool: Success then true and false if failed.</returns>
        public async Task<IResponse> SendNotification(Models.Entity.Submission.Submission? submission, string userId, string templateKey, int? days = null, bool IsMailOnly = false, string cc = "", string bcc = "", string userName = "", string email = "")
        {
            _logger.LogInfo("XC.XSC.Service.Notification.NotificationService called - SendNotification");
            if (!string.IsNullOrEmpty(userId))
            {
                Models.Mongo.Entity.MessageTemplate.MessageTemplate? messageTemplate = null;
                try
                {
                    messageTemplate = await _messageTemplateService.GetMessageTemplate(templateKey);
                }
                catch (Exception exception)
                {                   
                    _logger.LogInfo("XC.XSC.Service.Notification.NotificationService.SendNotification - " + exception.Message);
                }
                if (messageTemplate != null)
                {
                    string to = String.Empty;
                    string firstNameLastName = String.Empty;
                    if (!string.IsNullOrEmpty(email))
                    {
                        to = email;
                        firstNameLastName = userName;
                    }
                    else
                    {
                        UserResponseResult? userResponseResult = await GetUserEmail(userId);
                        if (userResponseResult != null)
                        {
                            to = userResponseResult.email;
                            firstNameLastName = userResponseResult.Name;
                        }
                    }

                    await EnrichTemplate(submission, messageTemplate, days, firstNameLastName);
                    _logger.LogInfo("XC.XSC.Service.Notification.NotificationService.SendNotification - EnrichTemplate success.");
                    if (!IsMailOnly)
                    {
                        if (submission != null)
                        {
                            Models.Entity.Notification.Notification notification = new Models.Entity.Notification.Notification();
                            notification.UserId = userId;
                            notification.MsgType = messageTemplate.MsgType;
                            notification.TemplateKey = templateKey;
                            notification.SubmissionId = submission.Id;
                            notification.Subject = messageTemplate.Notifications.Subject;
                            notification.Description = messageTemplate.Notifications.TemplateBody;
                            notification.TenantId = submission.TenantId;
                            notification.CreatedBy = (_userContext.UserInfo.UserName == null ? "System" : _userContext.UserInfo.UserName);
                            notification.IsActive = true;
                            await _notificationRepository.AddAsync(notification);
                            _logger.LogInfo("XC.XSC.Service.Notification.NotificationService.SendNotification - notification added.");
                        }
                    }
                    Preference? preference = await _preferenceService.GetPreferenceAsync(key: PreferenceConstants.SendMail, _userContext.UserInfo.TenantId, string.Empty);
                    bool isSendMail = false;
                    if (preference != null)
                    {
                        bool.TryParse(preference.Value, out isSendMail);
                        if (isSendMail)
                        {
                            if (!string.IsNullOrEmpty(to))
                            {
                                await SendMail(submission, messageTemplate, to, cc, bcc);
                            }
                        }
                    }
                    _operationResponse.IsSuccess = true;
                    _operationResponse.Message = "SUCCESS";
                }
                else
                {
                    _operationResponse.IsSuccess = false;
                    _operationResponse.Message = "ERROR";
                }
            }
            else
            {
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "ERROR";
            }
            return _operationResponse;
        }

        /// <summary>
        /// Send Mail
        /// </summary>
        /// <param name="submission"></param>
        /// <param name="submissionStatusType"></param>
        private async Task SendMail(Models.Entity.Submission.Submission? submission, Models.Mongo.Entity.MessageTemplate.MessageTemplate messageTemplate, string to, string cc, string bcc)
        {
            Models.Mongo.Entity.MessageSent.MessageSent messageSent = new Models.Mongo.Entity.MessageSent.MessageSent();
            string exceptions = string.Empty;
            try
            {
                _logger.LogInfo("XC.XSC.Service.Notification.NotificationService.SendMail - called.");
                if (messageTemplate != null)
                {
                    messageSent.TenantId = _userContext.UserInfo.TenantId;
                    if (submission != null)
                    {
                        messageSent.SubmissionId = submission.Id;
                    }
                    messageSent.MsgType = MessageType.Email;
                    messageSent.ToEmail = to;
                    messageSent.FromEmail = _keyVaultConfig.SmtpUser;
                    messageSent.CCEmail = cc;
                    messageSent.BccEmail = bcc;
                    messageSent.Subject = messageTemplate.Mails.Subject;
                    messageSent.Body = messageTemplate.Mails.TemplateBody;
                    messageSent.IsSuccess = true;
                    messageSent.IsActive = true;
                    messageSent.CreatedBy = (_userContext.UserInfo.UserName == null ? "System" : _userContext.UserInfo.UserName);
                    Email email = new Email() { Body = messageSent.Body, Subject = messageSent.Subject, To = to };
                    await _emailService.SendEmailAsync(email);
                }
            }
            catch (Exception exception)
            {
                exceptions = exception.Message;
            }
            if (!string.IsNullOrEmpty(exceptions))
            {
                messageSent.IsSuccess = false;
                messageSent.FailureStatus = exceptions;
            }
            await _messageSentService.AddMessageSent(messageSent);
            _logger.LogInfo("XC.XSC.Service.Notification.NotificationService.SendMail - AddMessageSent successfully..");
        }

        /// <summary>
        /// Replace client place holder
        /// </summary>
        /// <param name="submission"></param>
        /// <param name="messageTemplate"></param>
        /// <returns></returns>
        private async Task<Models.Mongo.Entity.MessageTemplate.MessageTemplate> EnrichTemplate(Models.Entity.Submission.Submission? submission, Models.Mongo.Entity.MessageTemplate.MessageTemplate messageTemplate, int? days, string firstNameLastName)
        {
            _logger.LogInfo("XC.XSC.Service.Notification.NotificationService.EnrichTemplate - Called.");
            if (submission != null)
            {
                List<Preference> preferences = await _preferenceService.GetPreferenceByTenantAsync(_userContext.UserInfo.TenantId);
                Preference? preference = null;
                if (preferences.Any())
                {
                    preference = preferences.Where(x => x.Key == PreferenceConstants.ClientPlaceHolder).FirstOrDefault();
                }
                if (preference != null)
                {
                    ClientPlaceHolder? clientPlaceHolder = JsonConvert.DeserializeObject<ClientPlaceHolder>(preference.Value);
                    preference = null;
                    if (clientPlaceHolder != null)
                    {
                        if (preferences.Any())
                        {
                            preference = preferences.Where(x => x.Key == PreferenceConstants.NotificationDateFormat).FirstOrDefault();
                        }
                        foreach (PlaceHolder placeHolder in clientPlaceHolder.PlaceHolders)
                        {
                            string field = $"##{placeHolder.Field}##";
                            PropertyInfo? propertyInfo = typeof(Models.Entity.Submission.Submission).GetProperty(placeHolder.ColumnName);
                            if (propertyInfo != null)
                            {
                                string? value = string.Empty;
                                if (propertyInfo.GetValue(submission) != null)
                                {
                                    value = Convert.ToString(propertyInfo.GetValue(submission));
                                }
                                if (!string.IsNullOrEmpty(value))
                                {
                                    DateTime dateTimeResult;
                                    if (DateTime.TryParse(value, out dateTimeResult))
                                    {
                                        value = preference != null ? dateTimeResult.ToString(preference.Value) : dateTimeResult.ToString("MM/dd/yyyy");
                                    }

                                    if (!string.IsNullOrEmpty(messageTemplate.Mails.Subject))
                                    {
                                        messageTemplate.Mails.Subject = messageTemplate.Mails.Subject.Replace(field, value);
                                    }
                                    if (!string.IsNullOrEmpty(messageTemplate.Mails.TemplateBody))
                                    {
                                        messageTemplate.Mails.TemplateBody = messageTemplate.Mails.TemplateBody.Replace(field, value);
                                    }
                                    if (!string.IsNullOrEmpty(messageTemplate.Notifications.Subject))
                                    {
                                        messageTemplate.Notifications.Subject = messageTemplate.Notifications.Subject.Replace(field, value);
                                    }
                                    if (!string.IsNullOrEmpty(messageTemplate.Notifications.TemplateBody))
                                    {
                                        messageTemplate.Notifications.TemplateBody = messageTemplate.Notifications.TemplateBody.Replace(field, value);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //Replace USERNAME
            string userName = $"{_userContext.UserInfo.FirstName} {_userContext.UserInfo.MiddleName}";
            if (!string.IsNullOrEmpty(messageTemplate.Mails.Subject))
            {
                messageTemplate.Mails.Subject = messageTemplate.Mails.Subject.Replace("##USERNAME##", userName);
            }
            if (!string.IsNullOrEmpty(messageTemplate.Mails.TemplateBody))
            {
                messageTemplate.Mails.TemplateBody = messageTemplate.Mails.TemplateBody.Replace("##USERNAME##", userName);
            }
            if (!string.IsNullOrEmpty(messageTemplate.Notifications.Subject))
            {
                messageTemplate.Notifications.Subject = messageTemplate.Notifications.Subject.Replace("##USERNAME##", userName);
            }
            if (!string.IsNullOrEmpty(messageTemplate.Notifications.TemplateBody))
            {
                messageTemplate.Notifications.TemplateBody = messageTemplate.Notifications.TemplateBody.Replace("##USERNAME##", userName);
            }

            //Replace FIRSTNAMELASTNAME
            if (!string.IsNullOrEmpty(messageTemplate.Mails.Subject))
            {
                messageTemplate.Mails.Subject = messageTemplate.Mails.Subject.Replace("##FIRSTNAMELASTNAME##", firstNameLastName);
            }
            if (!string.IsNullOrEmpty(messageTemplate.Mails.TemplateBody))
            {
                messageTemplate.Mails.TemplateBody = messageTemplate.Mails.TemplateBody.Replace("##FIRSTNAMELASTNAME##", firstNameLastName);
            }
            if (!string.IsNullOrEmpty(messageTemplate.Notifications.Subject))
            {
                messageTemplate.Notifications.Subject = messageTemplate.Notifications.Subject.Replace("##FIRSTNAMELASTNAME##", firstNameLastName);
            }
            if (!string.IsNullOrEmpty(messageTemplate.Notifications.TemplateBody))
            {
                messageTemplate.Notifications.TemplateBody = messageTemplate.Notifications.TemplateBody.Replace("##FIRSTNAMELASTNAME##", firstNameLastName);
            }

            if (days != null)
            {
                if (!string.IsNullOrEmpty(messageTemplate.Mails.Subject))
                {
                    messageTemplate.Mails.Subject = messageTemplate.Mails.Subject.Replace("##REMAININGDUEDAYS##", Convert.ToString(days));
                }
                if (!string.IsNullOrEmpty(messageTemplate.Mails.TemplateBody))
                {
                    messageTemplate.Mails.TemplateBody = messageTemplate.Mails.TemplateBody.Replace("##REMAININGDUEDAYS##", Convert.ToString(days));
                }
                if (!string.IsNullOrEmpty(messageTemplate.Notifications.Subject))
                {
                    messageTemplate.Notifications.Subject = messageTemplate.Notifications.Subject.Replace("##REMAININGDUEDAYS##", Convert.ToString(days));
                }
                if (!string.IsNullOrEmpty(messageTemplate.Notifications.TemplateBody))
                {
                    messageTemplate.Notifications.TemplateBody = messageTemplate.Notifications.TemplateBody.Replace("##REMAININGDUEDAYS##", Convert.ToString(days));
                }
            }
            return messageTemplate;
        }

        #region Due date reminder notification

        /// <summary>
        /// Notification send when a case is about to reach its due date (2 days,1 Day and same day earlier)
        /// </summary>
        /// <returns>true if success</returns>
        public async Task<IResponse> SendDueDateReminderNotificationAsync()
        {
            int notStartedSubmissionStatusId = 0;
            int inProgressPlaySubmissionStatusId = 0;
            int inProgressPausedSubmissionStatusId = 0;
            int reviewPendingSubmissionStatusId = 0;
            int underReviewPlaySubmissionStatusId = 0;
            int underReviewPausedSubmissionStatusId = 0;
            int notificationDays = 0;
            List<Preference> preferences = await _preferenceService.GetPreferenceByTenantAsync(_userContext.UserInfo.TenantId);
            if (preferences.Any())
            {
                Preference? preference = preferences.Where(x => x.Key == PreferenceConstants.NotStarted).FirstOrDefault();
                if (preference != null)
                {
                    int.TryParse(preference.Value, out notStartedSubmissionStatusId);
                }
                preference = null;

                preference = preferences.Where(x => x.Key == PreferenceConstants.InProgressPlay).FirstOrDefault();
                if (preference != null)
                {
                    int.TryParse(preference.Value, out inProgressPlaySubmissionStatusId);
                }
                preference = null;

                preference = preferences.Where(x => x.Key == PreferenceConstants.InProgressPaused).FirstOrDefault();
                if (preference != null)
                {
                    int.TryParse(preference.Value, out inProgressPausedSubmissionStatusId);
                }
                preference = null;

                preference = preferences.Where(x => x.Key == PreferenceConstants.ReminderNotificationDays).FirstOrDefault();
                if (preference != null)
                {
                    int.TryParse(preference.Value, out notificationDays);
                }
                preference = null;

                preference = preferences.Where(x => x.Key == PreferenceConstants.ReviewPending).FirstOrDefault();
                if (preference != null)
                {
                    int.TryParse(preference.Value, out reviewPendingSubmissionStatusId);
                }
                preference = null;

                preference = preferences.Where(x => x.Key == PreferenceConstants.UnderReviewPlay).FirstOrDefault();
                if (preference != null)
                {
                    int.TryParse(preference.Value, out underReviewPlaySubmissionStatusId);
                }
                preference = null;

                preference = preferences.Where(x => x.Key == PreferenceConstants.UnderReviewPaused).FirstOrDefault();
                if (preference != null)
                {
                    int.TryParse(preference.Value, out underReviewPausedSubmissionStatusId);
                }
                preference = null;
            }

            List<Models.Entity.Submission.Submission> submissions = await _submissionRepository.GetAll()
                                                   .Where(t => t.IsInScope == true
                                                     && t.IsActive == true
                                                     && t.TenantId == _userContext.UserInfo.TenantId
                                                     && t.DueDate < DateTime.Now.AddDays(notificationDays)
                                                     && (t.SubmissionStatusId == notStartedSubmissionStatusId
                                                     || t.SubmissionStatusId == inProgressPlaySubmissionStatusId
                                                     || t.SubmissionStatusId == inProgressPausedSubmissionStatusId
                                                     || t.SubmissionStatusId == reviewPendingSubmissionStatusId
                                                     || t.SubmissionStatusId == underReviewPlaySubmissionStatusId
                                                     || t.SubmissionStatusId == underReviewPausedSubmissionStatusId
                                                     )
                                                     ).ToListAsync<Models.Entity.Submission.Submission>();


            if (submissions.Any())
            {
                foreach (var submission in submissions)
                {
                        if (await IsSlaEscalation(submission) == true)
                        {
                            #region All Notification Code
                            string subject = string.Empty;
                            string? userId = submission.AssignedId;
                            int days;
                            if (userId != null)
                            {
                                DateTime DueDate = submission.DueDate.Date;
                                if (DueDate >= DateTime.Now.Date)
                                {
                                    days = (DueDate - DateTime.Now.Date).Days;
                                    if (days <= notificationDays)
                                    {
                                        if (submission.SubmissionStatusId == notStartedSubmissionStatusId
                                        || submission.SubmissionStatusId == inProgressPlaySubmissionStatusId
                                        || submission.SubmissionStatusId == inProgressPausedSubmissionStatusId
                                        )
                                        {
                                            //Processor Notification
                                            await SendNotification(submission: submission, userId: userId, templateKey: "REACHDUEDATEREVIEWERAL", days: days);
                                            userId = await GetAllocatorUserId(submission.AssignedId, notStartedSubmissionStatusId);
                                            if (userId != null)
                                            {
                                                if (userId != submission.AssignedId)
                                                {
                                                    //Allocator Notification
                                                    await SendNotification(submission: submission, userId: userId, templateKey: "REACHDUEDATEREVIEWERAL", days: days);
                                                }

                                            }
                                        }

                                        if (submission.SubmissionStatusId == reviewPendingSubmissionStatusId
                                        || submission.SubmissionStatusId == underReviewPlaySubmissionStatusId
                                        || submission.SubmissionStatusId == underReviewPausedSubmissionStatusId
                                        )
                                        {
                                            userId = submission.ReviewerId;
                                            if (userId != null)
                                            {
                                                //Reviewer Notification
                                                await SendNotification(submission: submission, userId: userId, templateKey: "REACHDUEDATEREVIEWERAL", days: days);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    #region TAT Breached 
                                    if (submission.SubmissionStatusId == notStartedSubmissionStatusId
                                        || submission.SubmissionStatusId == inProgressPlaySubmissionStatusId
                                        || submission.SubmissionStatusId == inProgressPausedSubmissionStatusId
                                        )
                                    {
                                        //Processor Notification
                                        await SendNotification(submission: submission, userId: userId, templateKey: "BRCHDTTDDT");
                                        userId = await GetAllocatorUserId(submission.AssignedId, notStartedSubmissionStatusId);
                                        if (userId != null)
                                        {
                                            if (userId != submission.AssignedId)
                                            {
                                                //Allocator Notification
                                                await SendNotification(submission: submission, userId: userId, templateKey: "BRCHDTTDDT");
                                            }
                                        }
                                    }

                                    if (submission.SubmissionStatusId == reviewPendingSubmissionStatusId
                                    || submission.SubmissionStatusId == underReviewPlaySubmissionStatusId
                                    || submission.SubmissionStatusId == underReviewPausedSubmissionStatusId
                                    )
                                    {
                                        userId = submission.ReviewerId;
                                        if (userId != null)
                                        {
                                            //Reviewer Notification
                                            await SendNotification(submission: submission, userId: userId, templateKey: "BRCHDTTDDT");
                                        }
                                    }
                                    #endregion
                                }
                            }
                            #endregion
                        }
                }
                _operationResponse.IsSuccess = true;
                _operationResponse.Message = "SUCCESS";
            }
            else
            {
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "No result found";
            }
            return _operationResponse;
        }

        /// <summary>
        /// Get User Email
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>email</returns>
        public async Task<UserResponseResult?> GetUserEmail(string userId)
        {
            UserFilterRequest userFilterRequest = new UserFilterRequest();
            userFilterRequest.Attributes.Add(new Attribute() { Name = "UserId", Value = new List<string>() { userId } });
            var usersList = await _uamService.GetUsersByFilters(userFilterRequest);
            if (usersList.Result != null)
            {
                List<UserResponseResult> userResponseResults = (List<UserResponseResult>)usersList.Result;
                if (userResponseResults.Any())
                {
                    return userResponseResults.Select(a => a).FirstOrDefault();
                }
            }
            else
            {
                throw new Exception($"GetUserEmail(): User email not exists.");
            }
            return null;
        }

        #endregion

        /// <summary>
        /// Get current logged in User Notification 
        /// </summary>
        /// <returns>SUCCESS</returns>
        public async Task<IResponse> GetUserNotification()
        {
            List<Models.Entity.Notification.Notification> notifications = await _notificationRepository.GetAll()
                                       .Where(t => t.UserId == _userContext.UserInfo.UserId
                                         && t.IsActive == true
                                         && t.TenantId == _userContext.UserInfo.TenantId
                                         && t.IsRead == false
                                         ).OrderByDescending(a => a.Id).ToListAsync<Models.Entity.Notification.Notification>();
            if (notifications.Any())
            {
                _operationResponse.Result = notifications;
                _operationResponse.IsSuccess = true;
                _operationResponse.Message = "SUCCESS";
            }
            else
            {
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "No result found";
            }
            return _operationResponse;
        }

        /// <summary>
        /// Update Notifications Status by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResponse> UpdateNotificationsStatus(int id)
        {
            Models.Entity.Notification.Notification? notification = await _notificationRepository.GetSingleAsync(a => a.Id == id);
            if (notification != null)
            {
                notification.IsRead = true;
                notification.ModifiedBy = _userContext.UserInfo.UserId;
                notification.ModifiedDate = DateTime.Now;
                notification.IsActive = false;
                await _notificationRepository.UpdateAsync(notification);
                _operationResponse.IsSuccess = true;
                _operationResponse.Message = "SUCCESS";
            }
            else
            {
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "No result found";
            }
            return _operationResponse;
        }

        /// <summary>
        /// Get Allocator UserId based on submission assignedId and not started
        /// </summary>
        /// <param name="assignedId"></param>
        /// <param name="notStartedSubmissionStatusId">Not Started Status</param>
        /// <returns>AllocatorUserId</returns>
        public async Task<string> GetAllocatorUserId(string? assignedId, int notStartedSubmissionStatusId)
        {
            if (assignedId == null)
            {
                return "";
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

        /// <summary>
        /// Get Sla Configuration based on submission
        /// </summary>
        /// <param name="submission"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<bool?> IsSlaEscalation(Models.Entity.Submission.Submission submission)
        {
            if (submission == null)
            {
                throw new Exception($"GetSlaConfigurationAsync(): Submission does not exists.");
            }
            
            var lob = _lobService.GetLobById(submission.LobId).Result;
            if (lob == null)
            {
                throw new Exception($"GetSlaConfigurationAsync(): Lob does not exists.");
            }

            IResponse mailBoxResponse = await _emsClient.GetMailBoxList(submission.RegionId, lob.LOBID, submission.TeamId, submission.TenantId);
            if (mailBoxResponse.Result == null || !mailBoxResponse.IsSuccess)
            {
                throw new Exception($"GetSlaConfigurationAsync(): MailBox does not exists.");
            }

            List<EmailBoxResponse> emailBoxes = (List<EmailBoxResponse>)mailBoxResponse.Result;
            var emailBox = emailBoxes.Select(e => e).Where(e => e.MailboxEmailID.Equals(submission.EmailInfo.MailboxName));

            if (emailBox.Any())
            {
                EmailBoxResponse? emailBoxResponse = emailBox.FirstOrDefault();
                if (emailBoxResponse != null)
                {
                    var slaConfiguration = await _slaConfigRepository.GetSingleAsync(k => k.MailBoxId == emailBoxResponse.MailBoxId
                                                   && k.LobId == submission.LobId
                                                   && k.TeamId == submission.TeamId
                                                   && k.TenantId == submission.TenantId
                                                   && k.RegionId == submission.RegionId
                                                   && k.Type == SlaType.TAT
                                                   && k.IsEscalation == true);

                    return (slaConfiguration != null) ? true : false;
                    
                }
                else
                {
                    throw new Exception($"GetSlaConfigurationAsync(): MailBox does not found.");
                }
            }
            else
            {
                throw new Exception($"GetSlaConfigurationAsync(): MailBox does not exist.");
            }
        }
    }
}
