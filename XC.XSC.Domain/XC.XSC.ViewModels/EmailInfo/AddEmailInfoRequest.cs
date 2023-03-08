using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.EmailInfo
{
    public class AddEmailInfoRequest
    {
        public long Id { get; set; }
        [Required]
        [StringLength(250)]
        public string EmailId { get; set; }=string.Empty;
        [StringLength(50)]
        public string FromName { get; set; } = string.Empty;
        [StringLength(50)]
        public string FromEmail { get; set; } = string.Empty;
        [StringLength(50)]
        public string ToEmail { get; set; } = string.Empty;
        [StringLength(50)]
        public string CCEmail { get; set; } = string.Empty;
        [StringLength(100)]
        public string Subject { get; set; } = string.Empty;
        [StringLength(5000)]
        public string Body { get; set; } = string.Empty;
        [StringLength(50)]
        public string LobId { get; set; } = string.Empty;
        [StringLength(50)]
        public string MessageId { get; set; } = string.Empty;
        [StringLength(50)]
        public string ParentMessageId { get; set; } = string.Empty;
        public int TotalDocuments { get; set; }
        public DateTime ReceivedDate { get; set; }
        [StringLength(50)]
        public string DocumentId { get; set; } = string.Empty;
        public bool IsDuplicate { get; set; }
        [StringLength(50)]
        public string TenantId { get; set; } = string.Empty;
        [StringLength(50)]
        public string CreatedBy { get; set; } = string.Empty;
        [StringLength(100)]
        public string MailboxName { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string ConfigurationId { get; set; } = String.Empty;

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

        public List<EmailInfoAttachment.AddEmailInfoAttachmentRequest> Attachments { get; set; }
    }
}
