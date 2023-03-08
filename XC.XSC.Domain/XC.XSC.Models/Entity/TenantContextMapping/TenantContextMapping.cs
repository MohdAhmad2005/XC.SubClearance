using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Models.Interface.TenantContextMapping;

namespace XC.XSC.Models.Entity.TenantContextMapping
{
    /// <summary>
    /// This model is used to return the TenantContextMappings table data
    /// </summary>
    public class TenantContextMapping: BaseEntity<int>, ITenantContextMapping
    {
        /// <summary>
        /// Region of the corresponding TenantContextMapping
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Lob of the corresponding TenantContextMapping
        /// </summary>
        public string Lob { get; set; }

        /// <summary>
        /// ContextId of the corresponding TenantContextMapping
        /// </summary>
        [Required]
        public int ContextId { get; set; }

        /// <summary>
        /// EntityId of the corresponding TenantContextMapping
        /// </summary>
        [Required]
        public int EntityId { get; set; }
    }
}
