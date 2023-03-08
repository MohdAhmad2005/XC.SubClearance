using Microsoft.Extensions.Configuration;
using XC.CCMP.KeyVault.Manager;

namespace XC.CCMP.KeyVault
{
    public class KeyVaultConfig: IKeyVaultConfig
    {
        private readonly IKeyVaultManager _keyVaultManager;
        private readonly IConfiguration _configuration;

        public KeyVaultConfig(IKeyVaultManager keyVaultManager, IConfiguration configuration) {
            
            _keyVaultManager = keyVaultManager;
            _configuration = configuration;

            this.GetKeyVaultSecrets();
        }

        /* Create property for smtp to get the key vault key. */
        private string SmtpServerKey { get { return "xsc-smtp-server"; } }
        private string SmtpPortKey { get { return "xsc-smtp-port"; } }
        private string SmtpPasswordKey { get { return "xsc-smtp-password"; } }
        private string SmtpUserKey { get { return "xsc-smtp-user"; } }
        private string SmtpEnableSslKey { get { return "xsc-smtp-enable-ssl"; } }
        private string SmtpEnableTlsKey { get { return "xsc-smtp-enable-tls"; } }
        private string SmtpSenderKey { get { return "xsc-smtp-sender"; } }
        
        /* Create property for database to get the key vault key. */
        private string DatabaseNameKey { get { return "xsc-db-name"; } }
        private string DatabaseConnectionStringKey { get { return "xsc-db-connection-string"; } }
        private string XscWebUrlKey { get { return "xsc-web-url"; } }
        private string IAMTypeKey { get { return "iam-type"; } }

        /* Create property for keycloak to get the key vault key. */
        
        private string KeycloakClientSecretKey { get { return "keycloak-client-secret"; } }
        private string KeycloakClientIdKey { get { return "keycloak-client-id"; } }
        private string KeycloakRealmKey { get { return "keycloak-realm"; } }
        private string KeycloakBaseUrlKey { get { return "keycloak-base-url"; } }
        private string KeycloakPublicKey_Key { get { return "keycloak-public-key"; } }
        private string DSApiBaseUrlKey { get { return "ds-api-base-url"; } }

        /* Create property for Rule Engine to get the key vault key. */
        public string RuleEngineApiBaseUrlKey { get { return "rule-engine-api-base-url"; } }
        private string QueueTypeKey { get { return "xsc-mailscope-queue-type"; } }
        private string QueueNameKey { get { return "xsc-mailscope-queue-name"; } }
        private string QueueConnectionStringKey { get { return "xsc-mailscope-queue-connection-string"; } }

        /* Create property for mongo db connection strings*/
        private string MongoConnectionStringKey { get { return "xsc-mongo-db-connection-string"; } }
        private string MongoDatabaseNameKey { get { return "xsc-mongo-db-name"; } }

        /* Create property for UAM api key vault key value*/
        private string UamApiBaseUrlKey { get { return "uam-api-base-url"; } }

        /* Create property for UAM api key vault key value*/
        private string EmsApiBaseUrlKey { get { return "ems-api-base-url"; } }

        /* Create private property for Workflow api key vault key value*/
        private string WorkflowAdminUserNameKey { get { return "workflow-admin-username"; } }
        private string WorkflowAdminUserPasswordKey { get { return "workflow-admin-password"; } }
        private string WorkflowBaseUrlKey { get { return "workflow-base-url"; } }


