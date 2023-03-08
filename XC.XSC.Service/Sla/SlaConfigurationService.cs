using AutoMapper;
using Microsoft.Azure.Amqp.Sasl;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using XC.XSC.EMS.Connector;
using XC.XSC.EMS.Model;
using XC.XSC.Models.Entity.Prefrence;
using XC.XSC.Models.Entity.Sla;
using XC.XSC.Repositories.Sla;
using XC.XSC.Service.EMS;
using XC.XSC.Service.Lobs;
using XC.XSC.Service.PagerExtensions;
using XC.XSC.Service.Preferences;
using XC.XSC.Service.User;
using XC.XSC.UAM.Models;
using XC.XSC.UAM.UAM;
using XC.XSC.ViewModels;
using XC.XSC.ViewModels.Comment;
using XC.XSC.ViewModels.Configuration;
using XC.XSC.ViewModels.EmailInfo;
using XC.XSC.ViewModels.Enum;
using XC.XSC.ViewModels.Paging;
using XC.XSC.ViewModels.Sla;
using XC.XSC.ViewModels.Submission;

namespace XC.XSC.Service.Sla
{
    public class SlaConfigurationService : ISlaConfigurationService
    {
        private readonly IUserContext _userContext;
        private readonly ISlaConfigurationRepository _slaConfigRepository;
        private readonly IResponse _operationResponse;
        private readonly IMapper _mapper;
        private readonly IUamService _uamService;
        private readonly IEmsService _emsService;
        private readonly IPreferenceService _preferenceService;
        private readonly IEMSClient _emsClient;
        private readonly ILobService _lobService;

        public SlaConfigurationService(ISlaConfigurationRepository slaRepository, IResponse response, IUserContext userContext, IUamService uamService, IEmsService emsService
            , IPreferenceService preferenceService, IMapper mapper, ILobService lobService, IEMSClient emsClient)
        {
            _slaConfigRepository = slaRepository;
            this._operationResponse = response;
            this._userContext = userContext;
            this._mapper = mapper;
            _uamService = uamService;
            _emsService = emsService;
            _preferenceService = preferenceService;
            _lobService = lobService;
            _emsClient = emsClient;
        }

        /// <summary>
        /// This method is used to obtained the Sla details of Day,Hours,Min, from database base of Region,Lob,Team,Sla Type,Mailbox.
        /// </summary>
        /// <param name="Region,">this parameter use for region</param>
        /// <param name="Lob,">this parameter use for Lob</param>
        /// <param name="TeamId,">this parameter use for TeamId</param>
        /// <param name="Sla Type,">this parameter use for Type</param>
        /// <param name="Mailbox Id,">this parameter use for Mailbox</param>
        /// <returns>"Day"</returns>
        ///<returns>"Hours"</returns>
        ///<returns>"min"</returns>
        public async Task<IResponse> GetSlaConfiguration(int RegionId, int TeamdId, int LobId, SlaType slaType, Guid mailBoxId)
        {
            var slaConfiguration = await _slaConfigRepository.GetSingleAsync(k => k.MailBoxId == mailBoxId
                                                   && k.LobId == LobId
                                                   && k.TeamId == TeamdId
                                                   && k.TenantId == _userContext.UserInfo.TenantId
                                                   && k.RegionId == RegionId
                                                   && k.Type == slaType);

            var slaResponse = new SlaConfigurationResponse()
            {
                Day = slaConfiguration.Day,
                Hours = slaConfiguration.Hours,
                IsEscalation = slaConfiguration.IsEscalation,
                LobId = slaConfiguration.LobId,
                MailBoxId = slaConfiguration.MailBoxId,
                Min = slaConfiguration.Min,
                Name = slaConfiguration.Name,
                RegionId = slaConfiguration.RegionId,
                Id = slaConfiguration.Id,
                TaskType = slaConfiguration.TaskType,
                TeamId = slaConfiguration.TeamId,
                TypeName = slaConfiguration.Type.GetType().GetMember(slaConfiguration.Type.ToString()).First()
                                .GetCustomAttribute<DisplayAttribute>().Name,
            };
            _operationResponse.Result = slaResponse;
            _operationResponse.IsSuccess = true;
            _operationResponse.Message = "SUCCESS";
            return _operationResponse;
        }

