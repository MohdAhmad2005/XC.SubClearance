using Microsoft.EntityFrameworkCore;
using XC.XSC.Models.Entity.Prefrence;
using XC.XSC.Repositories.Preferences;
using XC.XSC.Service.User;

namespace XC.XSC.Service.Preferences
{
    public class PreferenceService: IPreferenceService
    {
        private readonly IPreferenceRepository _preferenceRepository;
        private readonly IUserContext _userContext;

        public PreferenceService(IPreferenceRepository preferenceRepository, IUserContext userContext)
        {
            _preferenceRepository = preferenceRepository;
            _userContext= userContext;
        }

        /// <summary>
        /// This method is used to retrive preference data based on key, tenantid and userid.
        /// </summary>
        /// <param name="key">Preference Key</param>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="userId">User Id</param>
        public async Task<Preference?> GetPreferenceAsync(string key, string tenantId, string userId)
        {
            return await _preferenceRepository.GetAll()
                                              .Where(a => a.Key == key && a.TenantId == _userContext.UserInfo.TenantId && 
                                               a.IsActive == true
                                               ).SingleOrDefaultAsync();
        }

        /// <summary>
        /// This method is used to retrive preference data based  tenantid.
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        public async Task<List<Preference>> GetPreferenceByTenantAsync(string tenantId)
        {
            return await _preferenceRepository.GetAll()
                                              .Where(a => a.TenantId == tenantId && a.IsActive == true)
                                              .ToListAsync<Preference>();
        }

        /// <summary>
        /// This method is used to update preference data based on preferenceKey, tenantid and preferenceKeyValue .
        /// </summary>
        /// <param name="tenantId">Tenant Id</param>
        /// <param name="preferenceKey">Preference Key</param>
        /// <param name="preferenceKeyValue">User Id</param>
        public async Task<bool> UpdatePreferenceByKey(string tenantId, string preferenceKey,string preferenceKeyValue)
        {

            Preference? preference= await  _preferenceRepository.GetAll().Where(a => a.Key == preferenceKey && a.TenantId == tenantId
                                               && a.IsActive == true
                                               ).SingleOrDefaultAsync();

            if (preference != null)
            {
                preference.Value = preferenceKeyValue;
                Preference? updatedPreference =await _preferenceRepository.UpdateAsync(preference);
                if(updatedPreference != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}
