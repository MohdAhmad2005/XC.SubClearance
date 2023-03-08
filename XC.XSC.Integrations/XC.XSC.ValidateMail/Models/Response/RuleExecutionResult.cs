using System.Data;

namespace XC.XSC.ValidateMail.Models.Response
{
    /// <summary>
    /// Used for Rule Execution Result
    /// </summary>
    public class RuleExecutionResult
    {
        /// <summary>
        /// List of rule execution detail
        /// </summary>
        public List<ExecutedRule> RulesExecutedDetail { get; set; }
        /// <summary>
        /// RuleEngine return data 
        /// </summary>
        public DataTable ResultantData { get; set; }
    }
}
