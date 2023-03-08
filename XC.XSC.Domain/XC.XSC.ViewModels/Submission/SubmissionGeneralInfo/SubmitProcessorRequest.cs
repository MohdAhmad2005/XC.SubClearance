using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.Submission.SubmissionGeneralInfo
{
    public class SubmitProcessorRequest
    {
        /// <summary>
        /// Comment from User
        /// </summary>
        public string CommentText { get; set; }

        /// <summary>
        /// Comment Type Id of Comment.
        /// </summary>
        public string CommentType { get; set; }

        /// <summary>
        /// Id of the submission
        /// </summary>
        public long SubmissionId { get; set; }

        /// <summary>
        /// Json Data in Comment.
        /// </summary>
        public string? JsonData { get; set; }
    }
}
