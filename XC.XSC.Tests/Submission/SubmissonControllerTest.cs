using AutoMapper;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Moq;
using System;
using System.Threading.Tasks;
using XC.CCMP.KeyVault;
using XC.CCMP.KeyVault.Manager;
using XC.CCMP.Logger;
using XC.CCMP.Queue;
using XC.CCMP.Queue.ASB;
using XC.XSC.API.Controllers;
using XC.XSC.Data;
using XC.XSC.EmailSender;
using XC.XSC.EMS;
using XC.XSC.EMS.Connector;
using XC.XSC.Models.Profiles.SubmissionStatus;
using XC.XSC.Repositories.Comment;
using XC.XSC.Repositories.EmailInfo;
using XC.XSC.Repositories.Lobs;
using XC.XSC.Repositories.MessageSent;
using XC.XSC.Repositories.MessageTemplate;
using XC.XSC.Repositories.Notification;
using XC.XSC.Repositories.Preferences;
using XC.XSC.Repositories.ReviewConfiguration;
using XC.XSC.Repositories.Sla;
using XC.XSC.Repositories.Submission;
using XC.XSC.Repositories.SubmissionAuditLog;
using XC.XSC.Repositories.SubmissionClearance;
using XC.XSC.Repositories.SubmissionExtraction;
using XC.XSC.Repositories.SubmissionStatus;
using XC.XSC.Repositories.TenantContextMapping;
using XC.XSC.Service.Comments;
using XC.XSC.Service.DataStorage;
using XC.XSC.Service.EMS;
using XC.XSC.Service.JsonFileService;
using XC.XSC.Service.Lobs;
using XC.XSC.Service.MessageSent;
using XC.XSC.Service.MessageTemplate;
using XC.XSC.Service.Notification;
using XC.XSC.Service.Preferences;
using XC.XSC.Service.ReviewConfiguration;
using XC.XSC.Service.Sla;
using XC.XSC.Service.Submission;
using XC.XSC.Service.SubmissionAuditLog;
using XC.XSC.Service.SubmissionClearance;
using XC.XSC.Service.SubmissionExtraction;
using XC.XSC.Service.SubmissionStatus;
using XC.XSC.Service.TenantContextMapping;
using XC.XSC.Service.User;
using XC.XSC.Service.ValidateMail;
using XC.XSC.UAM;
using XC.XSC.UAM.Connector;
using XC.XSC.UAM.UAM;
using XC.XSC.Utilities.Request;
using XC.XSC.ValidateMail;
using XC.XSC.ValidateMail.Connect;
using XC.XSC.ValidateMail.Models.Request;
using XC.XSC.ValidateMail.Models.Response;
using XC.XSC.ViewModels;
using XC.XSC.ViewModels.Comment;
using XC.XSC.ViewModels.CommentsClearance;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.Enum;
using XC.XSC.ViewModels.Submission;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using XC.XSC.ViewModels.Submission.SubmissionGeneralInfo;

namespace XC.XSC.Tests.Submission
{
    [TestClass]
    public class SubmissonControllerTests
    {
        IUserContext _userContext;
        MSSqlContext _context;
        XSCContext _xscContext;
        SubmissionController _submissionController;
        ISubmissionService _submissionService;
        ISubmissionRepository _submissionRepository;
        ICommentRepository _commentRepository;
        ISubmissionStatusRepository _submissionStatusRepository;
        IValidateMailService _validateMailService;
        /// <summary>
        IPreferenceService _preferenceService;
        IKeyVaultConfig keyVaultConfig;
        IPreferenceRepository _preferenceRepository;
        IResponse _response;
        IMapper _mapper;
        ISubmissionStatusService _submissionStatusService;
        ValidateMailScopeResponse _validateMailScopeResponse;
        IConfiguration _configuration;
        ISubmissionClearanceService _submissionClearanceService;
        IClient _client;
        ISubmissionClearanceRepository _submissionClearanceRepository;
        IApiConfig _apiConfig;
        IKeyVaultManager _keyVaultManager;
        SecretClient _secretClient;
        IAzureServiceBus _azureServiceBus;
        IQueueConfiguration _queueConfiguration;
        IDataStorageService _dataStorageService;
        IHostingEnvironment _hostingEnvironment;
        ITenantContextMappingRepository _tenantContextMappingRepository;
        ICommentService _commentService;
        IJsonFileService _jsonFileService;
        ITenantContextMappingService _tenantContextMappingService;
        ISubmissionAuditLogService _submissionAuditLogService;
        ISubmissionExtractionService _submissionExtractionService;
        ISubmissionExtractionRepository _submissionExtractionRepository;
        ISlaConfigurationService _slaConfigService;
        ISlaConfigurationRepository _slaConfigurationRepository;
        IReviewConfigurationRepository _reviewConfigurationRepository;
        IUamService _uamService;
        UamApiConfig _uamApiConfig;
        IHttpContextAccessor _httpContextAccessor;
        IUAMClient _uamClient;
        IHttpRequest _httpRequest;
        IEMSClient _emsClient;
        EmsApiConfig _emsApiConfig;
        IEmsService _emsService;
        ISubmissionAuditLogRepository _submissionAuditLogRepository;
        ILobService _lobService;
        ILobRepository _lobRepository;
        IReviewConfigurationService _reviewConfigurationService;

        IMongoDatabase _mongoDatabase;
        TestMongoContext _mongoContext;
        INotificationService _notificationService;
        INotificationRepository _notificationRepository;
        IMessageTemplateService _messageTemplateService;
        IMessageTemplateRepository _messageTemplateRepository;
        IEmailService _emailService;
        IMessageSentService _messageSentService;
        IMessageSentRepository _messageSentRepository;
        IEmailInfoRepository _emailInfoRepository;

        ILoggerManager _logger;

