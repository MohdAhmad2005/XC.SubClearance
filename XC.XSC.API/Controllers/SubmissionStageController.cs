using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using XC.XSC.Service.SubmissionStage;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.SubmissionStage;

namespace XC.XSC.API.Controllers.SubmissionStage
{
    /// <summary>
    /// This is the controller class having four methods. These method basically intracts with UI.
    /// </summary>
    [Route("api/xsc/[controller]")]
    [ApiController]
    public class SubmissionStageController : ControllerBase
    {
        private readonly ISubmissionStageService _submissionStageService;

        /// <summary>
        /// This is a constructor method to initialize the SubmissionStageServices.
        /// </summary>
        /// <param name="submissionStageService"></param>
        public SubmissionStageController(ISubmissionStageService submissionStageService)
        {
            _submissionStageService = submissionStageService;
        }

        /// <summary>
        /// Endpoint to add new SubmissionStage.It will add if and only if provided name in request body is not exists in the database.
        /// </summary>
        /// <param name="addSubmissionStageRequest"></param>
        /// <returns>Added object.</returns>
        //[HttpPost("addSubmissionStage")]
        //public async Task<IResponse> AddSubmissionStage(AddSubmissionStageRequest addSubmissionStageRequest)
        //{
        //    return await _submissionStageService.AddSubmissionStageAsync(addSubmissionStageRequest);   
        //}

        /// <summary>
        /// Endpoint to get list of SubmissionStages or pass submission stage id to get perticular stage data.
        /// </summary>
        /// <param name="submissionStageId">submission stage id</param>
        /// <returns>It return a list of SubmissionStage</returns>
        [HttpGet("getSubmissionStage")]
        public async Task<IResponse> GetAllSubmissionStage(int? submissionStageId)
        {
            if (submissionStageId == null)
                return await _submissionStageService.GetAllSubmissionStageAsync();
            else
                return await _submissionStageService.GetSubmissionStageByIdAsync((int)submissionStageId);
        }

        /// <summary>
        /// Endpoint to get a single SubmissionStage record based on provided Id.
        /// </summary>
        /// <param name="submissionStageId" example="12">The SubmissionStage Id</param>
        /// <returns>SubmissionStage retrieved</returns>
        //[HttpGet("getSubmissionStageById/{submissionStageId}")]
        //public async Task<IResponse> GetSubmissionStageById([FromRoute] int submissionStageId)
        //{
        //    return await _submissionStageService.GetSubmissionStageByIdAsync(submissionStageId);
        //}

        /// <summary>
        /// Endpoint to update SubmissionStage record based on provided Id in Request body.It will update if and only if provided name in the request body is not exists in the database.
        /// </summary>
        /// <param name="updateSubmissionStageRequest"></param>
        /// <returns>returns Updated record.</returns>
        //[HttpPut("updateSubmissionStageById")]
        //public async Task<IResponse> UpdateSubmissionStage(UpdateSubmissionStageRequest updateSubmissionStageRequest)
        //{
        //    return await _submissionStageService.UpdateSubmissionStageAsync(updateSubmissionStageRequest);
        //}
    }
}
