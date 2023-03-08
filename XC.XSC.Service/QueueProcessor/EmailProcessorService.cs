using XC.XSC.Service.EmailInfo;
using XC.XSC.ViewModels.EmailInfo;
using XC.XSC.ViewModels.EmailInfoAttachment;
using XC.XSC.ViewModels.QueueProcessor;
using XC.XSC.ViewModels.Submission;
using XC.XSC.Service.Preferences;
using XC.XSC.Models.Entity.Prefrence;
using XC.XSC.Utilities.GenricCaseNumber;
using XC.XSC.Service.Lobs;
using XC.XSC.Models.Entity.Lob;
using XC.XSC.Repositories.Submission;
using XC.XSC.UAM.UAM;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.Service.Sla;
using XC.XSC.ViewModels.Sla;
using XC.XSC.ViewModels.Enum;
using XC.XSC.UAM.Models;
using Attribute = XC.XSC.UAM.Models.Attribute;
using XC.XSC.Service.Notification;
using XC.XSC.Models.Entity.Submission;
using XC.XSC.Models.Interface.Submission;
using XC.XSC.EMS.Connector;
using XC.XSC.Utilities.UnitExtension;

namespace XC.XSC.Service.Submission
{
    /// <summary>
    /// This service is used to process data and send it in EmailInfo.
    /// </summary>
    public class EmailProcessorService : IEmailProcessorService
    {

        private readonly IEmailInfoService _emailInfoService;
        private readonly ISubmissionRepository _submissionRepository;
        private readonly IEMSClient _emsClient;

        private readonly ISubmissionService _submissionService;
        private readonly IPreferenceService _preferenceService;
        private readonly ILobService _lobService;
        private readonly ISlaConfigurationService _slaConfigService;
        private readonly IUamService _uamService;
        private readonly INotificationService _notificationService;

        /// <summary>
        /// This method is a constructor of EmailProcessor service.
        /// </summary>
        /// <param name="emailInfoService"></param>

        public EmailProcessorService(IEmailInfoService emailInfoService, ISubmissionRepository submissionRepository, ISubmissionService submissionService, IPreferenceService preferenceService, ILobService lobService
            , ISlaConfigurationService slaConfigService, IUamService uamService, INotificationService notificationService, IEMSClient emsClient)
        {
            _submissionRepository = submissionRepository;
            _emailInfoService = emailInfoService;
            _submissionService = submissionService;
            _preferenceService = preferenceService;
            _lobService = lobService;
            _slaConfigService = slaConfigService;
            _uamService = uamService;
            _notificationService = notificationService;
            _emsClient = emsClient;
        }

