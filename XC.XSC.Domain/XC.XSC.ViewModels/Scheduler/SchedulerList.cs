using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.ViewModels.Enum;
using XC.XSC.ViewModels.Submission;

namespace XC.XSC.ViewModels.Scheduler
{
    public class SchedulerList
    {
        /// <summary>
        /// To get the information of paging
        /// </summary>
        public Paging.Paging Paging { get; set; }

        /// <summary>
        /// To get list schedulers 
        /// </s
        public List<SchedulerResponse> Schedulers { get; set; }
    }
}
