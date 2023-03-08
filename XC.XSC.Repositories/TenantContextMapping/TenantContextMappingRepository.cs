using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using XC.XSC.Data;

namespace XC.XSC.Repositories.TenantContextMapping
{
    /// <summary>
    /// Implement Tenant Context Mapping Repository
    /// </summary>
    public class TenantContextMappingRepository : ITenantContextMappingRepository
    {
        private readonly MSSqlContext _msSqlContext;

        public TenantContextMappingRepository(MSSqlContext msSqlContext)
        {
            _msSqlContext = msSqlContext;
        }

        public Task AddAsync(Models.Entity.TenantContextMapping.TenantContextMapping obj)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Expression<Func<Models.Entity.TenantContextMapping.TenantContextMapping, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Models.Entity.TenantContextMapping.TenantContextMapping> GetAll()
        {
            return _msSqlContext.TenantContextMappings.AsQueryable();
        }

        /// <summary>
        /// Asynchronously returns the only element of a sequence that satisfies a specified condition,
        /// and throws an exception if more than one such element exists.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<Models.Entity.TenantContextMapping.TenantContextMapping> GetSingleAsync(Expression<Func<Models.Entity.TenantContextMapping.TenantContextMapping, bool>> predicate)
        {
            return await GetAll().SingleAsync<Models.Entity.TenantContextMapping.TenantContextMapping>(predicate);
        }

        public Task<Models.Entity.TenantContextMapping.TenantContextMapping> UpdateAsync(Models.Entity.TenantContextMapping.TenantContextMapping obj)
        {
            throw new NotImplementedException();
        }
    }
}
