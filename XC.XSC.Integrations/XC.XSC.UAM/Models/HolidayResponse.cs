namespace XC.XSC.UAM.Models
{
    public class HolidayResponse
    {
        /// <summary>
        /// Success status for Region listing.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Message of operation on Region listing.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Result of Region listing.
        /// </summary>
        public List<Holiday> Result { get; set; }
    }

    /// <summary>
    /// This class is used to keep the information about the holiday
    /// </summary>
    public class Holiday
    {
        /// <summary>
        /// This property is used to store the name of the holiday
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// This property is used to store the description of the holiday
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// This property is used to store the date of the holiday
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// This property is used to store the id of the holiday list
        /// </summary>
        public long HolidayListId { get; set; }

        public long Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string TenantId { get; set; } = string.Empty;

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual HolidayList HolidayList { get; set; }
    }

    /// <summary>
    /// This class is used to store the information about holiday list
    /// </summary>
    public class HolidayList 
    {
        /// <summary>
        /// This property is defiened to store the name of the holiday
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// This property is defiened to store the description of the holiday
        /// </summary>
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Holiday> Holiday { get; set; }
        public virtual ICollection<Team> Teams { get; set; }

    }

    /// <summary>
    /// Model of team.
    /// </summary>
    public class Team 
    {
        /// <summary>
        /// Name of team.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// EffectivefFrom date of team.
        /// </summary>
        public DateTime EffectiveFromDate { get; set; }

        /// <summary>
        /// Effective to date of team.
        /// </summary>
        public DateTime EffectiveToDate { get; set; }

        /// <summary>
        /// Holiday list id to link with team.
        /// </summary>
        public long HolidayListId { get; set; }
        public virtual HolidayList HolidayList { get; set; }
    }
}
