using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XC.XSC.ViewModels.Scheduler
{
    public class GetSchedulerRequest
    {
        /// <summary>
        /// to get the paging information
        /// </summary>
        public Paging.Paging Paging { get; set; }
    }
}
