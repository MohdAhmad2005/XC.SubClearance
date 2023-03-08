using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.UAM.Models
{
    /// <summary>
    /// User response model.
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// Success status for user listing.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Message of operation on user listing.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Result of user listing.
        /// </summary>
        public List<UserResponseResult> Result { get; set; }
    }

    /// <summary>
    /// User response result model.
    /// </summary>
    public class UserResponseResult
    {
        /// <summary>
        /// Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Full name of the user. 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// First name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// Is team manager.
        /// </summary>
        public string isTeamManager { get; set; }

        /// <summary>
        /// Manager.
        /// </summary>
        public List<ManagerResponse> Manager { get; set; }

        /// <summary>
        /// Holiday list.
        /// </summary>
        public List<HolidayListResponse> HolidayList { get; set; }

        /// <summary>
        /// Region.
        /// </summary>
        public List<UserRegionResponse> Region { get; set; }

        /// <summary>
        /// Lob.
        /// </summary>
        public List<LobResponse> Lob { get; set; }

        /// <summary>
        /// Business details.
        /// </summary>
        public List<string> BusinessDetails { get; set; }

        /// <summary>
        /// Team.
        /// </summary>
        public List<TeamResponseResult> Team { get; set; }

        /// <summary>
        /// Role.
        /// </summary>
        public List<string> Role { get; set; }

        /// <summary>
        /// Enabled property of user.
        /// </summary>
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        /// <summary>
        /// Created time stamp of user.
        /// </summary>
        public long CreatedTimestamp { get; set; }
    }
}
