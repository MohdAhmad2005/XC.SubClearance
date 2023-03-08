using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.ViewModels.Enum;

namespace XC.XSC.ViewModels.Scheduler
{
    public class SchedulerResponse
    {
        /// <summary>
        /// Id as Scheduler Configuration Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Team Name
        /// </summary>

        public string Team { get; set; }

        /// <summary>
        /// Region Name
        /// </summary>

        public string Region { get; set; }

        /// <summary>
        /// Lob Name
        /// </summary>

        public string Lob { get; set; }

        /// <summary>
        /// MailBox Name
        /// </summary>

        public string MailBox { get; set; }

        /// <summary>
        /// FrequencyType i.e Hours,Minutes  
        /// </summary>

        public string FrequencyType { get; set; }

        /// <summary>
        /// FrequencyOccurrence according hours and minutes
        /// </summary>

        public int FrequencyOccurrence { get; set; }
    }
}
