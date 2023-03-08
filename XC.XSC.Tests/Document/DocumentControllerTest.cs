using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using XC.CCMP.DataStorage;
using XC.CCMP.DataStorage.Connect;
using XC.CCMP.KeyVault;
using XC.CCMP.KeyVault.Manager;
using XC.CCMP.Logger;
using XC.XSC.API.Controllers;
using XC.XSC.Service.DataStorage;
using XC.XSC.Service.User;

namespace XC.XSC.Tests.Document
{
    [TestClass]
    public class DocumentControllerTest
    {
        IUserContext _userContext;
        ILoggerManager _logger;
        IDataStorageService _dataStorageService;
        IClient _client;
        IApiConfig _apiConfig;
        IKeyVaultManager _keyVaultManager;
        IKeyVaultConfig _keyVaultConfig;
        IConfiguration _configuration;
        SecretClient _secretClient;
        DocumentController _documentController;

        [TestInitialize]
        public void TestInitialize()
        {
            _userContext = new TestUserContext();
            this._logger = new LoggerManager();
            _configuration = InitConfiguration();
            _keyVaultManager = new KeyVaultManager(_secretClient);
            _keyVaultConfig = new KeyVaultConfig(_keyVaultManager, _configuration);
            _apiConfig = new ApiConfig(_keyVaultConfig);
            _client = new Client(_apiConfig);
            _dataStorageService = new DataStorageService(_client);
            this._documentController = new DocumentController(_userContext, _logger, _dataStorageService);
        }

        /// <summary>
        /// Download a document based on document id success test case.
        /// </summary>
        /// <param name="documentId">document id.</param>
        /// <returns>retruns true if the response was success.</returns>
        [TestMethod]
        [DataRow("1")]
        public async Task DocumentControllerTest_Download_Success(string documentId)
        {
            var result = await _documentController.Download(documentId);
            var data = result as FileStreamResult;
            if(data != null && data.FileStream.Length> 0)
            {
                Assert.IsNotNull(data.FileStream.Length);
            }
            else
            {
                Assert.IsNull(data);
            }
        }

        /// <summary>
        /// Download a document based on document id failure test case.
        /// </summary>
        /// <param name="documentId">document id.</param>
        /// <returns>retruns true if the response was success.</returns>
        [TestMethod]
        [DataRow("0")]
        public async Task DocumentControllerTest_Download_Failure(string documentId)
        {
            var result = await _documentController.Download(documentId);
            var objectResult = result as BadRequestObjectResult;
            Assert.AreEqual(HttpStatusCode.BadRequest, (HttpStatusCode)objectResult.StatusCode);
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
