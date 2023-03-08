namespace XC.XSC.ValidateMail
{
    /// <summary>
    /// List of rule engine endpoints.
    /// </summary>
    public interface IApiConfig
    {
        /// <summary>
        /// Api base url.
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// API RuleExecution/ExecuteRuleByContext end point.
        /// </summary>
        public string ExecuteRuleByContextEndPoint { get; }

        /// <summary>
        /// API RuleConfiguration/GetRuleSetList end point.
        /// </summary>
        public string GetRuleSetListEndPoint { get; }
    }
}
