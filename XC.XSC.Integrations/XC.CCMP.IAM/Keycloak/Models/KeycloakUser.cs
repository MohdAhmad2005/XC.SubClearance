using Newtonsoft.Json;
using System.Net;

namespace XC.CCMP.IAM.Keycloak.Models
{
    public class UserDetail:UserResponse
    {
        public List<KeycloakUser> KeycloakUser { get; set; }
    }

    public class KeycloakUser
    {
        public string Id { get; set; }

        public string CreatedTimestamp { get; set; }
        
        public string UserName { get; set; }

        public bool Enabled { get; set; }

        public bool ToTp { get; set; }
        public bool EmailVerified { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }


    }

    public class UserResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }
    }
}
