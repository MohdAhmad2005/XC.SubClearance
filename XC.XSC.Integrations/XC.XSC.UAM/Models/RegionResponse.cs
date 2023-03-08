using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.UAM.Models
{
    public class Region
    {
        /// <summary>
        /// It is Id propety and primary key for Region Table.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// This propety indicates the name of Adding region.
        /// </summary>
        public string? RegionName { get; set; }

        /// <summary>
        /// Name of the region.
        /// </summary>
        public string Name { get; set; }
    }
    public class RegionResponse
    {
        /// <summary>
        /// Success status for Region listing.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Message of operation on Region listing.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Result of Region listing.
        /// </summary>
        public List<Region> Result { get; set; }
    }
}
