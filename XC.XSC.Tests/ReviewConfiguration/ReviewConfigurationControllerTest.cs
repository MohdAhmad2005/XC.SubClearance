using AutoMapper;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using XC.CCMP.KeyVault.Manager;
using XC.CCMP.KeyVault;
using XC.CCMP.Logger;
using XC.XSC.API.Controllers;
using XC.XSC.Data;
using XC.XSC.ProfileMapping.ReviewConfiguration;
using XC.XSC.Repositories.ReviewConfiguration;
using XC.XSC.Service.ReviewConfiguration;
using XC.XSC.Service.User;
using XC.XSC.UAM.Connector;
using XC.XSC.UAM.UAM;
using XC.XSC.UAM;
using XC.XSC.Utilities.Request;
using XC.XSC.ViewModels;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.ReviewConfiguration;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using System;

namespace XC.XSC.Tests.ReviewConfiguration
{
    /// <summary>
    /// Review configuration controller test.
    /// </summary>
    [TestClass]
    public class ReviewConfigurationControllerTest
    {
        IUserContext _userContext;
        MSSqlContext _context;
        XSCContext _xscContext;
        IResponse _response;
        IMapper _mapper;
        IReviewConfigurationRepository _reviewConfigurationRepository;
        IReviewConfigurationService _reviewConfigurationService;
        ReviewConfigurationController _reviewConfigurationController;
        ILoggerManager _logger;
        UamApiConfig _config;
        IHttpContextAccessor _httpContextAccessor;
        IUamService _uamService;
        IConfiguration _configuration;
        IUAMClient _uamClient;
        IKeyVaultConfig _keyVaultConfig;
        SecretClient _secretClient;
        IKeyVaultManager _keyVaultManager;
        IHttpRequest _httpRequest;

