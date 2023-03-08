using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.UAM.Models
{
    /// <summary>
    /// Region response based on user profile.
    /// </summary>
    public class UserRegionResponse
    {
        /// <summary>
        /// Region id.
        /// </summary>
        public string RegionId { get; set; }

        /// <summary>
        /// Region name.
        /// </summary>
        public string RegionName { get; set; }
    }
}
