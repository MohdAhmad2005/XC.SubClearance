using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XC.XSC.Models.Interface.SubmissionClearance;

namespace XC.XSC.Models.Entity.SubmissionClearance
{
    /// <summary>
    /// This model is used to return the SubmissionClearance table data
    /// </summary>
    public class SubmissionClearance : BaseEntity<Guid>, ISubmissionClearance
    {
        /// <summary>
        /// This field is used to store the submisson id.
        /// </summary>
        [ForeignKey(nameof(SubmissionId))]
        public long SubmissionId { get; set; }

        /// <summary>
        /// Rule name of the corresponding submission
        /// </summary>
        [Required]
        [StringLength(250)]
        public string RuleName { get; set; }

        /// <summary>
        /// Rule status of the corresponding submission
        /// </summary>
        [Required]
        public bool RuleStatus { get; set; }

        /// <summary>
        /// Description the corresponding submission
        /// </summary>
        [StringLength(500)]
        public string Description { get; set; }

        /// <summary>
        /// Remark of corresponding submission
        /// </summary>
        [StringLength(500)]
        public string Remark { get; set; }

        /// <summary>
        /// ContextId of the corresponding submission
        /// </summary>
        [Required]
        public int ContextId { get; set; }

        /// <summary>
        /// EntityId of the corresponding submission
        /// </summary>
        [Required]
        public int EntityId { get; set; }
        public virtual Submission.Submission Submission { get; set; }
    }
}
