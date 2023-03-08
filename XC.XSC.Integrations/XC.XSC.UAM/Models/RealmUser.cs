using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.UAM.Models
{
    public class UpdateUser
    {
        /// <summary>
        /// Id of user.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Created time stamp of user.
        /// </summary>
        [JsonProperty("createdTimestamp")]
        public long CreatedTimestamp { get; set; }

        /// <summary>
        /// User name of users.
        /// </summary>
        [JsonProperty("username")]
        public string UserName { get; set; }

        /// <summary>
        /// Enabled of users.
        /// </summary>
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        /// <summary>
        /// totp of users.
        /// </summary>
        [JsonProperty("totp")]
        public bool Totp { get; set; }

        /// <summary>
        /// Email verified of users.
        /// </summary>
        [JsonProperty("emailVerified")]
        public bool EmailVerified { get; set; }

        /// <summary>
        /// First name of users.
        /// </summary>
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of users.
        /// </summary>
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Email of users.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Attributes of users.
        /// </summary>
        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

        /// <summary>
        /// disableable Credential Types of users.
        /// </summary>
        [JsonProperty("disableableCredentialTypes")]
        public List<object> DisableableCredentialTypes { get; set; }

        /// <summary>
        /// Required actions of user.
        /// </summary>
        [JsonProperty("requiredActions")]
        public List<object> RequiredActions { get; set; }

        /// <summary>
        /// Not before of users.
        /// </summary>
        [JsonProperty("notBefore")]
        public int NotBefore { get; set; }

        /// <summary>
        /// Access of users.
        /// </summary>
        [JsonProperty("access")]
        public Access Access { get; set; }

        /// <summary>
        /// Realm roles of users.
        /// </summary>
        [JsonProperty("realmRoles")]
        public List<string> RealmRoles { get; set; }
    }

    /// <summary>
    /// Attribute for user.
    /// </summary>
    public class Attributes
    {
        /// <summary>
        /// Holiday list of attributes.
        /// </summary>
        public List<string> HolidayListId { get; set; }

        /// <summary>
        /// Manager name of attributes.
        /// </summary>
        public List<string> ManagerId { get; set; }

        /// <summary>
        /// Is team manager of attribute.
        /// </summary>
        public List<string> IsTeamManager { get; set; }

        /// <summary>
        /// Region of User Attributes.
        /// </summary>
        public List<string> RegionId { get; set; }

        /// <summary>
        /// Team of Attributes.
        /// </summary>
        public List<string> TeamId { get; set; }

        /// <summary>
        /// Business details of attributes.
        /// </summary>
        public List<string> BusinessDetails { get; set; }

        /// <summary>
        /// Lob of attributes.
        /// </summary>
        public List<string> LobId { get; set; }

        /// <summary>
        /// Role of user.
        /// </summary>
        public List<string> Role { get; set; }
    }

    /// <summary>
    /// Model of access of user response.
    /// </summary>
    public class Access
    {
        /// <summary>
        /// Group membership of user.
        /// </summary>
        public bool ManageGroupMembership { get; set; }

        /// <summary>
        /// View of user.
        /// </summary>
        public bool View { get; set; }

        /// <summary>
        /// Map role of user.
        /// </summary>
        public bool MapRoles { get; set; }

        /// <summary>
        /// Impersonate of user.
        /// </summary>
        public bool Impersonate { get; set; }

        /// <summary>
        /// Manage property of user.
        /// </summary>
        public bool Manage { get; set; }
    }

    public class UpdateUserResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public UpdateUser Result { get; set; }
    }
}
