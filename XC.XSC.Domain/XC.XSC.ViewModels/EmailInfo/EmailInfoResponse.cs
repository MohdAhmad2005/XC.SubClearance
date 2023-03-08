using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.EmailInfo
{
    /// <summary>
    /// This model will contain the email info response details.
    /// </summary>
    public class EmailInfoResponse
    {
        /// <summary>
        /// table id of the email info response.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// email id of the email.
        /// </summary>
        public string? EmailId { get; set; }

        /// <summary>
        /// the name of the person who has sent the email.
        /// </summary>
        public string? FromName { get; set; }

        /// <summary>
        /// the email id of the person who has sent the email.
        /// </summary>
        public string? FromEmail { get; set; }

        /// <summary>
        /// email id which the email has received.
        /// </summary>
        public string? ToEmail { get; set; }

        /// <summary>
        /// is there any cc email was present on the email.
        /// </summary>
        public string? CCEmail { get; set; }

        /// <summary>
        /// subject of the email.
        /// </summary>
        public string? Subject { get; set; }

        /// <summary>
        /// body of the email.
        /// </summary>
        public string? Body { get; set; }

        /// <summary>
        /// lob id of the email.
        /// </summary>
        public string? LobId { get; set; }

        /// <summary>
        /// message id of the email.
        /// </summary>
        public string? MessageId { get; set; }

        /// <summary>
        /// parent message id.
        /// </summary>
        public string? ParentMessageId { get; set; }

        /// <summary>
        /// total documents count attached in the email.
        /// </summary>
        public int TotalDocuments { get; set; }

        /// <summary>
        /// when the email has received.
        /// </summary>
        public DateTime ReceivedDate { get; set; }

        /// <summary>
        /// document id of the email.
        /// </summary>
        public string? DocumentId { get; set; }

        /// <summary>
        /// is email duplicate or not.
        /// </summary>
        public bool IsDuplicate { get; set; }

        /// <summary>
        /// extracted email body details.
        /// </summary>
        public string? ExtractedBodyDetails { get; set; }

        /// <summary>
        /// full details of the attachments in the email.
        /// </summary>
        public List<EmailInfoAttachment.EmailInfoAttachmentResponse>? Attachments { get; set; }

    }
}


