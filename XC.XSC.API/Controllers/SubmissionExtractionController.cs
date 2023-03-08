using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XC.XSC.Service.SubmissionExtraction;
using XC.XSC.Service.User;
using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.API.Controllers
{
    /// <summary>
    /// SubmissionExtractionController for handling all operations related to submission extraction.
    /// </summary>
    [Authorize]
    [Route("api/xsc/[controller]")]
    [ApiController]
    public class SubmissionExtractionController : ControllerBase
    {
        private readonly IUserContext _userContext;
        private readonly IResponse _operationResponse;
        private readonly ISubmissionExtractionService _submissionExtractionService;

        /// <summary>
        /// SubmissionExtractionController constructor.
        /// </summary>
        /// <param name="userContext"> Login user detail. </param>
        /// <param name="operationResponse">Operation Response.</param>
        /// <param name="submissionExtractionService">Submission Extraction service.</param>
        public SubmissionExtractionController(IUserContext userContext, IResponse operationResponse, ISubmissionExtractionService submissionExtractionService)
        {
            _userContext = userContext;
            _operationResponse = operationResponse;
            _submissionExtractionService = submissionExtractionService;
        }

        /// <summary>
        /// Get Submission form Data.
        /// </summary>
        /// <returns> Submission edit screen form schema.  </returns>
        [HttpGet]
        [Route("getSubmissionForm")]
        public async Task<IResponse> GetSubmissionForm()
        {

            return await _submissionExtractionService.GetSubmissionForm();

        }


        /// <summary>
        /// Get Submission Edit screen details.
        /// </summary>
        /// <param name="submissionId">Submission id of case.</param>
        /// <returns> IResponse  as SUCCESS.  </returns>
        [HttpGet]
        [Route("getSubmissionTransformedData/{submissionId}")]
        public async Task<IResponse> GetSubmissionTransformedData(long submissionId)
        {
            if (submissionId > 0)
            {
                return await _submissionExtractionService.GetSubmissionTransformedData(submissionId);
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "Please add required parameters";
                return _operationResponse;
            }
        }

        /// <summary>
        /// update submission edit screen data in mongo.
        /// </summary>
        /// <param name="submissionExtraction">Modal object of Submission Extraction.</param>
        /// <returns> IResponse  as SUCCESS.  </returns>
        [HttpPost]
        [Route("updateSubmissionTransformedData")]
        public async Task<IResponse> UpdateSubmissionTransformedData(Models.Mongo.Entity.SubmissionExtraction submissionExtraction)
        {
            if (submissionExtraction.Id != null)
            {
                return await _submissionExtractionService.UpdateSubmissionTransformedData(submissionExtraction);
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "Please add required parameters";
                return _operationResponse;
            }
        }
    }
}