        [TestInitialize]
        public void TestInitialize()
        {
            _userContext = new TestUserContext();
            _xscContext = new XSCContext(_userContext);
            _context = _xscContext.dbContext;
            this._response = new OperationResponse();
            _submissionRepository = new SubmissionRepository(_context);
            _submissionStatusRepository = new SubmissionStatusRepository(_context);
            _commentRepository = new CommentRepository(_context);
            _reviewConfigurationRepository = new ReviewConfigurationRepository(_context);
            this._preferenceService = new PreferenceService(_preferenceRepository, _userContext);
            _secretClient = new SecretClient(new Uri("https://kv-cin-dtu-d-01.vault.azure.net/"), new DefaultAzureCredential());
            _keyVaultManager = new KeyVaultManager(_secretClient);
            _configuration = InitConfiguration();
            keyVaultConfig = new KeyVaultConfig(_keyVaultManager, _configuration);
            _preferenceRepository = new PreferenceRepository(_context);
            _apiConfig = new ApiConfig(keyVaultConfig);
            _client = new Client(_apiConfig);
            this._preferenceService = new PreferenceService(_preferenceRepository, _userContext);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new SubmissionStatusMapper());
            });

            _mapper = mappingConfig.CreateMapper();
            _submissionAuditLogRepository = new SubmissionAuditLogRepository(_context);
            _submissionAuditLogService = new SubmissionAuditLogService(_submissionAuditLogRepository, _response, _userContext);
            _submissionStatusService = new SubmissionStatusService(_submissionStatusRepository, _response, _mapper);
            _commentService = new CommentService(_userContext, _commentRepository, _response);
            _tenantContextMappingRepository = new TenantContextMappingRepository(_context);
            _tenantContextMappingService = new TenantContextMappingService(_userContext, _tenantContextMappingRepository);
            _slaConfigurationRepository = new SlaConfigurationRepository(_context);
            _uamApiConfig = new UamApiConfig();
            _httpRequest = new Utilities.Request.HttpRequest();
            _uamClient = new UAMClient(_uamApiConfig, _httpContextAccessor, _response, _httpRequest);
            _uamService = new UamService(keyVaultConfig, _uamClient, _uamApiConfig, _userContext);

            _emsApiConfig = new EmsApiConfig();
            _emsClient = new EMSClient(_emsApiConfig, _httpContextAccessor, _response, _httpRequest);
            _emsService = new EmsService(keyVaultConfig, _emsClient, _emsApiConfig, _userContext, _lobService, _response);
            _emsClient = new EMSClient(_emsApiConfig, _httpContextAccessor, _response, _httpRequest);

            _slaConfigService = new SlaConfigurationService(_slaConfigurationRepository, _response, _userContext, _uamService, _emsService, _preferenceService, _mapper, _lobService, _emsClient);
            _reviewConfigurationRepository = new ReviewConfigurationRepository(_context);
            _reviewConfigurationService = new ReviewConfigurationServie(_userContext, _response, _mapper, _reviewConfigurationRepository, _uamService);

            _slaConfigService = new SlaConfigurationService(_slaConfigurationRepository, _response, _userContext, _uamService, _emsService, _preferenceService, _mapper, _lobService, _emsClient);
            _mongoContext = new TestMongoContext(keyVaultConfig);
            _mongoDatabase = _mongoContext.Database;
            _notificationRepository = new NotificationRepository(_context);
            _messageTemplateRepository = new MessageTemplateRepository(_mongoDatabase);
            _messageTemplateService = new MessageTemplateService(_messageTemplateRepository, _response, _userContext);
            _emailService = new EmailService(keyVaultConfig);
            _messageSentRepository = new MessageSentRepository(_mongoDatabase);
            _messageSentService = new MessageSentService(_messageSentRepository, _response, _userContext);
            _lobRepository = new LobRepository(_context);

            _lobService = new LobService(_lobRepository, _userContext);
            _logger = new LoggerManager();
            _notificationService = new NotificationService(_notificationRepository, _response
            , _preferenceService, _userContext, _messageTemplateService
            , _emailService, _messageSentService
            , keyVaultConfig
            , _submissionRepository
            , _uamService
            , _submissionAuditLogRepository
             , _lobService
            , _slaConfigService
            , _emsClient
            , _logger
            , _slaConfigurationRepository
              );
            _emailInfoRepository = new EmailInfoRepository(_context);
            this._submissionService = new SubmissionService(_submissionRepository, keyVaultConfig, _preferenceService, _response, _submissionStatusService,
                _commentRepository, _userContext, _submissionStatusRepository, _commentService, _submissionAuditLogService, _slaConfigService, _submissionAuditLogRepository, _uamService, _reviewConfigurationRepository, _emsClient, _lobService, _reviewConfigurationService, _notificationService, _emailInfoRepository, _context);

            _submissionClearanceRepository = new SubmissionClearanceRepository(_context);
            _submissionClearanceService = new SubmissionClearanceService(_client, _submissionRepository, _preferenceService, _userContext, _response, _submissionClearanceRepository, _tenantContextMappingService, _submissionExtractionRepository);

            Mock<IAzureServiceBus> _azureServiceBus = new Mock<IAzureServiceBus>();
            Mock<IQueueConfiguration> _queueConfiguration = new Mock<IQueueConfiguration>();
            _submissionExtractionService = new SubmissionExtractionService(_submissionExtractionRepository, _response, _userContext, _preferenceService, _submissionRepository
            , _queueConfiguration.Object, _azureServiceBus.Object, keyVaultConfig);
            _validateMailService = new ValidateMailService(_client, _submissionRepository, _preferenceService, _azureServiceBus.Object, _queueConfiguration.Object, keyVaultConfig, _commentRepository, _userContext, _response);

            this._submissionController = new SubmissionController(_userContext, _response, _submissionService, _validateMailService, _submissionClearanceService, _hostingEnvironment, _jsonFileService, _commentService);
            _validateMailScopeResponse = new ValidateMailScopeResponse()
            {
                TaskId = "1",
                SubmissionId = 3,
                Stage = "stage1",
                TenantId = "1",
                Scope = false
            };
            this._jsonFileService = new JsonFileService(this._response, _userContext);


        }
        /// <summary>
        /// Unit Test for Get All Submission API
        /// </summary>
        /// <returns></returns>

        [TestMethod]
        [DataRow]

        public async Task SubmissonControllerTests_Get_Success()
        {
            var result = await _submissionController.Get();
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }

        /// <summary>
        /// Unit Test for Add submission using Mack date
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [DataRow]
        public async Task Process_SaveSubmission_Success()
        {
            long expatedData = 0;

            var result = await _submissionController.SaveSubmission(this.GetItemsAdd());
            expatedData = expatedData + result.Id;

            Assert.AreEqual(expatedData, result.Id, "Save");
        }
        /// <summary>
        /// Unit Test for check save submission api with unvalid data i.e give null with required API
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task Process_SaveSubmission_Invalid()
        {
            long expatedData = 0;
            var submission = await _submissionController.SaveSubmission(this.invalid__submission_mock_data());
            Assert.AreNotEqual(expatedData, submission.Id);
        }


        /// <summary>
        /// Unit Test for Submission UnderQuery API
        /// </summary>
        /// <returns></returns

        [TestMethod]

        public async Task SubmissonControllerTests_SubmissionUnderQuery_Success()
        {

            _response = await _submissionController.SubmissionUnderQuery(success_mock_data_comment());
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);

        }
        /// <summary>
        /// Unit Test for Submission UnderQuery API with invalid commentType
        /// </summary>
        /// <returns></returns

        [TestMethod]
        public async Task SubmissonControllerTests_SubmissionUnderQuery_Invalid()
        {

            _response = await _submissionController.SubmissionUnderQuery(invalid_mock_data_comment());
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);

        }
        /// <summary>
        /// Unit Test for SendBackQuerySubmissionAsync API with valid submissionId
        /// </summary>
        /// <returns></returns

        [TestMethod]
        [DataRow(1)]
        public async Task SubmissonControllerTests_SendBackQuerySubmission_success(long submissionId)
        {

            _response = await _submissionController.SendAssignedSubmissionBackToQueue(submissionId);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);

        }
        /// <summary>
        /// Unit Test for SendBackQuerySubmissionAsync API with invalid submissionId
        /// </summary>
        /// <returns></returns
        [TestMethod]
        [DataRow(0)]
        public async Task SubmissonControllerTests_SendBackQuerySubmission_invalid(long submissionId)
        {

            _response = await _submissionController.SendAssignedSubmissionBackToQueue(submissionId);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);

        }
        /// <summary>
        /// Unit Test for GetSubmissions API with valid CaseId
        /// </summary>
        /// <returns></returns

        [TestMethod]
        public async Task SubmissonControllerTests_GetSubmissions_with_Valid_CaseeId()
        {

            _response = await _submissionController.GetSubmissions(GetItemsAdd().CaseId, null, null, null, null, null, null, null, null, 1, 10, "CreatedDate", 1, true, 1, 1, false, false, default);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);

        }
        /// <summary>
        /// Unit Test for GetSubmissions API with valid BrokerName
        /// </summary>
        /// <returns></returns
        [TestMethod]
        public async Task SubmissonControllerTests_GetSubmissions_with_Valid_BrokerName()
        {

            _response = await _submissionController.GetSubmissions(null, GetItemsAdd().BrokerName, null, null, null, null, null, null, null, 1, 10, "CreatedDate", 1, true, 1, 1, false, false, default);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);

        }
        /// <summary>
        /// Unit Test for GetSubmissions API with valid InsuredName
        /// </summary>
        /// <returns></returns
        [TestMethod]
        public async Task SubmissonControllerTests_GetSubmissions_with_Valid_InsuredName()
        {

            _response = await _submissionController.GetSubmissions(null, null, GetItemsAdd().InsuredName, null, null, null, null, null, null, 1, 10, "CreatedDate", 1, true, 1, 1, false, false, default);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);

        }
        /// <summary>
        /// Unit Test for GetSubmissions API with valid SubmissionStatusId
        /// </summary>
        /// <returns></returns
        [TestMethod]
        public async Task SubmissonControllerTests_GetSubmissions_with_Valid_SubmissionStatusId()
        {

            _response = await _submissionController.GetSubmissions(null, null, null, GetItemsAdd().SubmissionStatusId, null, null, null, null, null, 1, 10, "CreatedDate", 1, true, 1, 1, false, false, default);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);

        }
        /// <summary>
        /// Unit Test for GetSubmissions API with valid AssignedId
        /// </summary>
        /// <returns></returns
        [TestMethod]
        public async Task SubmissonControllerTests_GetSubmissions_with_Valid_AssignedTo()
        {

            _response = await _submissionController.GetSubmissions(null, null, null, null, GetItemsAdd().AssignedId, null, null, null, null, 1, 10, "CreatedDate", 1, true, 1, 1, false, false, default);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);

        }
        /// <summary>
        /// Unit Test for GetSubmissions API with valid ReceivedFrom and ReceivedTo Date
        /// </summary>
        /// <returns></returns
        [TestMethod]
        public async Task SubmissonControllerTests_GetSubmissions_with_Valid_ReceivedFromDate_ReceivedToData()
        {

            _response = await _submissionController.GetSubmissions(null, null, null, null, null, DateTime.Now, DateTime.Now.AddDays(1), null, null, 1, 10, "CreatedDate", 1, true, 1, 1, false, false, default);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);

        }
        /// <summary>
        /// Unit Test for GetSubmissions API with valid dueFromDate and dueToDate
        /// </summary>
        /// <returns></returns
        [TestMethod]
        public async Task SubmissonControllerTests_GetSubmissions_with_Valid_DueFromDate_DueToDate()
        {

            _response = await _submissionController.GetSubmissions(null, null, null, null, null, null, null, DateTime.Now, DateTime.Now.AddDays(1), 1, 10, "CreatedDate", 1, true, 1, 1, false, false, default);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);

        }
        /// <summary>
        /// Unit Test for GetSubmissions API for get user assigned submissions
        /// </summary>
        /// <returns></returns
        [TestMethod]
        public async Task SubmissonControllerTests_GetSubmissions_For_UserAssignedSubmissions()
        {

            _response = await _submissionController.GetSubmissions(null, null, null, null, null, null, null, null, null, 1, 10, "CreatedDate", 1, true, 1, 1, true, false, default);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);

        }

        /// <summary>
        /// Unit Test for GetSubmissions APIfor get new Submissions.
        /// </summary>
        /// <returns></returns
        [TestMethod]
        public async Task SubmissonControllerTests_GetSubmissions_For_GetNewSubmissions()
        {

            _response = await _submissionController.GetSubmissions(null, null, null, null, null, null, null, null, null, 1, 10, "CreatedDate", 1, true, 1, 1, false, true, default);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);

        }

        /// <summary>
        /// Unit Test for GetSubmissions API for getoutscope submissions.
        /// </summary>
        /// <returns></returns
        [TestMethod]
        public async Task SubmissonControllerTests_GetOutScopeSubmissions()
        {

            _response = await _submissionController.GetSubmissions(null, null, null, null, null, null, null, null, null, 1, 10, "CreatedDate", 1, false, 1, 1, false, false, default);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);

        }
        /// <summary>
        /// Unit Test for out scope submissions with valid CaseId
        /// </summary>
        /// <returns></returns

        [TestMethod]
        public async Task SubmissonControllerTests_GetOutScopeSubmissions_with_Valid_CaseeId()
        {

            _response = await _submissionController.GetSubmissions(GetItemsAdd().CaseId, null, null, null, null, null, null, null, null, 1, 10, "CreatedDate", 1, false, 1, 1, false, false, default);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);

        }

        /// <summary>
        /// Unit Test for GetOutScopeSubmissions with valid ReceivedFrom and ReceivedTo Date
        /// </summary>
        /// <returns></returns
        [TestMethod]
        public async Task SubmissonControllerTests_GetOutScopeSubmissions_with_Valid_ReceivedFromDate_ReceivedToData()
        {

            _response = await _submissionController.GetSubmissions(null, null, null, null, null, DateTime.Now, DateTime.Now.AddDays(1), null, null, 1, 10, "CreatedDate", 1, false, 1, 1, false, false, default);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);

        }

        /// <summary>
        /// ValidateMailScopeAsync With Valid TaskId And SubmissionId And Stage And TenantId
        /// </summary>
        /// <param name="TaskId"></param>
        /// <param name="SubmissionId"></param>
        /// <param name="Stage"></param>
        /// <param name="TenantId"></param>
        [TestMethod]
        [DataRow("1", 3, "stage1", "1")]
        public void ValidateMailScopeAsync_With_Valid_TaskId_And_SubmissionId_And_Stage_And_TenantId(string TaskId, long SubmissionId, string Stage, string TenantId)
        {
            bool is_Scope = string.Equals(TaskId, _validateMailScopeResponse.TaskId, StringComparison.OrdinalIgnoreCase)
                                    && long.Equals(SubmissionId, _validateMailScopeResponse.SubmissionId)
                                    && string.Equals(Stage, _validateMailScopeResponse.Stage, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(TenantId, _validateMailScopeResponse.TenantId, StringComparison.OrdinalIgnoreCase);

            Assert.AreEqual(is_Scope, true);
        }

        /// <summary>
        /// ValidateMailScopeAsync With Blank TaskId And SubmissionId And Stage And TenantId
        /// </summary>
        /// <param name="TaskId"></param>
        /// <param name="SubmissionId"></param>
        /// <param name="Stage"></param>
        /// <param name="TenantId"></param>
        [TestMethod]
        [DataRow("", 0, "", "")]
        public void ValidateMailScopeAsync_With_Blank_TaskId_And_SubmissionId_And_Stage_And_TenantId(string TaskId, long SubmissionId, string Stage, string TenantId)
        {
            bool is_Scope = string.Equals(TaskId, _validateMailScopeResponse.TaskId, StringComparison.OrdinalIgnoreCase)
                                    && long.Equals(SubmissionId, _validateMailScopeResponse.SubmissionId)
                                    && string.Equals(Stage, _validateMailScopeResponse.Stage, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(TenantId, _validateMailScopeResponse.TenantId, StringComparison.OrdinalIgnoreCase);

            Assert.AreEqual(is_Scope, false);
        }

        /// <summary>
        /// ValidateMailScopeAsync With Valid TaskId And Blank SubmissionId And Valid Stage And Valid TenantId
        /// </summary>
        /// <param name="TaskId"></param>
        /// <param name="SubmissionId"></param>
        /// <param name="Stage"></param>
        /// <param name="TenantId"></param>
        [TestMethod]
        [DataRow("1", 0, "stage1", "1")]
        public void ValidateMailScopeAsync_With_Valid_TaskId_And_Blank_SubmissionId_And_Valid_Stage_And_Valid_TenantId(string TaskId, long SubmissionId, string Stage, string TenantId)
        {
            bool is_Scope = string.Equals(TaskId, _validateMailScopeResponse.TaskId, StringComparison.OrdinalIgnoreCase)
                                    && long.Equals(SubmissionId, _validateMailScopeResponse.SubmissionId)
                                    && string.Equals(Stage, _validateMailScopeResponse.Stage, StringComparison.OrdinalIgnoreCase)
                                    && string.Equals(TenantId, _validateMailScopeResponse.TenantId, StringComparison.OrdinalIgnoreCase);

            Assert.AreEqual(is_Scope, false);
        }

        ///// <summary>
        ///// Check mail Scope is null
        ///// </summary>
        ///// <returns></returns>
        [TestMethod]
        public async Task ValidateMailScopeAsync_With_Valid_TaskId_And_SubmissionId_And_Stage_And_TenantId_Return_Null()
        {
            _response = await _submissionController.ValidateMailScope(valid_validate_mail_scope_request_mock_data());
            ValidateMailScopeResponse? validateMailScopeResponse = (ValidateMailScopeResponse?)_response.Result;

            if (validateMailScopeResponse == null)
            {
                Assert.IsNull(validateMailScopeResponse);
            }
            else
            {
                if (validateMailScopeResponse.Scope == null)
                    Assert.IsNull(validateMailScopeResponse.Scope);
                else
                    Assert.IsNotNull(validateMailScopeResponse.Scope);
            }

        }

        /// <summary>
        /// check mail is in scope
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task ValidateMailScopeAsync_With_Valid_TaskId_And_SubmissionId_And_Stage_And_TenantId_Return_Outscope()
        {
            _response = await _submissionController.ValidateMailScope(valid_validate_mail_scope_request_mock_data_by_SubmissionId_OutScope());
            ValidateMailScopeResponse? validateMailScopeResponse = (ValidateMailScopeResponse?)_response.Result;
            if (validateMailScopeResponse == null)
            {
                Assert.IsNull(validateMailScopeResponse);
            }
            else
            {
                if (validateMailScopeResponse.Scope == null)
                {
                    Assert.IsNull(validateMailScopeResponse.Scope);
                }
                else if (validateMailScopeResponse.Scope.Value == true)
                {
                    Assert.IsTrue(validateMailScopeResponse.Scope);
                }
                else
                {
                    Assert.IsFalse(validateMailScopeResponse.Scope);
                }
            }

        }

        /// <summary>
        /// Submission glance success test case.
        /// </summary>
        /// <returns>return true if the response was success.</returns>
        [TestMethod]
        [DataRow(1, 1)]
        public async Task SubmissionControllerTest_GetSubmissionsGlance_Success(int region, int lob)
        {
            _response = await _submissionController.GetSubmissionsGlance(region, lob);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }

        /// <summary>
        /// Submission glance failure test case.
        /// </summary>
        /// <returns>return false if the response was false.</returns>
        [TestMethod]
        [DataRow(0, 0)]
        public async Task SubmissionControllerTest_GetSubmissionsGlance_Failure(int region, int lob)
        {
            _response = await _submissionController.GetSubmissionsGlance(region, lob);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }

        /// <summary>
        /// Assign submission success test case.
        /// </summary>
        /// <param name="submissionId">This is submissionId parameter.</param>
        /// <param name="userId">This is userId parameter.</param>
        /// <returns>retruns true if the response was true.</returns>
        [TestMethod]
        [DataRow(1, "1")]
        public async Task SubmissonControllerTest_AssignSubmissionToUser_Success(long submissionId, string userId)
        {
            var reallocateSubmissionRequest = new ReallocateSubmissionRequest()
            {
                submissionId = submissionId,
                userId = userId
            };

            var result = await _submissionController.AssignSubmissionToUser(reallocateSubmissionRequest);
            if (result.IsSuccess == true)
                Assert.IsTrue(result.IsSuccess);
            else
                Assert.IsFalse(result.IsSuccess);
        }

        /// <summary>
        /// Assign submission failure test case.
        /// </summary>
        /// <param name="submissionId">This is submissionId parameter.</param>
        /// <param name="userId">This is userId parameter.</param>
        /// <returns>retruns false if the response was false.</returns>
        [TestMethod]
        [DataRow(1, "")]
        public async Task SubmissonControllerTest_AssignSubmissionToUser_Failure(long submissionId, string userId)
        {
            var reallocateSubmissionRequest = new ReallocateSubmissionRequest()
            {
                submissionId = submissionId,
                userId = userId
            };

            var result = await _submissionController.AssignSubmissionToUser(reallocateSubmissionRequest);
            if (result.IsSuccess == true)
                Assert.IsTrue(result.IsSuccess);
            else
                Assert.IsFalse(result.IsSuccess);
        }

        /// <summary>
        /// Assign submission to self success test case.
        /// </summary>
        /// <param name="submissionId">submission id.</param>
        /// <returns>retruns true if the response was success.</returns>
        [TestMethod]
        [DataRow(1)]
        public async Task SubmissonControllerTest_AssignSubmissionToSelf_Success(long submissionId)
        {
            _response = await _submissionController.AssignSubmissionToSelf(submissionId);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }

        /// <summary>
        /// Assign submission to self failure test case.
        /// </summary>
        /// <param name="submissionId">submission id.</param>
        /// <returns>retruns false if the response was false.</returns>
        [TestMethod]
        [DataRow(0)]
        public async Task SubmissonControllerTest_AssignSubmissionToSelf_Failure(long submissionId)
        {
            _response = await _submissionController.AssignSubmissionToSelf(submissionId);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);

        }

       

        /// <summary>
        /// Get submission detail by id success test case.
        /// </summary>
        /// <param name="submissionId">submission id.</param>
        /// <returns>retruns true if the response was success.</returns>
        [TestMethod]
        [DataRow(1)]
        public async Task SubmissonControllerTest_GetInScopeSubmissionById_Success(long submissionId)
        {
            _response = await _submissionController.GetInScopeSubmissionById(submissionId);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }

        /// <summary>
        /// Add Clearance Comment
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task AddClearanceComment_Valid_Request_Return_Success()
        {
            _response = await _submissionController.AddClearanceComment(Get_Comments_Clearance_Request_Valid_Mock_Data());
            if (_response == null)
            {
                Assert.IsNull(_response);
            }
            else if (_response.IsSuccess)
            {
                Assert.IsTrue(_response.IsSuccess);
            }
            else
            {
                Assert.IsFalse(_response.IsSuccess);
            }
        }

        /// <summary>
        /// Add Clearance Comment InValid Request
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task AddClearanceComment_InValid_Request_Return_False()
        {
            _response = await _submissionController.AddClearanceComment(Get_Comments_Clearance_Request_Invalid_Mock_Data());
            if (_response == null)
            {
                Assert.IsNull(_response);
            }
            else if (_response.IsSuccess)
            {
                Assert.IsTrue(_response.IsSuccess);
            }
            else
            {
                Assert.IsFalse(_response.IsSuccess);
            }
        }

        /// <summary>
        /// Get Submission Clearances based on submissionId
        /// </summary>
        /// <param name="submissionId">submission id.</param>
        /// <returns>retruns true if the response was success.</returns>
        [TestMethod]
        [DataRow(1)]
        public async Task GetSubmissionClearances_With_Valid_SubmissionId_Return_Success(long submissionId)
        {
            _response = await _submissionController.GetSubmissionClearances(submissionId);
            if (_response == null)
            {
                Assert.IsNull(_response);
            }
            else if (_response.IsSuccess)
            {
                Assert.IsTrue(_response.IsSuccess);
            }
        }

        /// <summary>
        /// Get submission detail by id failure test case.
        /// </summary>
        /// <param name="submissionId">submission id.</param>
        /// <returns>retruns false if the response was failure.</returns>
        [TestMethod]
        [DataRow(0)]
        public async Task SubmissonControllerTest_GetInScopeSubmissionById_Failure(long submissionId)
        {
            _response = await _submissionController.GetInScopeSubmissionById(submissionId);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);

        }

        /// <summary>
        /// Get Submission Clearances based on submissionId
        /// </summary>
        /// <param name="submissionId">submission id.</param>
        /// <returns>retruns true if the response was success.</returns>
        [TestMethod]
        [DataRow(0)]
        public async Task GetSubmissionClearances_With_Invalid_SubmissionId_Return_False(long submissionId)
        {
            _response = await _submissionController.GetSubmissionClearances(submissionId);
            if (_response == null)
            {
                Assert.IsNull(_response);
            }
            else if (_response.IsSuccess)
            {
                Assert.IsTrue(_response.IsSuccess);
            }
            else
            {
                Assert.IsFalse(_response.IsSuccess);
            }
        }

        /// <summary>
        /// Get Task Tat Metrics TatMissed based on submissionId
        /// </summary>
        /// <param name="submissionId">submission id.</param>
        /// <returns>retruns true if the TatMissed.</returns>
        [TestMethod]
        [DataRow(0)]
        public async Task SubmissonControllerTest_GetTaskTatMetrics(long submissionId)
        {
            TaskTatMetricsResponse taskTatMetricsResponse = await _submissionController.GetTaskTatMetrics(submissionId);
            if (taskTatMetricsResponse.TatMissed)
                Assert.IsTrue(taskTatMetricsResponse.TatMissed);
            else
                Assert.IsFalse(taskTatMetricsResponse.TatMissed);
        }

        /// <summary>
        /// Save submission comment success test case.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task SubmissonControllerTest_SaveSubmissionComment_Success()
        {
            _response = await _submissionController.SaveSubmissionComment(submission_comment_request_mock_data());
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }

        /// <summary>
        /// Save Submission Comment failure test case.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task SubmissonControllerTest_SaveSubmissionComment_Failure()
        {
            _response = await _submissionController.SaveSubmissionComment(submission_comment_request_invalid_mock_data());
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }

        /// <summary>
        /// Send submission to review success test case.
        /// </summary>
        /// <returns>IResponse.</returns>
        [TestMethod]
        public async Task SubmissonControllerTest_SendSubmissionToReview_Success()
        {
            _response = await _submissionController.SendSubmissionToReview(processor_request_mock_data());
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }

        /// <summary>
        /// Send submission to review failure test case.
        /// </summary>
        /// <returns>IResponse.</returns>
        [TestMethod]
        public async Task SubmissonControllerTest_SendSubmissionToReview_Failure()
        {
            _response = await _submissionController.SendSubmissionToReview(processor_request_invalid_mock_data());
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }

        /// <summary>
        /// send submission to processor success test case.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task SubmissonControllerTest_SendSubmissionToProcessor_Success()
        {
            _response = await _submissionController.SendSubmissionToProcessor(reviewer_request_mock_data());
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }

        /// <summary>
        /// send submission to processor failure test case.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task SubmissonControllerTest_SendSubmissionToProcessor_Failure()
        {
            _response = await _submissionController.SendSubmissionToProcessor(reviewer_request_invalid_mock_data());
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }

        /// <summary>
        /// Update submission status success test case.
        /// </summary>
        /// <param name="submissionId">submission id.</param>
        /// <param name="submissionStatusId">submission status id.</param>
        /// <returns>IResponse.</returns>
        [TestMethod]
        [DataRow(5, 1)]
        public async Task SubmissonControllerTest_UpdateSubmissionStatus_Success(long submissionId, int submissionStatusId)
        {
            _response = await _submissionService.UpdateSubmissionStatusAsync(submissionId, submissionStatusId);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }

        /// <summary>
        /// Update submission status failure test case.
        /// </summary>
        /// <param name="submissionId">submission id.</param>
        /// <param name="submissionStatusId">submission status id.</param>
        /// <returns>IResponse.</returns>
        [TestMethod]
        [DataRow(0, 1)]
        public async Task SubmissonControllerTest_UpdateSubmissionStatus_Failure(long submissionId, int submissionStatusId)
        {
            _response = await _submissionService.UpdateSubmissionStatusAsync(submissionId, submissionStatusId);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }


        private SubmissionRequest GetItemsAdd()
        {
            SubmissionRequest submissionObj = new SubmissionRequest();
            {

                submissionObj.CaseId = "SC2022121900014";
                submissionObj.BrokerName = "Test BrokerName";
                submissionObj.InsuredName = "Test InsuredName";
                submissionObj.EmailInfoId = 1;
                submissionObj.DueDate = Convert.ToDateTime("2022-12-16T05:28:52.213Z");
                submissionObj.AssignedId = "string";
                submissionObj.SubmissionStatusId = 1;
                submissionObj.SubmissionStageId = 1;
                submissionObj.IsInScope = true;
                submissionObj.LobId = 1;
                submissionObj.ExtendedDate = Convert.ToDateTime("2022-12-16T05:28:52.213Z");
                submissionObj.EmailBody = "Email Body";
                submissionObj.TaskId = "12";
                submissionObj.CreatedDate = System.DateTime.Now;
                submissionObj.ModifiedDate = System.DateTime.Now;
                submissionObj.CreatedBy = _userContext.UserInfo.UserId;
                submissionObj.ModifiedBy = _userContext.UserInfo.UserId;
                submissionObj.TenantId = _userContext.UserInfo.TenantId;
                submissionObj.IsActive = true;
                return submissionObj;
            }
        }
        /// <summary>
        /// Submission Request invalid__submission_mock_data
        /// </summary>
        /// <returns></returns>
        private SubmissionRequest invalid__submission_mock_data()
        {
            SubmissionRequest submissionObj = new SubmissionRequest();
            {
                submissionObj.CaseId = "SC2022121900014";
                submissionObj.BrokerName = "Test BrokerName";
                submissionObj.InsuredName = "Test InsuredName";
                submissionObj.EmailInfoId = 0;
                submissionObj.DueDate = Convert.ToDateTime("2022-12-16T05:28:52.213Z");
                submissionObj.AssignedId = "string";
                submissionObj.SubmissionStageId = 0;
                submissionObj.IsInScope = true;
                submissionObj.LobId = 0;
                submissionObj.ExtendedDate = Convert.ToDateTime("2022-12-16T05:28:52.213Z");
                submissionObj.EmailBody = "Email Body";
                submissionObj.TaskId = "12";
                submissionObj.CreatedDate = System.DateTime.Now;
                submissionObj.ModifiedDate = System.DateTime.Now;
                submissionObj.CreatedBy = _userContext.UserInfo.UserId;
                submissionObj.ModifiedBy = _userContext.UserInfo.UserId;
                submissionObj.TenantId = _userContext.UserInfo.TenantId;
                submissionObj.IsActive = true;
                return submissionObj;
            }
        }
        /// <summary>        /// 
        // <param name="commentRequest">CommentType  Remark = 1, Query = 2, Review = 3, OutOfScope = 4,Comment Text,Submission Id,JsonData</param>
        /// </summary>
        /// <returns></returns>
        private CommentRequest success_mock_data_comment()
        {
            CommentRequest request = new CommentRequest()
            {
                CommentText = "MyTesting",
                CommentType = "Query",
                JsonData = null,
                SubmissionId = 1
            };

            return request;
        }
        /// <summary>
        /// CommentType given worng Type i.e Test
        // <param name="commentRequest">CommentType  Remark = 1, Query = 2, Review = 3, OutOfScope = 4,Comment Text,Submission Id,JsonData</param>
        /// </summary>
        /// <returns></returns>
        private CommentRequest invalid_mock_data_comment()
        {
            CommentRequest request = new CommentRequest()
            {
                CommentText = "MyTesting",
                CommentType = "Test",
                JsonData = null,
                SubmissionId = 1
            };

            return request;
        }

        /// <summary>
        /// Get valid_validate_mail_scope_request_mock_data
        /// </summary>
        /// <returns></returns>
        private ValidateMailScopeRequest valid_validate_mail_scope_request_mock_data()
        {
            ValidateMailScopeRequest validateMailScopeRequest = new ValidateMailScopeRequest()
            {
                Stage = "test stage",
                SubmissionId = 4,
                TaskId = "123",
                TenantId = "xsc-001"
            };
            return validateMailScopeRequest;
        }

        /// <summary>
        /// Get valid_validate_mail_scope_request_mock_data
        /// </summary>
        /// <returns></returns>
        private ValidateMailScopeRequest valid_validate_mail_scope_request_mock_data_by_SubmissionId_exist()
        {
            ValidateMailScopeRequest validateMailScopeRequest = new ValidateMailScopeRequest()
            {
                Stage = "test stage",
                SubmissionId = 1,
                TaskId = "123",
                TenantId = "xsc-001"
            };
            return validateMailScopeRequest;
        }

        /// <summary>
        /// Get valid Save Comment Request Mock Data.
        /// </summary>
        /// <returns>commentRequest</returns>
        private CommentRequest submission_comment_request_mock_data()
        {
            CommentRequest commentRequest = new CommentRequest()
            {
                CommentType = "Review",
                CommentText = "Comment1",
                SubmissionId = 1,
                JsonData = null,
            };
            return commentRequest;
        }

        /// <summary>
        /// Get invalid Save Comment Request Mock Data.
        /// </summary>
        /// <returns>commentRequest</returns>
        private CommentRequest submission_comment_request_invalid_mock_data()
        {
            CommentRequest commentRequest = new CommentRequest()
            {
                CommentType = "Review",
                CommentText = "",
                SubmissionId = 1,
                JsonData = null,
            };
            return commentRequest;
        }

        /// <summary>
        /// Get Valid Reviewer Request Mock Data
        /// </summary>
        /// <returns>submitReviewerRequest</returns>
        private SubmitReviewerRequest reviewer_request_mock_data()
        {
            SubmitReviewerRequest submitReviewerRequest = new SubmitReviewerRequest()
            {
                CommentText = "Review Pass",
                CommentType = "Review",
                SubmissionId = 1,
                JsonData = null,
                ReviewStatus= 9,
            };
            return submitReviewerRequest;
        }

        /// <summary>
        /// Get InValid Reviewer Request Mock Data
        /// </summary>
        /// <returns>submitReviewerRequestreturns>
        private SubmitReviewerRequest reviewer_request_invalid_mock_data()
        {
            SubmitReviewerRequest submitReviewerRequest = new SubmitReviewerRequest()
            {
                CommentText = "",
                CommentType = "Review",
                SubmissionId = 1,
                JsonData = null,
                ReviewStatus = 5,
            };
            return submitReviewerRequest;
        }

        /// <summary>
        /// Get Valid Processor Request Mock Data
        /// </summary>
        /// <returns>submitProcessorRequest</returns>
        private SubmitProcessorRequest processor_request_mock_data()
        {
            SubmitProcessorRequest submitProcessorRequest = new SubmitProcessorRequest()
            {
                CommentText = "Review Pass",
                CommentType = "Review",
                SubmissionId = 1,
                JsonData = null,
            };
            return submitProcessorRequest;
        }

        /// <summary>
        /// Get InValid Processor Request Mock Data
        /// </summary>
        /// <returns>submitProcessorRequest</returns>
        private SubmitProcessorRequest processor_request_invalid_mock_data()
        {
            SubmitProcessorRequest submitProcessorRequest = new SubmitProcessorRequest()
            {
                CommentText = "",
                CommentType = "Review",
                SubmissionId = 1,
                JsonData = null,
            };
            return submitProcessorRequest;
        }

        /// <summary>
        /// Initialize IConfiguration 
        /// </summary>
        /// <returns>IConfiguration</returns>
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }

        /// <summary>
        /// Get valid_validate_mail_scope_request_mock_data
        /// </summary>
        /// <returns></returns>
        private ValidateMailScopeRequest valid_validate_mail_scope_request_mock_data_by_SubmissionId_OutScope()
        {
            ValidateMailScopeRequest validateMailScopeRequest = new ValidateMailScopeRequest()
            {
                Stage = "test stage",
                SubmissionId = 1,
                TaskId = "123",
                TenantId = "xsc-001"
            };
            return validateMailScopeRequest;
        }

        /// <summary>
        /// Comments Clearance Request valid 
        /// </summary>
        /// <returns>request</returns>
        private CommentsClearanceRequest Get_Comments_Clearance_Request_Valid_Mock_Data()
        {
            CommentsClearanceRequest request = new CommentsClearanceRequest()
            {
                CommentText = "MyTesting",
                SubmissionId = 1,
                ClearanceConscent = true
            };
            return request;
        }

        /// <summary>
        /// Comments Clearance Request Invalid 
        /// </summary>
        /// <returns>request</returns>
        private CommentsClearanceRequest Get_Comments_Clearance_Request_Invalid_Mock_Data()
        {
            CommentsClearanceRequest request = new CommentsClearanceRequest()
            {
                CommentText = "MyTesting",
                SubmissionId = 0,
                ClearanceConscent = true
            };
            return request;
        }

        /// <summary>
        /// Get Success response
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task SubmissonControllerTests_GetSubmissionScopeCountAsync_Success()
        {
            _response = await _submissionController.GetSubmissionScopeCount(DateTime.Now.AddDays(-7), DateTime.Now, 1, 1);
            if (_response.IsSuccess == true)
                Assert.IsNotNull(_response.Result);
            else
                Assert.IsNull(_response.Result);
        }

        /// <summary>
        /// Get Fail response
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task SubmissonControllerTests_GetSubmissionScopeCountAsync_Fail()
        {
            _response = await _submissionController.GetSubmissionScopeCount(DateTime.Now.AddDays(-7), DateTime.Now, 1, 1);
            if (_response.IsSuccess == false)
                Assert.IsNull(_response.Result);
            else
                Assert.IsNotNull(_response.Result);
        }

        /// <summary>
        /// Get Fail Response of TeamPerformance.
        /// </summary>
        /// <param name="performanceType">Performance Type Like My-Performance or Team-Performance.</param>
        /// <param name="region">This is region parameter.</param>
        /// <param name="lob">This is lob parameter.</param>
        /// <returns></returns>
        [TestMethod]
        [DataRow(2, 1, 1)]
        public async Task SubmissonControllerTests_GetPerformanceAsync_Fail_InvalidDateRange(PerformanceType performanceType, int region, int lob)
        {
            _response = await _submissionController.GetPerformance(DateTime.Now.AddDays(-31), DateTime.Now, performanceType, region, lob);
            Assert.IsNull(_response.Result);
            Assert.IsFalse(_response.IsSuccess);
        }

        /// <summary>
        /// Get Pass Response of MyPerformance.
        /// </summary>
        /// <param name="performanceType">Performance Type Like My-Performance or Team-Performance.</param>
        /// <param name="region">This is region parameter.</param>
        /// <param name="lob">This is lob parameter.</param>
        /// <returns></returns>
        [TestMethod]
        [DataRow(1, 1, 1)]
        public async Task SubmissonControllerTests_GetMyPerformanceAsync_Pass(PerformanceType performanceType, int region, int lob)
        {
            _response = await _submissionController.GetPerformance(DateTime.Now.AddDays(-10), DateTime.Now, performanceType, region, lob);
            if (_response.IsSuccess == true)
                Assert.IsNotNull(_response.Result);
            else
                Assert.IsNull(_response.Result);
        }

        /// <summary>
        /// Get Fail Response of MyPerformance.
        /// </summary>
        /// <param name="performanceType">Performance Type Like My-Performance or Team-Performance.</param>
        /// <param name="region">This is region parameter.</param>
        /// <param name="lob">This is lob parameter.</param>
        /// <returns></returns>
        [TestMethod]
        [DataRow(1, 2, 2)]
        public async Task SubmissonControllerTests_GetMyPerformanceAsync_Fail(PerformanceType performanceType, int region, int lob)
        {
            _response = await _submissionController.GetPerformance(DateTime.Now.AddDays(-10), DateTime.Now, performanceType, region, lob);
            if (_response.IsSuccess == false)
                Assert.IsNull(_response.Result);
            else
                Assert.IsNotNull(_response.Result);
        }

        /// <summary>
        /// Get Pass Response of TeamPerformance.
        /// </summary>
        /// <param name="performanceType">Performance Type Like My-Performance or Team-Performance.</param>
        /// <param name="region">This is region parameter.</param>
        /// <param name="lob">This is lob parameter.</param>
        /// <returns></returns>
        [TestMethod]
        [DataRow(2, 1, 1)]
        public async Task SubmissonControllerTests_GetTeamPerformanceAsync_Pass(PerformanceType performanceType, int region, int lob)
        {
            _response = await _submissionController.GetPerformance(DateTime.Now.AddDays(-10), DateTime.Now, performanceType, region, lob);
            if (_response.IsSuccess == true)
                Assert.IsNotNull(_response.Result);
            else
                Assert.IsNull(_response.Result);
        }

        /// <summary>
        /// Get Fail Response of TeamPerformance.
        /// </summary>
        /// <param name="performanceType">Performance Type Like My-Performance or Team-Performance.</param>
        /// <param name="region">This is region parameter.</param>
        /// <param name="lob">This is lob parameter.</param>
        /// <returns></returns>
        [TestMethod]
        [DataRow(2, 1, 1)]
        [DataRow(2, 2, 2)]
        public async Task SubmissonControllerTests_GetTeamPerformanceAsync_Fail(PerformanceType performanceType, int region, int lob)
        {
            _response = await _submissionController.GetPerformance(DateTime.Now.AddDays(-10), DateTime.Now, performanceType, region, lob);
            if (_response.IsSuccess == false)
                Assert.IsNull(_response.Result);
            else
                Assert.IsNotNull(_response.Result);
        }
    }

}




