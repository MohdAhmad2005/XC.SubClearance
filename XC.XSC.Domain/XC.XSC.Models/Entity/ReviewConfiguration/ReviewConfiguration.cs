using System.ComponentModel.DataAnnotations;
using XC.XSC.Models.Interface.ReviewConfiguration;
using XC.XSC.ViewModels.Enum;

namespace XC.XSC.Models.Entity.ReviewConfiguration
{
    /// <summary>
    /// This class is a review configuration entity
    /// </summary>
    public class ReviewConfiguration : BaseEntity<long>, IReviewConfiguration
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

        /// <summary>
        /// Foreign key relationship with the lob table.
        /// </summary>
        public virtual Lob.Lob Lob { get; set; }
    }
}
