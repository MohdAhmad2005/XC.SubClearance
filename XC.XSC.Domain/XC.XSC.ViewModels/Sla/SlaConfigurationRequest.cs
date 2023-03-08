using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.ViewModels.Enum;

namespace XC.XSC.ViewModels.Sla
{
    /// <summary>
    /// This View model class regarding the Sla Configuration request
    /// </summary>
    public class SlaConfigurationRequest
    {
        /// <summary>
        /// Id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// This is regarding the Team Id 
        /// </summary>
        [Required]
        public int RegionId { get; set; }


        /// <summary>
        /// Team Id  of Sla.
        /// </summary>
        [Required]
        public int TeamId { get; set; }

        /// <summary>
        /// Lob Id of Sla.
        /// </summary>
        [Required]
        public int LobId { get; set; }

        /// <summary>
        ///MailBoxId  of corresponding selected MailBox
        /// </summary>
        [Required]
        public Guid MailBoxId { get; set; }

        /// <summary>
        /// Name of Sla.
        /// </summary>
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Sla Type
        /// </summary>
        public SlaType Type { get; set; }

        public int TypeId { get; set; }

        /// <summary>
        /// Sla Defination in  Day
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// Sla Defination in Hours
        /// </summary>
        public int Hours { get; set; }

        /// <summary>
        /// Sla Defination in Min
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// Sla Defination in Percentage
        /// </summary>
        public decimal Percentage { get; set; }

        /// <summary>
        /// Sla Defination in SamplePercentage
        /// </summary>
        public int SamplePercentage { get; set; }

        /// <summary>
        /// TaskType of Sla
        /// </summary>
        public TaskType TaskType { get; set; }

        /// <summary>
        /// To check whether the sla is Escalation
        /// </summary>
        public bool IsEscalation { get; set; }

        public bool IsActive { get; set; }
    }
}
