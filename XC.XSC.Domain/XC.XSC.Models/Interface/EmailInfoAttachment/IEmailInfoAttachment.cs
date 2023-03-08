using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Models.Entity.EmailInfo;

namespace XC.XSC.Models.Interface.EmailInfoAttachment
{
    internal interface IEmailInfoAttachment
    {
        public long EmailInfoId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string DocumentId { get; set; }
        public string AttachmentId { get; set; }
        public int FileSize { get; set; }
        public string SizeUnit { get; set; }

    }
}
