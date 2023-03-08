using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.Models.Interface.Master
{
    public interface ICase
    {
        public string CaseNumber { get; set; }
        public string InusranceNo { get; set; }
        public string BrokerName { get; set; }
        public int SubmissionStatusId { get; set; }
        public DateTime ReceivedDate { get; set; }
        public DateTime DueDate { get; set; }
        public string AssignedTo { get; set; }

    }
}
