using XC.XSC.ViewModels.Enum;

namespace XC.XSC.Models.Interface.Notification
{
    /// <summary>
    /// This model is used to return the Notification table data
    /// </summary>
    public interface INotification
    {
        /// <summary>
        ///User Id of the corresponding Notification
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// This field is used to store the submisson id.
        /// </summary>
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
        public string Subject { get; set; }

        /// <summary>
        /// Description of the corresponding Notification
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// TemplateKey of the corresponding Notification
        /// </summary>
        public string TemplateKey { get; set; }
    }
}
