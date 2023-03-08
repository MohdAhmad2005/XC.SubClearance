using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using XC.CCMP.KeyVault.Manager;

namespace XC.CCMP.KeyVault
{
    public interface IKeyVaultConfig
    {
        public string SmtpServer { get; set; }
        public string SmtpPort { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpEnableSsl { get; set; }
        public string SmtpEnableTls { get; set; }
        public string SmtpSender { get; set; }
        public string DatabaseName { get; set; }
        public string DatabaseConnectionString { get; set; }
        public string IAMType { get; set; }

        /* Create property for keycloak to set the key vault key value. */
        public string KeycloakAdminPassword { get; set; }
        public string KeycloakAdminUserName { get; set; }
        public string KeycloakClientSecret { get; set; }
        public string KeycloakClientId { get; set; }
        public string KeycloakRealm { get; set; }
        public string KeycloakBaseUrl { get; set; }
        public string KeycloakPublicKey { get; set; }
        
        /* XSC Vault properties */
        public string XscWebUrl { get; set; }
        public string DSApiBaseUrl { get; }


        /* XSC Rule Engine properties */
        public string RuleEngineApiBaseUrl { get; set; }
        public string QueueType { get; set; }
        public string QueueName { get; set; }
        public string QueueConnectionString { get; set; }

        /* UAM properties*/
        public string UamApiBaseUrl { get; set; }

        /* Mongo Db properties */
        public string MongoConnectionString { get; set; }
        public string MongoDatabaseName { get; set; }

        /* EMS properties*/
        public string EmsApiBaseUrl { get; set; }

        /* Create public property for Workflow api key vault key value*/
        public string WorkflowAdminUserName { get; set; } 
        public string WorkflowAdminPassword { get; set; }
        public string WorkflowAdminBaseUrl { get; set; } 

    }

}
