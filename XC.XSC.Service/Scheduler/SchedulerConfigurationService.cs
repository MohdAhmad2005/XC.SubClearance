using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XC.XSC.Models.Entity.Scheduler;
using XC.XSC.Models.Entity.Sla;
using XC.XSC.Models.Entity.Submission;
using XC.XSC.Repositories.Scheduler;
using XC.XSC.Service.PagerExtensions;
using XC.XSC.Service.User;
using XC.XSC.UAM.Models;
using XC.XSC.UAM.UAM;
using XC.XSC.ViewModels;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.Scheduler;
using XC.XSC.ViewModels.Submission;
using XC.XSC.ViewModels.SubmissionStatus;

namespace XC.XSC.Service.Scheduler
{
    public class SchedulerConfigurationService : ISchedulerConfigurationService
    {
        private readonly IUserContext _userContext;
        private readonly IUamService _uamService;
        private readonly ISchedulerConfigurationRepository _schedulerConfigurationRepository;
        private readonly IResponse _response;
        private readonly IMapper _mapper;

        /// <summary>
        /// this constructor is use for intilize the service 
        /// </summary>
        public SchedulerConfigurationService(IUserContext userContext, ISchedulerConfigurationRepository schedulerConfigurationRepository, IResponse response, IMapper mapper, IUamService uamService)
        {
            this._userContext = userContext;
            this._schedulerConfigurationRepository = schedulerConfigurationRepository;
            this._response = response;
            this._mapper = mapper;
            _uamService = uamService;
        }

        /// <summary>
        /// this GetAllSchedulers is use for intilize the service 
        /// </summary>
        public async Task<IResponse> GetAllSchedulers(GetSchedulerRequest getScheduler, CancellationToken cancellationToken)
        {
            var region = await _uamService.GetUserRegions();
            var schedulers = await _schedulerConfigurationRepository.GetAll()
                                                                 .PaginateAsync(getScheduler.Paging.PageSize, getScheduler.Paging.Limit, getScheduler.Paging.SortOrder, getScheduler.Paging.SortField, cancellationToken);
            if (schedulers.Items.Count > 0)
            {
                var regions = (List<Region>)region.Result;
                _response.Result = new SchedulerList
                {
                    Paging = new ViewModels.Paging.Paging
                    {
                        PageSize = schedulers.CurrentPage,
                        TotalItems = schedulers.TotalItems,
                        TotalPages = schedulers.TotalPages
                    },
                    Schedulers = schedulers.Items.Select(p => new SchedulerResponse
                    {
                        FrequencyOccurrence = p.FrequencyOccurrence,
                        FrequencyType = p.FrequencyType.ToString(),
                        Lob = p.Lob.Name,
                        Region = regions!=null ? regions.FirstOrDefault(t => t.Id == p.RegionId).RegionName : "",

                    }).ToList()
                };
                _response.IsSuccess = true;
                _response.Message = "SUCCESS";
            }
            return _response;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schedulerId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IResponse> GetScheduler(int schedulerId)
        {
            var scheduler = await _schedulerConfigurationRepository.GetSingleAsync(s => s.Id == schedulerId);
            if (scheduler != null)
            {
                SchedulerRequest schedulerDetail = new SchedulerRequest
                {
                    FrequencyOccurrence = scheduler.FrequencyOccurrence,
                    FrequencyType = scheduler.FrequencyType,
                    Id = scheduler.Id,
                    LobId = scheduler.LobId,
                    MailBoxId = scheduler.MailBoxId,
                    RegionId = scheduler.RegionId,
                    TeamId = scheduler.TeamId
                };
                _response.Result = schedulerDetail;

            }
            return _response;
        }

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
        public async Task<IResponse> SaveSchedulerConfiguration(SchedulerRequest request)
        {

            var schedulerDetail = await _schedulerConfigurationRepository.GetSingleAsync(s => s.Id == request.Id);

            if (schedulerDetail != null)
            {
                _response.IsSuccess = false;
                _response.Message = "Already Exists";
            }
            else
            {
                var scheduler = _mapper.Map<SchedulerConfiguration>(request);
                scheduler.TenantId = _userContext.UserInfo.TenantId;
                scheduler.CreatedBy = _userContext.UserInfo.UserId;
                scheduler.CreatedDate = DateTime.Now;
                scheduler.IsActive = true;
                await _schedulerConfigurationRepository.AddAsync(scheduler);
                _response.IsSuccess = true;
                _response.Message = "Success";
            }

            return _response;
        }

        /// <summary>
        /// Update Scheduler Configuration detail based on Scheduler fire the job
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
        public async Task<IResponse> UpdateSchedulerConfiguration(SchedulerRequest request)
        {
            var schedulerDetail = await _schedulerConfigurationRepository.GetSingleAsync(s => s.Id == request.Id);

            if (schedulerDetail != null)
            {
                schedulerDetail.LobId = request.LobId;
                schedulerDetail.TeamId = request.TeamId;
                schedulerDetail.RegionId = request.RegionId;
                schedulerDetail.FrequencyType = request.FrequencyType;
                schedulerDetail.FrequencyOccurrence = request.FrequencyOccurrence;
                schedulerDetail.MailBoxId = request.MailBoxId;
                schedulerDetail.TenantId = _userContext.UserInfo.TenantId;
                schedulerDetail.ModifiedBy = _userContext.UserInfo.UserId;
                schedulerDetail.ModifiedDate = DateTime.Now;

                await _schedulerConfigurationRepository.UpdateAsync(schedulerDetail);
                _response.IsSuccess = true;
                _response.Message = "Success";
            }
            return _response;
        }
    }
}
