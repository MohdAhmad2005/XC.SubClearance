using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Models.Entity.Scheduler;
using XC.XSC.ViewModels.Scheduler;


namespace XC.XSC.ProfileMapping.Scheduler
{
    /// <summary>
    /// SchedulerMapper
    /// </summary>
    public class SchedulerMapper : Profile
    {
        /// <summary>
        /// initialization of  Scheduler Mapper
        /// </summary>
        public SchedulerMapper()
        {
            CreateMap<SchedulerRequest,SchedulerConfiguration>().ReverseMap();
        }
    }
}
