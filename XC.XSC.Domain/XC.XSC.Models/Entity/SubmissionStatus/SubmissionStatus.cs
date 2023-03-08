using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Models.Entity.SubmissionStatusStageMapping;
using XC.XSC.Models.Interface.SubmissionStatus;

namespace XC.XSC.Models.Entity.SubmissionStatus
{
    /// <summary>
    /// Model for Submission status.
    /// </summary>
    public class SubmissionStatus : BaseEntity<int>, ISubmissionStatus
    {
        /// <summary>
        /// Status name of submission.
        /// </summary>
        [Required(ErrorMessage ="Status name is required.")]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Status color according to stage.
        /// </summary>
        [StringLength(50)]
        public string Color { get; set; }

        /// <summary>
        /// Label for submission.
        /// </summary>
        [StringLength(50)]
        public string Label { get; set; }

        /// <summary>
        /// Order no. of status to display.
        /// </summary>
        public int OrderNo { get; set; }

        public virtual ICollection<Submission.Submission> Submissions { get; set; }
        public virtual ICollection<SubmissionStatusStageMapping.SubmissionStatusStageMapping> SubmissionStatusStageMappings { get; set; }
        public virtual ICollection<TaskAuditHistory.TaskAuditHistory> TaskAuditHistory { get; set; }


    }
}
