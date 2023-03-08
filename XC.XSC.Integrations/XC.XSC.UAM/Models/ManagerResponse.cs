using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.UAM.Models
{
    /// <summary>
    /// Manager response model.
    /// </summary>
    public class ManagerResponse
    {
        /// <summary>
        /// Manager id.
        /// </summary>
        public string ManagerId { get; set; }

        /// <summary>
        /// Manager name.
        /// </summary>
        public object ManagerName { get; set; }
    }
}