        /// <summary>
        /// Save EmailInfo and attachment 
        /// </summary>
        /// <param name="emailProcessorRequest"></param>
        /// <returns></returns>
        public async Task<bool> SaveEmailInfo(EmailProcessorRequest emailProcessorRequest)
        {
            List<Preference> preferencelist = _preferenceService.GetPreferenceByTenantAsync(emailProcessorRequest.TenantId).Result;

            if (!preferencelist.Any())
            {
                throw new Exception($"Preferences are missing for tenant: {emailProcessorRequest.TenantId}");
            }

            string caseNumberPrefix = preferencelist.Where(x => x.Key == PreferenceConstants.SubmissionCaseNumberPrefix).FirstOrDefault().Value;
            string caseNumberDateFormat = preferencelist.Where(x => x.Key == PreferenceConstants.SubmissionCaseNumberDateFormat).FirstOrDefault().Value;
            string caseNumberLastSequence = preferencelist.Where(x => x.Key == PreferenceConstants.SubmissionCaseNumberSequence).FirstOrDefault().Value;
            string caseNumberPadding = preferencelist.Where(x => x.Key == PreferenceConstants.SubmissionCaseNumberPadding).FirstOrDefault().Value;

            if (string.IsNullOrEmpty(caseNumberPrefix))
            {
                throw new Exception("CSPRFX: Submission case number: starting/prefix starting with, is missing in preferences");
            }
            if (string.IsNullOrEmpty(caseNumberDateFormat))
            {
                throw new Exception("CSDTFRMT: Submission case number: dateformat, is missing in preferences");
            }
            if (string.IsNullOrEmpty(caseNumberLastSequence))
            {
                throw new Exception("CSSQNC: Submission case number: sequence number and it will be always the last generated number, is missing in preferences");
            }
            if (string.IsNullOrEmpty(caseNumberPadding))
            {
                throw new Exception("CSPDNG: Submission case number: Preceeding zero(0) before the generated last sequence number, is missing in preferences");
            }

            Lob lob = await _lobService.GetLobByTenantAndIdAsync(emailProcessorRequest.LobId, emailProcessorRequest.TenantId);
            if (lob == null)
            {
                throw new Exception($"Lob is missing for lobId: {emailProcessorRequest.LobId}");
            }

            var lobResponse = _lobService.GetLobById(lob.Id).Result;
            if (lobResponse == null)
            {
                throw new Exception($"CalculateDueDate(): Lob does not exists.");
            }

            IResponse mailBoxResponse = await _emsClient.GetMailBoxList(emailProcessorRequest.RegionId, lob.LOBID, emailProcessorRequest.TeamId, emailProcessorRequest.TenantId);
            if (mailBoxResponse.Result == null || !mailBoxResponse.IsSuccess)
            {
                throw new Exception($"CalculateDueDate(): MailBox does not exists.");
            }

            AddEmailInfoRequest emailInfoRequest = new AddEmailInfoRequest
            {
                EmailId = emailProcessorRequest.Id,
                FromName = emailProcessorRequest.FromName,
                FromEmail = emailProcessorRequest.FromEmail,
                ToEmail = emailProcessorRequest.ToEmail,
                CCEmail = emailProcessorRequest.CCEmail,
                Subject = emailProcessorRequest.SubjectEmail,
                MessageId = emailProcessorRequest.MessageId,
                ParentMessageId = emailProcessorRequest.ParentMailId,
                TotalDocuments = emailProcessorRequest.AttachmentMessages.Count,
                ReceivedDate = emailProcessorRequest.EmailRecieveDate,
                DocumentId = emailProcessorRequest.DocumentId,
                IsDuplicate = emailProcessorRequest.IsDuplicate,
                TenantId = emailProcessorRequest.TenantId,
                LobId = emailProcessorRequest.LobId,
                Body = emailProcessorRequest.Body,
                CreatedBy = Guid.NewGuid().ToString(),
                MailboxName = emailProcessorRequest.MailBoxName,
                ConfigurationId = emailProcessorRequest.ConfigurationId,
                TeamId = emailProcessorRequest.TeamId,
                RegionId = emailProcessorRequest.RegionId,
                BodyLength = emailProcessorRequest.BodyLength,
            };

            emailInfoRequest.Attachments = new List<AddEmailInfoAttachmentRequest>();

            foreach (var attachment in emailProcessorRequest.AttachmentMessages)
            {
                FileInfo fi = new FileInfo(attachment.FilePath);
                emailInfoRequest.Attachments.Add(new AddEmailInfoAttachmentRequest()
                {
                    FileName = attachment.FileName,
                    FileType = fi.Extension,
                    DocumentId = attachment.DocumentId,
                    AttachmentId = attachment.AttachmentId,
                    FileSize = attachment.FileSize.GetSize(UnitExtension.SizeUnits.KB),
                    TenantId = emailProcessorRequest.TenantId,
                    SizeUnit = UnitExtension.SizeUnits.KB.ToString()
                });
            };
            
            emailInfoRequest.Attachments.Add(new AddEmailInfoAttachmentRequest()
            {
                FileName = emailProcessorRequest.FileName,
                FileType = new FileInfo(emailProcessorRequest.FileName).Extension,
                DocumentId = emailProcessorRequest.DocumentId,
                AttachmentId = "",
                FileSize = emailProcessorRequest.FileSize.GetSize(UnitExtension.SizeUnits.KB),
                TenantId = emailProcessorRequest.TenantId,
                SizeUnit = UnitExtension.SizeUnits.KB.ToString()
            });

            var _emailInfoResponse = await _emailInfoService.SaveEmailInfo(emailInfoRequest);

            if (_emailInfoResponse.IsSuccess)
            {
                int lastGeneratedSequence = 0;

                SubmissionRequest submission = new SubmissionRequest()
                {
                    CaseId = Utility.GenerateUniqueSerial(caseNumberPrefix, caseNumberDateFormat, Convert.ToInt32(caseNumberLastSequence), out lastGeneratedSequence, Convert.ToInt32(caseNumberPadding)),
                    BrokerName = string.Empty,
                    InsuredName = string.Empty,
                    EmailInfoId = emailInfoRequest.Id,
                    AssignedId = string.Empty,
                    SubmissionStatusId = 1,
                    SubmissionStageId = 1,
                    IsInScope = false,
                    LobId = lob.Id,
                    ExtendedDate = DateTime.Now,
                    EmailBody = emailProcessorRequest.Body,
                    TaskId = string.Empty,
                    TenantId = emailProcessorRequest.TenantId,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "system",
                    ModifiedDate = null,
                    ModifiedBy = null,
                    IsActive = true,
                };
                submission.DueDate = await _slaConfigService.CalculateDueDate(submission, emailInfoRequest);

                var _submission =  await this.SaveSubmission(emailInfoRequest, submission);

                await _preferenceService.UpdatePreferenceByKey(emailProcessorRequest.TenantId, PreferenceConstants.SubmissionCaseNumberSequence, lastGeneratedSequence.ToString());

                if (_submission.Id > 0)
                {
                    await TaskCreationNotification(_submission);
                }
            }
            return true;
        }

