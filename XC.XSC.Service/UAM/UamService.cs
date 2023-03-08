using XC.CCMP.KeyVault;
using XC.XSC.UAM.Connector;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.Service.User;
using Region = XC.XSC.UAM.Models.Region;
using XC.XSC.UAM.Models;

namespace XC.XSC.UAM.UAM
{
    public class UamService : IUamService
    {
        /// <summary>
        /// Service class which is used to invoke the uam web api.
        /// </summary>

        private readonly IUAMClient _uamClient;
        private readonly IKeyVaultConfig _keyVaultConfig;
        private readonly IUamApiConfig _uamApiConfig;
        private readonly IUserContext _userContext;

        /// <summary>
        /// Constructor of service which initialize the dependencies. 
        /// </summary>
        /// <param name="keyVaultConfig"></param>
        /// <param name="httpContextAccessor"></param>
        public UamService(IKeyVaultConfig keyVaultConfig, IUAMClient uamClient, IUamApiConfig uamApiConfig, IUserContext userContext)
        {
            _keyVaultConfig = keyVaultConfig;
            _uamApiConfig = uamApiConfig;
            _uamApiConfig.BaseUrl = keyVaultConfig.UamApiBaseUrl;
            _uamClient = uamClient;
            _userContext = userContext;

        }

        /// <summary>
        /// Method used to return the region list.
        /// </summary>
        /// <returns>List of region.</returns>
        public async Task<IResponse> GetUserRegions()
        {
            var regionResponse = await _uamClient.GetRegionList();

            if (regionResponse.Result != null)
            {
                List<Region> regionList = (List<Region>)regionResponse.Result;
                var userRegionList = regionList.Where(c => _userContext.UserInfo.Region.Any(c2 => c.Id == c2)).ToList();
                regionResponse.Result = userRegionList;
            }

            return regionResponse;
        }

        /// <summary>
        /// Method used to return the Holiday List By TeamId
        /// </summary>
        /// <returns>List of Holiday.</returns>
        public async Task<IResponse> GetHolidayListByTeamId(int teamId)
        {
            return await _uamClient.GetHolidayListByTeamId(teamId);
        }

        /// <summary>
        /// Get team list.
        /// </summary>
        /// <returns>IResponse.</returns>
        public async Task<IResponse> GetTeamList()
        {
            return await _uamClient.GetTeamList();
        }

        /// <summary>
        /// Get list of users according to given filters.
        /// </summary>
        /// <param name="filterRequest">Model class property of type UserFilterRequest.</param>
        /// <returns>Returns User details with respect to filters as IResponse.</returns>
        public async Task<IResponse> GetUsersByFilters(UserFilterRequest filterRequest)
        {
            return await _uamClient.GetUsersByFilters(filterRequest);
        }

        /// <summary>
        /// Update user details.
        /// </summary>
        /// <param name="updateUser"></param>
        /// <returns></returns>
        public async Task<IResponse> UpdateUser(UpdateUser updateUser)
        {
            return await _uamClient.UpdateUser(updateUser);
        }

        /// <summary>
        /// Method to return user last login details from keycloak.
        /// </summary>
        /// <returns></returns>
        public async Task<IResponse> GetLastLoginDetails()
        {
            return await _uamClient.GetLastLoginDetails();
        }
    }
}