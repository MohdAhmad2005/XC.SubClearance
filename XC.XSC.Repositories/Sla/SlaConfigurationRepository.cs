using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using XC.XSC.Data;
using XC.XSC.Models.Entity.Sla;

namespace XC.XSC.Repositories.Sla
{
    public class SlaConfigurationRepository : ISlaConfigurationRepository
    {
        private readonly MSSqlContext _msSqlContext;

        /// <summary>
        /// SlaRepository Constructor 
        /// </summary>
        /// <param name="msSqlContext">DBContext contain Entity model </param>
        public SlaConfigurationRepository(MSSqlContext msSqlContext)
        {
            _msSqlContext = msSqlContext;
        }

        /// <summary>
        ///To Add the Sla Configuartion
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>slaConfiguration</returns>
        public async Task AddAsync(Models.Entity.Sla.SlaConfiguration slaConfiguration)
        {
            await _msSqlContext.SlaConfiguration.AddAsync(slaConfiguration);
            await _msSqlContext.SaveChangesAsync();

        }


        public Task DeleteAsync(Expression<Func<Models.Entity.Sla.SlaConfiguration, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all the Sla configutation 
        /// </summary>
        /// <returns></returns>
        public IQueryable<Models.Entity.Sla.SlaConfiguration> GetAll()   
        {
            return _msSqlContext.SlaConfiguration.Include(lob=>lob.Lob).AsQueryable();
          
        }

        /// <summary>
        /// Get the single data for Sla configuarion 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<Models.Entity.Sla.SlaConfiguration> GetSingleAsync(Expression<Func<Models.Entity.Sla.SlaConfiguration, bool>> predicate)
        {
         return await _msSqlContext.SlaConfiguration.Include(lob => lob.Lob).AsQueryable().SingleOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Update Sla Configuration Detail
        /// </summary>
        /// <param name="SlaConfiguration"></param>
        /// <returns>slaConfiguration model</returns>
        public async Task<Models.Entity.Sla.SlaConfiguration> UpdateAsync(Models.Entity.Sla.SlaConfiguration slaConfiguration)
        {
            _msSqlContext.Entry(slaConfiguration).State = EntityState.Modified;
            await _msSqlContext.SaveChangesAsync();
            return slaConfiguration;
        }
    }
}
