using System.ComponentModel.DataAnnotations;

namespace XC.XSC.ValidateMail.Models.Request
{
    /// <summary>
    /// Validate Mail Scope Request
    /// </summary>
    public class ValidateMailScopeRequest
    {
        /// <summary>
        /// Camunda TaskId
        /// </summary>
        public string? TaskId { get; set; }

        /// <summary>
        /// SubmissionId from database
        /// </summary>
        [Required(ErrorMessage = "SubmissionId is required.")]
        [Range(1, long.MaxValue)]
        public long SubmissionId { get; set; }

        /// <summary>
        /// Camunda Stage
        /// </summary>
        public string? Stage { get; set; }

        /// <summary>
        /// TenantId
        /// </summary>
        [Required(ErrorMessage = "TenantId is required.")]
        public string TenantId { get; set; }
    }
}
