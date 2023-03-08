using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.SubmissionAllocation
{
    public class SubmissionAllocatedResponse
    {
        public long Id { get; set; }
        public long SubmissionId { get; set; }
        [StringLength(50)]
        public string AllocatedTo { get; set; }
        public DateTime AllocatedDate { get; set; }
        [StringLength(50)]
        public string AllocatedBy { get; set; }
        [StringLength(50)]
        public string TenantId { get; set; }
        public DateTime CreatedDate { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [StringLength(50)]
        public string? ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
