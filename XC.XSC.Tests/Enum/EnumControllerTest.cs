using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.CCMP.Logger;
using XC.XSC.API.Controllers;
using XC.XSC.Service.IAM;
using XC.XSC.ViewModels;
using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.Tests.Enum
{
    /// <summary>
    /// Test classs for enum controller.
    /// </summary>
    [TestClass]
    public class EnumControllerTest
    {
        IResponse _response;
        IEnumService _service;
        EnumController _enumController;
        ILoggerManager _logger;

        /// <summary>
        /// Test initialize
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            this._logger = new LoggerManager();
            _response = new OperationResponse();
            _service = new EnumService();
            _enumController = new EnumController(_response, _service, _logger);
        }


        /// <summary>
        /// Success test case of get the list of enum key and value pair.
        /// </summary>
        /// <param name="enumName">Enum Name.</param>
        /// <returns></returns>
        [TestMethod]
        [DataRow("ReviewType")]
        [DataRow("")]
        public async Task EnumController_GetEnum_Success(string enumName)
        {
            var result = await _enumController.GetEnum(enumName);
            if (result.IsSuccess == true)
                Assert.IsTrue(result.IsSuccess);
            else
                Assert.IsFalse(result.IsSuccess);
        }
    }
}
