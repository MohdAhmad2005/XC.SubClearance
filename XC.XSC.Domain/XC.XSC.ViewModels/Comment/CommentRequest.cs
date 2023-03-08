using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.Comment
{
    public class CommentRequest
    {
        /// <summary>
        /// CommentType  Remark = 1, Query = 2, Review = 3, OutOfScope = 4
        /// </summary>
        [Required]
        public string CommentType { get; set; }=string.Empty;
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
        [Required(ErrorMessage ="Submission Id is Required")]
        [Range(1, long.MaxValue, ErrorMessage = "Submission Id is Required")]
        public long SubmissionId { get; set; }
        /// <summary>
        /// JsonData
        /// </summary>
        public string? JsonData { get; set; } = string.Empty;       

    }
}
