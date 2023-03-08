using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.Models.Interface.Master
{
    public interface IStatus
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SysStatus { get; set; }
        public string SysDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
