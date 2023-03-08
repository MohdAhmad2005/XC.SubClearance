namespace XC.XSC.ValidateMail.Models.Response
{
    /// <summary>
    /// Validate Mail Scope Response
    /// </summary>
    public class ValidateMailScopeResponse
    {
        /// <summary>
        /// Camunda TaskId
        /// </summary>
        public string? TaskId { get; set; }
        /// <summary>
        /// SubmissionId from database
        /// </summary>
        public long? SubmissionId { get; set; }
        /// <summary>
        /// Camunda Stage
        /// </summary>
        public string? Stage { get; set; }
        /// <summary>
        /// TenantId
        /// </summary>
        public string? TenantId { get; set; }
        /// <summary>
        /// If InScope then true else false
        /// </summary>
        public bool? Scope { get; set; }

    }
}
