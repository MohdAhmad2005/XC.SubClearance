using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Models.Entity;
using System.ComponentModel.DataAnnotations;
using XC.XSC.Models.Interface.Master;

namespace XC.XSC.Models.Entity.Master
{
    public class Client : BaseEntity<int>, IClient
    {
        [StringLength(50)]
        [Required]
        public string ClientName { get; set; }
        [StringLength(30)]
        public string ClientEmail { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
    }
}
