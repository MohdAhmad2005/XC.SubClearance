namespace XC.XSC.ValidateMail.Models
{
    /// <summary>
    /// OutScopeMessage is used to send message to ASB
    /// </summary>
    public class OutScopeMessage
    {
        /// <summary>
        /// Emalil Message Id from Submission table
        /// </summary>
        public string? MessageId { get; set; }
        /// <summary>
        /// SubmissionId from Submission table
        /// </summary>
        public long SubmissionId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Stage { get; set; }
        /// <summary>
        /// TenantId from Submission table
        /// </summary>
        public string? TenantId { get; set; }
        /// <summary>
        /// Scope from Submission table
        /// </summary>
        public bool Scope { get; set; }

        /// <summary>
        /// MailboxName
        /// </summary>
        public string? MailboxEmailId { get; set; } = string.Empty;

        /// <summary>
        /// Mail-box configurationId
        /// </summary>
        public string? ConfigurationId { get; set; } = string.Empty;
    }
}
