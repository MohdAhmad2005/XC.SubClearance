using Newtonsoft.Json;

namespace XC.CCMP.IAM.Keycloak.Models.Permission
{
    public class Resource
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("owner")]
        public Owner owner { get; set; }

        [JsonProperty("ownerManagedAccess")]
        public bool OwnerManagedAccess { get; set; }

        [JsonProperty("uris")]
        public List<string> Uris { get; set; }

        [JsonProperty("icon_uri")]
        public string IconUri { get; set; }
    }

    public class Owner
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
