using Microsoft.AspNetCore.Mvc;
using XC.XSC.Service.SubmissionStatus;
using XC.XSC.ViewModels;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.Submission;
using XC.XSC.ViewModels.SubmissionStatus;

namespace XC.XSC.API.Controllers
{
    /// <summary>
    /// This is controller class. It basically intracts with users.
    /// </summary>
    [Route("api/xsc/[controller]")]
    [ApiController]
    public class SubmissionStatusController : Controller
    {
        private readonly ISubmissionStatusService _submissionStatus;
        private readonly IResponse _operationResponse;

        /// <summary>
        /// Constructor of controller.
        /// </summary>
        /// <param name="submissionStatus"></param>
        /// <param name="operationResponse"></param>
        public SubmissionStatusController(ISubmissionStatusService submissionStatus, IResponse operationResponse)
        {
            _submissionStatus = submissionStatus;
            _operationResponse = operationResponse;
        }

        /// <summary>
        /// Endpoint to add a new submission status. It will add new submission status if and only if provide submission status name in the request body is not exists in the database.
        /// </summary>
        /// <param name="addSubmissionStatusObj"></param>
        /// <returns>SubmissionStatus Object provided in request body</returns>
        //[HttpPost]
        //[Route("addSubmissionStatus")]
        //public async Task<IResponse> AddSubmissionStatus(AddSubmissionStatusRequest addSubmissionStatusRequest)
        //{
        //    return await _submissionStatus.AddSubmissionStatusAsync(addSubmissionStatusRequest);
        //}

        /// <summary>
        /// Endpoint to get submission status list or pass submission status id to get perticular status data.
        /// </summary>
        /// <param name="submissionStatusId">submission status id</param>
        /// <returns>List of submission Status</returns>
        [HttpGet]
        [Route("getSubmissionStatus")]
        public async Task<IResponse> GetAllSubmissionStatus(int? submissionStatusId)
        {
            if (submissionStatusId == null)
                return await _submissionStatus.GetSubmissionStatusListAsync();
            else
                return await _submissionStatus.GetSubmissionStatusByIdAsync((int)submissionStatusId);
        }

        /// <summary>
        /// Endpoint to get a single submission status based on provided Id.
        /// </summary>
        /// <param name="submissionStatusId" example="12"></param>
        /// <returns>Submission status object.</returns>
        //[HttpGet("getSubmissionListById/{submissionStatusId}")]
        //public async Task<IResponse> GetSubmissionStatusById([FromRoute] int submissionStatusId)
        //{
        //    if (submissionStatusId > 0)
        //        return await _submissionStatus.GetSubmissionStatusByIdAsync(submissionStatusId);
        //    else
        //    {
        //        _operationResponse.Result = null;
        //        _operationResponse.IsSuccess = false;
        //        _operationResponse.Message = "Please add valid submissionStatusId ";
        //        return _operationResponse;
        //    }
        //}

        /// <summary>
        /// Endpoint to Update an existing record. It will update the record if and only if provided Submission status name in request body is not exists in the database.
        /// </summary>
        /// <param name="updateSubmissionStatusObj"></param>
        /// <returns>Updated record.</returns>

        //[HttpPut("UpdateSubmissionStatus")]
        //public async Task<IResponse> UpdateSubmissionStatus(UpdateSubmissionStatusRequest updateSubmissionStatusRequest)
        //{
        //    return await _submissionStatus.UpdateSubmissionStatusAsync(updateSubmissionStatusRequest);
        //}
    }
}
