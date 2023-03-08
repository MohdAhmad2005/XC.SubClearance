using System.Data;

namespace XC.XSC.ValidateMail.Models.Request
{
    /// <summary>
    /// RuleEngine Rule Execution Request
    /// </summary>
    public class RuleExecutionRequest
    {
        /// <summary>
        /// RuleContextId from RuleEngine Database
        /// </summary>
        public int RuleContextId { get; set; }
        /// <summary>
        /// DataTable used in RuleEngine
        /// </summary>
        public DataTable? SourceData { get; set; }
        /// <summary>
        /// SourceDataRowIdentifier is initialize the blank string
        /// </summary>
        public string? SourceDataRowIdentifier { get; set; }
        /// <summary>
        /// SourceEntityId used in RuleEngine
        /// </summary>
        public int SourceEntityId { get; set; }
    }
}
