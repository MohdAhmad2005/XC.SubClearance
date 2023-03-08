
namespace XC.CCMP.IAM.Keycloak
{
    public interface IApiConfig
    {
        public IAMType IAMType { get { return IAMType.Keycloak; } }
        public string AdminUsername { get; }
        public string AdminPassword { get; }
        public string BaseUrl { get; }
        public string Realm { get; }
        public string ClientId { get; }
        public string ClientSecret { get; }
        public string PublicKey { get; }

        public string Authority { get; }

        public string MetadataAddress { get; }

        public string LoginEndpoint { get; }

        public string AdminLoginEndpoint { get; }

        public string UserEndpoint { get; }

        public string LogoutEndpoint { get; }

        public string ClientEndpoint { get; }

        public string ResetPasswordEndpoint { get; }

        public string ValidIssuers { get; }

        public string GetResourcesByUriEndPoint { get; }

    }

}
