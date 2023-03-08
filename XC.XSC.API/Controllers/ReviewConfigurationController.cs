using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XC.CCMP.Logger;
using XC.XSC.Service.ReviewConfiguration;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.ReviewConfiguration;

namespace XC.XSC.API.Controllers
{
    /// <summary>
    /// This controller will handle all the review configuration related methods.
    /// </summary>
    [Authorize]
    [Route("api/xsc/[controller]")]
    [ApiController]
    public class ReviewConfigurationController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IReviewConfigurationService _reviewConfigurationService;
        private readonly IResponse _response;

        /// <summary>
        /// Review configuration controller constructor.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        /// <param name="reviewConfigurationService">Review configuration service instance.</param>
        /// <param name="response">IResponse instane.</param>
        public ReviewConfigurationController(ILoggerManager logger, IReviewConfigurationService reviewConfigurationService, IResponse response)
        {
            _logger = logger;
            _reviewConfigurationService = reviewConfigurationService;
            _response = response;
            _logger.LogInfo("API Called - Review configuration Controller");
        }

        /// <summary>
        /// This method is used to add new review configuration to the table.
        /// </summary>
        /// <param name="reviewConfigurationRequest">review configuration request model.</param>
        /// <returns>IResponse.</returns>
        [HttpPost]
        [Route("saveReviewConfiguration")]
        [Authorize]
        public async Task<IResponse> SaveReviewConfiguration(ReviewConfigurationRequest reviewConfigurationRequest)
        {
            _logger.LogInfo("API Called - Review configuration add method");
            if (ModelState.IsValid)
            {
                return await _reviewConfigurationService.SaveReviewConfigurationAsync(reviewConfigurationRequest);
            }
            else
            {
                _response.Result = null;
                _response.IsSuccess = false;
                _response.Message = "Please add required parameters";
                return _response;
            }
        }

        /// <summary>
        /// This method is used to update the review configuration entry.
        /// </summary>
        /// <param name="reviewConfigurationUpdate">review configuration update request.</param>
        /// <returns>IResponse.</returns>
        [HttpPost]
        [Route("updateReviewConfiguration")]
        [Authorize]
        public async Task<IResponse> UpdateReviewConfiguration(ReviewConfigurationUpdate reviewConfigurationUpdate)
        {
            _logger.LogInfo("API Called - Review configuration update method");
            if (ModelState.IsValid)
            {
                return await _reviewConfigurationService.UpdateReviewConfigurationAsync(reviewConfigurationUpdate);
            }
            else
            {
                _response.Result = null;
                _response.IsSuccess = false;
                _response.Message = "Please add required parameters";
                return _response;
            }
        }

        /// <summary>
        /// This method is used to delete a review configuration entry from the table.
        /// </summary>
        /// <param name="reviewConfigId">review configuration id.</param>
        /// <returns>IResponse.</returns>
        [HttpPost]
        [Route("deleteReviewConfigurationById")]
        [Authorize]
        public async Task<IResponse> DeleteReviewConfigurationById([FromBody] long reviewConfigId)
        {
            _logger.LogInfo("API Called - Review configuration delete method");
            if (reviewConfigId > 0)
            {
                return await _reviewConfigurationService.DeleteReviewConfigurationAsync(reviewConfigId);
            }
            else
            {
                _response.Result = null;
                _response.IsSuccess = false;
                _response.Message = "Invalid review configuration id.";
                return _response;
            }
        }

        /// <summary>
        /// This method is used to retrieve all the review configuration entrys from the table.
        /// </summary>
        /// <returns>IResponse.</returns>
        [HttpGet]
        [Route("getAllReviewConfiguration")]
        [Authorize]
        public async Task<IResponse> GetAllReviewConfiguration()
        {
                _logger.LogInfo("API Called - Get al review configuration id.");
                return await _reviewConfigurationService.GetAllReviewConfigurationAsync();
        }

        /// <summary>
        ///  This method is used to get the review configuration details by id or user id.
        /// </summary>
        /// <param name="id">review config id.</param>
        /// <param name="userId">user id.</param>
        /// <returns>IResponse.</returns>
        [HttpGet]
        [Route("getReviewConfig")]
        [Authorize]
        public async Task<IResponse> GetReviewConfig(long? id, bool? userId)
        {
            _logger.LogInfo("API Called - Get Review details by Id or user id");
            if (userId.HasValue || id > 0)
            {
                return await _reviewConfigurationService.GetReviewConfig(id, userId);
            }
            else
            {
                _response.Result = null;
                _response.IsSuccess = false;
                _response.Message = "Review configuarion id or fetch fromuser id parameter is required.";
                return _response;
            }
        }

    }
}
