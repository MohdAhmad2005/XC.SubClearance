using XC.CCMP.DataStorage;
using XC.CCMP.DataStorage.Connect;
using XC.CCMP.KeyVault;
using XC.CCMP.KeyVault.Manager;
using XC.CCMP.Logger;
using XC.CCMP.Queue;
using XC.CCMP.Queue.ASB;
using XC.CCMP.Queue.Broker;
using XC.CCMP.Queue.MessageAdapter;
using XC.CCMP.Queue.MessageAdapter.AzureBlob;
using XC.XSC.EmailSender;
using XC.XSC.Repositories.Comment;
using XC.XSC.Repositories.EmailInfo;
using XC.XSC.Repositories.EmailInfoAttachment;
using XC.XSC.Repositories.Preferences;
using XC.XSC.Repositories.Submission;
using XC.XSC.Repositories.SubmissionStage;
using XC.XSC.Repositories.SubmissionStatus;
using XC.XSC.Repositories.TaskAuditHistory;
using XC.XSC.Service.EmailInfo;
using XC.XSC.Service.IAM;
using XC.XSC.Service.Preferences;
using XC.XSC.Service.Submission;
using XC.XSC.Service.SubmissionStage;
using XC.XSC.Service.SubmissionStatus;
using XC.XSC.Service.TaskAuditHistory;
using XC.XSC.Service.User;
using XC.XSC.Service.ValidateMail;
using XC.XSC.ViewModels;
using XC.XSC.ViewModels.Configuration;
using IMapper = XC.CCMP.Queue.MessageAdapter.IMapper;
using XC.XSC.Service.Lobs;
using XC.XSC.Repositories.Lobs;
using XC.XSC.Service.SubmissionClearance;
using XC.XSC.Repositories.SubmissionClearance;
using XC.XSC.Data;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using XC.XSC.Service.DataStorage;
using XC.XSC.ViewModels.User;
using XC.XSC.Service.SubmissionAuditLog;
using XC.XSC.Repositories.SubmissionAuditLog;
using XC.XSC.Repositories.TenantContextMapping;
using XC.XSC.Service.Comments;
using XC.XSC.Service.JsonFileService;
using XC.XSC.Service.TenantContextMapping;
using XC.XSC.UAM.UAM;
using XC.XSC.Utilities.Request;
using XC.XSC.UAM.Connector;
using XC.XSC.UAM;
using XC.XSC.Service.Sla;
using XC.XSC.Repositories.Sla;
using XC.XSC.Repositories.Scheduler;
using XC.XSC.Service.Scheduler;
using XC.XSC.Repositories.ReviewConfiguration;
using XC.XSC.Service.ReviewConfiguration;
using XC.XSC.Service.SubmissionExtraction;
using XC.XSC.Repositories.SubmissionExtraction;
using XC.XSC.Service.EMS;
using XC.XSC.EMS.Connector;
using XC.XSC.EMS;
using XC.XSC.Models.Mongo.Interface.MessageTemplate;
using XC.XSC.Models.Mongo.Entity.MessageTemplate;
using XC.XSC.Service.MessageTemplate;
using XC.XSC.Repositories.MessageTemplate;
using XC.XSC.Models.Mongo.Interface.MessageSent;
using XC.XSC.Repositories.MessageSent;
using XC.XSC.Models.Mongo.Entity.MessageSent;
using XC.XSC.Service.MessageSent;
using XC.XSC.Models.Interface.Notification;
using XC.XSC.Models.Entity.Notification;
using XC.XSC.Service.Notification;
using XC.XSC.Repositories.Notification;
using XC.XSC.Service.Workflow;
using XC.XSC.Workflow.Workflow.Connect;
using XC.XSC.Workflow.Workflow;
using XC.CCMP.IAM.Keycloak.Connect;
using XC.CCMP.IAM.Keycloak.Models;

