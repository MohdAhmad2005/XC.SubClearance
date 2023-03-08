using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.Models.Interface.Comment
{
    /// <summary>
    /// This Interface is used to inject the Comment class for further use.
    /// </summary>
    public interface IComment
    {
        /// <summary>
        /// This field is used to store the comment text.
        /// </summary>
        public string CommentText { get; set; }
        /// <summary>
        /// This field is used to used to store the comment type.
        /// </summary>
        public int CommentTypeId { get; set; }
        /// <summary>
        /// This field is used to store the submisson id.
        /// </summary>
        public long SubmissionId { get; set; }
        /// <summary>
        /// This field is used to store the JSON data.
        /// </summary>
        public string? JsonData { get; set; }
    }
}
