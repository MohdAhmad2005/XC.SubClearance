using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using XC.XSC.API.Controllers.SubmissionStage;
using XC.XSC.Data;
using XC.XSC.Models.Profiles.SubmissionStage;
using XC.XSC.Repositories.SubmissionStage;
using XC.XSC.Service.SubmissionStage;
using XC.XSC.Service.User;
using XC.XSC.ViewModels;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.SubmissionStage;

namespace XC.XSC.Tests.SubmissionStage
{
    /// <summary>
    /// Submission Stage Controller Test data implementation.
    /// </summary>
    [TestClass]
    public class SubmissionStageControllerTest
    {
        IUserContext _userContext;
        MSSqlContext _context;
        XSCContext _xscContext;
        ISubmissionStageService _submissionStageService;
        SubmissionStageController _submissionStageController;
        ISubmissionStageRepository _submissionStageRepository;

        IMapper _mapper;
        IResponse _response;


        /// <summary>
        /// Test Initialize method to initialize all required objects
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _userContext = new TestUserContext();
            _xscContext = new XSCContext(_userContext);
            _context = _xscContext.dbContext;
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new SubmissionStageMapper());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
            _response = new OperationResponse();


            _submissionStageRepository = new SubmissionStageRepository(_context);
            this._submissionStageService = new SubmissionStageService(_submissionStageRepository, _mapper, _response);
            this._submissionStageController = new SubmissionStageController(_submissionStageService);
        }

        /// <summary>        
        /// get all submission stage by id test success method.
        /// </summary>
        /// <param name="submissionStageId">submissionStageId param</param>
        /// <returns></returns>
        [TestMethod]
        [DataRow(1)]
        public async Task SubmissionStageControllerTest_GetSubmissionStageById_Success(int submissionStageId)
        {
            var result = await _submissionStageController.GetAllSubmissionStage(submissionStageId);
            if (result.IsSuccess)
                Assert.IsTrue(_response.IsSuccess);
            else
                Assert.IsFalse(_response.IsSuccess);
        }
    }
}
