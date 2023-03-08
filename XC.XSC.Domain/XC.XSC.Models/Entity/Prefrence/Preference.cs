using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Models.Interface.Prefrence;

namespace XC.XSC.Models.Entity.Prefrence
{
    public class Preference : BaseEntity<long>, IPreference
    {
        [StringLength(50)]
        public string UserId { get; set ; }
        
        [StringLength(20)]
        public string Key { get; set; }
        
        public string Value { get; set; }
        
        [StringLength(150)]
        public string Description { get; set; }
    }
}
