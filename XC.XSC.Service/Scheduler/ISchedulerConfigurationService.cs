using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.Scheduler;
using XC.XSC.ViewModels.Submission;

namespace XC.XSC.Service.Scheduler
{
    public interface ISchedulerConfigurationService
    {
        /// <summary>
        /// get Scheduler detail based on Scheduler fire the job
        /// </summary>
        /// <param name="request">
        /// </param>
        /// <returns>Success</returns>
        Task<IResponse> GetAllSchedulers(GetSchedulerRequest getScheduler, CancellationToken cancellationToken);

        /// <summary>
        /// get Scheduler detail based on Scheduler fire the job
        /// </summary>
        /// <param name="schedulerId">
        /// </param>
        /// <returns>Success</returns>
        Task<IResponse> GetScheduler(int schedulerId);

        /// <summary>
        /// Save Scheduler Configuration detail based on Scheduler fire the job
        /// </summary>
        /// <param name="request">
        /// TeamId,
        /// RegionId,
        /// LobId,
        /// MailBoxId,
        /// FrequencyType as (HR,MIN)
        /// FrequencyOccurrence as 0,1
        /// </param>
        /// <returns>Success</returns>
        Task<IResponse> SaveSchedulerConfiguration(SchedulerRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<IResponse> UpdateSchedulerConfiguration(SchedulerRequest request);
    }
}
