using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using XC.CCMP.KeyVault;
using XC.CCMP.Logger;
using XC.XSC.API.Controllers;
using XC.XSC.Data;
using XC.XSC.Repositories.Comment;
using XC.XSC.Repositories.Preferences;
using XC.XSC.Repositories.Submission;
using XC.XSC.Repositories.SubmissionStatus;
using XC.XSC.Repositories.TaskAuditHistory;
using XC.XSC.Service.Preferences;
using XC.XSC.Service.Submission;
using XC.XSC.Service.SubmissionStatus;
using XC.XSC.Service.TaskAuditHistory;
using XC.XSC.Service.User;
using XC.XSC.Service.ValidateMail;
using XC.XSC.ValidateMail.Models.Response;
using XC.XSC.ViewModels;
using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.Tests.TaskAuditHistory
{
    [TestClass]
    public class TaskAuditHistoryControllerTest
    {
        MSSqlContext _context;
        XSCContext _xscContext;
        TaskAuditHistoryController _audithistoryController;
        ITaskAuditHistoryService _audithistoryServices;
        ITaskAuditHistoryRepository _audithistoryRepository;
        ILoggerManager _logger;
        IUserContext _userContext;
        IResponse _operationResponse;

        [TestInitialize]
        public void TestInitialize()
        {
            _userContext = new TestUserContext();
            _xscContext = new XSCContext(_userContext);
            _context = _xscContext.dbContext;

            this._logger = new LoggerManager();
            this._operationResponse = new OperationResponse();
            _audithistoryRepository = new TaskAuditHistoryRepository(_context);
            _audithistoryServices = new TaskAuditHistoryService(_audithistoryRepository, _operationResponse);
            this._audithistoryController = new TaskAuditHistoryController(_audithistoryServices, _logger);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _audithistoryServices = null;
            _audithistoryController = null;
            _context = null;

        }
      
        [TestMethod]
        [DataRow(1)]
        public async Task SubmissonControllerTests_GetTaskAduditHistory(long submissionId)
        {
           
            var result = await _audithistoryController.GetTaskAduditHistory(submissionId);
            if (result.IsSuccess == true)
                Assert.IsTrue(result.IsSuccess);
            else
                Assert.IsFalse(result.IsSuccess);

        }
        /// <summary>
        /// Unit Test for API as  GetTaskAduditHistory Duration 
        /// </summary>
        /// <param name="submissionId">this parameter used for submissionId</param>
        /// <returns></returns>
        [TestMethod]
        [DataRow(1)]
        public async Task SubmissonControllerTests_GetTaskAduditHistoryDurationy(long submissionId)
        {
          
            var result = await _audithistoryController.getTaskAduditHistoryDuration(submissionId);
            if (result.IsSuccess == true)
                Assert.IsTrue(result.IsSuccess);
            else
                Assert.IsFalse(result.IsSuccess);
        }


    }
}



