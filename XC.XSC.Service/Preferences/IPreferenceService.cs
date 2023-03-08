using XC.XSC.Models.Entity.Prefrence;

namespace XC.XSC.Service.Preferences
{
    public interface IPreferenceService
    {
        /// <summary>
        /// This method is used to retrive preference data based on key, tenantid and userid.
        /// </summary>
        /// <param name="key">Preference Key</param>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="userId">User Id</param>
        Task<Preference?> GetPreferenceAsync(string key, string tenantId, string userId);


        /// <summary>
        /// This method is used to retrive preference data based  tenantid.
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        Task<List<Preference>> GetPreferenceByTenantAsync(string tenantId);


        /// <summary>
        /// This method is used to update preference data based on preferenceKey, tenantid and preferenceKeyValue .
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="preferenceKey">Preference Key</param>
        /// <param name="preferenceKeyValue">User Id</param>
        Task<bool> UpdatePreferenceByKey(string tenantId, string preferenceKey, string preferenceKeyValue);
    }
}
