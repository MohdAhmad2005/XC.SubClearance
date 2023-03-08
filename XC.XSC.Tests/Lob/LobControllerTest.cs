using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.API.Controllers;
using XC.XSC.API.Controllers.SubmissionStage;
using XC.XSC.Data;
using XC.XSC.Repositories.Lobs;
using XC.XSC.Repositories.SubmissionStage;
using XC.XSC.Service.Lobs;
using XC.XSC.Service.SubmissionStage;
using XC.XSC.Service.User;
using XC.XSC.ViewModels;
using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.Tests.Lob
{
    /// <summary>
    /// Class of lob object.
    /// </summary>
    [TestClass]
    public class LobControllerTest
    {
        IUserContext _userContext;
        MSSqlContext _context;
        XSCContext _xscContext;
        ILobService _lobService;
        LobController _lobController;
        ILobRepository _lobRepository;
        IResponse _response;

        /// <summary>
        /// Method is used to initialize the dependent objects.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _userContext = new TestUserContext();
            _xscContext = new XSCContext(_userContext);
            _context = _xscContext.dbContext;
            _response = new OperationResponse();


            _lobRepository = new LobRepository(_context);
            _lobService = new LobService(_lobRepository, _userContext);
            _lobController = new LobController(_userContext,_response, _lobService);
        }

        /// <summary>
        /// Test method to test get all lob list.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task LobControllerTest_GetAllLobList_Success()
        {
            var result = await _lobController.GetAll();
            Assert.IsTrue(result.IsSuccess);
        }
    }
}
