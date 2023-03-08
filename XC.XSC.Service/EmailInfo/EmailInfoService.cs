using MimeKit;
using XC.XSC.Models.Entity.EMailInfoAttachment;
using XC.XSC.Models.Entity.Submission;
using XC.XSC.Models.Interface.Submission;
using XC.XSC.Repositories.EmailInfo;
using XC.XSC.Repositories.EmailInfoAttachment;
using XC.XSC.Service.DataStorage;
using XC.XSC.Service.User;
using XC.XSC.ViewModels;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.EmailInfo;
using XC.XSC.ViewModels.EmailInfoAttachment;
using XC.XSC.ViewModels.Submission;

namespace XC.XSC.Service.EmailInfo
{
    /// <summary>
    /// This service is responsible for add or retrieve information about EmailInfo details.
    /// </summary>
    public class EmailInfoService : IEmailInfoService
    {
        private readonly IEmailInfoRepository _EmailInfoRepository;
        private readonly IEmailInfoAttachmentRepository _EmailInfoAttachmentRepository;
        private readonly IResponse _IResponse;
        private readonly IUserContext _userContext;
        private readonly IDataStorageService _dataStorageService;

        /// <summary>
        /// This method is a constructor of EmailInfo Service.
        /// </summary>
        /// <param name="emailInfoRepository"></param>
        /// <param name="emailInfoAttachmentRepository"></param>
        /// <param name="response"></param>
        public EmailInfoService(IEmailInfoRepository emailInfoRepository, IEmailInfoAttachmentRepository emailInfoAttachmentRepository, IResponse response, IUserContext userContext, IDataStorageService dataStorageService)
        {
            _EmailInfoRepository = emailInfoRepository;
            _EmailInfoAttachmentRepository = emailInfoAttachmentRepository;
            _IResponse = response;
            _userContext = userContext;
            _dataStorageService = dataStorageService;
        }
        /// <summary>
        /// This method is used to save Email Info to the database.
        /// </summary>
        /// <param name="emailInfoRequest"></param>
        /// <returns></returns>
        public async Task<IResponse> SaveEmailInfo(AddEmailInfoRequest emailInfoRequest)
        {
            Models.Entity.EmailInfo.EmailInfo emailInfo = new Models.Entity.EmailInfo.EmailInfo()
            {
                EmailId = emailInfoRequest.EmailId,
                FromName = emailInfoRequest.FromName,
                FromEmail = emailInfoRequest.FromEmail,
                ToEmail = emailInfoRequest.ToEmail,
                CCEmail = emailInfoRequest.CCEmail,
                Subject = emailInfoRequest.Subject,
                Body = emailInfoRequest.Body,
                LobId = emailInfoRequest.LobId,
                MessageId = emailInfoRequest.MessageId,
                ParentMessageId = emailInfoRequest.ParentMessageId,
                TotalDocuments = emailInfoRequest.TotalDocuments,
                ReceivedDate = emailInfoRequest.ReceivedDate,
                DocumentId = emailInfoRequest.DocumentId,
                IsDuplicate = emailInfoRequest.IsDuplicate,
                TenantId = emailInfoRequest.TenantId,
                CreatedBy = "System",
                CreatedDate = DateTime.Now,
                MailboxName = emailInfoRequest.MailboxName,
                ConfigurationId = emailInfoRequest.ConfigurationId,
                IsActive = true,
                BodyLength = emailInfoRequest.BodyLength
            };

            await _EmailInfoRepository.AddAsync(emailInfo);
            List<EmailInfoAttachment> listattacment = new List<EmailInfoAttachment>();
            foreach (var attachment in emailInfoRequest.Attachments)
            {

                listattacment.Add(new EmailInfoAttachment()
                {
                    EmailInfoId = emailInfo.Id,
                    FileName = attachment.FileName,
                    FileType = attachment.FileType,
                    DocumentId = attachment.DocumentId,
                    AttachmentId = attachment.AttachmentId,
                    FileSize = attachment.FileSize,
                    SizeUnit = attachment.SizeUnit,
                    TenantId = emailInfoRequest.TenantId,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now,
                    IsActive= true
                });
            }
            await _EmailInfoAttachmentRepository.AddListAsync(listattacment);

            emailInfoRequest.Id = emailInfo.Id;

            _IResponse.IsSuccess = true;
            _IResponse.Message = "SUCCESS";
            _IResponse.Result = emailInfoRequest;
            return _IResponse;

        }

        ///<summary>
        ///Get email info details based on the email info id.
        /// </summary>
        /// <param name="emailInfoId">email info id.</param>
        /// <returns>return the single email info response as common IResponse.</returns>
        public async Task<IResponse> GetEmailInfoDetailByIdAsync(long emailInfoId)
        {
         var emailInfo = await _EmailInfoRepository.GetSingleAsync(a => a.Id == emailInfoId &&a.IsActive == true
                         && a.TenantId == _userContext.UserInfo.TenantId);
            if (emailInfo != null)
            {
                var result = new EmailInfoResponse()
                {
                    Body = emailInfo.Body,
                    CCEmail = emailInfo.CCEmail,
                    DocumentId = emailInfo.DocumentId,
                    EmailId = emailInfo.EmailId,
                    FromEmail = emailInfo.FromEmail,
                    ToEmail = emailInfo.ToEmail,
                    FromName = emailInfo.FromName,
                    Id = emailInfo.Id,
                    Subject = emailInfo.Subject,
                    ReceivedDate = emailInfo.ReceivedDate,
                    MessageId = emailInfo.MessageId,
                    TotalDocuments = emailInfo.TotalDocuments,
                };
                var emailDetails = await _dataStorageService.DownloadDocumentAsync(_userContext.UserInfo.TenantId, emailInfo.DocumentId);
                if(emailDetails.StreamData != null && emailDetails.StreamData.Length > 0)
                {
                    MimeMessage extractedEmailInfoDetails = MimeMessage.Load(emailDetails.StreamData);
                    result.ExtractedBodyDetails = extractedEmailInfoDetails.HtmlBody;
                }
                if (emailInfo.Attachments != null)
                {
                    List<EmailInfoAttachmentResponse> attachments = new List<EmailInfoAttachmentResponse>();
                    foreach (var item in emailInfo.Attachments)
                    {
                        EmailInfoAttachmentResponse emailInfoAttachmentResponse = new EmailInfoAttachmentResponse();
                        emailInfoAttachmentResponse.AttachmentId = item.AttachmentId;
                        emailInfoAttachmentResponse.DocumentId = item.DocumentId;
                        emailInfoAttachmentResponse.Id = item.Id;
                        emailInfoAttachmentResponse.FileName = item.FileName;
                        emailInfoAttachmentResponse.FileType = item.FileType;
                        attachments.Add(emailInfoAttachmentResponse);
                    }
                    result.Attachments = attachments;
                }
                _IResponse.Result = result;
                _IResponse.IsSuccess = true;
                _IResponse.Message = "SUCCESS";
            }
            else
            {
                _IResponse.Result = null;
                _IResponse.IsSuccess = false;
                _IResponse.Message = "Invalid email info id.";
            }
            return _IResponse;
        }
    }
}
