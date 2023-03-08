using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.EmailInfoAttachment
{
    /// <summary>
    /// This model will contain the email attachments details.
    /// </summary>
    public class EmailInfoAttachmentResponse
    {
        /// <summary>
        /// id of the attachments.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// email info id corresponding field.
        /// </summary>
        public long EmailInfoId { get; set; }

        /// <summary>
        /// file name of the attachment.
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// file type of the attachment.
        /// </summary>
        public string? FileType { get; set; }

        /// <summary>
        /// document id of the attachment.
        /// </summary>
        public string? DocumentId { get; set; }

        /// <summary>
        /// attachment id of the response.
        /// </summary>
        public string? AttachmentId { get; set; }

        /// <summary>
        /// file size of the attachment.
        /// </summary>
        public int FileSize { get; set; }

        /// <summary>
        /// size unit of the attachment.
        /// </summary>
        public string? SizeUnit { get; set; }

        /// <summary>
        /// who has created this attachment.
        /// </summary>
        public string? CreatedBy { get; set; }
    }
}