        /// <summary>
        /// To Save the Sla Configuration detail based on Sla fire the job
        /// </summary>
        /// <param name="RegionId">Region Id</param>
        /// <param name="TeamdId">Team Id</param>
        /// <param name="LobId"> Lob Id</param>
        /// <param name="slaType">SLA Type</param>
        /// <param name="mailBoxId">Mail Box Guid</param>
        /// <param name="tenantId">Tenant Id</param>
        /// <returns></returns>
        public async Task<IResponse> GetSlaConfiguration(int RegionId, int TeamdId, int LobId, SlaType slaType, Guid mailBoxId, string tenantId)
        {
            var slaConfiguration = await _slaConfigRepository.GetSingleAsync(k => k.MailBoxId == mailBoxId
                                                   && k.LobId == LobId
                                                   && k.TeamId == TeamdId
                                                   && k.TenantId == tenantId
                                                   && k.RegionId == RegionId
                                                   && k.Type == slaType);
            if (slaConfiguration != null)
            {
                var slaResponse = new SlaConfigurationResponse()
                {
                    Day = slaConfiguration.Day,
                    Hours = slaConfiguration.Hours,
                    IsEscalation = slaConfiguration.IsEscalation,
                    LobId = slaConfiguration.LobId,
                    MailBoxId = slaConfiguration.MailBoxId,
                    Min = slaConfiguration.Min,
                    Name = slaConfiguration.Name,
                    RegionId = slaConfiguration.RegionId,
                    Id = slaConfiguration.Id,
                    TaskType = slaConfiguration.TaskType,
                    TeamId = slaConfiguration.TeamId,
                    TypeName = slaConfiguration.Type.GetType().GetMember(slaConfiguration.Type.ToString()).First()
                                    .GetCustomAttribute<DisplayAttribute>().Name,
                    SamplePercentage = slaConfiguration.SamplePercentage
                };
                _operationResponse.Result = slaResponse;
                _operationResponse.IsSuccess = true;
                _operationResponse.Message = "SUCCESS";
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "SLA Not Found";
            }
            return _operationResponse;
        }



        /// <summary>
        /// Calculate DueDate based on submission created date,region,team,lob and mailBoxId
        /// </summary>
        /// <param name="submission"></param>
        /// <param name="emailInfoRequest"></param>
        /// <returns>dueDate</returns>
        public async Task<DateTime> CalculateDueDate(SubmissionRequest submission, AddEmailInfoRequest emailInfoRequest)
        {
            DateTime dueDate = submission.CreatedDate;
            int tat = 0;
            
            var lob = _lobService.GetLobById(submission.LobId).Result;
            if (lob == null)
            {
                throw new Exception($"CalculateDueDate(): Lob does not exists.");
            }

            IResponse mailBoxResponse = await _emsClient.GetMailBoxList(emailInfoRequest.RegionId, lob.LOBID, emailInfoRequest.TeamId, emailInfoRequest.TenantId);
            if (mailBoxResponse.Result == null || !mailBoxResponse.IsSuccess)
            {
                throw new Exception($"CalculateDueDate(): MailBox does not exists.");
            }
            else
            {
                List<EmailBoxResponse> emailBoxes = (List<EmailBoxResponse>)mailBoxResponse.Result;

                var emailBox = emailBoxes.Select(e => e).Where(e => e.MailboxEmailID.Equals(emailInfoRequest.MailboxName));
                if (emailBox.Any())
                {
                    IResponse slaConfig = await GetSlaConfiguration(emailInfoRequest.RegionId, emailInfoRequest.TeamId, submission.LobId, SlaType.TAT, emailBox.FirstOrDefault().MailBoxId, emailInfoRequest.TenantId);
                    if (slaConfig.Result != null || mailBoxResponse.IsSuccess)
                    {
                        tat = ((SlaConfigurationResponse)slaConfig.Result).Day;
                    }
                }
            }


            HashSet<DateTime> holidays = new HashSet<DateTime>();
            IResponse responseHoliday = await _uamService.GetHolidayListByTeamId(emailInfoRequest.TeamId);
            if (responseHoliday.Result != null)
            {
                List<Holiday> holidaysList = (List<Holiday>)responseHoliday.Result;
                foreach (var holiday in holidaysList)
                {
                    holidays.Add(holiday.Date);
                }
            }
            Preference? preference = await _preferenceService.GetPreferenceAsync(key: "WKFDYS", _userContext.UserInfo.TenantId, string.Empty);
            while (tat > 0)
            {
                dueDate = GetNextWorkingDay(dueDate.Date, holidays, preference);
                if (tat == 1)
                {
                    break;
                }
                dueDate = dueDate.AddDays(1);
                tat--;
            }
            return dueDate;
        }

        /// <summary>
        /// Calculate Next Working Day 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="holidays"></param>
        /// <param name="preference"></param>
        /// <returns></returns>
        public DateTime GetNextWorkingDay(DateTime date, HashSet<DateTime> holidays, Preference? preference)
        {
            while (IsHoliday(date, holidays) || IsWeekend(date, preference))
            {
                date = date.AddDays(1);
            }
            return date;
        }

