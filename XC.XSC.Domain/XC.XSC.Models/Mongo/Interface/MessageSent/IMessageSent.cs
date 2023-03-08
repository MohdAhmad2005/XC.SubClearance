using XC.XSC.ViewModels.Enum;

namespace XC.XSC.Models.Mongo.Interface.MessageSent
{
    public interface IMessageSent
    {
        /// <summary>
        /// This field is used to store the submisson id.
        /// </summary>
        public long SubmissionId { get; set; }

        /// <summary>
        /// Gets or sets MessageType
        /// </summary>
        public MessageType MsgType { get; set; }

        /// <summary>
        /// Gets or Sets ToEmail
        /// </summary>
        public string ToEmail { get; set; }

        /// <summary>
        /// Gets or Sets FromEmail
        /// </summary>
        public string FromEmail { get; set; }

        /// <summary>
        /// Gets or Sets CCEmail
        /// </summary>
        public string CCEmail { get; set; }

        /// <summary>
        /// Gets or Sets BccEmail
        /// </summary>
        public string BccEmail { get; set; }

        /// <summary>
        /// Gets or Sets Subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or Sets Body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or Sets IsSuccess
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or Sets FailureStatus
        /// </summary>
        public string FailureStatus { get; set; }

        /// <summary>
        /// List of Attachments
        /// </summary>
        public List<Attachment> Attachments { get; set; }
    }

    public class Attachment
    {
        /// <summary>
        /// Gets or Sets FileName
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or Sets FilePath
        /// </summary>
        public string FilePath { get; set; }
    }
}
