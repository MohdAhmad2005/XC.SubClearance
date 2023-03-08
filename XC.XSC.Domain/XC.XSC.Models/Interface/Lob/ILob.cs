using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.Models.Interface.Lob
{
    public interface ILob
    {
        public string Name { get; set; }

        public string LOBID { get; set; }

        public string? Description { get; set; }
    }
}
