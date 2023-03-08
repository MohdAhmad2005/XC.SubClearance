using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.SubmissionAllocation
{
    /// <summary>
    /// Request model to allocate the submission.
    /// </summary>
    public class AddSubmissionAllocatedRequest
    {
        /// <summary>
        /// Submission Id to allocate.
        /// </summary>
        [Required(ErrorMessage ="Submission id is required.")]
        public long SubmissionId { get; set; }

        /// <summary>
        /// User to whom submission will be allocated.
        /// </summary>
        [Required (ErrorMessage ="Allocated to is required.")]
        [StringLength(50)]
        public string AllocatedTo { get; set; }
    }
}
