using System.ComponentModel.DataAnnotations;
using XC.XSC.Models.Interface.Submission;

namespace XC.XSC.Models.Entity.Submission
{
    public class SubmissionAttachment : BaseEntity<long>, ISubmissionAttachment
    {
        [StringLength(100)]
        [Required]
        public string EmailMasterId { get; set; }
        [StringLength(100)]
        public string FileName { get; set; }
        [StringLength(200)]
        public string FilePath { get; set; }
        [StringLength(10)]
        public string FileSize { get; set; }
        [StringLength(100)]
        public string MessageId { get; set; }
        [StringLength(100)]
        public string DocumentId { get; set; }
        [StringLength(100)]
        public string SubmissionID { get; set; }
    }
}
