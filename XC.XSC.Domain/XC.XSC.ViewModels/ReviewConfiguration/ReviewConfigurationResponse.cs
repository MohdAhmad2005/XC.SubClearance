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
    /// Review configuration response.
    /// </summary>
    public class ReviewConfigurationResponse
    {
        /// <summary>
        /// Review configuration id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Region id of corresponding selected region.
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// Region name.
        /// </summary>
       public string RegionName { get; set; }

        /// <summary>
        /// Team id.
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// Team name.
        /// </summary>
        public string TeamName { get; set; }

        /// <summary>
        /// Lob id.
        /// </summary>
        public int LobId { get; set; }

        /// <summary>
        /// Lob name.
        /// </summary>
        public string LobName { get; set; }

        /// <summary>
        /// List of processor id of selected processor .
        /// </summary>
        public string ProcessorId { get; set; }

        /// <summary>
        /// Processor name.
        /// </summary>
        public string ProcessorName { get; set; }

        /// <summary>
        /// Review type.
        /// </summary>
        public string? ReviewType { get; set; }

        /// <summary>
        /// Review type id.
        /// </summary>
        public int ReviewTypeId { get; set; }

        /// <summary>
        /// Reviewer id.
        /// </summary>
        public string ReviewerId { get; set; }

        /// <summary>
        /// Reviewer name.
        /// </summary>
        public string ReviewerName { get; set; }

        /// <summary>
        /// Is active or not.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
