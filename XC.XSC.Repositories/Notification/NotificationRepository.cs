using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using XC.XSC.Data;

namespace XC.XSC.Repositories.Notification
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly MSSqlContext _msSqlContext;
        public NotificationRepository(MSSqlContext msSqlContext)
        {
            _msSqlContext = msSqlContext;
        }
        /// <summary>
        /// Add Notification based on userId
        /// </summary>
        /// <param name="addNotificationRequest"></param>
        /// <returns></returns>
        public async Task AddAsync(Models.Entity.Notification.Notification addNotificationRequest)
        {
            _msSqlContext.Notifications.Add(addNotificationRequest);
            await _msSqlContext.SaveChangesAsync();
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task DeleteAsync(Expression<Func<Models.Entity.Notification.Notification, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is implemented to retrieve all record of Notifications table.
        /// </summary>
        /// <returns> Returns all data from Notifications table</returns>
        public IQueryable<Models.Entity.Notification.Notification> GetAll()
        {
            return _msSqlContext.Notifications.AsQueryable();
        }

        /// <summary>
        /// Get Notification if exist other wise null
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<Models.Entity.Notification.Notification?> GetSingleAsync(Expression<Func<Models.Entity.Notification.Notification, bool>> predicate)
        {
            return await GetAll().SingleOrDefaultAsync<Models.Entity.Notification.Notification>(predicate);
        }

        /// <summary>
        /// Update notification IsRead true
        /// </summary>
        /// <param name="notification"></param>
        /// <returns>notification</returns>
        public async Task<Models.Entity.Notification.Notification> UpdateAsync(Models.Entity.Notification.Notification notification)
        {
            _msSqlContext.Entry(notification).State = EntityState.Modified;
            await _msSqlContext.SaveChangesAsync();
            return notification;
        }
    }
}
