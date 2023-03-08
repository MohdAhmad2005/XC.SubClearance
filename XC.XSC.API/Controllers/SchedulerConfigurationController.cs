using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using XC.XSC.Models.Entity.Submission;
using XC.XSC.Service.Scheduler;
using XC.XSC.Service.User;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.Scheduler;
using XC.XSC.ViewModels.Submission;

namespace XC.XSC.API.Controllers
{
    /// <summary>
    /// Scheduler Configuration API Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerConfigurationController : ControllerBase
    {
        private readonly IUserContext _userContext;
        private readonly ISchedulerConfigurationService _schedulerConfigurationService;
        private readonly IResponse _response;

        /// <summary>
        /// SchedulerConfigurationController initialization
        /// </summary>
        /// <param name="schedulerConfigurationService">scheduler Configuration Service injected into constructor for used service method </param>
        /// <param name="userContext">userContext used for used detail</param>
        /// <param name="response"> Response Model</param>
        public SchedulerConfigurationController(ISchedulerConfigurationService schedulerConfigurationService, IUserContext userContext, IResponse response)
        {
            this._schedulerConfigurationService = schedulerConfigurationService;
            this._userContext = userContext;
            this._response = response;
        }

        /// <summary>
        /// Get All Scheduler list
        /// </summary>
        /// <param name="getSchedulerRequest">pagesize, limit sorted field, sorting order </param>
        /// <returns></returns>
        [HttpGet]
        [Route("getAllSchedulers")]
        public async Task<IResponse> GetAllSchedulers([FromQuery] GetSchedulerRequest getSchedulerRequest)
        {
            return await _schedulerConfigurationService.GetAllSchedulers(getSchedulerRequest, default(CancellationToken));
        }
        /// <summary>
        /// Get Scheduler based on schedulerId
        /// </summary>
        /// <param name="schedulerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getScheduler/{schedulerId}")]
        public async Task<IResponse> GetScheduler(int schedulerId)
        {
            return await _schedulerConfigurationService.GetScheduler(schedulerId);
        }



        /// <summary>
        /// Save Scheduler Configuration detail based on Scheduler fire the job
        /// </summary>
        /// <param>
        /// TeamId,
        /// RegionId,
        /// LobId,
        /// MailBoxId,
        /// FrequencyType as (HR,MIN)
        /// FrequencyOccurrence as 0,1
        /// </param>
        /// <returns>Success</returns>
        [HttpPost]
        [Route("saveSchedulerConfiguration")]
        public async Task<IResponse> SaveSchedulerConfiguration([FromBody] SchedulerRequest schedulerDetail)
        {
            return await _schedulerConfigurationService.SaveSchedulerConfiguration(schedulerDetail);
        }


        /// <summary>
        /// Save Scheduler Configuration detail based on Scheduler fire the job
        /// </summary>
        /// <param>
        /// TeamId,
        /// RegionId,
        /// LobId,
        /// MailBoxId,
        /// FrequencyType as (HR,MIN)
        /// FrequencyOccurrence as 0,1
        /// </param>
        /// <returns>Success</returns>
        [HttpPost]
        [Route("updateSchedulerConfiguration")]
        public async Task<IResponse> UpdateSchedulerConfiguration([FromBody] SchedulerRequest schedulerDetail)
        {
            return await _schedulerConfigurationService.UpdateSchedulerConfiguration(schedulerDetail);
        }

    }
}
