using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Models.Interface.TaskAuditHistory;

namespace XC.XSC.Models.Entity.TaskAuditHistory
{
    public class TaskAuditHistory : BaseEntity<long>, ITaskAuditHistory
    {
        /// <summary>
        /// This field is used for SubmissionId.
        /// </summary>
        [Required]
        [ForeignKey(nameof(SubmissionId))]

        /// <summary>
        /// This field is used for Submission Id.
        /// </summary>
        public long SubmissionId { get; set; }

        /// <summary>
        /// This field is used for Status Id.
        /// </summary>
        [ForeignKey(nameof(SubmissionStatusId))]
        public int SubmissionStatusId { get; set; }


        /// <summary>
        /// This field is used for Stage Id.
        /// </summary>
         [ForeignKey(nameof(SubmissionStageId))]
        public int SubmissionStageId { get; set; }

        /// <summary>
        /// This field is used for Acrchived.
        /// </summary>
        public bool IsAcrchieved { get; set; }

        [StringLength(500)]
        /// <summary>
        /// This field is used for Collection of Stage Data.
        /// </summary>
        public string StageData { get; set; }

        [StringLength(50)]
        /// <summary>
        /// This field is used for Start time of stage or status.
        /// </summary>
        public DateTime StartTime { get; set; } = DateTime.Now;

        /// <summary>
        /// This field is used for End Time  time of stage or status.
        /// </summary>
        public DateTime EndTime { get; set; } = DateTime.Now;

        /// <summary>
        /// This field is used for User ID .
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// This field is used for Submission  .
        /// </summary>
        public virtual Submission.Submission Submission { get; set; }

        /// <summary>
        /// This field is used for Submission  Status.
        /// </summary>
        public virtual SubmissionStatus.SubmissionStatus SubmissionStatus{ get; set; }

        /// <summary>
        /// This field is used for Submission Stage.
        /// </summary>
        public virtual SubmissionStage.SubmissionStage SubmissionStage { get; set; }

    }
}
