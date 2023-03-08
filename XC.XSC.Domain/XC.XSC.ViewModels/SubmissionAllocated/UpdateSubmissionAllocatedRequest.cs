using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.SubmissionAllocation
{
    /// <summary>
    /// model to update submission allocation request.
    /// </summary>
    public class UpdateSubmissionAllocatedRequest
    {
        /// <summary>
        /// id from submission allocation table.
        /// </summary>
        [Required(ErrorMessage ="Id of submission allocation is required. ")]
        public long Id { get; set; }

        /// <summary>
        /// Submission id for allocation.
        /// </summary>
        [Required(ErrorMessage ="Subbmission id is required.")]
        public long SubmissionId { get; set; }

        /// <summary>
        /// User to whom submission is allocated
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage ="Allocated to is required.")]
        public string AllocatedTo { get; set; }

        /// <summary>
        /// Active status of Submission allocation record.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
