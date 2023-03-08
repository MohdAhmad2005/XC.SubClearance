using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Models.Interface.Comment;

namespace XC.XSC.Models.Entity.Comment
{
    /// <summary>
    /// This model is used to define the comment.
    /// </summary>
    public class Comment : BaseEntity<long>, IComment
    {
        /// <summary>
        /// This field is used to store the comment text.
        /// </summary>
        [Required]
        [StringLength(500)]
        public string CommentText { get; set; } = string.Empty; 
         /// <summary>
         /// This field is used to used to store the comment type.
         /// </summary>
        public int CommentTypeId { get; set; }
        /// <summary>
        /// This field is used to store the submisson id.
        /// </summary>
        [ForeignKey(nameof(SubmissionId))]
        public long SubmissionId { get; set; }
        /// <summary>
        /// This field is used to store the JSON data.
        /// </summary>
        [StringLength(5000)]
        public string? JsonData { get; set; }=string.Empty;
        public virtual Submission.Submission Submission { get; set; }
    }
}