namespace XC.XSC.API.Extentions
{
    public static class ServiceExtensions
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            //DI-KeyVault
            services.AddSingleton<IKeyVaultManager, KeyVaultManager>();
            services.AddSingleton<IKeyVaultConfig, KeyVaultConfig>();
            services.AddTransient<IIAMClient, XC.CCMP.IAM.Keycloak.Connect.Client>();
            services.AddScoped<XC.CCMP.IAM.Keycloak.IApiConfig, XC.CCMP.IAM.Keycloak.ApiConfig>();

            IKeyVaultConfig keyVaultConfig = null;
            var scopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();

            using (var scope = scopeFactory.CreateScope())
            {
                var provider = scope.ServiceProvider;
                {
                    keyVaultConfig = provider.GetRequiredService<IKeyVaultConfig>();
                }
            }

            services.AddSingleton<IMongoDatabase>(options =>
            {
                var client = new MongoClient(keyVaultConfig.MongoConnectionString);
                return client.GetDatabase(keyVaultConfig.MongoDatabaseName);
            });

            services.AddDbContext<MSSqlContext>(options => options.UseSqlServer(keyVaultConfig.DatabaseConnectionString));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserInfo, UserInfo>();
            services.AddScoped<IUserContext, UserContext>();

            //DI - Keycloak authentication library
            services.AddScoped<IIAMService, IAMService>();

            //DI - DB Context
            services.AddScoped<MongoContext>();

            //DI - for service layer           
            //services.AddScoped<IActivityLogService, ActivityLogService>();

            //DI - for repository layer           
            //services.AddScoped<IActivityLogRepository, ActivityLogRepository>();

            //DI - for Data Storage
            services.AddScoped<IApiConfig, XC.CCMP.DataStorage.ApiConfig>();
            services.AddScoped<IClient, XC.CCMP.DataStorage.Connect.Client>();

            //DI - for Operation Output(Add , Update)
            services.AddScoped<IResponse, OperationResponse>();
            services.AddScoped<IIAMResponse, IAMOperationResponse>();

            //DI - for others
            services.AddScoped<IQueueProcessor, QueueProcessor>();
            services.AddScoped<IMapper, StorageMapper>();
            services.AddScoped<IBlobConfig, BlobConfig>();
            services.AddScoped<IQueueConfiguration, QueueConfiguration>();

            //services.AddScoped<IRedisCache, RedisCache>();
            services.AddSingleton<ILoggerManager, LoggerManager>();

            //DI - Email sender service
            services.AddScoped<IEmailService, EmailService>();
            //DI-Submission Service
            services.AddScoped<ISubmissionRepository, SubmissionRepository>();
            services.AddScoped<ISubmissionService, SubmissionService>();

            //DI-EmailInfo
            services.AddScoped<IEmailInfoRepository, EmailInfoRepository>();
            services.AddScoped<IEmailInfoService, EmailInfoService>();
            services.AddScoped<IEmailInfoAttachmentRepository, EmailInfoAttachmentRepository>();
            services.AddScoped<IEmailProcessorService, EmailProcessorService>();

            //DI-SubmissionStatus Service
            services.AddScoped<ISubmissionStatusService, SubmissionStatusService>();
            services.AddScoped<ISubmissionStatusRepository, SubmissionStatusRepository>();
            services.AddScoped<ISubmissionStageRepository, SubmissionStageRepository>();
            services.AddScoped<ISubmissionStageService, SubmissionStageService>();
            services.AddScoped<IResponse, OperationResponse>();
            services.AddScoped<IPreferenceService, PreferenceService>();
            services.AddScoped<IPreferenceRepository, PreferenceRepository>();

