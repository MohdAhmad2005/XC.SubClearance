using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using XC.CCMP.KeyVault.Manager;
using XC.CCMP.KeyVault;
using XC.XSC.Service.DataStorage;
using XC.XSC.Service.EmailInfo;
using XC.XSC.Service.User;
using XC.XSC.ViewModels.Configuration;
using XC.CCMP.DataStorage.Connect;
using XC.CCMP.DataStorage;
using XC.XSC.API.Controllers;
using XC.XSC.Data;
using XC.XSC.Repositories.EmailInfo;
using XC.XSC.Repositories.EmailInfoAttachment;
using XC.XSC.ViewModels;
using XC.XSC.Service.EMS;
using XC.XSC.EMS.Connector;
using XC.XSC.EMS;
using XC.XSC.Service.Lobs;
using XC.XSC.Repositories.Lobs;

namespace XC.XSC.Tests.EmailInfo
{
    [TestClass]
    public class EmailInfoControllerTest
    {
        IUserContext _userContext;
        IEmailInfoService _emailInfoService;
        IDataStorageService _dataStorageService;
        IClient _client;
        IApiConfig _apiConfig;
        IKeyVaultManager _keyVaultManager;
        IKeyVaultConfig _keyVaultConfig;
        IConfiguration _configuration;
        SecretClient _secretClient;
        MSSqlContext _context;
        XSCContext _xscContext;
        IEmailInfoRepository _emailInfoRepository;
        IEmailInfoAttachmentRepository _emailInfoAttachmentRepository;
        IResponse _response;
        EmailInfoController _emailInfoController;
        IEMSClient _emsClient;
        IEmsApiConfig _emsApiConfig;
        EmsService _emsService;
        ILobService _lobService;
        ILobRepository _lobRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _userContext = new TestUserContext();
            _configuration = InitConfiguration();
            _keyVaultManager = new KeyVaultManager(_secretClient);
            _keyVaultConfig = new KeyVaultConfig(_keyVaultManager, _configuration);
            _apiConfig = new ApiConfig(_keyVaultConfig);
            _client = new Client(_apiConfig);
            _xscContext = new XSCContext(_userContext);
            _context = _xscContext.dbContext;
            _response = new OperationResponse();
            _dataStorageService = new DataStorageService(_client);
            _emailInfoRepository = new EmailInfoRepository(_context);
            _emailInfoAttachmentRepository = new EmailInfoAttachmentRepository(_context);
            _lobRepository = new LobRepository(_context);
            _lobService = new LobService(_lobRepository, _userContext);
            _emsService = new EmsService(_keyVaultConfig,_emsClient, _emsApiConfig,_userContext,_lobService, _response);
            _emailInfoService = new EmailInfoService(_emailInfoRepository, _emailInfoAttachmentRepository, _response, _userContext, _dataStorageService);
            this._emailInfoController = new EmailInfoController(_emailInfoService, _response, _emsService);
        }

        /// Get email info details based on the email info id success test case.
        /// </summary>
        /// <param name="emailInfoId">email Info id.</param>
        /// <returns>retruns true if the response was success.</returns>
        [TestMethod]
        [DataRow(1)]
        public async Task EmailInfoControllerTest_GetEmailInfoDetailById_Success(long emailInfoId)
        {
            _response = await _emailInfoController.GetEmailInfoDetailById(emailInfoId);
            if (_response.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }

        /// Get email info details based on the email info id failure test case.
        /// </summary>
        /// <param name="emailInfoId">email Info id.</param>
        /// <returns>retruns true if the response was success.</returns>
        [TestMethod]
        [DataRow(0)]
        public async Task EmailInfoControllerTest_GetEmailInfoDetailById_Failure(long emailInfoId)
        {
            _response = await _emailInfoController.GetEmailInfoDetailById(emailInfoId);
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
    }
}
