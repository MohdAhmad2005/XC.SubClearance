using XC.XSC.ViewModels.Enum;

namespace XC.XSC.ViewModels.Sla
{
    /// <summary>
    /// This View Model is crate the sla configuration response 
    /// </summary>
    public class SlaConfigurationResponse
    {
        /// <summary>
        ///  Region.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///  Region.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Team   of Sla.
        /// </summary>
        public string Team { get; set; }

        /// <summary>
        /// Lob  of Sla.
        /// 
        public string Lob { get; set; }

        /// <summary>
        ///MailBox  of corresponding selected MailBox
        /// </summary>
        public string MailBox { get; set; }

        /// <summary>
        /// Name of Sla.
        /// </summary>
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
        /// Region Id of Sla.
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// Team Id  of Sla.
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// Lob Id of Sla.
        /// </summary>
        public int LobId { get; set; }

        /// <summary>
        ///MailBoxId  of corresponding selected MailBox
        /// </summary>
        public Guid MailBoxId { get; set; }

        /// <summary>
        /// Updated BY Id of Sla.
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Updated BY Id of Sla.
        /// </summary>
        public string SlaDefinition { get; set; }

        /// <summary>
        /// Is active or not.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Type name.
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Mail box name.
        /// </summary>
        public string MailBoxName { get; set; }
    }
}