            //DI-Submission Status Service
            services.AddScoped<ISubmissionStatusService, SubmissionStatusService>();
            services.AddScoped<ISubmissionStatusRepository, SubmissionStatusRepository>();
            services.AddTransient<XC.XSC.ValidateMail.IApiConfig, XC.XSC.ValidateMail.ApiConfig>();
            services.AddTransient<XC.XSC.ValidateMail.Connect.IClient, XC.XSC.ValidateMail.Connect.Client>();
            services.AddTransient<IValidateMailService, ValidateMailService>();
            services.AddTransient<IAzureServiceBus, AzureServiceBus>();
            services.AddTransient<ICommentRepository, CommentRepository>();

            //Task Audit  Status Service
            services.AddScoped<ITaskAuditHistoryService, TaskAuditHistoryService>();
            services.AddScoped<ITaskAuditHistoryRepository, TaskAuditHistoryRepository>();

            //Lob Service
            services.AddScoped<ILobService, LobService>();
            services.AddScoped<ILobRepository, LobRepository>();

            //Submission Clearance Service
            services.AddScoped<ISubmissionClearanceService, SubmissionClearanceService>();
            services.AddScoped<ISubmissionClearanceRepository, SubmissionClearanceRepository>();

            //Document Servie
            services.AddScoped<IDataStorageService, DataStorageService>();

            //Tenant Context Mapping Repository
            services.AddScoped<ITenantContextMappingRepository, TenantContextMappingRepository>();
            services.AddScoped<ITenantContextMappingService, TenantContextMappingService>();

            //Comment Service
            services.AddScoped<ICommentService, CommentService>();

            //Submission Audit Log  Service
            services.AddScoped<ISubmissionAuditLogService, SubmissionAuditLogService>();
            services.AddScoped<ISubmissionAuditLogRepository, SubmissionAuditLogRepository>();
            
            //Json File Service
            services.AddScoped<IJsonFileService,JsonFileService>();

            //DI - UAM library
            services.AddScoped<IUamApiConfig, UamApiConfig>();
            services.AddScoped<IUamService, UamService>();
            services.AddScoped<IHttpRequest, Utilities.Request.HttpRequest>();
            services.AddScoped<IUAMClient, UAMClient>();


            //Sla  Service
            services.AddScoped<ISlaConfigurationService, SlaConfigurationService>();
            services.AddScoped<ISlaConfigurationRepository, SlaConfigurationRepository>();

            //Scheduler
            services.AddScoped<ISchedulerConfigurationRepository, SchedulerConfigurationRepository>();
            services.AddScoped<ISchedulerConfigurationService, SchedulerConfigurationService>();

            //Review configuration
            services.AddScoped<IReviewConfigurationRepository, ReviewConfigurationRepository>();
            services.AddScoped<IReviewConfigurationService, ReviewConfigurationServie>();

            //Submission Extraction
            services.AddScoped<ISubmissionExtractionService, SubmissionExtractionService>();
            services.AddScoped<ISubmissionExtractionRepository, SubmissionExtractionRepository>();

            //Enum service to get the enum data
            services.AddScoped<IEnumService, EnumService>();

            //Sla  Service
            services.AddScoped<IEmsService, EmsService>();
            services.AddScoped<IEMSClient, EMSClient>();
            services.AddScoped<IEmsApiConfig, EmsApiConfig>();

            //Message Template
            services.AddScoped<IMessageTemplate, MessageTemplate>();
            services.AddScoped<IMessageTemplateService, MessageTemplateService>();
            services.AddScoped<IMessageTemplateRepository, MessageTemplateRepository>();

            services.AddScoped<IMessageSent, MessageSent>();
            services.AddScoped<IMessageSentService, MessageSentService>();
            services.AddScoped<IMessageSentRepository, MessageSentRepository>();

            services.AddScoped<INotification, Notification>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INotificationRepository, NotificationRepository>();

            //Workflow Library
            services.AddScoped<IWorkflowApiConfig, WorkflowApiConfig>();
            services.AddScoped<IWorkflowClient, WorkflowClient>();
            services.AddScoped<IWorkflowService, WorkflowService>();


        }
    }
}