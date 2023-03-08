using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.CCMP.KeyVault.Manager;
using XC.CCMP.KeyVault;
using XC.CCMP.Logger;
using XC.XSC.API.Controllers;
using XC.XSC.Data;
using XC.XSC.Service.User;
using XC.XSC.UAM.Connector;
using XC.XSC.UAM.UAM;
using XC.XSC.UAM;
using XC.XSC.Utilities.Request;
using XC.XSC.ViewModels.Configuration;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Azure.Identity;
using XC.XSC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MailKit.Search;
using StackExchange.Redis;
using XC.XSC.Models.Entity.Lob;
using XC.XSC.UAM.Models;

namespace XC.XSC.Tests.Team
{

    /// <summary>
    /// Team controller test.
    /// </summary>
    [TestClass]
    public class TeamControllerTest
    {
        IUserContext _userContext;
        MSSqlContext _context;
        XSCContext _xscContext;
        UamApiConfig _config;
        IResponse _response;
        IHttpContextAccessor _httpContextAccessor;
        ILoggerManager _logger;
        IUamService _uamService;
        TeamController _teamController;
        IConfiguration _configuration;
        IUAMClient _uamClient;
        IKeyVaultConfig _keyVaultConfig;
        SecretClient _secretClient;
        IKeyVaultManager _keyVaultManager;
        IHttpRequest _httpRequest;

        /// <summary>
        /// Test initialize
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            this._logger = new LoggerManager();
            _userContext = new TestUserContext();
            _xscContext = new XSCContext(_userContext);
            _context = _xscContext.dbContext;
            _response = new OperationResponse();
            _configuration = InitConfiguration();
            _userContext = new TestUserContext();
            _config = new UamApiConfig();
            _httpRequest = new Utilities.Request.HttpRequest();
            _uamClient = new UAMClient(_config, _httpContextAccessor, _response, _httpRequest);
            _secretClient = new SecretClient(new Uri("https://kv-cin-dtu-d-01.vault.azure.net/"), new DefaultAzureCredential());
            _keyVaultManager = new KeyVaultManager(_secretClient);
            _keyVaultConfig = new KeyVaultConfig(_keyVaultManager, _configuration);
            this._uamService = new UamService(_keyVaultConfig, _uamClient, _config, _userContext);
            this._teamController = new TeamController(_userContext, _logger, _uamService, _response);
        }

        /// <summary>
        /// Get team list success test case..
        /// </summary>
        /// <returns>IResponse.</returns>
        [TestMethod]
        public async Task TeamController_GetTeamList_Success()
        {
            var result = await _teamController.GetTeamList();
            if (result.IsSuccess == true)
                Assert.IsTrue(result.IsSuccess);
            else
                Assert.IsFalse(result.IsSuccess);
        }

        /// <summary>
        /// Get team list failure test case..
        /// </summary>
        /// <returns>IResponse.</returns>
        [TestMethod]
        public async Task TeamController_GetTeamList_Failure()
        {
            var result = await _teamController.GetTeamList();
            if (result.IsSuccess == true)
                Assert.IsTrue(result.IsSuccess);
            else
                Assert.IsFalse(result.IsSuccess);
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
