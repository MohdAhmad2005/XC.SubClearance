using XC.CCMP.KeyVault;

namespace XC.XSC.ValidateMail
{
    /// <summary>
    /// List of rule engine endpoints.
    /// </summary>
    public class ApiConfig : IApiConfig
    {
        private readonly IKeyVaultConfig _keyVaultConfig;

        /// <summary>
        /// Api config Constructor.
        /// </summary>
        /// <param name="keyVaultConfig"></param>
        public ApiConfig(IKeyVaultConfig keyVaultConfig)
        {
            _keyVaultConfig = keyVaultConfig;

            this.BaseUrl = _keyVaultConfig.RuleEngineApiBaseUrl;
        }

        /// <summary>
        /// Api base url.
        /// </summary>
        public string BaseUrl { get; set; } = String.Empty;

        /// <summary>
        /// API RuleExecution/ExecuteRuleByContext end point.
        /// </summary>
        public string ExecuteRuleByContextEndPoint
        {
            get
            {
                return $"{BaseUrl}RuleExecution/ExecuteRuleByContext";
            }
        }

        /// <summary>
        /// API RuleConfiguration/GetRuleSetList end point.
        /// </summary>
        public string GetRuleSetListEndPoint
        {
            get
            {
                return $"{BaseUrl}RuleConfiguration/GetRuleSetList";
            }
        }
    }
}
