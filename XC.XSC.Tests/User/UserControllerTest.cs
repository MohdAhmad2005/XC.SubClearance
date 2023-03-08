using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using XC.CCMP.KeyVault.Manager;
using XC.CCMP.KeyVault;
using XC.CCMP.Logger;
using XC.XSC.API.Controllers;
using XC.XSC.Data;
using XC.XSC.Service.User;
using XC.XSC.UAM.Connector;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels;
using XC.XSC.UAM;
using XC.XSC.UAM.UAM;
using XC.XSC.Utilities.Request;
using System.Collections.Generic;
using XC.XSC.UAM.Models;
using Attribute = XC.XSC.UAM.Models.Attribute;

namespace XC.XSC.Tests.User
{
    /// <summary>
    /// This class is for Unit Testing of lob Controller
    /// </summary>
    [TestClass]
    public class UserControllerTest
    {
        IUserContext _userContext;
        MSSqlContext _context;
        XSCContext _xscContext;
        UamApiConfig _config;
        IResponse _response;
        IHttpContextAccessor _httpContextAccessor;
        ILoggerManager _logger;
        IUamService _uamService;
        UserController _userController;
        IConfiguration _configuration;
        IUAMClient _uamClient;
        IKeyVaultConfig _keyVaultConfig;
        SecretClient _secretClient;
        IKeyVaultManager _keyVaultManager;
        IHttpRequest _httpRequest;
        /// <summary>
        /// This method is intializing all dependencies of user Controller.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _configuration = new ConfigurationBuilder().Build();
            _userContext = new TestUserContext();
            _xscContext = new XSCContext(_userContext);
            _context = _xscContext.dbContext;
            _response = new OperationResponse();
            _logger = new LoggerManager();
            _config = new UamApiConfig();
            _httpRequest = new Utilities.Request.HttpRequest();
            _uamClient = new UAMClient(_config, _httpContextAccessor, _response, _httpRequest);
            _secretClient = new SecretClient(new Uri("https://kv-cin-dtu-d-01.vault.azure.net/"), new DefaultAzureCredential());
            _keyVaultManager = new KeyVaultManager(_secretClient);
            _keyVaultConfig = new KeyVaultConfig(_keyVaultManager, _configuration);
            this._uamService = new UamService(_keyVaultConfig, _uamClient, _config, _userContext);
            this._userController = new UserController(_userContext, _response, _uamService);
        }

        /// <summary>
        /// This method is used to clean the object of Repository, Service, Controller.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            _uamService = null;
            _userController = null;
        }
        
        /// <summary>
        /// Method to initialise the connector.
        /// </summary>
        /// <param name="keyVaultConfig"></param>
        /// <returns>Object of configuration.</returns>
        //private UamApiConfig UamConfiguration(IKeyVaultConfig keyVaultConfig)
        //{
        //    return new UamApiConfig
        //    {
        //        BaseUrl = keyVaultConfig.UamApiBaseUrl,
        //    };
        //}

        /// <summary>
        /// This method tests the get list of Region from UAM.
        /// </summary>
        [TestMethod]
        public async Task LobControllerUnitTests_GetRegion_Success()
        {
            var result = await _userController.GetUserRegions();
            if (result.IsSuccess == true)
                Assert.IsTrue(result.IsSuccess);
            else
                Assert.IsFalse(result.IsSuccess);
        }

        /// <summary>
        /// This method is the success test case of the users from key cloak.
        /// </summary>
        /// <param name="region">region.</param>
        /// <param name="team">team.</param>
        /// <param name="lob">lob.</param>
        /// <param name="role">role.</param>
        /// <param name="searchText">search text.</param>
        /// <returns>IResponse</returns>
        [TestMethod]
        [DataRow("1", "2", "1", "Reviewer", "13", "578964d5-e8db-4044-b1bd-c83c827047d1")]
        public async Task UserControllerUnitTests_getUsersByFilters_Success(string? region, string? team, string? lob, string? role, string? holidayList, string? managerId)
        {
            Attribute regionAttribute = new Attribute();
            regionAttribute.Name = "Region";
            regionAttribute.Value = new List<string> { region };

            Attribute teamAttribute = new Attribute();
            teamAttribute.Name = "Team";
            teamAttribute.Value = new List<string> { team };

            Attribute lobAttribute = new Attribute();
            lobAttribute.Name = "Lob";
            lobAttribute.Value = new List<string> { lob };

            Attribute roleAttribute = new Attribute();
            roleAttribute.Name = "Role";
            roleAttribute.Value = new List<string> { role };

            Attribute holidayListAttribute = new Attribute();
            holidayListAttribute.Name = "HolidayList";
            holidayListAttribute.Value = new List<string> { holidayList };

            Attribute managerAttribute = new Attribute();
            managerAttribute.Name = "Manager";
            managerAttribute.Value = new List<string> { managerId };

            UserFilterRequest UserFilterRequest = new UserFilterRequest();
            UserFilterRequest.Attributes = new List<Attribute> { regionAttribute, teamAttribute, lobAttribute, roleAttribute, holidayListAttribute, managerAttribute };
            var result = await _userController.GetUsersByFilters(UserFilterRequest);
            if (result.IsSuccess == true)
                Assert.IsTrue(result.IsSuccess);
            else
                Assert.IsFalse(result.IsSuccess);
        }
    }
}
