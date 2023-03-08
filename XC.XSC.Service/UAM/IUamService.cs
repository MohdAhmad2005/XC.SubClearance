using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.UAM.Models;
using XC.XSC.ViewModels.Configuration;

namespace XC.XSC.UAM.UAM
{
    /// <summary>
    /// Interface to invoke the UAM service.
    /// </summary>
    public interface IUamService
    {
        /// <summary>
        /// Method used to invoke the Region list.
        /// </summary>
        /// <returns> Region Response list.</returns>
        Task<IResponse> GetUserRegions();

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
        /// Method to represent the user last login info.
        /// </summary>
        /// <returns></returns>
        Task<IResponse> GetLastLoginDetails();
    }
}
