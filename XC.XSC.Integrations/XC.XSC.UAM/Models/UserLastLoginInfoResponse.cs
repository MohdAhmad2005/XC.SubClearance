using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.UAM.Models
{
    /// <summary>
    /// Model to represent user last login info from UAM.
    /// </summary>
    public class UserLastLoginInfoResponse
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
        public UserLoginDetails Result { get; set; }
    }

    /// <summary>
    /// Details model for user event details
    /// </summary>
    public class Details
    {
        /// <summary>
        /// Auth method for event details.
        /// </summary>
        [JsonProperty("auth_method")]
        public string AuthMethod { get; set; }

        /// <summary>
        /// Token id for event details.
        /// </summary>
        [JsonProperty("token_id")]
        public string TokenId { get; set; }

        /// <summary>
        /// Grant type of user events.
        /// </summary>
        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

        /// <summary>
        /// Refresh token type for user events.
        /// </summary>
        [JsonProperty("refresh_token_type")]
        public string RefreshTokenType { get; set; }

        /// <summary>
        /// Scope for user events.
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; set; }

        /// <summary>
        /// Refresh token id of user events.
        /// </summary>
        [JsonProperty("refresh_token_id")]
        public string RefreshTokenId { get; set; }

        /// <summary>
        /// Client auth method of user events.
        /// </summary>
        [JsonProperty("client_auth_method")]
        public string ClientAuthMethod { get; set; }

        /// <summary>
        /// User name of user event.
        /// </summary>
        [JsonProperty("username")]
        public string UserName { get; set; }
    }

    /// <summary>
    /// User login event details model.
    /// </summary>
    public class UserLoginDetails
    {
        /// <summary>
        /// Time of event.
        /// </summary>
        [JsonProperty("time")]
        public object Time { get; set; }

        /// <summary>
        /// Type of event.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Realm id of event.
        /// </summary>
        [JsonProperty("realmId")]
        public string RealmId { get; set; }

        /// <summary>
        /// Client id of event.
        /// </summary>
        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// User id of event.
        /// </summary>
        [JsonProperty("userId")]
        public string UserId { get; set; }

        /// <summary>
        /// Session id of event.
        /// </summary>
        [JsonProperty("sessionId")]
        public string SessionId { get; set; }

        /// <summary>
        /// Ip address of event.
        /// </summary>
        [JsonProperty("ipAddress")]
        public string IpAddress { get; set; }

        /// <summary>
        /// Details of event.
        /// </summary>
        [JsonProperty("details")]
        public Details Details { get; set; }

        /// <summary>
        /// Error of event.
        /// </summary>
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}
