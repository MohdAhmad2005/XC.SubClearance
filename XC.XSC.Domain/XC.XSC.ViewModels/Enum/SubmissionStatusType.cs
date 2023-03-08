using System.ComponentModel.DataAnnotations;

namespace XC.XSC.ViewModels.Enum
{
    /// <summary>
    /// enum for submisison status type.
    /// </summary>
    public enum SubmissionStatusType
    {
        /// <summary>
        /// not assigned yet submission status.
        /// </summary>
        [Display(Name = "Not Assigned Yet")]
        NotAssignedYet = 1,

        /// <summary>
        /// not started submission status.
        /// </summary>
        [Display(Name = "Not Started")]
        NotStarted = 2,

        /// <summary>
        /// in progress play submision status.
        /// </summary>
        [Display(Name = "In Progress (Play)")]
        InProgressPlay = 3,

        /// <summary>
        /// in progress paused submision status.
        /// </summary>
        [Display(Name = "In Progress (Paused)")]
        InProgressPaused = 4,

        /// <summary>
        /// under query submission status.
        /// </summary>
        [Display(Name = "Under Query")]
        UnderQuery = 5,

        /// <summary>
        /// review pending submission status.
        /// </summary>
        [Display(Name = "Review Pending")]
        ReviewPending = 6,

        /// <summary>
        /// under review play submission status.
        /// </summary>
        [Display(Name = "Under Review (Play)")]
        UnderReviewPlay = 7,

        /// <summary>
        /// under review paused submission status.
        /// </summary>
        [Display(Name = "Under Review (Paused)")]
        UnderReviewPaused = 8,

        /// <summary>
        /// review fail submission status.
        /// </summary>
        [Display(Name = "Review Fail")]
        ReviewFail = 9,

        /// <summary>
        /// review fail submission status.
        /// </summary>
        [Display(Name = "Review Pass")]
        ReviewPass = 10,

        /// <summary>
        /// submitted to pass submisson status.
        /// </summary>
        [Display(Name = "Submitted to PAS")]
        SubmittedtoPAS = 11,
    }
}
