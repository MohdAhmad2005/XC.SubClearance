using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.Models.Interface.EmailInfo
{
    public interface IEmailInfo
    {
        public string EmailId { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string? CCEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string LobId { get; set; }
        public string MessageId { get; set; }
        public string? ParentMessageId { get; set; }
        public int TotalDocuments { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string DocumentId { get; set; }
        public bool IsDuplicate { get; set; }
        public string MailboxName { get; set; }

        /// <summary>
        /// Email Body Length
        /// </summary>
        public int BodyLength { get; set; }
    }
}
