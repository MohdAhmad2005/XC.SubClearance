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
    public interface IUamApiConfig
    {

        /// <summary>
        /// Base url to access the UAM application.
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Property to return the get region list url.
        /// </summary>
        public string GetRegionListEndPoint { get; }

        /// <summary>
        /// Property to return the get Get Holiday List By TeamId End Point list url.
        /// </summary>
        public string GetHolidayListByTeamIdEndPoint { get; }

        /// <summary>
        /// Get team list.
        /// </summary>
        public string GetTeamListEndPoint { get; }

        /// <summary>
        /// Get user list by filters.
        /// </summary>
        public string GetUsersByFiltersEndPoint { get; }

        /// <summary>
        /// Update user end point.
        /// </summary>
        public string UpdateUserUAMEndPoint { get; }

        /// <summary>
        /// Get user last login details from UAM.
        /// </summary>
        public string GetUserLastLoginDetailsEndPoint { get; }

    }
}
