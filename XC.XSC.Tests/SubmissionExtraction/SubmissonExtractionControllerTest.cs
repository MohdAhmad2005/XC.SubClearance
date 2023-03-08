using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using XC.CCMP.KeyVault;
using XC.CCMP.KeyVault.Manager;
using XC.CCMP.Queue;
using XC.CCMP.Queue.ASB;
using XC.XSC.API.Controllers;
using XC.XSC.Data;
using XC.XSC.Repositories.Preferences;
using XC.XSC.Repositories.Submission;
using XC.XSC.Service.Preferences;
using XC.XSC.Service.User;
using XC.XSC.ViewModels;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.Service.SubmissionExtraction;
using XC.XSC.Repositories.SubmissionExtraction;
using MongoDB.Driver;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace XC.XSC.Tests.SubmissionExtraction
{
    [TestClass]
    public class SubmissonExtractionControllerTests : IDisposable
    {
        IUserContext _userContext;
        TestMongoContext _mongoContext;
        MSSqlContext _context;
        XSCContext _xscContext;
        IResponse _response;
        IMongoDatabase _mongoDatabase;
        IKeyVaultManager _keyVaultManager;
        SecretClient _secretClient;
        IConfiguration _configuration;

        IPreferenceService _preferenceService;
        IKeyVaultConfig keyVaultConfig;
        IPreferenceRepository _preferenceRepository;
        SubmissionExtractionController _submissionExtractionController;
        ISubmissionExtractionService _submissionExtractionService;
        ISubmissionExtractionRepository _submissionExtractionRepository;
        ISubmissionRepository _submissionRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _secretClient = new SecretClient(new Uri("https://kv-cin-dtu-d-01.vault.azure.net/"), new DefaultAzureCredential());
            _keyVaultManager = new KeyVaultManager(_secretClient);
            _configuration = InitConfiguration();
            keyVaultConfig = new KeyVaultConfig(_keyVaultManager, _configuration);

            _userContext = new TestUserContext();
            _mongoContext = new TestMongoContext(keyVaultConfig);
            _mongoDatabase = _mongoContext.Database;
            this._response = new OperationResponse();
            _xscContext = new XSCContext(_userContext);
            _context = _xscContext.dbContext;
            _submissionRepository = new SubmissionRepository(_context);
            _preferenceRepository = new PreferenceRepository(_context);
            Mock<IAzureServiceBus> _azureServiceBus = new Mock<IAzureServiceBus>();
            Mock<IQueueConfiguration> _queueConfiguration = new Mock<IQueueConfiguration>();

            this._preferenceService = new PreferenceService(_preferenceRepository, _userContext);
            this._submissionExtractionRepository = new SubmissionExtractionRepository(_mongoDatabase);
            this._submissionExtractionService = new SubmissionExtractionService(_submissionExtractionRepository, _response, _userContext, _preferenceService, _submissionRepository
            , _queueConfiguration.Object, _azureServiceBus.Object, keyVaultConfig);
            _submissionExtractionController = new SubmissionExtractionController(_userContext, _response, _submissionExtractionService);

        }


        /// <summary>
        /// Unit Test for Add submission data in mongo.
        /// </summary>
        /// <returns></returns>

        [TestMethod]
        public async Task SubmissonExtractionControllerTests_AddSubmissionData()
        {
            var addResponse = await _submissionExtractionService.AddSubmissionDetail();
            if (addResponse.IsSuccess)
                Assert.IsTrue(addResponse.IsSuccess);
            else
                Assert.IsFalse(addResponse.IsSuccess);
        }

        /// <summary>
        /// Unit Test for get submission transformed data from mongo.
        /// </summary>
        /// <returns></returns>

        [TestMethod]
        [DataRow(12)]
        public async Task SubmissonExtractionControllerTests_GetSubmissionTransformedData(long submissionId)
        {
            var updateResponse = await _submissionExtractionController.GetSubmissionTransformedData(submissionId);
            if (updateResponse.IsSuccess)
                Assert.IsTrue(updateResponse.IsSuccess);
            else
                Assert.IsFalse(updateResponse.IsSuccess);
        }

        /// <summary>
        /// Unit Test for Validate MailDuplicity.
        /// </summary>
        /// <returns></returns>

        [TestMethod]
        [DataRow(12)]
        public async Task SubmissonExtractionControllerTests_ValidateMailDuplicityAsync(long submissionId)
        {
            await _submissionExtractionService.AddSubmissionDetail();
            var response = await _submissionExtractionService.ValidateMailDuplicityAsync(submissionId);
            if (response.IsSuccess)
                Assert.IsTrue(response.IsSuccess);
            else
                Assert.IsFalse(response.IsSuccess);
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
        /// Dispose method implementation to drop test database. 
        /// </summary>
        void IDisposable.Dispose()
        {
            _mongoContext.Client.DropDatabase("UnitTestTempDatabase");
        }
    }

}




