using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Models.Entity;
using XC.XSC.Models.Entity.SubmissionStatus;
using XC.XSC.Models.Interface.Master;

namespace XC.XSC.Models.Entity.Master
{
    public class Case : BaseEntity<long>, ICase
    {
        [StringLength(20)]
        [Required]
        public string CaseNumber { get; set; }
        [StringLength(30)]
        public string InusranceNo { get; set; }
        [StringLength(30)]
        public string BrokerName { get; set; }
        [ForeignKey(nameof(SubmissionStatusId))]
        public int SubmissionStatusId { get; set; }
        public DateTime ReceivedDate { get; set; }
        public DateTime DueDate { get; set; }
        [StringLength(50)]
        public string AssignedTo { get; set; }
        public virtual SubmissionStatus.SubmissionStatus SubmissionStatus { get; set; }
    }
}
