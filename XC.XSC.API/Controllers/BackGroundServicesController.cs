using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XC.XSC.Service.Notification;
using XC.XSC.Service.SubmissionClearance;
using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.API.Controllers
{
    /// <summary>
    /// All Back Ground Services API
    /// </summary>
    [Authorize]
    [Route("api/xsc/[controller]")]
    [ApiController]
    public class BackGroundServicesController : ControllerBase
    {
        private readonly IResponse _operationResponse;
        private readonly ISubmissionClearanceService _submissionClearanceService;
        private readonly INotificationService _notificationService;

        /// <summary>
        /// Initialize the Back Ground Services Controller
        /// </summary>
        /// <param name="operationResponse"></param>
        /// <param name="submissionClearanceService"></param>
        ///  /// <param name="notificationService"></param>
        public BackGroundServicesController(IResponse operationResponse, ISubmissionClearanceService submissionClearanceService
            , INotificationService notificationService
            )
        {
            _operationResponse = operationResponse;
            _submissionClearanceService = submissionClearanceService;
            _notificationService = notificationService;
        }

        /// <summary>
        /// Execute submission clearance check rule on the basis of predefined rules
        /// </summary>
        /// <param name="submissionId">Accept submissionId</param>
        /// <returns>SUCCESS</returns>
        [HttpGet]
        [Route("submissionClearanceCheck/{submissionId}")]

        public async Task<IResponse> SubmissionClearanceCheck(long submissionId)
        {
            if (submissionId > 0)
            {
                return await _submissionClearanceService.SubmissionClearanceCheckAsync(submissionId);
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "In-Valid submission.";

                return _operationResponse;
            }

        }

        /// <summary>
        /// Notification send when a case is about to reach its due date (2 days,1 Day and same day earlier)
        /// </summary>
        /// <returns>true if success</returns>
        [HttpGet]
        [Route("sendDueDateReminderNotification")]
        public async Task<IResponse> SendDueDateReminderNotification()
        {
            return await _notificationService.SendDueDateReminderNotificationAsync();
        }
    }
}
