using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.ViewModels.Enum;

namespace XC.XSC.ViewModels.Sla
{
    /// <summary>
    /// 
    /// </summary>
    public class GetSlaConfigurationRequest
    {
        /// <summary>
        /// For Region ID 
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// For Teamd ID 
        /// </summary>
        public int TeamdId { get; set; }

        /// <summary>
        /// For Lob ID 
        /// </summary>
        public int LobId { get; set; }

        /// <summary>
        /// For sla Type 
        /// </summary>
        public SlaType slaType { get; set; }

        /// <summary>
        /// For mail Box Id
        /// </summary>
        public Guid mailBoxId { get; set; }
      

        /// <summary>
        /// Page Size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Page Limt
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Sort Field
        /// </summary>
        public string? SortField { get; set; }

        /// <summary>
        /// Sort Order
        /// </summary>
        public int SortOrder { get; set; }

    }
}