        /// <summary>
        /// This method is used to obtained the Sla details base of Region,Lob,Team,Sla Type,Mailbox.
        /// </summary>
        /// <param name="Region,">this parameter use for region</param>
        /// <param name="Lob,">this parameter use for Lob</param>
        /// <param name="TeamId,">this parameter use for TeamId</param>
        /// <param name="Sla Type,">this parameter use for Type</param>
        /// <param name="Mailbox Id,">this parameter use for Mailbox</param>
        public async Task<IResponse> getAllSlas(GetSlaConfigurationRequest getSlaRequest)
        {
            var region = await _uamService.GetUserRegions();
            var slaConfiguration = await _slaConfigRepository.GetAll().
                                                   Where(k => k.MailBoxId == getSlaRequest.mailBoxId
                                                   && k.LobId == getSlaRequest.LobId
                                                   && k.TeamId == k.TeamId
                                                   && k.TenantId == _userContext.UserInfo.TenantId
                                                   && k.RegionId == k.RegionId
                                                   && k.Type == getSlaRequest.slaType)
                                                   .PaginateAsync(getSlaRequest.PageSize, getSlaRequest.Limit, getSlaRequest.SortOrder, getSlaRequest.SortField, default(CancellationToken));

            if (slaConfiguration.Items.Count > 0)
            {
                var Regiondetail = (List<XC.XSC.UAM.Models.Region>)region.Result;
                _operationResponse.Result = new SlaConfigurationList
                {
                    Paging = new ViewModels.Paging.Paging
                    {
                        PageSize = slaConfiguration.CurrentPage,
                        TotalItems = slaConfiguration.TotalItems,
                        TotalPages = slaConfiguration.TotalPages
                    },
                    SlaConfigurationResponse = slaConfiguration.Items.Select(p => new SlaConfigurationResponse
                    {
                        Day = p.Day,
                        Hours = p.Hours,
                        IsEscalation = p.IsEscalation,
                        LobId = p.LobId,
                        MailBoxId = p.MailBoxId,
                        Min = p.Min,
                        Name = p.Name,
                        RegionId = p.RegionId,
                        Id = p.Id,
                        TaskType = p.TaskType,
                        TeamId = p.TeamId,
                        TypeName = p.Type.GetType().GetMember(p.Type.ToString()).First()
                                .GetCustomAttribute<DisplayAttribute>().Name,
                        Region = Regiondetail.FirstOrDefault(t => t.Id == p.RegionId) != null ? Regiondetail.FirstOrDefault(t => t.Id == p.RegionId).RegionName : "",

                    }).ToList()
                };
                _operationResponse.IsSuccess = true;
                _operationResponse.Message = "SUCCESS";
            }
            else
            {
                _operationResponse.IsSuccess = true;
                _operationResponse.Message = "SUCCESS";
            }
            return _operationResponse;
        }

        /// <summary>
        /// Save SLa Configuration detail based on Sla fire the job
        /// </summary>
        /// <param name="request">
        /// TeamId,
        /// RegionId,
        /// LobId,
        /// MailBoxId,
        /// Type,Day,Hours ,Min
        /// Percentage 
        /// Sample Percentage
        /// Task Type as 0,1,2
        /// </param>
        /// <returns>Success</returns>
        public async Task<IResponse> SaveSlaConfiguration(SlaConfigurationRequest request)
        {
            var slaConfiguration = _mapper.Map<SlaConfiguration>(request);
            var slaConfigurationAlreadyExists = _slaConfigRepository.GetAll().Where(item => item.TenantId == _userContext.UserInfo.TenantId &&
            item.RegionId == request.RegionId && item.TeamId == request.TeamId && item.MailBoxId == request.MailBoxId && item.Type == request.Type).ToList();
            if (slaConfigurationAlreadyExists.Count > 0)
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "The SLA for the selected Mailbox (Region, LOB and Team) has already been created";
            }
            else
            {
                var lob = _lobService.GetLobById(request.LobId).Result;
                if (lob != null && lob.Id > 0)
                {
                    IResponse mailBoxResponse = await _emsClient.GetMailBoxList(request.RegionId, lob.LOBID, request.TeamId, _userContext.UserInfo.TenantId);
                    if (mailBoxResponse.IsSuccess && mailBoxResponse.Result != null)
                    {
                        List<EmailBoxResponse> emailBoxes = (List<EmailBoxResponse>)mailBoxResponse.Result;
                        slaConfiguration.MailBoxName = emailBoxes.Where(item => item.Id == request.MailBoxId).FirstOrDefault().Name;
                        slaConfiguration.TenantId = _userContext.UserInfo.TenantId;
                        slaConfiguration.CreatedBy = _userContext.UserInfo.UserId;
                        slaConfiguration.CreatedDate = DateTime.Now;
                        slaConfiguration.ModifiedBy = _userContext.UserInfo.UserId;
                        slaConfiguration.ModifiedDate = DateTime.Now;
                        slaConfiguration.IsActive = true;
                        await _slaConfigRepository.AddAsync(slaConfiguration);
                        _operationResponse.Result = null;
                        _operationResponse.IsSuccess = true;
                        _operationResponse.Message = "Successfully completed";
                    }
                    else
                    {
                        _operationResponse.Result = null;
                        _operationResponse.IsSuccess = false;
                        _operationResponse.Message = "Mailbox details aren't found";
                    }

                }
                else
                {
                    _operationResponse.Result = null;
                    _operationResponse.IsSuccess = false;
                    _operationResponse.Message = "Lob details aren't found";
                }
            }
            return _operationResponse;
        }

