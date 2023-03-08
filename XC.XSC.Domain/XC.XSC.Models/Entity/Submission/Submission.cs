using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using XC.XSC.Models.Entity.Comment;
using XC.XSC.Models.Entity.SubmissionsAllocated;
using XC.XSC.Models.Entity.TaskAuditHistory;
using XC.XSC.Models.Interface.Submission;

namespace XC.XSC.Models.Entity.Submission
{
    /// <summary>
    /// Model for Submission.
    /// </summary>
    [Index(nameof(CaseId), IsUnique = true)]
    public class Submission : BaseEntity<long>, ISubmission
    {
        /// <summary>
        /// Case id of submission.
        /// </summary>
        [Required(ErrorMessage ="Case id required.")]
        [StringLength(25)]
        public string CaseId { get; set; }

        /// <summary>
        /// Broaker name of submission.
        /// </summary>
        [StringLength(100)]
        public string BrokerName { get; set; }

        /// <summary>
        /// Insured name of submission.
        /// </summary>
        [StringLength(100)]
        public string InsuredName { get; set; }

        /// <summary>
        /// Email info id of submission.
        /// </summary>
        [ForeignKey(nameof(EmailInfoId))]
        public long EmailInfoId { get; set; }

        /// <summary>
        /// Due date of submission.
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Assigned id of submission.
        /// </summary>
        [StringLength(100)]
        public string? AssignedId { get; set; }

        /// <summary>
        /// Submission status id of submission.
        /// </summary>
        [ForeignKey(nameof(SubmissionStatusId))]
        public int SubmissionStatusId { get; set; }

        /// <summary>
        /// Submission stage id of submission.
        /// </summary>
        [ForeignKey(nameof(SubmissionStageId))]
        public int SubmissionStageId { get; set; }

        /// <summary>
        /// Is in scope or out scope of submission.
        /// </summary>
        public bool IsInScope { get; set; }

        /// <summary>
        /// Lob id of submission.
        /// </summary>
        [ForeignKey(nameof(LobId))]
        public int LobId { get; set; }

        /// <summary>
        /// Extended Date of Submission.
        /// </summary>
        public DateTime ExtendedDate { get; set; }

        /// <summary>
        /// Email body of submission.
        /// </summary>
        [StringLength(5000)]
        public string EmailBody { get; set; }

        /// <summary>
        /// TAsk id of submission.
        /// </summary>
        [StringLength(50)]
        public string TaskId { get; set; }

        /// <summary>
        /// To check whether the submission is in ClearanceConscent
        /// </summary>
        public bool ClearanceConscent { get; set; }

        /// <summary>
        /// User TeamId
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// User RegionId
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// Gets or Sets the CompletionDate
        /// </summary>
        public DateTime? CompletionDate { get; set; }

        /// <summary>
        /// Gets or Sets the DataReferenceId
        /// </summary>
        public Guid? DataReferenceId { get; set; }

        /// <summary>
        /// Reviewer id field.
        /// </summary>
        public string? ReviewerId { get; set; }

        /// <summary>
        /// Is data completed.
        /// </summary>
        public bool isDataCompleted { get; set; }

        public virtual EmailInfo.EmailInfo EmailInfo { get; set; }
        public virtual SubmissionStatus.SubmissionStatus SubmissionStatus { get; set; }
        public virtual SubmissionStage.SubmissionStage SubmissionStage { get; set; }
        public virtual Lob.Lob Lob { get; set; }
        public virtual ICollection<TaskAuditHistory.TaskAuditHistory> TaskAuditHistory { get; set; }
        public virtual ICollection<Comment.Comment> Comment { get; set; }
        public virtual ICollection<SubmissionAllocated> SubmissionAllocated { get; set; }
    }
}
