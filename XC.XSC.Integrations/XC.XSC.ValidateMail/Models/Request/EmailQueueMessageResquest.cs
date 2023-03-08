namespace XC.XSC.ValidateMail.Models.Request
{
    /// <summary>
    /// used to send data to rule engine
    /// </summary>
    public class EmailQueueMessageResquest
    {
        /// <summary>
        /// TaskId from Submission table
        /// </summary>
        public string? TaskId { get; set; }
        /// <summary>
        /// SubmissionId from Submission table
        /// </summary>
        public long SubmissionId { get; set; }
        /// <summary>
        /// FromEmail from Submission table
        /// </summary>
        public string? FromEmail { get; set; }
        /// <summary>
        /// ToEmail from Submission table
        /// </summary>
        public string? ToEmail { get; set; }
        /// <summary>
        /// EmailSubject from Submission table
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

        /// <summary>
        /// Email Body Length
        /// </summary>
        public int BodyLength { get; set; }
    }
}
