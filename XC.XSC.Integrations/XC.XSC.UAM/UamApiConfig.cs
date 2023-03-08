using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.UAM
{
    /// <summary>
    /// Class to invoke UAM API.
    /// </summary>
    public class UamApiConfig : IUamApiConfig
    {

        /// <summary>
        /// Base url to access the UAM application.
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Property to return the get region list url.
        /// </summary>
        public string GetRegionListEndPoint
        {
            get
            {
                return $"{BaseUrl}api/ccmp/uam/getAllRegions";
            }
        }

        /// <summary>
        /// Property to return the get Get Holiday List By TeamId End Point list url.
        /// </summary>
        public string GetHolidayListByTeamIdEndPoint
        {
            get
            {
                return $"{BaseUrl}api/ccmp/uam/getHolidayListByTeamId";
            }
        }

        /// <summary>
        /// Get team list.
        /// </summary>
        public string GetTeamListEndPoint
        {
            get
            {
                return $"{BaseUrl}api/ccmp/uam/getTeamList";
            }
        }

        /// <summary>
        /// Get user list by filters.
        /// </summary>
        public string GetUsersByFiltersEndPoint
        {
            get
            {
                return $"{BaseUrl}api/ccmp/uam/getUsersByFilters";
            }
        }

        /// <summary>
        /// Update user end point
        /// </summary>
        public string UpdateUserUAMEndPoint
        {
            get
            {
                return $"{BaseUrl}api/ccmp/uam/updateUser";
            }
        }

        /// <summary>
        /// Update user end point
        /// </summary>
        public string GetUserLastLoginDetailsEndPoint
        {
            get
            {
                return $"{BaseUrl}api/ccmp/uam/getUserLastLoginDetails";
            }
        }
    }
}
