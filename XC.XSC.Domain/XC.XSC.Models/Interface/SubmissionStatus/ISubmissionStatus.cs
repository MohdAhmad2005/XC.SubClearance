using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.Models.Interface.SubmissionStatus
{
    public interface ISubmissionStatus
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string Label { get; set; }
        public int OrderNo { get; set; }
    }
}
