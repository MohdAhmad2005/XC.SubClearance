using XC.XSC.UAM.Models;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.Enum;
using XC.XSC.ViewModels.Sla;

namespace XC.XSC.Service.Notification
{
    public interface INotificationService
    {
        /// <summary>
        /// Add Notification
        /// </summary>
        /// <param name="notification"> notification request.</param>
        /// <returns>Returns bool: Success then true and false if failed.</returns>
        Task<IResponse> AddNotification(Models.Entity.Notification.Notification notification);

        /// <summary>
        /// Send Notification
        /// </summary>
        /// <param name="submission"> submission request.</param>
        /// <param name="subject"> subject.</param>
        /// <returns>Returns bool: Success then true and false if failed.</returns>
        Task<IResponse> SendNotification(Models.Entity.Submission.Submission submission, string userId, string templateKey, int? days = null, bool IsMailOnly = false, string cc = "", string bcc = "", string userName = "", string email = "");

        /// <summary>
        /// Notification send when a case is about to reach its due date (2 days,1 Day and same day earlier)
        /// </summary>
        /// <returns>true if success</returns>
        Task<IResponse> SendDueDateReminderNotificationAsync();

        /// <summary>
        /// Get current logged in User Notification 
        /// </summary>
        /// <returns>SUCCESS</returns>
        Task<IResponse> GetUserNotification();

        /// <summary>
        /// Update Notifications Status by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResponse> UpdateNotificationsStatus(int id);

    }
}
