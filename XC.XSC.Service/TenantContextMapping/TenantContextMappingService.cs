using Microsoft.EntityFrameworkCore;
using XC.XSC.Repositories.TenantContextMapping;
using XC.XSC.Service.User;

namespace XC.XSC.Service.TenantContextMapping
{
    /// <summary>
    /// Tenant Context Mapping Service
    /// </summary>
    public class TenantContextMappingService : ITenantContextMappingService
    {
        private readonly IUserContext _userContext;
        private readonly ITenantContextMappingRepository _tenantContextMappingRepository;

        public TenantContextMappingService(IUserContext userContext, ITenantContextMappingRepository tenantContextMappingRepository)
        {
            _userContext = userContext;
            _tenantContextMappingRepository = tenantContextMappingRepository;
        }

        /// <summary>
        /// Get Tenant Context Mapping based on TenantId, Region and Lob
        /// </summary>
        /// <param name="submission"></param>
        /// <returns>tenantContextMapping</returns>
        public async Task<Models.Entity.TenantContextMapping.TenantContextMapping> GetTenantContextMapping(Models.Entity.Submission.Submission submission)
        {
            var tenantContextMapping = await _tenantContextMappingRepository.GetAll().FirstOrDefaultAsync(a => a.IsActive == true
                                       && a.TenantId == _userContext.UserInfo.TenantId
                                       && a.Region == Convert.ToString(submission.RegionId)
                                       && a.Lob == Convert.ToString(submission.LobId));
            return tenantContextMapping;
        }
    }
}
