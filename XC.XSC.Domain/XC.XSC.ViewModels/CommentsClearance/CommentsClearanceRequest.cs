using System.ComponentModel.DataAnnotations;

namespace XC.XSC.ViewModels.CommentsClearance
{
    /// <summary>
    /// Comments Clearance Request
    /// </summary>
    public class CommentsClearanceRequest
    {
        /// <summary>
        /// Comment Text
        /// </summary>
        /// 
        [Required]
        public string CommentText { get; set; } = string.Empty;

        /// <summary>
        /// SubmissionId
        /// </summary>
        /// 
        [Required(ErrorMessage = "Submission Id is Required")]
        [Range(1, long.MaxValue, ErrorMessage = "Submission Id is Required")]
        public long SubmissionId { get; set; }

        /// <summary>
        /// Sanction screening and the account can be cleared.
        /// </summary>
        [Required]
        public bool ClearanceConscent { get; set; }

    }
}