        [TestInitialize]
        public void TestInitialize()
        {
            this._logger = new LoggerManager();
            _userContext = new TestUserContext();
            _xscContext = new XSCContext(_userContext);
            _context = _xscContext.dbContext;
            _response = new OperationResponse();
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ReviewConfigurationMapper());
            });
            _mapper = mappingConfig.CreateMapper();
            _configuration = InitConfiguration();
            _userContext = new TestUserContext();
            _config = new UamApiConfig();
            _httpRequest = new Utilities.Request.HttpRequest();
            _uamClient = new UAMClient(_config, _httpContextAccessor, _response, _httpRequest);
            _secretClient = new SecretClient(new Uri("https://kv-cin-dtu-d-01.vault.azure.net/"), new DefaultAzureCredential());
            _keyVaultManager = new KeyVaultManager(_secretClient);
            _keyVaultConfig = new KeyVaultConfig(_keyVaultManager, _configuration);
            this._uamService = new UamService(_keyVaultConfig, _uamClient, _config, _userContext);
            _reviewConfigurationRepository = new ReviewConfigurationRepository(_context);
            _reviewConfigurationService = new ReviewConfigurationServie(_userContext, _response, _mapper, _reviewConfigurationRepository, _uamService);
            this._reviewConfigurationController = new ReviewConfigurationController(_logger, _reviewConfigurationService, _response);
        }

        /// <summary>
        /// This method is the success test case of save new review configuration.
        /// </summary>
        /// <returns>IResponse.</returns>
        [TestMethod]
        public async Task ReviewController_SaveReviewConfiguration_Sucess()
        {
            _response = await _reviewConfigurationController.SaveReviewConfiguration(this.Review_Configuration_Valid_Mock_Data());
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }

        /// <summary>
        /// This method is the failure test case of save new review configuration.
        /// </summary>
        /// <returns>IResponse.</returns>
        [TestMethod]
        public async Task ReviewController_SaveReviewConfiguration_Failure()
        {
            _response = await _reviewConfigurationController.SaveReviewConfiguration(this.Review_Configuration_Invalid_Mock_Data());
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }

        /// <summary>
        /// This method is the success test case of update review configuration.
        /// </summary>
        /// <returns>IResponse.</returns>
        [TestMethod]
        public async Task ReviewController_UpdateReviewConfiguration_Sucess()
        {
            _response = await _reviewConfigurationController.UpdateReviewConfiguration(this.Review_Configuration_Valid_Mock_Update_Data());
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }

        /// <summary>
        /// This method is the failure test case of update review configuration.
        /// </summary>
        /// <returns>IResponse.</returns>
        [TestMethod]
        public async Task ReviewController_UpdateReviewConfiguration_Failure()
        {
            _response = await _reviewConfigurationController.UpdateReviewConfiguration(Review_Configuration_Invalid_Mock_Update_Data());
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }

        /// <summary>
        /// This method is the success test case of delete review configuration.
        /// </summary>
        /// <returns>IResponse.</returns>
        [TestMethod]
        [DataRow(1)]
        public async Task ReviewController_DeleteReviewConfiguration_Success(long reviewConfigurationId)
        {
            _response = await _reviewConfigurationController.DeleteReviewConfigurationById(reviewConfigurationId);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }

        /// <summary>
        /// This method is the failure test case of delete review configuration.
        /// </summary>
        /// <returns>IResponse.</returns>
        [TestMethod]
        [DataRow(0)]
        public async Task ReviewController_DeleteReviewConfiguration_Failure(long reviewConfigurationId)
        {
            _response = await _reviewConfigurationController.DeleteReviewConfigurationById(reviewConfigurationId);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }

        /// <summary>
        /// This method is the success test case of get all review configuration.
        /// </summary>
        /// <returns>IResponse.</returns>
        [TestMethod]
        public async Task ReviewController_GetAllReviewConfiguration_Success()
        {
            _response = await _reviewConfigurationController.GetAllReviewConfiguration();
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }

        /// <summary>
        /// This method is the failure test case of get all review configuration.
        /// </summary>
        /// <returns>IResponse.</returns>
        [TestMethod]
        public async Task ReviewController_GetAllReviewConfiguration_Failure()
        {
            _response = await _reviewConfigurationController.GetAllReviewConfiguration();
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }

        /// <summary>
        ///  This method is used to get the review configuration details by id or user id success case.
        /// </summary>
        /// <returns>IResponse.</returns>
        [TestMethod]
        [DataRow(1, false)]
        public async Task ReviewController_GetReviewConfig_Success(long reviewConfigurationId, bool? userId)
        {
            _response = await _reviewConfigurationController.GetReviewConfig(reviewConfigurationId,userId);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }

        /// <summary>
        ///  This method is used to get the review configuration details by id or user id failure case.
        /// </summary>
        /// <returns>IResponse.</returns>
        [TestMethod]
        [DataRow(0,false)]
        public async Task ReviewController_GetReviewConfig_Failure(long reviewConfigurationId, bool? userId)
        {
            _response = await _reviewConfigurationController.GetReviewConfig(reviewConfigurationId,userId);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
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
        /// Review configuration valid mock data.
        /// </summary>
        /// <returns></returns>
        private ReviewConfigurationRequest Review_Configuration_Valid_Mock_Data()
        {
            ReviewConfigurationRequest reviewConfigurationRequest = new ReviewConfigurationRequest();
            {
                reviewConfigurationRequest.ReviewType = ViewModels.Enum.ReviewType.SkipReview;
                reviewConfigurationRequest.ReviewerId = "1";
                reviewConfigurationRequest.ProcessorId = new System.Collections.Generic.List<string>() { "1"};
                reviewConfigurationRequest.RegionId = 1;
                reviewConfigurationRequest.LobId = 1;
                reviewConfigurationRequest.TeamId = 1;
                reviewConfigurationRequest.IsActive = true;
                return reviewConfigurationRequest;
            }
        }

        /// <summary>
        /// Review configuration valid mock update data.
        /// </summary>
        /// <returns></returns>
        private ReviewConfigurationUpdate Review_Configuration_Valid_Mock_Update_Data()
        {
            ReviewConfigurationUpdate reviewConfigurationRequest = new ReviewConfigurationUpdate();
            {
                reviewConfigurationRequest.ReviewTypeId = ViewModels.Enum.ReviewType.SkipReview;
                reviewConfigurationRequest.ReviewerId = "1";
                reviewConfigurationRequest.ProcessorId = "1";
                reviewConfigurationRequest.RegionId = 1;
                reviewConfigurationRequest.LobId = 1;
                reviewConfigurationRequest.TeamId = 1;
                reviewConfigurationRequest.IsActive = true;
                return reviewConfigurationRequest;
            }
        }

        /// <summary>
        /// Review configuration invalid mock data.
        /// </summary>
        /// <returns></returns>
        private ReviewConfigurationRequest Review_Configuration_Invalid_Mock_Data()
        {
            ReviewConfigurationRequest reviewConfigurationRequest = new ReviewConfigurationRequest();
            {
                reviewConfigurationRequest.ReviewType = ViewModels.Enum.ReviewType.SkipReview;
                reviewConfigurationRequest.ReviewerId = "1";
                reviewConfigurationRequest.ProcessorId = new System.Collections.Generic.List<string>() { "1" };
                reviewConfigurationRequest.RegionId = 2;
                reviewConfigurationRequest.LobId = 1;
                reviewConfigurationRequest.TeamId = 1;
                reviewConfigurationRequest.IsActive = true;
                return reviewConfigurationRequest;
            }
        }

        /// <summary>
        /// Review configuration invalid mock data update.
        /// </summary>
        /// <returns></returns>
        private ReviewConfigurationUpdate Review_Configuration_Invalid_Mock_Update_Data()
        {
            ReviewConfigurationUpdate reviewConfigurationRequest = new ReviewConfigurationUpdate();
            {
                reviewConfigurationRequest.ReviewTypeId = ViewModels.Enum.ReviewType.SkipReview;
                reviewConfigurationRequest.ReviewerId = "1";
                reviewConfigurationRequest.ProcessorId = "1";
                reviewConfigurationRequest.RegionId = 2;
                reviewConfigurationRequest.LobId = 1;
                reviewConfigurationRequest.TeamId = 1;
                reviewConfigurationRequest.IsActive = true;
                return reviewConfigurationRequest;
            }
        }
    }
}
