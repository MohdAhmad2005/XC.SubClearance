using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.Models.Interface.SubmissionsAllocated
{
    public interface ISubmissionAllocated
    {
        public long SubmissionId { get; set; }
        public string AllocatedTo { get; set; }
        public DateTime AllocatedDate { get; set; }
        public string AllocatedBy { get; set; }
    }
}
