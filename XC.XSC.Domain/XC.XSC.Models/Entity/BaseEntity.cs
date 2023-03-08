using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace XC.XSC.Models.Entity
{
    public abstract class BaseEntity<T>: IBaseEntity<T>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
        [StringLength(50)]
        public string TenantId { get; set; }
        public DateTime CreatedDate { get; set; }=DateTime.Now;
        [StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [StringLength(50)]
        public string? ModifiedBy { get; set; }
        [DefaultValue(1)]
        public bool IsActive { get; set; }
    }
}
