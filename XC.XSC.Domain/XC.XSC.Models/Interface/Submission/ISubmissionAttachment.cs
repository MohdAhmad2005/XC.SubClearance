using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.Models.Interface.Submission
{
    public interface ISubmissionAttachment
    {
        public string EmailMasterId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileSize { get; set; }
        public string MessageId { get; set; }
        public string DocumentId { get; set; }
        public string SubmissionID { get; set; }
    }
}
