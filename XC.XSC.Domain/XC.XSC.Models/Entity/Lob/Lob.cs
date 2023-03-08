using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Models.Interface.Lob;

namespace XC.XSC.Models.Entity.Lob
{
    /// <summary>
    /// model for Lob
    /// </summary>
    public class Lob : BaseEntity<int>, ILob
    {
        /// <summary>
        /// Name of lob.
        /// </summary>
        [Required(ErrorMessage ="Lob name is required.")]
        public string Name { get; set; }

        /// <summary>
        /// Lob id for lob.
        /// </summary>
        [Required(ErrorMessage ="Lob id is required.")]
        [StringLength(50)]
        public string LOBID { get; set; }

        /// <summary>
        /// Lob description for lob.
        /// </summary>
        [Required(ErrorMessage ="Lob description is required.")]
        public string? Description { get; set; }

        public virtual ICollection<Submission.Submission> Submissions { get; set; }

    }
}