        public async Task<Models.Entity.Submission.Submission> SaveSubmission(AddEmailInfoRequest emailInfoRequest, SubmissionRequest submissionRequest)
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
                TenantId = emailInfoRequest.TenantId,
                CreatedDate = submissionRequest.CreatedDate,
                CreatedBy = "System",
                IsActive = submissionRequest.IsActive,
                TeamId = emailInfoRequest.TeamId,
                RegionId = emailInfoRequest.RegionId
            };

            await _submissionRepository.AddAsync(submission);
            
            return submission;

        }

        /// <summary>
        /// Send notification to allocator when task is created.
        /// </summary>
        /// <param name="submission"></param>
        /// <returns></returns>
        private async Task TaskCreationNotification(Models.Entity.Submission.Submission submission)
        {
            Preference? preference = await _preferenceService.GetPreferenceAsync(key: PreferenceConstants.Allocator, submission.TenantId, string.Empty);
            if (preference != null)
            {
                List<UserResponseResult> userResponseResults = new List<UserResponseResult>();
                UserFilterRequest userFilterRequest = new UserFilterRequest();
                userFilterRequest.Attributes.AddRange(new List<Attribute>(){
                new Attribute()
                    {
                        Name = "Lob", Value = new List<string>() { Convert.ToString(submission.LobId) }
                    }, new Attribute()
                    {
                        Name = "Region", Value = new List<string>() { Convert.ToString(submission.RegionId) }
                    }, new Attribute()
                    {
                        Name = "Team", Value = new List<string>() { Convert.ToString(submission.TeamId) }
                    }, new Attribute()
                    {
                        Name = "Role", Value = new List<string>() { preference.Value }
                    }
                });
                var usersList = await _uamService.GetUsersByFilters(userFilterRequest);
                if (usersList.Result != null)
                {
                    userResponseResults = (List<UserResponseResult>)usersList.Result;
                }
                foreach (UserResponseResult userResponseResult in userResponseResults)
                {
                    await _notificationService.SendNotification(submission: submission, userId: userResponseResult.Id, templateKey: "TSKCRTNTFCTNTCTR", email: userResponseResult.email, userName: userResponseResult.Name);
                }
            }
        }
    }
}
