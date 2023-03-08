using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XC.XSC.Models.Interface.SubmissionAuditLog;

namespace XC.XSC.Models.Entity.SubmissionAuditLog
{
    public class SubmissionAuditLog : BaseEntity<long>, ISubmissionAuditLog
    {
        /// <summary>
        /// This field is used for SubmissionId.
        /// </summary>
        [Required]
        [ForeignKey(nameof(SubmissionId))]
        public long SubmissionId { get; set; }

        /// <summary>
        /// This field is used for Previous Status Id.
        /// </summary>
        [Required]
        [ForeignKey(nameof(PrevStatus))]
        public int PrevStatus { get; set; }

        /// <summary>
        /// This field is used for New Status Id.
        /// </summary>
        [Required]
        [ForeignKey(nameof(NewStatus))]
        public int NewStatus { get; set; }

        /// <summary>
        /// This field is used for User Previous Assigned to user ID .
        /// </summary>
        [Required]
        public string PrevAssignedToId { get; set; }

        /// <summary>
        /// This field is used for User New Assigned to user ID .
        /// </summary>
        [Required]
        public string NewAssignedToId { get; set; }
    }
}