        /// <summary>
        /// Update SLa Configuration detail based on Sla fire the job
        /// </summary>
        /// <param name="request">
        /// TeamId,
        /// RegionId,
        /// LobId,
        /// MailBoxId,
        /// Type,Day,Hours ,Min
        /// Percentage 
        /// Sample Percentage
        /// Task Type as 0,1,2
        /// </param>
        /// <returns>Success</returns>
        public async Task<IResponse> UpdateSlaConfiguration(SlaConfigurationRequest request)
        {
            var slaConfigurations = await _slaConfigRepository.GetSingleAsync(s => s.Id == request.Id);
            if (slaConfigurations != null)
            {
                var lob = _lobService.GetLobById(request.LobId).Result;
                if (lob != null && lob.Id > 0)
                {
                    IResponse mailBoxResponse = await _emsClient.GetMailBoxList(request.RegionId, lob.LOBID, request.TeamId, _userContext.UserInfo.TenantId);
                    if (mailBoxResponse.IsSuccess && mailBoxResponse.Result != null)
                    {
                        List<EmailBoxResponse> emailBoxes = (List<EmailBoxResponse>)mailBoxResponse.Result;
                        slaConfigurations.TeamId = request.TeamId;
                        slaConfigurations.LobId = request.LobId;
                        slaConfigurations.RegionId = request.RegionId;
                        slaConfigurations.Hours = request.Hours;
                        slaConfigurations.Min = request.Min;
                        slaConfigurations.Day = request.Day;
                        slaConfigurations.IsEscalation = request.IsEscalation;
                        slaConfigurations.MailBoxName = emailBoxes.Where(item => item.Id == request.MailBoxId).FirstOrDefault().Name;
                        slaConfigurations.MailBoxId = request.MailBoxId;
                        slaConfigurations.TaskType = request.TaskType;
                        slaConfigurations.Type = request.Type;
                        slaConfigurations.Name = request.Name;
                        slaConfigurations.Percentage = request.Percentage;
                        slaConfigurations.SamplePercentage = request.SamplePercentage;
                        slaConfigurations.TenantId = _userContext.UserInfo.TenantId;
                        slaConfigurations.IsActive = request.IsActive;
                        slaConfigurations.ModifiedDate = DateTime.Now;
                        slaConfigurations.ModifiedBy = _userContext.UserInfo.UserId;
                        await _slaConfigRepository.UpdateAsync(slaConfigurations);
                        _operationResponse.Result = null;
                        _operationResponse.IsSuccess = true;
                        _operationResponse.Message = "Successfully completed";
                    }
                    else
                    {
                        _operationResponse.Result = null;
                        _operationResponse.IsSuccess = false;
                        _operationResponse.Message = "Mailbox details aren't found";
                    }
                }
                else
                {
                    _operationResponse.Result = null;
                    _operationResponse.IsSuccess = false;
                    _operationResponse.Message = "Lob details aren't found";
                }
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "Invalid sla id.";
            }
            return _operationResponse;
        }


        /// <summary>
        /// Check weather the team holiday exist or not
        /// </summary>
        /// <param name="date"></param>
        /// <param name="teamId"></param>
        /// <returns></returns>
        private bool IsHoliday(DateTime date, HashSet<DateTime> holidays)
        {
            return holidays.Any(x => x.Date == date.Date);
        }

