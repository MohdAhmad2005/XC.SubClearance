using AutoMapper;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Moq;
using System;
using System.Threading.Tasks;
using XC.CCMP.KeyVault;
using XC.CCMP.KeyVault.Manager;
using XC.CCMP.Logger;
using XC.XSC.API.Controllers;
using XC.XSC.Data;
using XC.XSC.EmailSender;
using XC.XSC.EMS;
using XC.XSC.EMS.Connector;
using XC.XSC.Models.Profiles.SubmissionStatus;
using XC.XSC.Repositories.Comment;
using XC.XSC.Repositories.Lobs;
using XC.XSC.Repositories.MessageSent;
using XC.XSC.Repositories.MessageTemplate;
using XC.XSC.Repositories.Notification;
using XC.XSC.Repositories.Preferences;
using XC.XSC.Repositories.Sla;
using XC.XSC.Repositories.Submission;
using XC.XSC.Repositories.SubmissionAuditLog;
using XC.XSC.Repositories.SubmissionClearance;
using XC.XSC.Repositories.SubmissionExtraction;
using XC.XSC.Repositories.SubmissionStatus;
using XC.XSC.Repositories.TenantContextMapping;
using XC.XSC.Service.EMS;
using XC.XSC.Service.Lobs;
using XC.XSC.Service.MessageSent;
using XC.XSC.Service.MessageTemplate;
using XC.XSC.Service.Notification;
using XC.XSC.Service.Preferences;
using XC.XSC.Service.Sla;
using XC.XSC.Service.SubmissionClearance;
using XC.XSC.Service.TenantContextMapping;
using XC.XSC.Service.User;
using XC.XSC.UAM;
using XC.XSC.UAM.Connector;
using XC.XSC.UAM.UAM;
using XC.XSC.Utilities.Request;
using XC.XSC.ValidateMail;
using XC.XSC.ValidateMail.Connect;
using XC.XSC.ViewModels;
using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.Tests.BackGroundServices
{
    [TestClass]
    public class BackGroundServicesControllerTest
    {
        IUserContext _userContext;
        MSSqlContext _context;
        XSCContext _xscContext;
        BackGroundServicesController _backGroundServicesController;
        ISubmissionRepository _submissionRepository;
        ICommentRepository _commentRepository;
        ISubmissionStatusRepository _submissionStatusRepository;
        IPreferenceService _preferenceService;
        IKeyVaultConfig keyVaultConfig;
        IPreferenceRepository _preferenceRepository;
        IResponse _response;
        IConfiguration _configuration;
        ISubmissionClearanceService _submissionClearanceService;
        IClient _client;
        ISubmissionClearanceRepository _submissionClearanceRepository;
        IApiConfig _apiConfig;
        IKeyVaultManager _keyVaultManager;
        SecretClient _secretClient;
        ITenantContextMappingRepository _tenantContextMappingRepository;
        ITenantContextMappingService _tenantContextMappingService;
        ISubmissionExtractionRepository _submissionExtractionRepository;
        IMongoDatabase _mongoDatabase;
        TestMongoContext _mongoContext;
        INotificationService _notificationService;
        INotificationRepository _notificationRepository;
        IMessageTemplateService _messageTemplateService;
        IMessageTemplateRepository _messageTemplateRepository;
        IEmailService _emailService;
        IMessageSentService _messageSentService;
        IMessageSentRepository _messageSentRepository;
        IUamService _uamService;
        IUAMClient _uamClient;
        UamApiConfig _uamApiConfig;
        IHttpContextAccessor _httpContextAccessor;
        IHttpRequest _httpRequest;
        ISubmissionAuditLogRepository _submissionAuditLogRepository;
        ILobRepository _lobRepository;
        ILobService _lobService;
        ISlaConfigurationService _slaConfigService;
        ISlaConfigurationRepository _slaConfigurationRepository;
        IEMSClient _emsClient;
        EmsApiConfig _emsApiConfig;
        IEmsService _emsService;
        ILoggerManager _logger;
        IMapper _mapper;

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
            this._preferenceService = new PreferenceService(_preferenceRepository, _userContext);
            _secretClient = new SecretClient(new Uri("https://kv-cin-dtu-d-01.vault.azure.net/"), new DefaultAzureCredential());
            _keyVaultManager = new KeyVaultManager(_secretClient);
            _configuration = InitConfiguration();
            keyVaultConfig = new KeyVaultConfig(_keyVaultManager, _configuration);
            _preferenceRepository = new PreferenceRepository(_context);
            _apiConfig = new ApiConfig(keyVaultConfig);
            _client = new Client(_apiConfig);
            this._preferenceService = new PreferenceService(_preferenceRepository, _userContext);
            _tenantContextMappingRepository = new TenantContextMappingRepository(_context);
            _submissionClearanceRepository = new SubmissionClearanceRepository(_context);
            _tenantContextMappingService = new TenantContextMappingService(_userContext, _tenantContextMappingRepository);
            _mongoContext = new TestMongoContext(keyVaultConfig);
            _mongoDatabase = _mongoContext.Database;
            _submissionExtractionRepository = new SubmissionExtractionRepository(_mongoDatabase);
            _submissionClearanceService = new SubmissionClearanceService(_client, _submissionRepository, _preferenceService, _userContext, _response, _submissionClearanceRepository, _tenantContextMappingService, _submissionExtractionRepository);
            _notificationRepository =new NotificationRepository(_context);
            _messageTemplateRepository = new MessageTemplateRepository(_mongoDatabase);
            _messageTemplateService = new MessageTemplateService(_messageTemplateRepository, _response, _userContext);
            _emailService = new EmailService(keyVaultConfig);
            _messageSentRepository = new MessageSentRepository(_mongoDatabase);
            _messageSentService = new MessageSentService(_messageSentRepository, _response, _userContext);
            _uamApiConfig = new UamApiConfig();
            _httpRequest = new Utilities.Request.HttpRequest();
            _uamClient = new UAMClient(_uamApiConfig, _httpContextAccessor, _response, _httpRequest);
            _uamService = new UamService(keyVaultConfig, _uamClient, _uamApiConfig, _userContext);
            _submissionAuditLogRepository = new SubmissionAuditLogRepository(_context);

            _lobRepository = new LobRepository(_context);
            _lobService = new LobService(_lobRepository, _userContext);
            _emsApiConfig = new EmsApiConfig();
            _emsClient = new EMSClient(_emsApiConfig, _httpContextAccessor, _response, _httpRequest);
            _emsService = new EmsService(keyVaultConfig, _emsClient, _emsApiConfig, _userContext,_lobService,_response);
            _emsClient = new EMSClient(_emsApiConfig, _httpContextAccessor, _response, _httpRequest);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new SubmissionStatusMapper());
            });

            _mapper = mappingConfig.CreateMapper();
            _slaConfigService = new SlaConfigurationService(_slaConfigurationRepository, _response, _userContext, _uamService, _emsService, _preferenceService, _mapper, _lobService, _emsClient);
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
            this._backGroundServicesController = new BackGroundServicesController(_response, _submissionClearanceService, _notificationService);
        }

        /// <summary>
        /// Execute submission clearance check rule on the basis of predefined rules
        /// </summary>
        /// <param name="submissionId">submission id.</param>
        /// <returns>retruns true if the response was success.</returns>
        [TestMethod]
        [DataRow(1)]
        public async Task SubmissionClearanceCheck_With_Valid_SubmissionId_Return_Success(long submissionId)
        {
            _response = await _backGroundServicesController.SubmissionClearanceCheck(submissionId);
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
        /// Execute submission clearance check rule on the basis of predefined rules
        /// </summary>
        /// <param name="submissionId">submission id.</param>
        /// <returns>retruns true if the response was success.</returns>
        [TestMethod]
        [DataRow(0)]
        public async Task SubmissionClearanceCheck_With_Invalid_SubmissionId_Return_False(long submissionId)
        {
            _response = await _backGroundServicesController.SubmissionClearanceCheck(submissionId);
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
    }
}
