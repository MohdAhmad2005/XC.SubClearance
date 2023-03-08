using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Models.Entity;
using XC.XSC.Models.Interface.Master;

namespace XC.XSC.Models.Entity.Master
{
    public class Status : BaseEntity<int>, IStatus
    {
        [StringLength(50)]
        [Required]
        public string Name { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        [StringLength(50)]
        public string SysStatus { get; set; }
        [StringLength(50)]
        public string SysDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
