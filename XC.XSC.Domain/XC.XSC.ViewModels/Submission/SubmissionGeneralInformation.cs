using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.Submission
{

    /// <summary>
    /// Response class for  submission general information.
    /// </summary>
    public class SubmissionGeneralInformation
    {
        /// <summary>
        /// ReviewInformation property of type ReviewInformation class.
        /// </summary>
        public ReviewInformation ReviewInformation { get; set; }

        /// <summary>
        /// PASInformation property of type PASInformation class.
        /// </summary>
        public PASInformation PASInformation { get; set; }
    }

    /// <summary>
    /// ReviewInformation class.
    /// </summary>
    public class ReviewInformation
    {
        /// <summary>
        /// ProcessorName property for ReviewInformation.
        /// </summary>
        public string ProcessorName { get; set; }

        /// <summary>
        /// ReceivedDate property for ReviewInformation.
        /// </summary>

        public DateTime ReceivedDate { get; set; }

        /// <summary>
        /// DueDate property for ReviewInformation.
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// ReviewerName property for ReviewInformation.
        /// </summary>
        public string ReviewerName { get; set; }

        /// <summary>
        /// ReviewSubmitDate property for ReviewInformation.
        /// </summary>
        public DateTime? ReviewSubmitDate { get; set; }

        /// <summary>
        /// ReviewApprovalDate property for ReviewInformation.
        /// </summary>
        public DateTime? ReviewApprovalDate { get; set; }

        /// <summary>
        /// ReviewStatus property for ReviewInformation.
        /// </summary>
        public int ReviewStatus { get; set; }

        /// <summary>
        /// Comments property for ReviewInformation of type Comment.
        /// </summary>
        public List<Comment> Comments { get; set; }

    }

    /// <summary>
    /// ReviewInformation class.
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Id property for Comment.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// CommentType property for Comment.
        /// </summary>
        public int CommentType { get; set; }

        /// <summary>
        /// CommentDate property for Comment.
        /// </summary>
        public DateTime CommentDate { get; set; }

        /// <summary>
        /// Description property for Comment.
        /// </summary>
        public string Description { get; set; } = String.Empty;

        /// <summary>
        /// CommentBy property for Comment.
        /// </summary>
        public string CommentBy { get; set; } = String.Empty;
    }
    public class PASInformation
    {
        /// <summary>
        /// SubmissionId property for PASInformation.
        /// </summary>
        public long SubmissionId { get; set; }

        /// <summary>
        /// SubmitToPASDate property for PASInformation.
        /// </summary>
        public DateTime SubmitToPASDate { get; set; }

        /// <summary>
        /// Status property for PASInformation.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// ErrorMessage property for PASInformation.
        /// </summary>
        public string ErrorMessage { get; set; } = String.Empty;
    }
}
