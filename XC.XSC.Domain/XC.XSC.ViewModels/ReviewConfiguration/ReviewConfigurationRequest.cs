using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.ViewModels.Enum;

namespace XC.XSC.ViewModels.ReviewConfiguration
{
    /// <summary>
    /// Review configuration request model.
    /// </summary>
    public class ReviewConfigurationRequest
    {
        /// <summary>
        /// Review configuration id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Region id of corresponding selected region.
        /// </summary>
        [Required(ErrorMessage = "Region Id is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Region Id is Required")]
        public int RegionId { get; set; }

        /// <summary>
        /// Team id of corresponding selected team.
        /// </summary>
        [Required(ErrorMessage = "Team Id is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Team Id is Required")]
        public int TeamId { get; set; }

        /// <summary>
        /// Lob id of corresponding selected lob.
        /// </summary>
        [Required(ErrorMessage = "Lob Id is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Lob Id is Required")]
        public int LobId { get; set; }

        /// <summary>
        /// List of processor id of selected processor .
        /// </summary>
        [Required(ErrorMessage = "Procesor Id is Required")]
        public List<string> ProcessorId { get; set; }

        /// <summary>
        /// Review type id of selected review type.
        /// </summary>
        [Required(ErrorMessage = "Review type is Required")]
        public ReviewType ReviewType { get; set; }

        /// <summary>
        /// Reviewer id of selected reviewer
        /// </summary>
        [Required(ErrorMessage = "Reviewer Id is Required")]
        public string ReviewerId { get; set; }

        /// <summary>
        /// Is active or not.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
