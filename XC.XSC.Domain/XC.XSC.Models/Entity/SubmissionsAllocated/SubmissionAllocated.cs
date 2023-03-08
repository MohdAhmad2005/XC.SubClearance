using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Models.Entity.SubmissionStatus;
using XC.XSC.Models.Interface.SubmissionsAllocated;

namespace XC.XSC.Models.Entity.SubmissionsAllocated
{
    public class SubmissionAllocated : BaseEntity<long>, ISubmissionAllocated
    {
        [ForeignKey(nameof(SubmissionId))]
        public long SubmissionId { get; set ; }
        [StringLength(50)]
        public string AllocatedTo { get; set; }
        public DateTime AllocatedDate { get; set; }
        [StringLength(50)]
        public string AllocatedBy { get; set; }
        public virtual Submission.Submission Submission { get; set; }
    }
}
