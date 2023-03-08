using XC.CCMP.KeyVault;


namespace XC.XSC.Workflow.Workflow
{
    public class WorkflowApiConfig : IWorkflowApiConfig
    {
        private readonly IKeyVaultConfig _keyVaultConfig;
        public WorkflowApiConfig(IKeyVaultConfig keyVaultConfig)
        {
            _keyVaultConfig = keyVaultConfig;
        }

        public WorkflowType WorkflowType { get { return WorkflowType.Camunda; } }
        public string AdminUsername { get { return _keyVaultConfig.WorkflowAdminUserName; } }
        public string AdminPassword { get { return _keyVaultConfig.WorkflowAdminPassword; } }
        public string BaseUrl { get { return _keyVaultConfig.WorkflowAdminBaseUrl; } }

        public string WorkflowAdminLoginEndpoint 
        {
            get
            {
                return $"{BaseUrl}authenticate";
            }
        }

        public string WorkflowEndpoint
        {
            get
            {
                return $"{BaseUrl}workflow/XSC-SUBMISSION-PROCESS/start";
            }
        }
    }
}
