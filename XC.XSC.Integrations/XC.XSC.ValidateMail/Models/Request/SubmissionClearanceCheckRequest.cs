namespace XC.XSC.ValidateMail.Models.Request
{
    /// <summary>
    /// used to send data to rule engine
    /// </summary>
    public class SubmissionClearanceCheckRequest
    {
        /// <summary>
        /// BrokerFirm from Submission table
        /// </summary>
        public string? BrokerFirm { get; set; }

        /// <summary>
        /// IsBrokerFirm true if rule pass
        /// </summary>
        public bool IsBrokerFirm { get; set; } = false;

        /// <summary>
        /// BrokerIndividual from Submission table
        /// </summary>
        public string? BrokerIndividual { get; set; }

        /// <summary>
        /// IsBrokerIndividual true if rule pass
        /// </summary>
        public bool IsBrokerIndividual { get; set; } = false;

        /// <summary>
        /// FromEmail from Submission table
        /// </summary>
        public string? FromEmail { get; set; }

        /// <summary>
        /// ToEmail from Submission table
        /// </summary>
        public string? EmailSubject { get; set; }

        /// <summary>
        /// Email Subject Prefix such as RE,FW
        /// </summary>
        public string? EmailSubjectPrefix { get; set; }

        /// <summary>
        /// EmailBody from Submission table
        /// </summary>
        public string? EmailBody { get; set; }
        /// <summary>
        /// AttachmentCount from Submission table
        /// </summary>
        public int AttachmentCount { get; set; }
        /// <summary>
        /// Scope from Submission table
        /// </summary>
        public bool Scope { get; set; } = false;
    }
}
