using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using XC.XSC.Data;
using XC.XSC.Models.Entity.Scheduler;

namespace XC.XSC.Repositories.Scheduler
{
    public class SchedulerConfigurationRepository : ISchedulerConfigurationRepository
    {
        private readonly MSSqlContext _msSqlContext;
        public SchedulerConfigurationRepository(MSSqlContext msSqlContext)
        {
            this._msSqlContext = msSqlContext;
        }
        /// <summary>
        /// add scheduler configuration.
        /// </summary>
        /// <param name="schedulerConfiguration"></param>
        /// <returns>success</returns>
        public async Task AddAsync(Models.Entity.Scheduler.SchedulerConfiguration schedulerConfiguration)
        {
            await _msSqlContext.SchedulerConfiguration.AddAsync(schedulerConfiguration);
            await _msSqlContext.SaveChangesAsync();

        }
        /// <summary>
        /// Delete scheduler Configuration detail 
        /// </summary>
        /// <param name="predicate">perdicate detail </param>


        public Task DeleteAsync(Expression<Func<SchedulerConfiguration, bool>> predicate)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// GetAll 
        /// </summary>
        /// <returns>List of scheduler Configuration detail </returns>
        public IQueryable<Models.Entity.Scheduler.SchedulerConfiguration> GetAll()
        {
            return _msSqlContext.SchedulerConfiguration.Include(t=>t.Lob).AsQueryable();
        }
        /// <summary>
        /// Get Single Detail based on Predicate filter 
        /// </summary>
        /// <param name="predicate">Get Single Detail based on Predicate filter </param>
        /// <returns></returns>
        public async Task<Models.Entity.Scheduler.SchedulerConfiguration> GetSingleAsync(Expression<Func<Models.Entity.Scheduler.SchedulerConfiguration, bool>> predicate)
        {
            return await _msSqlContext.SchedulerConfiguration.Include(t => t.Lob).SingleOrDefaultAsync(predicate);

        }
        /// <summary>
        /// Update Scheduler Configuration Detail
        /// </summary>
        /// <param name="schedulerConfiguration"></param>
        /// <returns></returns>
        public async Task<Models.Entity.Scheduler.SchedulerConfiguration> UpdateAsync(Models.Entity.Scheduler.SchedulerConfiguration schedulerConfiguration)
        {
            _msSqlContext.Entry(schedulerConfiguration).State = EntityState.Modified;
            await _msSqlContext.SaveChangesAsync();
            return schedulerConfiguration;
        }
    }
}
