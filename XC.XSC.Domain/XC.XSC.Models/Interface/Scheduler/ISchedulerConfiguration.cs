using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.ViewModels.Enum;

namespace XC.XSC.Models.Interface.Scheduler
{
    public interface ISchedulerConfiguration
    {
        /// <summary>
        /// TeamId of corresponding selected team
        /// </summary>
        [Required]
        public int TeamId { get; set; }

        /// <summary>
        /// RegionId of corresponding selected Region
        /// </summary>
        [Required]
        public int RegionId { get; set; }

        /// <summary>
        /// LobId of corresponding selected Lob
        /// </summary>
        [Required]
        public int LobId { get; set; }

        /// <summary>
        /// MailBoxId  of corresponding selected MailBox
        /// </summary>
        [Required]
        public Guid MailBoxId { get; set; }

        /// <summary>
        /// FrequencyType i.e Hours,Minutes  
        /// </summary>
        [Required]
       
        public SchedulerFrequency FrequencyType { get; set; }

        /// <summary>
        /// FrequencyOccurrence according hours and minutes
        /// </summary>
        [Required]        
        public int FrequencyOccurrence { get; set; }

        /// <summary>
        /// TenantId 
        /// </summary>
        [Required]
        [StringLength(50)]
        public string TenantId { get; set; }        
    }
}
