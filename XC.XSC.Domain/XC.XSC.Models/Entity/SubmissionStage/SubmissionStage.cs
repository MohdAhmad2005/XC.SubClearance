using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Models.Entity.SubmissionsAllocated;
using XC.XSC.Models.Interface.SubmissionStage;

namespace XC.XSC.Models.Entity.SubmissionStage
{
    /// <summary>
    /// Model of Submission stage.
    /// </summary>
    public class SubmissionStage:BaseEntity<int>,ISubmissionStage
    {
        /// <summary>
        /// Stage name for submission.
        /// </summary>
        [Required(ErrorMessage ="Stage name is required.")]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Color on the basis of submission stage.
        /// </summary>
        [StringLength(50)]
        public string Color { get; set; }

        /// <summary>
        /// Label for submission stage.
        /// </summary>
        [StringLength(50)]
        public string Label { get; set; }

        /// <summary>
        /// Order no. of stage in submission.
        /// </summary>
        public int OrderNo { get; set; }

        public virtual ICollection<Submission.Submission> Submissions { get; set; }
        public virtual ICollection<TaskAuditHistory.TaskAuditHistory> TaskAuditHistory { get; set; }
        public virtual ICollection<SubmissionStatusStageMapping.SubmissionStatusStageMapping> SubmissionStatusStageMappings { get; set; }

    }
}
