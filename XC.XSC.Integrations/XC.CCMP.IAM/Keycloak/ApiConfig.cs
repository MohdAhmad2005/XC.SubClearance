
using XC.CCMP.KeyVault;

namespace XC.CCMP.IAM.Keycloak
{
    public class ApiConfig: IApiConfig
    {
        private readonly IKeyVaultConfig _keyVaultConfig;

        public ApiConfig(IKeyVaultConfig keyVaultConfig)
        {
            _keyVaultConfig = keyVaultConfig;
        }

        public IAMType IAMType { get { return IAMType.Keycloak; } }
        public string AdminUsername { get { return _keyVaultConfig.KeycloakAdminUserName; } }
        public string AdminPassword { get { return _keyVaultConfig.KeycloakAdminPassword; } }
        public string BaseUrl { get { return _keyVaultConfig.KeycloakBaseUrl; } }
        public string Realm { get { return _keyVaultConfig.KeycloakRealm; } }
        public string ClientId { get { return _keyVaultConfig.KeycloakClientId; } }
        public string ClientSecret { get { return _keyVaultConfig.KeycloakClientSecret; } }
        public string PublicKey { get { return _keyVaultConfig.KeycloakPublicKey; } }

        public string Authority {
            get
            {
                return $"{BaseUrl}realms/{Realm}";
            }
        }

        public string MetadataAddress
        {
            get
            {
                return $"{BaseUrl}realms/{Realm}/.well-known/openid-configuration";
            }
        }

        public string LoginEndpoint
        {
            get
            { 
                return $"{BaseUrl}realms/{Realm}/protocol/openid-connect/token";
            }
        }
        public string AdminLoginEndpoint
        {
            get
            {
                return $"{BaseUrl}realms/{Realm}/protocol/openid-connect/token";
            }
        }
        public string UserEndpoint
        {
            get
            {
                return $"{BaseUrl}admin/realms/{Realm}/users";
            }
        }
        public string LogoutEndpoint
        {
            get
            {
                return $"{BaseUrl}realms/{Realm}/protocol/openid-connect/logout";
            }
        }
        public string ClientEndpoint
        {
            get
            {
                return $"{BaseUrl}admin/realms/{Realm}/clients";
            }
        }

        public string ResetPasswordEndpoint
        {
            get
            {
                return $"{BaseUrl}admin/realms/{Realm}/users";
            }
        }

        public string ValidIssuers
        {
            get
            {
                return $"{BaseUrl}realms/{Realm}";
            }
        }

        public string GetResourcesByUriEndPoint
        {
            get
            {
                return $"{BaseUrl}realms/{Realm}/authz/protection/resource_set";
            }
        }
    }

}
