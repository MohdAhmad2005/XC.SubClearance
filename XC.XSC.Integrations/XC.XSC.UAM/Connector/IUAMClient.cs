using XC.XSC.UAM.Models;
using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.UAM.Connector
{
    /// <summary>
    /// Interface to invoke api of uam.
    /// </summary>
    public interface IUAMClient
    {
        /// <summary>
        /// Method to invoke the list of region from uam.
        /// </summary>
        /// <returns>List of Regions.</returns>
         Task<IResponse> GetRegionList();

        /// <summary>
        /// Method used to return the Holiday List By TeamId
        /// </summary>
        /// <returns>List of Holiday.</returns>
        Task<IResponse> GetHolidayListByTeamId(int teamId);
         
        /// <summary>
        /// Get team list.
        /// </summary>
        /// <returns>IResponse.</returns>
        Task<IResponse> GetTeamList();

        /// <summary>
        /// Get list of users according to given filters.
        /// </summary>
        /// <param name="filterRequest">Model class property of type UserFilterRequest.</param>
        /// <returns>Returns User details with respect to filters as IResponse.</returns>
        Task<IResponse> GetUsersByFilters(UserFilterRequest filterRequest);

        /// <summary>
        /// Get list of users according to given filters.
        /// </summary>
        /// <param name="filterRequest">Model class property of type UserFilterRequest.</param>
        /// <returns>Returns User details with respect to filters as IResponse.</returns>
        Task<IResponse> UpdateUser(UpdateUser updateUser);

        /// <summary>
        /// Method to represent the last login details from UAM.
        /// </summary>
        /// <returns></returns>
        Task<IResponse> GetLastLoginDetails();
    }
}
