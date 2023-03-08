using System.Runtime.CompilerServices;

namespace XC.XSC.ViewModels.QueueProcessor
{
    public class EmailProcessorRequest
    {
        public EmailProcessorRequest()
        {
            this.AttachmentMessages = new List<AttachmentMessage>();
        }

        public string? Id { get; set; } = string.Empty;
        /// <summary>
        /// MessageId
        /// </summary>
        public string MessageId { get; set; } = string.Empty;
        /// <summary>
        /// Application Id
        /// </summary>
        public string ApplicationId { get; set; } = string.Empty;

        /// <summary>
        /// Mail-Box Configuration Id
        /// </summary>
        public string ConfigurationId { get; set; } = string.Empty;

        /// <summary>
        /// ParentMailId
        /// </summary>
        public string ParentMailId { get; set; } = string.Empty;
        /// <summary>
        /// Sender Email
        /// </summary>
        public string FromEmail { get; set; } = string.Empty;
        /// <summary>
        /// recipient  Email
        /// </summary>
        public string ToEmail { get; set; }= string.Empty;
        /// <summary>
        /// marke as CC 
        /// </summary>
        public string CCEmail { get; set; }    
        /// <summary>
        /// mark as B CC
        /// </summary>
        public string BccEmail { get; set; }
        /// <summary>
        /// Email subject
        /// </summary>
        public string SubjectEmail { get; set; }
        /// <summary>
        /// Sender name
        /// </summary>
        public string FromName { get; set; }
        /// <summary>
        /// attached  File name
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// attached Total Documents
        /// </summary>
        public int TotalDocuments { get; set; }
        /// <summary>
        /// Email Recevied date
        /// </summary>
        public DateTime EmailRecieveDate { get; set; }
        /// <summary>
        /// Mail Box Name
        /// </summary>
        public string MailBoxName { get; set; }
        /// <summary>
        /// Document path
        /// </summary>
        public string Path { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDuplicate { get; set; }
        /// <summary>
        /// Email Type Scope/outscoped
        /// </summary>
        public object Scope { get; set; }
        /// <summary>
        /// App Name
        /// </summary>
        public string ApplicationName { get; set; } = string.Empty;
        /// <summary>
        /// DocumentId
        /// </summary>
        public string DocumentId { get; set; } = string.Empty;
        /// <summary>
        /// LobId
        /// </summary>
        public string LobId { get; set; } = string.Empty;

        /// <summary>
        /// TenantId
        /// </summary>
        public string TenantId { get; set; } = string.Empty;
        /// <summary>
        /// SizeUnit as MB/KB
        /// </summary>
        public string SizeUnit { get; set; } = string.Empty;
        /// <summary>
        /// Email Body
        /// </summary>
        public string Body { get; set; } = string.Empty;

        /// <summary>
        /// User TeamId
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// User RegionId
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// Email Body Length
        /// </summary>
        public int BodyLength { get; set; }

        /// <summary>
        /// File Size 
        /// </summary>
        public int FileSize { get; set; }

        public List<AttachmentMessage> AttachmentMessages { get; set; }
    }


    public class AttachmentMessage
    {
        public string Id { get; set; }
        public string EmailMasterId { get; set; } = string.Empty;
        /// <summary>
        /// File Name
        /// </summary>
        public string FileName { get; set; } = string.Empty;
        /// <summary>
        /// File Exten.
        /// </summary>
        public string FileType { get; set; } = string.Empty;
        /// <summary>
        /// AttachmentId
        /// </summary>
        public string AttachmentId { get; set; } = string.Empty;
        /// <summary>
        /// File Path that added 
        /// </summary>
        public string FilePath { get; set; } = string.Empty;
        /// <summary>
        /// File Size 
        /// </summary>
        public int FileSize { get; set; }
        /// <summary>
        /// MessageId
        /// </summary>
        public string MessageId { get; set; } = string.Empty;
        /// <summary>
        /// DocumentId
        /// </summary>
        public string DocumentId { get; set; } = string.Empty;
        /// <summary>
        /// Unit Size MB/KB
        /// </summary>
        public string SizeUnit { get; set; } = string.Empty;
    }
}
