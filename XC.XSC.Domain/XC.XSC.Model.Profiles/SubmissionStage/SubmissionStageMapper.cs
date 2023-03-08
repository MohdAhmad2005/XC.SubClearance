using AutoMapper;
using XC.XSC.ViewModels.SubmissionStage;

namespace XC.XSC.Models.Profiles.SubmissionStage
{
    public class SubmissionStageMapper : Profile
    {
        public SubmissionStageMapper()
        {   
            CreateMap<XC.XSC.Models.Entity.SubmissionStage.SubmissionStage, SubmissionStageResponse>().ReverseMap();
            CreateMap<AddSubmissionStageRequest, Entity.SubmissionStage.SubmissionStage>().ReverseMap();
            CreateMap<UpdateSubmissionStageRequest, Entity.SubmissionStage.SubmissionStage>();
        }        
    }
}