        public string SmtpServer { get; set; } = String.Empty;
        public string SmtpPort { get; set; } = String.Empty;
        public string SmtpPassword { get; set; } = String.Empty;
        public string SmtpUser { get; set; } = String.Empty;
        public string SmtpEnableSsl { get; set; } = String.Empty;
        public string SmtpEnableTls { get; set; } = String.Empty;
        public string SmtpSender { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
        public string DatabaseConnectionString { get; set; } = String.Empty;
        public string IAMType { get; set; } = string.Empty;

        /* Create property for keycloak to set the key vault key value. */
        public string KeycloakAdminPassword { get; set; } = String.Empty;
        public string KeycloakAdminUserName { get; set; } = String.Empty;
        public string KeycloakClientSecret { get; set; } = String.Empty;
        public string KeycloakClientId { get; set; } = String.Empty;
        public string KeycloakRealm { get; set; } = String.Empty;
        public string KeycloakBaseUrl { get; set; } = String.Empty;
        public string KeycloakPublicKey { get; set; } = String.Empty;
        public string XscWebUrl { get; set; }

        /* Create property for Rule Engine to set the key vault key value. */
        public string RuleEngineApiBaseUrl { get; set; }
        public string QueueType { get; set; } = String.Empty;
        public string QueueName { get; set; } = String.Empty;
        public string QueueConnectionString { get; set; } = String.Empty;

        /* Create property for mongo db connection strings*/
        public string MongoConnectionString { get;set; } = String.Empty;
        public string MongoDatabaseName { get; set; } = String.Empty;

        /* Create property for UAM api key vault key value*/
        public string UamApiBaseUrl { get; set; } = String.Empty;

        public string DSApiBaseUrl { get; set; }

        /* Create property for UAM api key vault key value*/
        public string EmsApiBaseUrl { get; set; } = String.Empty;

        /* Create public property for Workflow api key vault key value*/
        public string WorkflowAdminUserName { get; set; } = String.Empty;
        public string WorkflowAdminPassword { get; set; } = String.Empty;
        public string WorkflowAdminBaseUrl { get; set; } = String.Empty;


        private KeyVaultConfig GetKeyVaultSecrets()
        {
            /*
            this.DatabaseConnectionString = _keyVaultManager.GetSecret(this.DatabaseConnectionStringKey).Result;
            this.DatabaseName = _keyVaultManager.GetSecret(this.DatabaseNameKey).Result;
            this.IAMType = _keyVaultManager.GetSecret(this.IAMTypeKey).Result;

            this.SmtpEnableSsl = _keyVaultManager.GetSecret(this.SmtpEnableSslKey).Result;
            this.SmtpEnableTls = _keyVaultManager.GetSecret(this.SmtpEnableTlsKey).Result;
            this.SmtpPort = _keyVaultManager.GetSecret(this.SmtpPortKey).Result;
            this.SmtpSender = _keyVaultManager.GetSecret(this.SmtpSenderKey).Result;
            this.SmtpServer = _keyVaultManager.GetSecret(this.SmtpServerKey).Result;
            this.SmtpUser = _keyVaultManager.GetSecret(this.SmtpUserKey).Result;
            this.SmtpPassword = _keyVaultManager.GetSecret(this.SmtpPasswordKey).Result;

            this.KeycloakBaseUrl = _keyVaultManager.GetSecret(this.KeycloakBaseUrlKey).Result;
            this.KeycloakClientId = _keyVaultManager.GetSecret(this.KeycloakClientIdKey).Result;
            this.KeycloakClientSecret = _keyVaultManager.GetSecret(this.KeycloakClientSecretKey).Result;
            this.KeycloakRealm = _keyVaultManager.GetSecret(this.KeycloakRealmKey).Result;
            this.KeycloakPublicKey = _keyVaultManager.GetSecret(this.KeycloakPublicKey_Key).Result;

            this.XscWebUrl = _keyVaultManager.GetSecret(this.XscWebUrlKey).Result; 
            this.RuleEngineApiBaseUrl = _keyVaultManager.GetSecret(this.RuleEngineApiBaseUrlKey).Result; 
            this.QueueType = _keyVaultManager.GetSecret(this.QueueTypeKey).Result;
            this.QueueName = _keyVaultManager.GetSecret(this.QueueNameKey).Result;
            this.QueueConnectionString = _keyVaultManager.GetSecret(this.QueueConnectionStringKey).Result;

            this.MongoConnectionString = _keyVaultManager.GetSecret(this.MongoConnectionStringKey).Result;
            this.MongoDatabaseName = _keyVaultManager.GetSecret(this.MongoDatabaseNameKey).Result;
            this.UamApiBaseUrl = _keyVaultManager.GetSection(this.UamApiBaseUrlKey).Value;

            this.WorkflowAdminUserName= _keyVaultManager.GetSection(this.WorkflowAdminUserNameKey).Value;
            this.WorkflowAdminPassword = _keyVaultManager.GetSection(this.WorkflowAdminUserPasswordKey).Value;
            this.WorkflowAdminBaseUrl = _keyVaultManager.GetSection(this.WorkflowBaseUrlKey).Value;
            */

            this.DatabaseConnectionString = _configuration.GetSection(this.DatabaseConnectionStringKey).Value;
            this.DatabaseName = _configuration.GetSection(this.DatabaseNameKey).Value;
            this.IAMType = _configuration.GetSection(this.IAMTypeKey).Value;

            this.SmtpEnableSsl = _configuration.GetSection(this.SmtpEnableSslKey).Value;
            this.SmtpEnableTls = _configuration.GetSection(this.SmtpEnableTlsKey).Value;
            this.SmtpPort = _configuration.GetSection(this.SmtpPortKey).Value;
            this.SmtpSender = _configuration.GetSection(this.SmtpSenderKey).Value;
            this.SmtpServer = _configuration.GetSection(this.SmtpServerKey).Value;
            this.SmtpUser = _configuration.GetSection(this.SmtpUserKey).Value;
            this.SmtpPassword = _configuration.GetSection(this.SmtpPasswordKey).Value;

            this.KeycloakBaseUrl = _configuration.GetSection(this.KeycloakBaseUrlKey).Value;
            this.KeycloakClientId = _configuration.GetSection(this.KeycloakClientIdKey).Value;
            this.KeycloakClientSecret = _configuration.GetSection(this.KeycloakClientSecretKey).Value;
            this.KeycloakRealm = _configuration.GetSection(this.KeycloakRealmKey).Value;
            this.KeycloakPublicKey = _configuration.GetSection(this.KeycloakPublicKey_Key).Value;

            this.XscWebUrl = _configuration.GetSection(this.XscWebUrlKey).Value;
            this.RuleEngineApiBaseUrl = _configuration.GetSection(this.RuleEngineApiBaseUrlKey).Value;

            this.QueueType = _configuration.GetSection(this.QueueTypeKey).Value;
            this.QueueName = _configuration.GetSection(this.QueueNameKey).Value;
            this.QueueConnectionString = _configuration.GetSection(this.QueueConnectionStringKey).Value;

            this.MongoConnectionString = _configuration.GetSection(this.MongoConnectionStringKey).Value;
            this.MongoDatabaseName = _configuration.GetSection(this.MongoDatabaseNameKey).Value;
            
            this.UamApiBaseUrl = _configuration.GetSection(this.UamApiBaseUrlKey).Value;
            this.DSApiBaseUrl = _configuration.GetSection(this.DSApiBaseUrlKey).Value;
            this.EmsApiBaseUrl= _configuration.GetSection(this.EmsApiBaseUrlKey).Value;

            this.WorkflowAdminUserName= _configuration.GetSection(this.WorkflowAdminUserNameKey).Value;
            this.WorkflowAdminPassword = _configuration.GetSection(this.WorkflowAdminUserPasswordKey).Value;
            this.WorkflowAdminBaseUrl = _configuration.GetSection(this.WorkflowBaseUrlKey).Value;
            return this;
        }
    }

}
