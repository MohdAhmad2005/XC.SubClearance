using System.ComponentModel.DataAnnotations;
using XC.XSC.Models.Interface.Sla;
using XC.XSC.ViewModels.Enum;

namespace XC.XSC.Models.Entity.Sla
{
    /// <summary>
    /// This class model is regarding the sla configuration
    /// </summary>
    public class SlaConfiguration :BaseEntity<long>, ISlaConfiguration
    {
        /// <summary>
        /// Region Id of Sla.
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
        public Nullable<bool> IsEscalation { get; set; }

        /// <summary>
        /// Mail box name.
        /// </summary>
        public string MailBoxName { get; set; }

        /// <summary>
        /// To get the Lob
        /// </summary>
        public virtual Lob.Lob Lob { get; set; }

    }
}
