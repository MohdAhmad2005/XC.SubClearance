using AutoMapper;
using XC.XSC.ViewModels.ReviewConfiguration;

namespace XC.XSC.ProfileMapping.ReviewConfiguration
{
    /// <summary>
    /// Review configuration profile mapping.
    /// </summary>
    public class ReviewConfigurationMapper : Profile
    {
        /// <summary>
        /// initialization of  review configuration mapper.
        /// </summary>
        public ReviewConfigurationMapper()
        {
            CreateMap<ReviewConfigurationRequest, Models.Entity.ReviewConfiguration.ReviewConfiguration>().ReverseMap();
        }
    }
}
