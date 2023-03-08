
using AutoMapper;
using XC.XSC.Models.Profiles.SubmissionStage;
using XC.XSC.Models.Profiles.SubmissionStatus;
using XC.XSC.ProfileMapping.ReviewConfiguration;
using XC.XSC.ProfileMapping.Sla;
using XC.XSC.ProfileMapping.Scheduler;

namespace XC.XSC.API.Extentions 
{
    /// <summary>
    /// Set AutoMapper profiles
    /// </summary>
    public static class AutoMapperExtensions
    {
        /// <summary>
        /// Add All mapper profiles of models
        /// </summary>
        /// <param name="services"></param>
        public static void AddAutoMapperProfiles(this IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new SubmissionStageMapper());
                mc.AddProfile(new SubmissionStatusMapper());
                mc.AddProfile(new SchedulerMapper());
                mc.AddProfile(new ReviewConfigurationMapper());
                mc.AddProfile(new SlaConfigurationMapping());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

        }
    }
}