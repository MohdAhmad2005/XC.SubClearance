using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace XC.XSC.Models.Mongo.Entity
{
    public abstract class BaseEntity : IBaseEntity
    {
        /// <summary>
        /// Gets or Sets TenantId
        /// </summary>
        [StringLength(50)]
        public string TenantId { get; set; }

        /// <summary>
        /// Gets or Sets IsActive
        /// </summary>
        [DefaultValue(1)]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or Sets CreatedDate
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or Sets CreatedBy
        /// </summary>
        [StringLength(50)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or Sets ModifiedDate
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or Sets ModifiedBy
        /// </summary>
        [StringLength(50)]
        public string? ModifiedBy { get; set; }
    }
}
