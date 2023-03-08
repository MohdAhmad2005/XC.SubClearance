namespace XC.XSC.ValidateMail.Models.Response
{
    /// <summary>
    /// Initialize the rule set response
    /// </summary>
    public class RuleSetResponse
    {
        /// <summary>
        /// We get Entity list from rule engine 
        /// </summary>
        public List<RuleSet> Entity { get; set; }

        /// <summary>
        /// Total rule count
        /// </summary>
        public int TotalRowCount { get; set; }
    }

    /// <summary>
    /// Initialize the rule
    /// </summary>
    public class RuleSet
    {
        /// <summary>
        /// RuleSetId for corresponding rule
        /// </summary>
        public int RuleSetId { get; set; }

        /// <summary>
        /// RuleSetName for corresponding rule
        /// </summary>
        public string RuleSetName { get; set; } = string.Empty;

        /// <summary>
        /// Message for corresponding rule
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Rule is active or not
        /// </summary>
        public object? IsActive { get; set; }

        /// <summary>
        /// ContextId for corresponding rule
        /// </summary>
        public int ContextId { get; set; }

        /// <summary>
        /// ContextName for corresponding rule
        /// </summary>
        public string? ContextName { get; set; }

        /// <summary>
        /// SourceEntityId for corresponding rule
        /// </summary>
        public int SourceEntityId { get; set; }

        /// <summary>
        /// SourceEntityName for corresponding rule
        /// </summary>
        public string? SourceEntityName { get; set; }

        /// <summary>
        /// TargetEntityId for corresponding rule
        /// </summary>
        public int TargetEntityId { get; set; }

        /// <summary>
        /// TargetEntityName for corresponding rule
        /// </summary>
        public string? TargetEntityName { get; set; }

        /// <summary>
        /// RuleTypeId for corresponding rule
        /// </summary>
        public int RuleTypeId { get; set; }

        /// <summary>
        /// RuleTypeName for corresponding rule
        /// </summary>
        public string? RuleTypeName { get; set; }

        /// <summary>
        /// RuleExecutionTypeId for corresponding rule
        /// </summary>
        public int RuleExecutionTypeId { get; set; }

        /// <summary>
        /// RuleExecutionTypeName for corresponding rule
        /// </summary>
        public string? RuleExecutionTypeName { get; set; }

        /// <summary>
        /// Rule is custom or not
        /// </summary>
        public bool IsCustom { get; set; }
    }
}
