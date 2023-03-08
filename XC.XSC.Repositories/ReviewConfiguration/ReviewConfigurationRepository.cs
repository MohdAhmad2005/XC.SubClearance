using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using XC.XSC.Data;

namespace XC.XSC.Repositories.ReviewConfiguration
{
    /// <summary>
    /// Review configuration repository.
    /// </summary>
    public class ReviewConfigurationRepository : IReviewConfigurationRepository
    {
        private readonly MSSqlContext _msSqlContext;

        /// <summary>
        /// Review configuration repository class.
        /// </summary>
        /// <param name="msSqlContext">Ms sql context instance.</param>
        public ReviewConfigurationRepository(MSSqlContext msSqlContext)
        {
            _msSqlContext = msSqlContext;
        }

        /// <summary>
        /// Adding a new entry to the review configuration table.
        /// </summary>
        /// <param name="obj">Review configuration object.</param>
        public async Task AddAsync(Models.Entity.ReviewConfiguration.ReviewConfiguration obj)
        {
            _msSqlContext.ReviewConfiguration.Add(obj);
            await _msSqlContext.SaveChangesAsync();
        }

        /// <summary>
        /// Removes an entry fromreview configuration table.
        /// </summary>
        /// <param name="predicate">Review configuration object.</param>
        /// <returns></returns>
        public async Task DeleteAsync(Expression<Func<Models.Entity.ReviewConfiguration.ReviewConfiguration, bool>> predicate)
        {
            var reviewConfiguration = await _msSqlContext.ReviewConfiguration
                               .SingleOrDefaultAsync(predicate);
            _msSqlContext.ReviewConfiguration.RemoveRange(reviewConfiguration);
            await _msSqlContext.SaveChangesAsync();
        }

        /// <summary>
        /// Get all data from the review configuration table.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Models.Entity.ReviewConfiguration.ReviewConfiguration> GetAll()
        {
            return _msSqlContext.ReviewConfiguration
                          .Include(lob => lob.Lob)
                          .AsQueryable();
        }

        /// <summary>
        /// Get a single review configuration object.
        /// </summary>
        /// <param name="predicate">Review configuration object.</param>
        /// <returns></returns>

        public async Task<Models.Entity.ReviewConfiguration.ReviewConfiguration> GetSingleAsync(Expression<Func<Models.Entity.ReviewConfiguration.ReviewConfiguration, bool>> predicate)
        {
            return await _msSqlContext.ReviewConfiguration
                               .Include(lob => lob.Lob)
                               .SingleOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Update a review configuration object.
        /// </summary>
        /// <param name="obj">Review configuration object.</param>
        /// <returns></returns>
        public async Task<Models.Entity.ReviewConfiguration.ReviewConfiguration> UpdateAsync(Models.Entity.ReviewConfiguration.ReviewConfiguration obj)
        {
            _msSqlContext.Entry(obj).State = EntityState.Modified;
            await _msSqlContext.SaveChangesAsync();
            return obj;
        }
    }
}