        /// <summary>
        /// Check if any weekend exist or not
        /// </summary>
        /// <param name="date"></param>
        /// <returns>true</returns>
        private bool IsWeekend(DateTime date, Preference? preference)
        {
            bool isWeekend = false;
            if (preference != null)
            {
                string dayName = date.DayOfWeek.ToString().ToUpper();
                string[] validNames = preference.Value.Split(',');
                if (Array.Exists(validNames, element => element == dayName))
                {
                    isWeekend = true;
                }
            }
            return isWeekend;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SlaId"></param>
        /// <returns></returns>
        public async Task<IResponse> getSlaConfigurationbyId(long SlaId)
        {
            var slaConfiguration = await _slaConfigRepository.GetSingleAsync(t => t.Id == SlaId && t.TenantId == _userContext.UserInfo.TenantId);

            var slaResponse = new SlaConfigurationResponse()
            {
                Day = slaConfiguration.Day,
                Hours = slaConfiguration.Hours,
                IsEscalation = slaConfiguration.IsEscalation,
                LobId = slaConfiguration.LobId,
                MailBoxId = slaConfiguration.MailBoxId,
                Min = slaConfiguration.Min,
                Name = slaConfiguration.Name,
                RegionId = slaConfiguration.RegionId,
                Id = slaConfiguration.Id,
                TaskType = slaConfiguration.TaskType,
                TeamId = slaConfiguration.TeamId,
                TypeName = slaConfiguration.Type.GetType().GetMember(slaConfiguration.Type.ToString()).First()
                                .GetCustomAttribute<DisplayAttribute>().Name,
            };
            _operationResponse.Result = slaResponse;
            _operationResponse.IsSuccess = true;
            _operationResponse.Message = "SUCCESS";
            return _operationResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        public async Task<IResponse> getAllSlaDetail()
        {
            var regionList= await _uamService.GetUserRegions();
            List<XC.XSC.UAM.Models.Region> regionLists = (List<XC.XSC.UAM.Models.Region>)regionList.Result;
            var slaConfiguration = await _slaConfigRepository.GetAll().
                                      Where(item => item.TenantId == _userContext.UserInfo.TenantId).
                                      OrderByDescending(item => item.CreatedDate).ToListAsync();
            if (slaConfiguration.Count > 0)
            {
                List<UserResponseResult> userListLookup = new List<UserResponseResult>();
                var usersList = await _uamService.GetUsersByFilters(new UserFilterRequest());
                if (usersList.Result != null)
                {
                    userListLookup = (List<UserResponseResult>)usersList.Result;
                }
                List<TeamModel> teamLookup = new List<TeamModel>();
                var teamList = await _uamService.GetTeamList();
                if (teamList.Result != null)
                {
                    teamLookup = (List<TeamModel>)teamList.Result;
                }
                var result = slaConfiguration.Select(p => new SlaConfigurationResponse
                {
                    Day = p.Day,
                    Hours = p.Hours,
                    IsEscalation = p.IsEscalation,
                    LobId = p.LobId,
                    Lob = p.Lob.Name,
                    MailBoxId = p.MailBoxId,
                    Min = p.Min,
                    Name = p.Name,
                    RegionId = p.RegionId,
                    Id = p.Id,
                    TaskType = p.TaskType,
                    TeamId = p.TeamId,
                    TypeName = p.Type.GetType().GetMember(p.Type.ToString()).First()
                                .GetCustomAttribute<DisplayAttribute>().Name,
                    Region = regionLists.Count > 0 ? regionLists.FirstOrDefault(r => r.Id == p.RegionId).RegionName : string.Empty,
                    Team = teamLookup.Count > 0 ? teamLookup.FirstOrDefault(r => r.Id == p.TeamId).Name : string.Empty,
                    UpdatedBy = userListLookup.Count > 0 ? userListLookup.Where(r => r.Id == p.ModifiedBy).
                                    Select(s => string.Format("{0} {1}", s.FirstName, s.LastName)).FirstOrDefault() : string.Empty,
                    SlaDefinition = (int)p.Type == 1 ? p.Day.ToString() + "D" : p.Percentage.ToString("N0") +"%",
                    IsActive =p.IsActive,
                    Type = p.Type,
                    Percentage =p.Percentage,
                    SamplePercentage = p.SamplePercentage,
                    MailBoxName = p.MailBoxName
                }).ToList();
                _operationResponse.Result = result;
                _operationResponse.IsSuccess = true;
                _operationResponse.Message = "SUCCESS";
            }
            else
            {
                _operationResponse.Result = null;
                _operationResponse.IsSuccess = false;
                _operationResponse.Message = "No result found";
            }
            return await Task.FromResult(_operationResponse);
        }

    }
}
