using System.ComponentModel.DataAnnotations;
using XC.XSC.ViewModels.Enum;

namespace XC.XSC.Models.Interface.ReviewConfiguration
{
    /// <summary>
    /// This is an interface of the review configuration entity.
    /// </summary>
    public interface IReviewConfiguration
    {
        /// <summary>
        /// Region Id.
        /// </summary>
        [Required]
        public int RegionId { get; set; }

        /// <summary>
        /// Team Id.
        /// </summary>
        [Required]
        public int TeamId { get; set; }

        /// <summary>
        /// Lob Id.
        /// </summary>
        [Required]
        public int LobId { get; set; }

        /// <summary>
        /// Processor Id.
        /// </summary>
        [Required]
        public string ProcessorId { get; set; }

        /// <summary>
        /// Review type Id.
        /// </summary>
        [Required]
        public ReviewType ReviewType { get; set; }

        /// <summary>
        /// Reviewer Id.
        /// </summary>
        [Required]
        public string ReviewerId { get; set; }
    }
}
