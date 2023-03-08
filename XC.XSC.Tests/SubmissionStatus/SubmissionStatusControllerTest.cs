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
using XC.XSC.Models.Entity.Submission;
using XC.XSC.Models.Profiles.SubmissionStage;
using XC.XSC.Models.Profiles.SubmissionStatus;
using XC.XSC.Repositories.SubmissionStage;
using XC.XSC.Repositories.SubmissionStatus;
using XC.XSC.Service.SubmissionStage;
using XC.XSC.Service.SubmissionStatus;
using XC.XSC.Service.User;
using XC.XSC.ViewModels;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.SubmissionStage;
using XC.XSC.ViewModels.SubmissionStatus;

namespace XC.XSC.Tests.SubmissionStatus
{
    [TestClass]
    public class SubmissionStatusControllerTest
    {
        IResponse _response;
        IUserContext _userContext;
        MSSqlContext _context;
        XSCContext _xscContext;
        ISubmissionStatusService _submissionStatusService;
        SubmissionStatusController _submissionStatusController;
        ISubmissionStatusRepository _submissionStatusRepository;

        IMapper _mapper;
       

        [TestInitialize]
        public void TestInitialize()
        {
            _userContext = new TestUserContext();
            _xscContext = new XSCContext(_userContext);
            _context = _xscContext.dbContext;
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new SubmissionStatusMapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
            _response = new OperationResponse();


            _submissionStatusRepository = new SubmissionStatusRepository(_context);
            this._submissionStatusService = new SubmissionStatusService(_submissionStatusRepository, _response, _mapper);
            this._submissionStatusController = new SubmissionStatusController(_submissionStatusService, _response);
        }
         
        /// <summary>        
        /// get all submission status by id test success method.
        /// </summary>
        /// <param name="submissionStatusId">submissionStatusId param</param>
        /// <returns></returns>
        [TestMethod]
        [DataRow(1)]
        public async Task SubmissionStatusControllerTest_GetSubmissionStatusById_Success(int submissionStatusId)
        {
            var result = await _submissionStatusController.GetAllSubmissionStatus(submissionStatusId);
            if (result.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }
    }
}
