using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XC.XSC.Models.Interface.Notification;
using XC.XSC.ViewModels.Enum;

namespace XC.XSC.Models.Entity.Notification
{
    /// <summary>
    /// Model for Notification.
    /// </summary>
    public class Notification : BaseEntity<long>, INotification
    {
        /// <summary>
        ///User Id of the corresponding Notification
        /// </summary>
        [StringLength(100)]
        [Required(ErrorMessage = "User id required.")]
        public string UserId { get; set; }

        /// <summary>
        /// This field is used to store the submisson id.
        /// </summary>
        [ForeignKey(nameof(SubmissionId))]
        public long SubmissionId { get; set; }

        /// <summary>
        /// Gets or sets MessageType
        /// </summary>
        public MessageType MsgType { get; set; }

        /// <summary>
        /// To check whether the Notification is in read
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// Subject of the corresponding Notification
        /// </summary>
        [Required(ErrorMessage = "User id required.")]
        [StringLength(500)]
        public string Subject { get; set; }

        /// <summary>
        /// Description of the corresponding Notification
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets TemplateKey
        /// </summary>
        [StringLength(100)]
        public string TemplateKey { get; set; }

        /// <summary>
        /// Used for Submission data
        /// </summary>
        public virtual Submission.Submission Submission { get; set; }

    }
}
