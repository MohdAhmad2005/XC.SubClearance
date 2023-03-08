using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Models.Interface.EmailInfo;

namespace XC.XSC.Models.Entity.EmailInfo
{
    /// <summary>
    /// This model will store the information of EmailInfo.
    /// </summary>
    public class EmailInfo : BaseEntity<long>, IEmailInfo
    {
        /// <summary>
        /// EmailID of EmailInfo.
        /// </summary>
        [Required(ErrorMessage ="Email id is required.")]
        [StringLength(250)]
        [Range(1, 250, ErrorMessage = "Max allowed email id length is 250.")]
        public string EmailId { get; set; }=string.Empty;

        /// <summary>
        /// From name of email info.
        /// </summary>

        [StringLength(50)]
        [Range(1, 50, ErrorMessage = "Max allowed from name length is 50.")]
        public string FromName { get; set; } = string.Empty;

        /// <summary>
        /// From email of email info.
        /// </summary>

        [StringLength(50)]
        [Range(1, 50, ErrorMessage = "Max allowed from email length is 50.")]
        public string FromEmail { get; set; } = string.Empty;

        /// <summary>
        /// To mailid for email info.
        /// </summary>

        [StringLength(50)]
        [Range(1, 50, ErrorMessage = "Max allowed to email length is 50.")]
        public string ToEmail { get; set; } = string.Empty;

        /// <summary>
        /// CC email for email info.
        /// </summary>

        [StringLength(50)]
        [Range(1, 50, ErrorMessage = "Max allowed cc email length is 50.")]
        public string? CCEmail { get; set; } = string.Empty;

        /// <summary>
        /// Subject of email info.
        /// </summary>

        [StringLength(2000)]
        [Range(1, 2000, ErrorMessage = "Max allowed subject length is 2000.")]
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Body of email info.
        /// </summary>

        [StringLength(5000)]
        [Range(1, 5000, ErrorMessage = "Max allowed body length is 5000.")]
        public string Body { get; set; } = string.Empty;

        /// <summary>
        /// Lob id of email info.
        /// </summary>

        [StringLength(50)]
        [Range(1, 50, ErrorMessage = "Max allowed lobid length is 50.")]
        public string LobId { get; set; } = string.Empty;

        /// <summary>
        /// Message id of email info.
        /// </summary>

        [StringLength(50)]
        [Range(1, 50, ErrorMessage = "Max allowed message Id length is 50.")]
        public string MessageId { get; set; } = string.Empty;

        /// <summary>
        /// Parent message id of email info.
        /// </summary>

        [StringLength(50)]
        [Range(1, 50, ErrorMessage = "Max allowed parent message Id length is 50.")]
        public string? ParentMessageId { get; set; } = string.Empty;

        /// <summary>
        /// Total document number of email info.
        /// </summary>

        public int TotalDocuments { get; set; }

        /// <summary>
        /// Received date of email info.
        /// </summary>
        public DateTime ReceivedDate { get; set; }

        /// <summary>
        /// Document id of email info.
        /// </summary>
        [StringLength(50)]
        [Range(1, 50, ErrorMessage = "Max allowed document id length is 50.")]
        public string DocumentId { get; set; } = string.Empty;

        /// <summary>
        /// Email info duplicate check field.
        /// </summary>

        public bool IsDuplicate { get; set; }

        /// <summary>
        /// Mailbox name of Email Info.
        /// </summary>
        [StringLength(100)]
        [Range(1, 100, ErrorMessage = "Max allowed mail box name length is 100.")]
        public string? MailboxName { get; set; } = string.Empty;

        /// <summary>
        /// Configuration id of email info.
        /// </summary>
        [StringLength(100)]
        [Range(1, 100, ErrorMessage = "Max allowed configuration id length is 100.")]
        public string? ConfigurationId { get; set; } = string.Empty;

        /// <summary>
        /// Email Body Length
        /// </summary>
        public int BodyLength { get; set; }

        public virtual ICollection<EMailInfoAttachment.EmailInfoAttachment> Attachments { get; set; }
        public virtual ICollection<Submission.Submission> Submissions { get; set; }
    }
}
