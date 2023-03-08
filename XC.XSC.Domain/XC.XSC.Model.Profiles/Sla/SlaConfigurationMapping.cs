using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Models.Entity.Scheduler;
using XC.XSC.Models.Entity.Sla;
using XC.XSC.ViewModels.Sla;

namespace XC.XSC.ProfileMapping.Sla
{
    public class SlaConfigurationMapping: Profile
    {
        /// <summary>
        /// initialization of  Sla Configuartion Mapper
        /// </summary>
        public SlaConfigurationMapping()
        {
            CreateMap<SlaConfigurationRequest, SlaConfiguration>().ReverseMap();
        }
    }
}
