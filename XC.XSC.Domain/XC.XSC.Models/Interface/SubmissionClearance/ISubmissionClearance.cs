namespace XC.XSC.Models.Interface.SubmissionClearance
{
    /// <summary>
    /// This model is used to return the SubmissionClearance table data
    /// </summary>
    public interface ISubmissionClearance
    {
        /// <summary>
        /// This field is used to store the submisson id.
        /// </summary>
        public long SubmissionId { get; set; }

        /// <summary>
        /// Rule name of the corresponding submission
        /// </summary>
        public string RuleName { get; set; }

        /// <summary>
        /// Rule status of the corresponding submission
        /// </summary>
        public bool RuleStatus { get; set; }

        /// <summary>
        /// Description the corresponding submission
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Remark of corresponding submission
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// ContextId of the corresponding submission
        /// </summary>
        public int ContextId { get; set; }

        /// <summary>
        /// EntityId of the corresponding submission
        /// </summary>
        public int EntityId { get; set; }
    }
}
