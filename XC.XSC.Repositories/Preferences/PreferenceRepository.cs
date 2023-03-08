using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using XC.XSC.Data;
using XC.XSC.Models.Entity.Prefrence;

namespace XC.XSC.Repositories.Preferences
{
    /// <summary>
    /// This is the implementation class of IPreferenceRepository interface.
    /// </summary>
    public class PreferenceRepository : IPreferenceRepository
    {
        private readonly MSSqlContext _msSqlContext;
        public PreferenceRepository(MSSqlContext msSqlContext)
        {
            _msSqlContext = msSqlContext;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="preference"> Preference modal object</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task AddAsync(Preference preference)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task DeleteAsync(Expression<Func<Preference, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is implemented to retrieve all record of preference table.
        /// </summary>
        /// <returns>List of Preference data</returns>
        public IQueryable<Preference> GetAll()
        {
            return _msSqlContext.Preferences.AsQueryable();
        }

        /// <summary>
        /// This method is implemented to retrieve single record of preference table.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns> Single Preference data</returns>
        public async Task<Preference> GetSingleAsync(Expression<Func<Preference, bool>> predicate)
        {
            return await GetAll().SingleAsync<Preference>(predicate);
        }

        /// <summary>
        /// This method is implemented to update record of preference table.
        /// </summary>
        /// <returns>Preference table updated record</returns>
        public async Task<Preference> UpdateAsync(Preference preference)
        {
            if (preference != null)
            {
                _msSqlContext.Entry(preference).State = EntityState.Modified;
                await _msSqlContext.SaveChangesAsync();
                return preference;
            }
            else
            {
                return preference;
            }
        }
    }
}
