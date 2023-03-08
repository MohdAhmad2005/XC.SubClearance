using System.Data;
using System.Threading.Tasks;
using XC.XSC.ValidateMail.Models.Request;
using XC.XSC.ValidateMail.Models.Response;

namespace XC.XSC.ValidateMail.Connect
{
    public interface IClient
    {
        /// <summary>
        /// ExecuteRuleByContextAsync is used to call rule engine Api
        /// </summary>
        /// <param name="ruleExecutionRequest"></param>
        /// <returns>RuleExecutionResult with rule detail and datatable</returns>
        Task<RuleExecutionResult> ExecuteRuleByContextAsync(RuleExecutionRequest ruleExecutionRequest);

        /// <summary>
        /// Get rule set list from rule engine
        /// </summary>
        /// <param name="ruleSetRequest"></param>
        /// <returns>ruleSetResponse</returns>
        Task<RuleSetResponse> GetRuleSetList(RuleSetRequest ruleSetRequest);
    }
}
