using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XC.XSC.Service.Notification;
using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.API.Controllers
{
    /// <summary>
    /// Used for notification 
    /// </summary>
    [Authorize]
    [Route("api/xsc/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IResponse _operationResponse;

        /// <summary>
        /// Initialize the Notification Controller
        /// </summary>
        /// <param name="notificationService"></param>
        /// <param name="operationResponse"></param>
        public NotificationController(INotificationService notificationService, IResponse operationResponse)
        {
            _notificationService = notificationService;
            _operationResponse = operationResponse;
        }

        /// <summary>
        /// Get current logged in User Notification 
        /// </summary>
        /// <returns>SUCCESS</returns>
        [HttpGet]
        [Route("getUserNotification")]
        public async Task<IResponse> GetUserNotification()
        {
            return await _notificationService.GetUserNotification();
        }

        /// <summary>
        /// Update Notifications Status by Id
        /// </summary>
        /// <param name="id">Notification Id</param>
        /// <returns>SUCCESS</returns>
        [HttpPost]
        [Route("updateNotificationsStatus")]

        public async Task<IResponse> UpdateNotificationsStatus([FromBody] int id)
        {
            if (id > 0)
            {
                return await _notificationService.UpdateNotificationsStatus(id);                
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "In-Valid id.";
                return _operationResponse;
            }
        }
    }
}
