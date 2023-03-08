using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.ViewModels.SubmissionStatus;

namespace XC.XSC.Models.Profiles.SubmissionStatus
{
    public class SubmissionStatusMapper : Profile
    {
        public SubmissionStatusMapper()
        {
            CreateMap<Entity.SubmissionStatus.SubmissionStatus, SubmissionStatusResponse>().ReverseMap();
            CreateMap<AddSubmissionStatusRequest, Entity.SubmissionStatus.SubmissionStatus>().ReverseMap();
            CreateMap<UpdateSubmissionStatusRequest, Entity.SubmissionStatus.SubmissionStatus>();
        }
    }
}
