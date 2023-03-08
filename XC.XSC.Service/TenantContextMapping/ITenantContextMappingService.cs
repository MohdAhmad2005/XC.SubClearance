using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.Service.TenantContextMapping
{
    public interface ITenantContextMappingService
    {
        /// <summary>
        /// Get Tenant Context Mapping based on TenantId, Region and Lob
        /// </summary>
        /// <param name="submission"></param>
        /// <returns>tenantContextMapping</returns>
        Task<Models.Entity.TenantContextMapping.TenantContextMapping> GetTenantContextMapping(Models.Entity.Submission.Submission submission);
    }
}
